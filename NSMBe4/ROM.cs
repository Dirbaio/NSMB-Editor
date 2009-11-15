using System;
using System.Collections.Generic;
using System.Text;
using NSMBe4.DSFileSystem;


/**
 * This class handles internal NSMB-specific data in the ROM.
 * Right now it can decompress the Overlay data and read
 * data from several tables contained in the ROM.
 * 
 * Data description about overlay 0: (From an old text file)
 * 76 max tilesets. Each table is 0x130 big.
 * 
 * 2F8E4: Object definition indexes (unt+hd) table
 * 2FA14: Object definitions (unt) table
 * 2FB44: Tile behaviours (chk) table
 * 2FC74: Animated tileset graphics (ncg) table
 * 2FDA4: Jyotyu tile behaviour file
 * 30D74: Background graphics (ncg) table
 * 30EA4: Tileset graphics (ncg) table
 * 30FD4: Foreground graphics (ncg) table
 * 31104: Foreground design (nsc) table
 * 31234: Background design (nsc) table
 * 31364: Background palette (ncl) table
 * 31494: Tileset palette (ncl) table
 * 315C4: Foreground palette (ncl) table
 * 316F4: Map16 (pnl) table
 * 
 **/

namespace NSMBe4 {
    public static class ROM {
        public static byte[] Overlay0;
        public static NitroFilesystem FS;
        public static string filename;

        public static void load(String filename)
        {
            ROM.filename = filename;
            FS = new NitroFilesystem(filename);

            Overlay0 = DecompressOverlay(FS.getFileById(0).getContents());

            if (Overlay0[28] == 0x84) {
                Region = Origin.US;
            } else if (Overlay0[28] == 0x64) {
                Region = Origin.EU;
            } else if (Overlay0[28] == 0x04) {
                Region = Origin.JP;
            } else {
                Region = Origin.US;
                System.Windows.Forms.MessageBox.Show(LanguageManager.Get("General", "UnknownRegion"), LanguageManager.Get("General", "Warning"), System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }
        }

        public static void SaveOverlay0() {
            FS.getFileById(0).replace(Overlay0);
        }

        public enum Origin {
            US = 0, EU = 1, JP = 2
        }

        public static Origin Region = Origin.US;

        public enum Data : int {
            Number_FileOffset = 0,
            Table_TS_UNT_HD = 1,
            Table_TS_UNT = 2,
            Table_TS_CHK = 3,
            Table_TS_ANIM_NCG = 4,
            Table_BG_NCG = 5,
            Table_TS_NCG = 6,
            Table_FG_NCG = 7,
            Table_FG_NSC = 8,
            Table_BG_NSC = 9,
            Table_BG_NCL = 10,
            Table_TS_NCL = 11,
            Table_FG_NCL = 12,
            Table_TS_PNL = 13,
            Table_Jyotyu_NCL = 14,
            File_Jyotyu_CHK = 15,
            File_Modifiers = 16
        }

        public static int[,] Offsets = {
                                           {131, 135, 131}, //File Offset (Overlay Count)
                                           {0x2F8E4, 0x2F0F8, 0x2ECE4}, //TS_UNT_HD
                                           {0x2FA14, 0x2F228, 0x2EE14}, //TS_UNT
                                           {0x2FB44, 0x2F358, 0x2EF44}, //TS_CHK
                                           {0x2FC74, 0x2F488, 0x2F074}, //TS_ANIM_NCG
                                           {0x30D74, 0x30588, 0x30174}, //BG_NCG
                                           {0x30EA4, 0x306B8, 0x302A4}, //TS_NCG
                                           {0x30FD4, 0x307E8, 0x303D4}, //FG_NCG
                                           {0x31104, 0x30918, 0x30504}, //FG_NSC
                                           {0x31234, 0x30A48, 0x30634}, //BG_NSC
                                           {0x31364, 0x30B78, 0x30764}, //BG_NCL
                                           {0x31494, 0x30CA8, 0x30894}, //TS_NCL
                                           {0x315C4, 0x30DD8, 0x309C4}, //FG_NCL
                                           {0x316F4, 0x30F08, 0x30AF4}, //TS_PNL
                                           {0x30CD8, 0x304EC, 0x300D8}, //Jyotyu_NCL
                                           {0x2FDA4, 0x2F5B8, 0x2F1A4}, //Jyotyu_CHK
                                           {0x2C930, 0x2BDF0, 0x2BD30}, //Modifiers
                                       };

        public static int[] FileSizes = {
                                            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, //Don't include tables
                                            0x400, //Jyotyu_CHK
                                            0x288, //Modifiers
                                        };

        public static ushort GetFileIDFromTable(int id, Data datatype) {
            return GetFileIDFromTable(id, GetOffset(datatype));
        }

        public static ushort GetFileIDFromTable(int id, int tableoffset) {
            int off = tableoffset + (id << 2);
            return (ushort)((Overlay0[off] | (Overlay0[off + 1] << 8)) + GetOffset(Data.Number_FileOffset));
        }

        public static int GetOffset(Data datatype) {
            return Offsets[(int)datatype, (int)Region];
        }

        public static byte[] GetInlineFile(Data datatype) {
            byte[] output = new byte[FileSizes[(int)datatype]];
            Array.Copy(Overlay0, GetOffset(datatype), output, 0, output.Length);
            return output;
        }

        public static void ReplaceInlineFile(Data datatype, byte[] NewFile) {
            Array.Copy(NewFile, 0, Overlay0, GetOffset(datatype), NewFile.Length);
            SaveOverlay0();
        }

        public static byte[] DecompressOverlay(byte[] sourcedata)
        {
            uint DataVar1, DataVar2;
            //uint last 8-5 bytes
            DataVar1 = (uint)(sourcedata[sourcedata.Length - 8] | (sourcedata[sourcedata.Length - 7] << 8) | (sourcedata[sourcedata.Length - 6] << 16) | (sourcedata[sourcedata.Length - 5] << 24));
            //uint last 4 bytes
            DataVar2 = (uint)(sourcedata[sourcedata.Length - 4] | (sourcedata[sourcedata.Length - 3] << 8) | (sourcedata[sourcedata.Length - 2] << 16) | (sourcedata[sourcedata.Length - 1] << 24));

            byte[] memory = new byte[sourcedata.Length + DataVar2];
            sourcedata.CopyTo(memory, 0);

            uint r0, r1, r2, r3, r5, r6, r7, r12;
            bool N, V;
            r0 = (uint)sourcedata.Length;

            if (r0 == 0) {
                return null;
            }
            r1 = DataVar1;
            r2 = DataVar2;
            r2 = r0 + r2; //length + datavar2 -> decompressed length
            r3 = r0 - (r1 >> 0x18); //delete the latest 3 bits??
            r1 &= 0xFFFFFF; //save the latest 3 bits
            r1 = r0 - r1;
        a958:
            if (r3 <= r1) { //if r1 is 0 they will be equal
                goto a9B8; //return the memory buffer
            }
            r3 -= 1;
            r5 = memory[r3];
            r6 = 8;
        a968:
            SubS(out r6, r6, 1, out N, out V);
            if (N != V) {
                goto a958;
            }
            if ((r5 & 0x80) != 0) {
                goto a984;
            }
            r3 -= 1;
            r0 = memory[r3];
            r2 -= 1;
            memory[r2] = (byte)r0;
            goto a9AC;
        a984:
            r3 -= 1;
            r12 = memory[r3];
            r3 -= 1;
            r7 = memory[r3];
            r7 |= (r12 << 8);
            r7 &= 0xFFF;
            r7 += 2;
            r12 += 0x20;
        a99C:
            r0 = memory[r2 + r7];
            r2 -= 1;
            memory[r2] = (byte)r0;
            SubS(out r12, r12, 0x10, out N, out V);
            if (N == V) {
                goto a99C;
            }
        a9AC:
            r5 <<= 1;
            if (r3 > r1) {
                goto a968;
            }
        a9B8:
            return memory;
        }

        private static void SubS(out uint dest, uint v1, uint v2, out bool N, out bool V) {
            dest = v1 - v2;
            N = (dest & 2147483648) != 0;
            V = ((((v1 & 2147483648) != 0) && ((v2 & 2147483648) == 0) && ((dest & 2147483648) == 0)) || ((v1 & 2147483648) == 0) && ((v2 & 2147483648) != 0) && ((dest & 2147483648) != 0));
        }

        public static byte[] LZ77_Compress(byte[] source)
        {
            // This should really be named LZ77_FakeCompress for more accuracy
            int DataLen = 4;
            DataLen += source.Length;
            DataLen += (int)Math.Ceiling((double)source.Length / 8);
            byte[] dest = new byte[DataLen];

            dest[0] = 0;
            dest[1] = (byte)(source.Length & 0xFF);
            dest[2] = (byte)((source.Length >> 8) & 0xFF);
            dest[3] = (byte)((source.Length >> 16) & 0xFF);

            int FilePos = 4;
            int UntilNext = 0;

            for (int SrcPos = 0; SrcPos < source.Length; SrcPos++)
            {
                if (UntilNext == 0)
                {
                    dest[FilePos] = 0;
                    FilePos++;
                    UntilNext = 8;
                }
                dest[FilePos] = source[SrcPos];
                FilePos++;
                UntilNext -= 1;
            }

            return dest;
        }

        /* DeLZ */
        public static byte[] LZ77_Decompress(byte[] source)
        {
            /* This code converted from Elitemap */
            int DataLen;
            DataLen = source[1] | (source[2] << 8) | (source[3] << 8);
            byte[] dest = new byte[DataLen];
            int i, j, xin, xout;
            xin = 4;
            xout = 0;
            int length, offset, windowOffset, data;
            byte d;
            while (DataLen > 0)
            {
                d = source[xin++];
                if (d != 0)
                {
                    for (i = 0; i < 8; i++)
                    {
                        if ((d & 0x80) != 0)
                        {
                            data = ((source[xin] << 8) | source[xin + 1]);
                            xin += 2;
                            length = (data >> 12) + 3;
                            offset = data & 0xFFF;
                            windowOffset = xout - offset - 1;
                            for (j = 0; j < length; j++)
                            {
                                dest[xout++] = dest[windowOffset++];
                                DataLen--;
                                if (DataLen == 0)
                                {
                                    return dest;
                                }
                            }
                        }
                        else
                        {
                            dest[xout++] = source[xin++];
                            DataLen--;
                            if (DataLen == 0)
                            {
                                return dest;
                            }
                        }
                        d <<= 1;
                    }
                }
                else
                {
                    for (i = 0; i < 8; i++)
                    {
                        dest[xout++] = source[xin++];
                        DataLen--;
                        if (DataLen == 0)
                        {
                            return dest;
                        }
                    }
                }
            }
            return dest;
        }
    }
}
