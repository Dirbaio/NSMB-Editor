using System;
using System.Collections.Generic;
using System.Text;

namespace NSMBe4
{
    public class ByteArrayInputStream
    {
        private byte[] array;
        private int pos = 0;
        private Stack<int> savedPositions = new Stack<int>();

        public ByteArrayInputStream(byte[] array)
        {
            this.array = array;
            pos = 0;
        }

        public void savePos()
        {
            savedPositions.Push(pos);
        }

        public void loadPos()
        {
            pos = savedPositions.Pop();
        }

        public int available()
        {
            return array.Length - pos;
        }

        public bool available(int len)
        {
            return pos + len <= array.Length;
        }

        public byte readByte()
        {
            return array[pos++];
        }

        public void dumpAsciiData()
        {
            for(int i = 0; i < array.Length; i++)
            {
                Console.Out.Write((char)array[i]);
                if ((i % 60) == 0)
                    Console.Out.WriteLine();
            }
        }

        public void seek(int pos)
        {
            this.pos = pos;
        }

        public void skip(int bytes)
        {
            pos += bytes;
        }

        public ushort readUShort()
        {
            pos+=2;
            return (ushort)(array[pos-2] | array[pos - 1] << 8);
        }

        public uint readUInt()
        {
            uint res = 0;
            for (int i = 0; i < 4; i++)
            {
                res |= (uint)readByte() << 8 * i;
            }
            return res;
        }
        public void read(byte[] dest)
        {
            Array.Copy(array, pos, dest, 0, dest.Length);
            pos += dest.Length;
        }

        public bool end()
        {
            return pos >= array.Length;
        }

        public string ReadString(int l)
        {
            if (l == 0) return ""; // simple error checking

            byte[] arr = new byte[l];
            read(arr);

            StringBuilder NewStr = new StringBuilder(l);
            for (int i = 0; i < l; i++)
                NewStr.Append((char)arr[i]);

            return NewStr.ToString();
        }
    }
}
