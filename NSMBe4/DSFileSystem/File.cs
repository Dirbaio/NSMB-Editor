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

        private File beginFile;
        private uint beginOffset;
        private File endFile;
        private uint endOffset;
        private bool endIsSize; //means that the end offset is the size of the file

        public uint fileBegin;
        public uint fileSize;

        private Filesystem parent;

        public Boolean beingEdited;

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

        private void refreshOffsets()
        {
            if (beginFile != null)
                fileBegin = beginFile.getUintAt(beginOffset);

            if (endFile != null)
            {
                uint end = endFile.getUintAt(endOffset);
                if (endIsSize)
                    fileSize = end;
                else
                    fileSize = end - fileBegin;
            }
        }

        private void saveOffsets()
        {
            if (beginFile != null)
                beginFile.setUintAt(beginOffset, fileBegin);

            if (endFile != null)
                if (endIsSize)
                    endFile.setUintAt(endOffset, fileSize);
                else
                    endFile.setUintAt(endOffset, fileBegin + fileSize);
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

        public void replace(byte[] newFile)
        {
            //if (!beingEdited)
            //    throw new Exception("NOT EDITING FILE " + name);

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
        }

        public int CompareTo(object obj)
        {
            File f = obj as File;
            if (fileBegin == f.fileBegin)
                return fileSize.CompareTo(f.fileSize);
            return fileBegin.CompareTo(f.fileBegin);
        }

        public void beginEdit()
        {
            if (beingEdited)
                throw new AlreadyEditingException(this);
            else
                beingEdited = true;
        }

        public void endEdit()
        {
            /*
            if (!beingEdited)
                throw new Exception("NOT EDITING FILE " + name);*/

            beingEdited = false;
        }
    }
}
