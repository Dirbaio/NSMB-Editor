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
using System.IO;

namespace NSMBe4.DSFileSystem
{
	public abstract class File
	{
        private Filesystem parentP;
        public Filesystem parent { get { return parentP; } }

        private Directory parentDirP;
        public Directory parentDir { get { return parentDirP; } }

        protected string nameP;
        public string name { get { return nameP; } }

        private int idP;
        public int id { get { return idP; } }
        public bool isSystemFile { get { return idP<0; } }

        protected int fileSizeP;
        public int fileSize { get { return fileSizeP; } }
		
		public File() {}
		
		public File(Filesystem parent, Directory parentDir, string name, int id)
		{
			this.parentP = parent;
			this.parentDirP = parentDir;
			this.nameP = name;
			this.idP = id;
		}

		//File functions
        public abstract byte[] getContents();
        public abstract void replace(byte[] newFile, object editor);
		public abstract byte[] getInterval(int start, int end);
        public abstract void replaceInterval(byte[] newFile, int start);
		
		//Handy read/write functions.
        public uint getUintAt(int offset)
        {
        	byte[] data = getInterval(offset, offset+4);
        	return (uint)(data[0] | (data[1]<<8) | (data[2]<<16) | (data[3]<<24));
        }

        public ushort getUshortAt(int offset)
        {
        	byte[] data = getInterval(offset, offset+2);
        	return (ushort)(data[0] | (data[1]<<8));
        }
        public byte getByteAt(int offset)
        {
        	byte[] data = getInterval(offset, offset+1);
        	return (byte)(data[0]);
        }

        public void setUintAt(int offset, uint val)
        {
        	byte[] data = {(byte)(val), (byte)(val>>8), (byte)(val>>16), (byte)(val>>24)};
        	beginEditInterval(offset, offset+data.Length);
        	replaceInterval(data, offset);
        	endEditInterval(offset, offset+data.Length);
        }
        
        public void setUshortAt(int offset, ushort val)
        {
        	byte[] data = {(byte)(val), (byte)(val>>8)};
        	beginEditInterval(offset, offset+data.Length);
        	replaceInterval(data, offset);
        	endEditInterval(offset, offset+data.Length);
        }

        public void setByteAt(int offset, byte val)
        {
        	byte[] data = {(byte)(val)};
        	beginEditInterval(offset, offset+data.Length);
        	replaceInterval(data, offset);
        	endEditInterval(offset, offset+data.Length);
        }
        
        
        //Lock/unlock functions
        public abstract void beginEdit(Object editor);
        public abstract void endEdit(Object editor);
        public abstract void beginEditInterval(int start, int end);
        public abstract void endEditInterval(int start, int end);
        public abstract bool beingEditedBy(Object editor);
        
        //Misc functions
        public string getPath()
        {
            return parentDir.getPath() + "/" + name;
        }

		protected void validateInterval(int start, int end)
		{
			if(end < start)
				throw new Exception("Wrong interval: end < start");
//			Console.Out.WriteLine("Checking interval "+start+" - " +end +" on "+name);
			if(start < 0 || start > fileSize)
				throw new Exception("Wrong interval: start out of bounds");
			if(end < 0 || end > fileSize)
				throw new Exception("Wrong interval: end out of bounds");
		}
	}
}

