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
    public class PhysicalFile : FileWithLock, IComparable
    {
        public bool isSystemFile;

		//File that specifies where the file begins.
        protected File beginFile;
        protected int beginOffset;
        
        //File that specifies where the file ends OR the file size.
        protected File endFile;
        protected int endOffset;
        protected bool endIsSize;
        
        //If true, file begin/size can't change at all.
        //TODO: Make sure these are set properly. I think they aren't.
        public bool canChangeOffset = true;
        public bool canChangeSize = true;

		//File begin offset
        public int fileBeginP;
        public int fileBegin { get { return fileBeginP; } }

        public int alignment = 4; // word align by default
        
        //For convenience
        public Stream filesystemStream { get { return ((PhysicalFilesystem)parent).s; } }
        public int filesystemDataOffset { get { return ((PhysicalFilesystem)parent).fileDataOffset; } }
        
        public PhysicalFile(Filesystem parent, Directory parentDir, string name)
        	:base(parent, parentDir, name, -1)
        {
        }
    
        public PhysicalFile(Filesystem parent, Directory parentDir, int id, string name, File alFile, int alBeg, int alEnd)
        	:base(parent, parentDir, name, id)
        {
            this.beginFile = alFile;
            this.endFile = alFile;
            this.beginOffset = alBeg;
            this.endOffset = alEnd;
            refreshOffsets();
        }

        public PhysicalFile(Filesystem parent, Directory parentDir, int id, string name, File alFile, int alBeg, int alEnd, bool endsize)
        	:base(parent, parentDir, name, id)
        {
            this.beginFile = alFile;
            this.endFile = alFile;
            this.beginOffset = alBeg;
            this.endOffset = alEnd;
            this.endIsSize = endsize;
            refreshOffsets();
        }

        public PhysicalFile(Filesystem parent, Directory parentDir, int id, string name, int alBeg, int alSize)
        	:base(parent, parentDir, name, id)
        {
            this.fileBeginP = alBeg;
            this.fileSizeP = alSize;
            this.canChangeOffset = false;
            this.canChangeSize = false;
            refreshOffsets();
        }

        public virtual void refreshOffsets()
        {
            if (beginFile != null)
                fileBeginP = (int)beginFile.getUintAt(beginOffset) + filesystemDataOffset;

            if (endFile != null)
            {
                int end = (int)endFile.getUintAt(endOffset);
                if (endIsSize)
                    fileSizeP = (int)end;
                else
                    fileSizeP = (int)end + filesystemDataOffset - fileBegin;
            }
        }

        public virtual void saveOffsets()
        {
            if (beginFile != null)
                beginFile.setUintAt(beginOffset, (uint)(fileBegin - filesystemDataOffset));

            if (endFile != null)
                if (endIsSize)
                    endFile.setUintAt(endOffset, (uint)fileSize);
                else
                    endFile.setUintAt(endOffset, (uint)(fileBegin + fileSize - filesystemDataOffset));
        }
	
		//Reading and writing!
		public override byte[] getInterval(int start, int end)
		{
			validateInterval(start, end);
			
			int len = end - start;
            byte[] file = new byte[len];
            filesystemStream.Seek(fileBegin+start, SeekOrigin.Begin);
            filesystemStream.Read(file, 0, len);
            return file;
		}

        public override byte[] getContents()
        {
            byte[] file = new byte[fileSize];
            filesystemStream.Seek(fileBegin, SeekOrigin.Begin);
            filesystemStream.Read(file, 0, file.Length);
            return file;
        }
		
        public override void replaceInterval(byte[] newFile, int start)
		{
			validateInterval(start, start+newFile.Length);
			if(!editedIntervals.Contains(new Interval(start, start+newFile.Length)) && editedBy == null)
	            throw new Exception("NOT CORRECT EDITOR " + name);
            filesystemStream.Seek(fileBegin+start, SeekOrigin.Begin);
            filesystemStream.Write(newFile, 0, newFile.Length);
		}
		
		//TODO: Clean up this mess.
        public override void replace(byte[] newFile, object editor)
        {
            if(!isAGoodEditor(editor))
                throw new Exception("NOT CORRECT EDITOR " + name);

            if(newFile.Length != fileSize && !canChangeSize)
                throw new Exception("TRYING TO RESIZE CONSTANT-SIZE FILE: " + name);

            int newStart = fileBegin;

            //if we insert a bigger file it might not fit in the current place
            if (newFile.Length > fileSize) 
            {
                if (canChangeOffset && !(parent is NarcFilesystem))
                {
                    newStart = ((PhysicalFilesystem)parent).findFreeSpace(newFile.Length, alignment);
                    if (newStart % alignment != 0)
                        newStart += alignment - newStart % alignment;
                }
                else
                {
                	//TODO: Keep the list always sorted in order to avoid stupid useless sorts.
                    parent.allFiles.Sort();
                    if (!(parent.allFiles.IndexOf(this) == parent.allFiles.Count - 1))
                    {
                        PhysicalFile nextFile = (PhysicalFile) parent.allFiles[parent.allFiles.IndexOf(this) + 1];
                        ((PhysicalFilesystem)parent).moveAllFiles(nextFile, fileBegin + newFile.Length);
                    }
                }
            }
            //This is for keeping NARC filesystems compact. Sucks.
            else if(parent is NarcFilesystem)
            {
                parent.allFiles.Sort();
                if (!(parent.allFiles.IndexOf(this) == parent.allFiles.Count - 1))
                {
                    PhysicalFile nextFile = (PhysicalFile) parent.allFiles[parent.allFiles.IndexOf(this) + 1];
                    ((PhysicalFilesystem)parent).moveAllFiles(nextFile, fileBegin + newFile.Length);
                }
            }
            
            //Stupid check.
            if (newStart % alignment != 0)
                Console.Out.Write("Warning: File is not being aligned: " + name + ", at " + newStart.ToString("X"));

            //write the file
            filesystemStream.Seek(newStart, SeekOrigin.Begin);
            filesystemStream.Write(newFile, 0, newFile.Length);
            
            //This should be handled in NarcFilesystem instead, in fileMoved (?)
            if(parent is NarcFilesystem)
            {
            	PhysicalFile lastFile = (PhysicalFile) parent.allFiles[parent.allFiles.Count - 1];
                filesystemStream.SetLength(lastFile.fileBegin + lastFile.fileSize + 16);
			}
			
            //update ending pos
            fileBeginP = newStart;
            fileSizeP = newFile.Length;
            saveOffsets();

			//Updates total used rom size in header, and/or other stuff.
            parent.fileMoved(this); 
        }

        public void moveTo(int newOffs)
        {
            if (newOffs % alignment != 0)
                Console.Out.Write("Warning: File is not being aligned: " + name + ", at " + newOffs.ToString("X"));

            byte[] data = getContents();
            filesystemStream.Seek(newOffs, SeekOrigin.Begin);
            filesystemStream.Write(data, 0, data.Length);

            fileBeginP = newOffs;
            saveOffsets();
        }
		

		//Misc crap
		
        public int CompareTo(object obj)
        {
            PhysicalFile f = (PhysicalFile) obj;
            if (fileBegin == f.fileBegin)
                return fileSize.CompareTo(f.fileSize);
            return fileBegin.CompareTo(f.fileBegin);
        }

        public bool isAddrInFdile(int addr)
        {
            return addr >= fileBegin && addr < fileBegin + fileSize;
        }
    }
}
