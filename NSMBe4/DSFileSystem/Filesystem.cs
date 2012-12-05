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
using System.Drawing;

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
        public int fileDataOffset = 0;

        public FilesystemBrowser viewer;

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

        public Directory getDirByPath(string path)
        {
            string[] shit = path.Split(new char[] { '/' });
            Directory dir = mainDir;
            for (int i = 0; i < shit.Length; i++)
            {
                Directory newDir = null;
                foreach(Directory d in dir.childrenDirs)
                    if(d.name == shit[i])
                    {
                        newDir = d;
                        break;
                    }
                if(newDir == null) return null;

                dir = newDir;
            }
            return dir;
        }

        protected void addFile(File f)
        {
            allFiles.Add(f);
            if(f.id != -1)
                if(!filesById.ContainsKey(f.id))
                    filesById.Add(f.id, f);
//            filesByName.Add(f.name, f);
        }


        protected void addDir(Directory d)
        {
            allDirs.Add(d);
            if(d.id != -1)
               dirsById.Add(d.id, d);
//            dirsByName.Add(d.name, d);
        }

        int alignUp(int what, int align)
        {
            if (what % align != 0)
                what += align - what % align;
            return what;
        }
        int alignDown(int what, int align)
        {
            what -= what % align;
            return what;
        }


        //Tries to find LEN bytes of continuous unused space AFTER the freeSpaceDelimiter (usually fat or fnt)
        public int findFreeSpace(int len, int align)
        {
            allFiles.Sort(); //sort by offset

            File bestSpace = null;
            int bestSpaceLeft = int.MaxValue;
            int bestSpaceBegin = -1;

            for (int i = allFiles.IndexOf(freeSpaceDelimiter); i < allFiles.Count - 1; i++)
            {
                int spBegin = allFiles[i].fileBegin + allFiles[i].fileSize; //- 1 + 1;
                spBegin = alignUp(spBegin, align);

                int spEnd = allFiles[i + 1].fileBegin;
                spEnd = alignDown(spEnd, align);

                int spSize = spEnd - spBegin;

                if (spSize >= len)
                {
                    int spLeft = spSize - len;
                    if (spLeft < bestSpaceLeft)
                    {
                        bestSpaceLeft = spLeft;
                        bestSpace = allFiles[i];
                        bestSpaceBegin = spBegin;
                    }
                }
            }

            if (bestSpace != null)
                return bestSpaceBegin;
            else
                return alignUp(allFiles[allFiles.Count - 1].fileBegin + allFiles[allFiles.Count - 1].fileSize, align);
        }
        
        //yeah, i'm tired of looking through the dump myself ;)
        public bool findErrors()
        {
            allFiles.Sort();
            bool res = false;
            for (int i = 0; i < allFiles.Count - 1; i++)
            {
                int firstEnd = allFiles[i].fileBegin + allFiles[i].fileSize - 1;
                int secondStart = allFiles[i + 1].fileBegin;

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

        public void dumpFilesOrdered(TextWriter outs)
        {
            allFiles.Sort();
            foreach (File f in allFiles)
                outs.WriteLine(f.fileBegin.ToString("X8") + " .. " + (f.fileBegin + f.fileSize - 1).ToString("X8") + ":  " + f.getPath());
        }

        public virtual void fileMoved(File f)
        {
        }

        public int getFilesystemEnd()
        {
            allFiles.Sort();
            File lastFile = allFiles[allFiles.Count - 1];
            int end = lastFile.fileBegin + lastFile.fileSize; //well, 1 byte doesnt matter
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


        public void moveAllFiles(File first, int firstOffs)
        {
            allFiles.Sort();
            //Console.Out.WriteLine("Moving file " + first.name);
            //Console.Out.WriteLine("Into " + firstOffs.ToString("X"));


            int firstStart = first.fileBegin;
            int diff = (int)firstOffs - (int)firstStart;
            //Console.Out.WriteLine("DIFF " + diff.ToString("X"));
            
            //WARNING: I assume all the aligns are powers of 2
            int maxAlign = 4;
            for(int i = allFiles.IndexOf(first); i < allFiles.Count; i++)
            {
                if(allFiles[i].alignment > maxAlign)
                    maxAlign = allFiles[i].alignment;
            }

            //To preserve the alignment of all the moved files
            if(diff % maxAlign != 0)
                diff += (int)(maxAlign - diff % maxAlign);


            int fsEnd = getFilesystemEnd();
            int toCopy = (int)fsEnd - (int)firstStart;
            byte[] data = new byte[toCopy];

            s.Seek(firstStart, SeekOrigin.Begin);
            s.Read(data, 0, toCopy);
            s.Seek(firstStart + diff, SeekOrigin.Begin);
            s.Write(data, 0, toCopy);

            for (int i = allFiles.IndexOf(first); i < allFiles.Count; i++)
                allFiles[i].fileBegin += diff;
            for (int i = allFiles.IndexOf(first); i < allFiles.Count; i++)
                allFiles[i].saveOffsets();
        }

        public void filesystemModified()
        {
            if (viewer != null && !viewer.IsDisposed)
                viewer.Load(this);
        }

        public Bitmap renderFilesystemMap()
        {
            int height = 4096;
            int width = 256;
            int div = 256;
            Bitmap b = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(b);
            g.FillRectangle(Brushes.Black, 0, 0, 1024, height);

            for (int i = 0; i < allFiles.Count; i++)
            {
                int start = allFiles[i].fileBegin/ div;
                int size = (allFiles[i].fileSize+div-1) / div;
                for (int j = 0; j < size; j++)
                {
                    int x = start + j;
                    if (x >= width * height) continue;
                    int y = x / width;
                    x %= width;
                    Color c = allFiles[i].id < 0 ? (i%2 == 0?Color.Crimson: Color.Coral): (i%2 == 0?Color.LightBlue: Color.LightCyan);
                    b.SetPixel(x, y, c);
                }
            }
            return b;
        }
        public void replaceRandomFiles()
        {
            Random r = new Random();
            for (int i = 0; i < 200; i++)
            {
                File f = allFiles[r.Next(allFiles.Count)];
                if (f.id < 180) continue;

                byte[] newd = new byte[r.Next(1024*10)];
                f.beginEdit(this);
                f.replace(newd, this);
                f.endEdit(this);
            }
        }
    }
}
