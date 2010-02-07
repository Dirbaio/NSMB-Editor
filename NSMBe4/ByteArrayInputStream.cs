using System;
using System.Collections.Generic;
using System.Text;

namespace NSMBe4
{
    public class ByteArrayInputStream
    {
        private byte[] array;
        private uint pos = 0, origin = 0;
        private Stack<uint> savedPositions = new Stack<uint>();

        public ByteArrayInputStream(byte[] array)
        {
            this.array = array;
            pos = 0;
        }

        public void setOrigin(uint o)
        {
            this.origin = o;
        }

        public void savePos()
        {
            savedPositions.Push(pos);
        }

        public void loadPos()
        {
            pos = savedPositions.Pop();
        }

        public uint available()
        {
            return (uint)(array.Length - pos - origin);
        }

        public bool available(int len)
        {
            return available() >= len;
        }

        public byte readByte()
        {
            return array[origin+pos++];
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

        public void write(byte[] data)
        {
            Array.Copy(data, 0, array, pos + origin, data.Length);
            pos += (uint) data.Length;
        }

        public void writeByte(byte b)
        {
            array[origin + pos++] = b;
        }

        public void seek(uint pos)
        {
            this.pos = pos;
        }
        public void seek(int pos)
        {
            this.pos = (uint)pos;
        }

        public void skip(uint bytes)
        {
            pos += bytes;
        }

        public void skipback(uint bytes)
        {
            pos -= bytes;
        }


        public uint getPos()
        {
            return pos;
        }

        public ushort readUShort()
        {
            pos+=2;
            return (ushort)(array[pos-2+origin] | array[pos - 1+origin] << 8);
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
            Array.Copy(array, pos+origin, dest, 0, dest.Length);
            pos += (uint)(dest.Length);
        }

        public bool end()
        {
            return pos + origin >= array.Length;
        }

        public string ReadString(int l)
        {
            if (l == 0) return ""; // simple error checking

            byte[] arr = new byte[l];
            read(arr);

            StringBuilder NewStr = new StringBuilder(l);
            for (int i = 0; i < l; i++)
                NewStr.Append((char)arr[i]);

            return NewStr.ToString().Trim();
        }
    }
}
