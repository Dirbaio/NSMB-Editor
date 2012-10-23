/*
*   This file is part of NSMB Editor 5.
*
*   NSMB Editor 5 is free software: you can redistribute it and/or modify
*   it under the terms of the GNU General Public License as published by
*   the Free Software Foundation, either version 3 of the License, or
*   (at your option) any later version.
*
*   NSMB Editor 5 is distributed in the hope that it will be useful,
*   but WITHOUT ANY WARRANTY; without even the implied warranty of
*   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*   GNU General Public License for more details.
*
*   You should have received a copy of the GNU General Public License
*   along with NSMB Editor 5.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace NSMBe4
{
    public class ByteArrayInputStream : System.IO.Stream
    {
        private byte[] array;
        private uint pos = 0, origin = 0;
        private Stack<uint> savedPositions = new Stack<uint>();

        public override long Position {
            get {
                return pos;
            }
            set {
                pos = (uint)value;
            }
        }

        public override long Length
        {
            get { return array.Length - origin; }
        }

        public override bool CanRead {
            get { return true; } }

        public override bool CanWrite {
            get { return true; } }

        public override bool CanSeek {
            get { return true; } }

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

        public int available
        {
            get
            {
                return (int)(array.Length - pos - origin);
            }
        }

        public bool lengthAvailable(int len)
        {
            return available >= len;
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

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (!lengthAvailable(count))
                SetLength(pos + count);
            Array.Copy(buffer, offset, array, pos + origin, count);
            pos += (uint)count;
        }

        public override void WriteByte(byte value)
        {
            if (pos + origin == array.Length)
                Array.Resize<byte>(ref array, array.Length + 1);
            array[origin + pos++] = value;
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

        public override long Seek(long offset, System.IO.SeekOrigin origin)
        {
            switch (origin)
            {
                case System.IO.SeekOrigin.Begin:
                    pos = this.origin + (uint)offset;
                    break;
                case System.IO.SeekOrigin.Current:
                    pos += (uint)offset;
                    break;
                case System.IO.SeekOrigin.End:
                    pos = (uint)array.Length - this.origin - (uint)offset;
                    break;
            }
            return pos;
        }

        public void skip(uint bytes)
        {
            pos += bytes;
        }

        public byte[] getData()
        {
            return array;
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
        public int readInt()
        {
            uint res = 0;
            for (int i = 0; i < 4; i++)
            {
                res |= (uint)readByte() << 8 * i;
            }
            return (int) res;
        }

        public long readLong()
        {
            long res = 0;
            for (int i = 0; i < 8; i++)
            {
                res |= (long)readByte() << 8 * i;
            }
            return res;
        }

        public void read(byte[] dest)
        {
            Array.Copy(array, pos+origin, dest, 0, dest.Length);
            pos += (uint)(dest.Length);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            Array.Copy(array, pos + origin, buffer, offset, count);
            pos += (uint)count;
            return count;
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
                if(arr[i] != 0)
                    NewStr.Append((char)arr[i]);

            return NewStr.ToString().Trim();
        }

        // What is this function supposed to do lol
        public override void Flush()
        {
            
        }

        public override void SetLength(long value)
        {
            Array.Resize<byte>(ref array, (int)value + (int)origin);
        }
    }
}
