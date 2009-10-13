using System;
using System.Collections.Generic;
using System.Text;

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
 **/

namespace NSMBe4 {
    public static class NSMBDataHandler {
        public static NitroClass ROM;
        public static byte[] Overlay0;

        public static void load(NitroClass ROM) {
            NSMBDataHandler.ROM = ROM;
            Overlay0 = DecompressOverlay(ROM.ExtractFile(0));
        }

        public const int Table_TS_UNT_HD = 0x2F8E4;
        public const int Table_TS_UNT = 0x2FA14;
        public const int Table_TS_CHK = 0x2FB44;
        public const int Table_TS_ANIM_NCG = 0x2FC74;
        public const int Table_BG_NCG = 0x30D74;
        public const int Table_TS_NCG = 0x30EA4;
        public const int Table_FG_NCG = 0x30FD4;
        public const int Table_FG_NSC = 0x31104;
        public const int Table_BG_NSC = 0x31234;
        public const int Table_BG_NCL = 0x31364;
        public const int Table_TS_NCL = 0x31494;
        public const int Table_FG_NCL = 0x315C4;
        public const int Table_TS_PNL = 0x316F4;

        public const int File_Jyotyu_CHK = 0x2FDA4;

        public static ushort GetFileIDFromTable(int id, int tableoffset) {
            int off = tableoffset + (id << 2);
            return (ushort)((Overlay0[off] | (Overlay0[off + 1] << 8)) + 131);
        }

        public static byte[] DecompressOverlay(byte[] sourcedata) {
            uint DataVar1, DataVar2;
            DataVar1 = (uint)(sourcedata[sourcedata.Length - 8] | (sourcedata[sourcedata.Length - 7] << 8) | (sourcedata[sourcedata.Length - 6] << 16) | (sourcedata[sourcedata.Length - 5] << 24));
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
            r2 = r0 + r2;
            r3 = r0 - (r1 >> 0x18);
            r1 &= 0xFFFFFF;
            r1 = r0 - r1;
        a958:
            if (r3 <= r1) {
                goto a9B8;
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
    }
}
