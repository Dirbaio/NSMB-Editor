using System;
using System.Collections.Generic;
using System.Text;

namespace NSMBe4
{
    public class ByteArrayInputStream
    {
        private byte[] array;
        private int pos = 0;

        public ByteArrayInputStream(byte[] array)
        {
            this.array = array;
            pos = 0;
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

        public void read(byte[] array)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] = readByte();
        }

        public bool end()
        {
            return pos >= array.Length;
        }
    }
}
