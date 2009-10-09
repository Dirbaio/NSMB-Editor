using System;
using System.Collections.Generic;
using System.Text;

namespace NSMBe4
{
	public class ByteArrayOutputStream
	{

        //implements an unbonded array to store unlimited data.
        //writes in amortized constant time.

        private byte[] buf = new byte[16];
        private int pos = 0;

        public ByteArrayOutputStream()
        {
        }

        public int getPos()
        {
            return pos;
        }

        public byte[] getArray()
        {
            byte[] ret = new byte[pos];
            Array.Copy(buf, ret, pos);
            return ret;
        }

        public void writeByte(byte b)
        {
            if (buf.Length <= pos)
                grow();

            buf[pos] = b;
            pos++;
        }

        public void writeUShort(ushort u)
        {
            writeByte((byte)u);
            writeByte((byte)(u >> 8));
        }

        private void grow()
        {
            byte[] nbuf = new byte[buf.Length * 2];
            Array.Copy(buf, nbuf, buf.Length);
            buf = nbuf;
        }

        public void write(byte[] ar)
        {
            for (int i = 0; i < ar.Length; i++)
                writeByte(ar[i]);
        }
    }
}
