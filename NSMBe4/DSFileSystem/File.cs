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
    public class File : IComparable
    {
        private bool isSystemFileP;
        public bool isSystemFile {get{return isSystemFileP;}}

        private string nameP;
        public string name { get { return nameP; } }

        private int idP;
        public int id { get { return idP; } }

        private Directory parentDirP;
        public Directory parentDir { get { return parentDirP; } }

        //if allocationFile is not null,
        //these are the offsets within the alloc file where the offsets
        //of this file are found.

        protected File beginFile;
        protected uint beginOffset;
        protected File endFile;
        protected uint endOffset;
        protected bool endIsSize; //means that the end offset is the size of the file
        protected bool fixedFile; //means that the file cant be moved nor changed size

        public uint fileBegin;
        public uint fileSize;

        protected Filesystem parent;

        private Object editedBy = null;
        public Boolean beingEdited
        {
            get { return editedBy != null; }
        }

        public File(Filesystem parent, Directory parentDir, bool systemFile, int id, string name, File alFile, uint alBeg, uint alEnd)
        {
            this.parent = parent;
            this.parentDirP = parentDir;
            this.isSystemFileP = systemFile;
            this.idP = id;
            this.nameP = name;
            this.beginFile = alFile;
            this.endFile = alFile;
            this.beginOffset = alBeg;
            this.endOffset = alEnd;
            refreshOffsets();
        }

        public File(Filesystem parent, Directory parentDir, bool systemFile, int id, string name, File alFile, uint alBeg, uint alEnd, bool endsize)
        {
            this.parent = parent;
            this.parentDirP = parentDir;
            this.isSystemFileP = systemFile;
            this.idP = id;
            this.nameP = name;
            this.beginFile = alFile;
            this.endFile = alFile;
            this.beginOffset = alBeg;
            this.endOffset = alEnd;
            this.endIsSize = endsize;
            refreshOffsets();
        }

        public File(Filesystem parent, Directory parentDir, bool systemFile, int id, string name, uint alBeg, uint alSize)
        {
            this.parent = parent;
            this.parentDirP = parentDir;
            this.isSystemFileP = systemFile;
            this.idP = id;
            this.nameP = name;
            this.fileBegin = alBeg;
            this.fileSize = alSize;
            refreshOffsets();
        }

        public byte[] getContents()
        {
            byte[] file = new byte[fileSize];
            parent.s.Seek(fileBegin, SeekOrigin.Begin);
            parent.s.Read(file, 0, file.Length);
            return file;
        }

        public void dumpFile(int ind)
        {
            for (int i = 0; i < ind; i++)
                Console.Out.Write(" ");
            Console.Out.WriteLine("[" + id + "] " + name + ", at " + fileBegin.ToString("X") + ", size: " + fileSize);
        }


        protected void refreshOffsets()
        {
            if (beginFile != null)
                fileBegin = beginFile.getUintAt(beginOffset) + parent.fileDataOffset;

            if (endFile != null)
            {
                uint end = endFile.getUintAt(endOffset);
                if (endIsSize)
                    fileSize = end;
                else
                    fileSize = end + parent.fileDataOffset - fileBegin;
            }
        }

        private void saveOffsets()
        {
            if (beginFile != null)
                beginFile.setUintAt(beginOffset, fileBegin - parent.fileDataOffset);

            if (endFile != null)
                if (endIsSize)
                    endFile.setUintAt(endOffset, fileSize);
                else
                    endFile.setUintAt(endOffset, fileBegin + fileSize - parent.fileDataOffset);
        }

        public uint getUintAt(uint offset)
        {
            long pos = parent.s.Position;
            parent.s.Seek(fileBegin + offset, SeekOrigin.Begin);

            uint res = 0;
            for (int i = 0; i < 4; i++)
            {
                res |= (uint)parent.s.ReadByte() << 8 * i;
            }
            parent.s.Seek(pos, SeekOrigin.Begin);
            return res;
        }

        public void setUintAt(uint offset, uint val)
        {
            long pos = parent.s.Position;
            parent.s.Seek(fileBegin + offset, SeekOrigin.Begin);
            for (int i = 0; i < 4; i++)
            {
                parent.s.WriteByte((byte)(val & 0xFF));
                val >>= 8;
            }
            parent.s.Seek(pos, SeekOrigin.Begin);
        }

        public ushort getUshortAt(uint offset)
        {
            long pos = parent.s.Position;
            parent.s.Seek(fileBegin + offset, SeekOrigin.Begin);

            ushort res = 0;
            for (int i = 0; i < 2; i++)
            {
                res |= (ushort) (parent.s.ReadByte() << 8 * i);
            }
            parent.s.Seek(pos, SeekOrigin.Begin);
            return res;
        }


        public void setUshortAt(uint offset, ushort val)
        {
            long pos = parent.s.Position;
            parent.s.Seek(fileBegin + offset, SeekOrigin.Begin);
            for (int i = 0; i < 2; i++)
            {
                parent.s.WriteByte((byte)(val & 0xFF));
                val >>= 8;
            }
            parent.s.Seek(pos, SeekOrigin.Begin);
        }

        public byte getByteAt(uint offs)
        {
            long pos = parent.s.Position;
            parent.s.Seek(fileBegin + offs, SeekOrigin.Begin);
            byte res = (byte)parent.s.ReadByte();
            parent.s.Seek(pos, SeekOrigin.Begin);
            return res;
        }

        public void setByteAt(uint offs, byte val)
        {
            long pos = parent.s.Position;
            parent.s.Seek(fileBegin + offs, SeekOrigin.Begin);
            parent.s.WriteByte(val);
            parent.s.Seek(pos, SeekOrigin.Begin);
        }

        /*
        //This routine moves the next file to Pos
        //if its before pos
        //It moves further files if needed
        //This ensures that the space from this file
        //to Pos-1 (included) is unused
        private void ensureFreeSpaceUntil(uint pos)
        {
            File nextFile = null;
            uint nextFileStart = uint.MaxValue;
            uint start = fileBegin;

            foreach (File f in parent.allFiles)
            {
                uint fstart = f.fileBegin;
                if (fstart > start) //if the file is after this one
                    if (fstart < nextFileStart)
                    {
                        nextFile = f;
                        nextFileStart = fstart;
                    }
            }

            // check how much we'll have to move it
            int diff = (int)pos - (int)nextFileStart;
            if (diff > 0)
            {
                nextFile.moveTo(pos);
            }
        }

        public void moveTo(uint pos)
        {
//            Console.Out.WriteLine("Moving: [" + id + "] " + name);
            ensureFreeSpaceUntil(pos + fileSize);
            byte[] file = getContents();
            parent.s.Seek(pos, SeekOrigin.Begin);
            parent.s.Write(file, 0, file.Length);

            fileBegin = pos;
            saveOffsets();
        }
        */

        public void replace(byte[] newFile, object editor)
        {
            if (!beingEdited)
                throw new Exception("NOT EDITING FILE " + name);

            if(editor != editedBy)
                throw new Exception("NOT CORRECT EDITOR " + name);

            if(newFile.Length != fileSize && fixedFile)
                throw new Exception("TRYING TO RESIZE FIXED FILE: " + name);

//            Console.Out.WriteLine("Replacing: [" + id + "] " + name);
            uint newStart = fileBegin;

            if (newFile.Length > fileSize) //if we insert a bigger file
            {                         //it might not fit in the current place
                File before = parent.findFreeSpace(newFile.Length);
//                Console.Out.WriteLine("After " + before.name);
                newStart = before.fileBegin + before.fileSize;
            }

            //write the file
            parent.s.Seek(newStart, SeekOrigin.Begin);
            parent.s.Write(newFile, 0, newFile.Length);

            //update ending pos
            fileBegin = newStart;
            fileSize = (uint)newFile.Length;
            saveOffsets();
            parent.fileMoved(this);
        }

        public void moveTo(uint newOffs)
        {
            byte[] data = getContents();
            parent.s.Seek(newOffs, SeekOrigin.Begin);
            parent.s.Write(data, 0, data.Length);
            fileBegin = newOffs;
            saveOffsets();
        }

        public int CompareTo(object obj)
        {
            File f = obj as File;
            if (fileBegin == f.fileBegin)
                return fileSize.CompareTo(f.fileSize);
            return fileBegin.CompareTo(f.fileBegin);
        }

        public void beginEdit(Object editor)
        {
            if (beingEdited)
                throw new AlreadyEditingException(this);
            else
                editedBy = editor;
        }

        public void endEdit(Object editor)
        {
            if (!beingEdited)
                throw new Exception("NOT EDITING FILE " + name);
            if (editor != editedBy)
                throw new Exception("NOT CORRECT EDITOR" + name);

            editedBy = null;
        }

        public bool beingEditedBy(Object ed)
        {
            return ed == editedBy;
        }
        public virtual void fileModified() { }
    }
}
