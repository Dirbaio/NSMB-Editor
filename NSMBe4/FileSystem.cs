using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace NSMBe4
{
    public abstract class FileSystem
    {
        public FileLister lister;

        /* Files */
        public Dictionary<string, ushort> FileIDs;
        public Dictionary<ushort, string> FileNames;
        public Dictionary<ushort, uint> FileOffsets;
        public Dictionary<ushort, uint> FileSizes;
        public Dictionary<ushort, ushort> FileParentIDs;

        /* Dirs */
        public Dictionary<string, ushort> DirIDs;

        public abstract byte[] ExtractFile(ushort FileID);
        public abstract void ReplaceFile(ushort FileID, byte[] NewFile);

        protected void ResetDictionaries()
        {
            // reset dictionaries
            if (FileIDs == null)
            {
                // not initialised!
                FileIDs = new Dictionary<string, ushort>();
                FileNames = new Dictionary<ushort, string>();
                FileOffsets = new Dictionary<ushort, uint>();
                FileSizes = new Dictionary<ushort, uint>();
                FileParentIDs = new Dictionary<ushort, ushort>();

                DirIDs = new Dictionary<string, ushort>();
            }
            FileIDs.Clear();
            FileNames.Clear();
            FileOffsets.Clear();
            FileSizes.Clear();
            FileParentIDs.Clear();
            DirIDs.Clear();
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

        /* Supporting Functions */
        public uint ReadUInt(Stream fs)
        {
            // get an unsigned int from a passed Stream
            // operates in little-endian
            byte[] TempByte = new byte[4];
            fs.Read(TempByte, 0, 4);
            uint NewVal = TempByte[0];
            NewVal += (uint)TempByte[1] * 0x100;
            NewVal += (uint)TempByte[2] * 0x10000;
            NewVal += (uint)TempByte[3] * 0x1000000;
            return NewVal;
        }

        public ushort ReadUShort(Stream fs)
        {
            // get an unsigned short from a passed Stream
            // operates in little-endian
            byte[] TempByte = new byte[2];
            fs.Read(TempByte, 0, 2);
            ushort NewVal = (ushort)(TempByte[0] + (TempByte[1] * 0x100));
            return NewVal;
        }

        public string ReadString(Stream fs, int StringLength)
        {
            // get a string from a passed Stream
            if (StringLength == 0) return ""; // simple error checking
            byte[] TempByte = new byte[StringLength];
            fs.Read(TempByte, 0, StringLength);
            StringBuilder NewStr = new StringBuilder(StringLength);
            for (int CharPos = 0; CharPos < StringLength; CharPos++)
            {
                NewStr.Append((char)TempByte[CharPos]);
            }
            return NewStr.ToString();
        }

        public void WriteUInt(Stream fs, uint WriteThis)
        {
            // write an unsigned int to a passed Stream
            byte[] TempByte = new byte[4];
            uint OldVal = WriteThis;
            TempByte[0] = (byte)(OldVal & 0xFF);
            OldVal >>= 8;
            TempByte[1] = (byte)(OldVal & 0xFF);
            OldVal >>= 8;
            TempByte[2] = (byte)(OldVal & 0xFF);
            OldVal >>= 8;
            TempByte[3] = (byte)OldVal;
            fs.Write(TempByte, 0, 4);
        }
    }
}
