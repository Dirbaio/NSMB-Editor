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
    public abstract class Filesystem
    {
        protected FilesystemSource source;
        public List<File> allFiles = new List<File>();
        public List<Directory> allDirs = new List<Directory>();
        protected Dictionary<int, File> filesById = new Dictionary<int, File>();
//        protected Dictionary<string, File> filesByName = new Dictionary<string, File>();
        protected Dictionary<int, Directory> dirsById = new Dictionary<int, Directory>();
//        protected Dictionary<string, Directory> dirsByName = new Dictionary<string, Directory>();
        public Stream s;
        public Directory mainDir;
        protected File freeSpaceDelimiter;
        public uint fileDataOffset = 0;

        protected Filesystem(FilesystemSource fs)
        {
            this.source = fs;
            this.s = source.load();
        }

        public File getFileById(int id)
        {
            if (!filesById.ContainsKey(id))
                return null;
            return filesById[id];
        }

        public File getFileByName(string name)
        {
            foreach (File f in allFiles)
                if (!f.isSystemFile && f.name == name)
                    return f;

            return null;
        }

        protected void addFile(File f)
        {
            allFiles.Add(f);
            filesById.Add(f.id, f);
//            filesByName.Add(f.name, f);
        }


        protected void addDir(Directory d)
        {
            allDirs.Add(d);
            dirsById.Add(d.id, d);
//            dirsByName.Add(d.name, d);
        }

        //Tries to find LEN bytes of continuous unused space AFTER the freeSpaceDelimiter (usually fat or fnt)
        public File findFreeSpace(int len, uint align)
        {
            allFiles.Sort(); //sort by offset

            File bestSpace = null;
            int bestSpaceLeft = int.MaxValue;

            for (int i = allFiles.IndexOf(freeSpaceDelimiter); i < allFiles.Count - 1; i++)
            {
                uint spBegin = (uint)allFiles[i].fileBegin + (uint)allFiles[i].fileSize; //- 1 + 1;
                if (spBegin % align != 0)
                    spBegin += align - spBegin % align;

                uint spEnd = (uint)allFiles[i + 1].fileBegin - 1;
                if (spEnd % align != 0)
                    spEnd -= spEnd % align;
                uint spSize = spEnd - spBegin + 1;
                if (spSize >= len)
                {
                    int spLeft = (int)len - (int)spSize;
                    if (spLeft < bestSpaceLeft)
                    {
                        bestSpaceLeft = spLeft;
                        bestSpace = allFiles[i];
                    }
                }
            }

            if (bestSpace != null)
                return bestSpace;
            else //if theres no space
                return allFiles[allFiles.Count - 1]; //just add the file at the very end 
        }

        //yeah, i'm tired of looking through the dump myself ;)
        public bool findErrors()
        {
            allFiles.Sort();
            bool res = false;
            for (int i = 0; i < allFiles.Count - 1; i++)
            {
                int firstEnd = (int)allFiles[i].fileBegin + (int)allFiles[i].fileSize - 1;
                int secondStart = (int)allFiles[i + 1].fileBegin;

                if (firstEnd >= secondStart)
                {
                    Console.Out.WriteLine("ERROR: FILES OVERLAP:");
                    allFiles[i].dumpFile(2);
                    allFiles[i + 1].dumpFile(2);
                    res = true;
                }
            }

            return res;
        }

        public void close()
        {
            source.close();
        }

        public void save()
        {
            source.save();
        }

        public void dumpFilesOrdered()
        {
            allFiles.Sort();
            foreach (File f in allFiles)
            {
                Console.Out.WriteLine(f.name + " " + f.fileBegin.ToString("X") + " - " + (f.fileBegin + f.fileSize - 1).ToString("X"));
            }
        }

        public virtual void fileMoved(File f)
        {
        }

        public uint getFilesystemEnd()
        {
            allFiles.Sort();
            File lastFile = allFiles[allFiles.Count - 1];
            uint end = lastFile.fileBegin + lastFile.fileSize; //well, 1 byte doesnt matter
            return end;
        }



        public uint readUInt(Stream s)
        {
            uint res = 0;
            for (int i = 0; i < 4; i++)
            {
                res |= (uint)s.ReadByte() << 8 * i;
            }
            return res;
        }


        public void moveAllFilesForward(File first, uint firstOffs)
        {
            allFiles.Sort();
            Console.Out.WriteLine("Moving file " + first.name);
            Console.Out.WriteLine("Into " + firstOffs.ToString("X"));


            uint firstStart = first.fileBegin;
            int diff = (int)firstOffs - (int)firstStart;
            Console.Out.WriteLine("DIFF " + diff.ToString("X"));
            if (diff < 0)
                //throw new Exception("DOSADJODJOSAJD");
                return;

            //WARNING: I assume all the aligns are powers of 2
            uint maxAlign = 4;
            for(int i = allFiles.IndexOf(first); i < allFiles.Count; i++)
            {
                if(allFiles[i].alignment > maxAlign)
                    maxAlign = allFiles[i].alignment;
            }

            //To preserve the alignment of all the moved files
            if(diff % maxAlign != 0)
                diff += (int)(maxAlign - diff % maxAlign);


            uint fsEnd = getFilesystemEnd();
            int toCopy = (int)fsEnd - (int)firstStart;
            byte[] data = new byte[toCopy];

            s.Seek(firstStart, SeekOrigin.Begin);
            s.Read(data, 0, toCopy);
            s.Seek(firstStart + diff, SeekOrigin.Begin);
            s.Write(data, 0, toCopy);

            for (int i = allFiles.IndexOf(first); i < allFiles.Count; i++)
                allFiles[i].fileBegin += (uint)diff;
            for (int i = allFiles.IndexOf(first); i < allFiles.Count; i++)
                allFiles[i].saveOffsets();


        }
    }
}
