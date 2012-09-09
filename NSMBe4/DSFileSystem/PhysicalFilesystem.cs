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
    public abstract class PhysicalFilesystem : Filesystem
    {
        protected FilesystemSource source;
        public Stream s;
        protected File freeSpaceDelimiter;

        protected int fileDataOffsetP;
        public int fileDataOffset { get { return fileDataOffsetP; } }

        protected PhysicalFilesystem(FilesystemSource fs)
        {
            this.source = fs;
            this.s = source.load();
        }

        //Tries to find LEN bytes of continuous unused space AFTER the freeSpaceDelimiter (usually fat or fnt)
        public int findFreeSpace(int len, int align)
        {
            allFiles.Sort(); //sort by offset

            PhysicalFile bestSpace = null;
            int bestSpaceLeft = int.MaxValue;
            int bestSpaceBegin = -1;

            for (int i = allFiles.IndexOf(freeSpaceDelimiter); i < allFiles.Count - 1; i++)
            {
            	PhysicalFile a = (PhysicalFile) allFiles[i];
            	PhysicalFile b = (PhysicalFile) allFiles[i+1];
            	
                int spBegin = a.fileBegin + a.fileSize; //- 1 + 1;
                spBegin = alignUp(spBegin, align);

                int spEnd = b.fileBegin;
                spEnd = alignDown(spEnd, align);

                int spSize = spEnd - spBegin;

                if (spSize >= len)
                {
                    int spLeft = spSize - len;
                    if (spLeft < bestSpaceLeft)
                    {
                        bestSpaceLeft = spLeft;
                        bestSpace = a;
                        bestSpaceBegin = spBegin;
                    }
                }
            }

            if (bestSpace != null)
                return bestSpaceBegin;
            else
            {
            	PhysicalFile last = (PhysicalFile) allFiles[allFiles.Count - 1];
                return alignUp(last.fileBegin + last.fileSize, align);
            }
        }

        public void moveAllFiles(PhysicalFile first, int firstOffs)
        {
            allFiles.Sort();
            Console.Out.WriteLine("Moving file " + first.name);
            Console.Out.WriteLine("Into " + firstOffs.ToString("X"));

            int firstStart = first.fileBegin;
            int diff = (int)firstOffs - (int)firstStart;
            Console.Out.WriteLine("DIFF " + diff.ToString("X"));
            //if (diff < 0)
                //throw new Exception("DOSADJODJOSAJD");
            //    return;

            //WARNING: I assume all the aligns are powers of 2
            int maxAlign = 4;
            for(int i = allFiles.IndexOf(first); i < allFiles.Count; i++)
            {
            	int align = ((PhysicalFile)allFiles[i]).alignment;
                if(align > maxAlign)
                    maxAlign = align;
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
                ((PhysicalFile)allFiles[i]).fileBeginP += diff;
            for (int i = allFiles.IndexOf(first); i < allFiles.Count; i++)
                ((PhysicalFile)allFiles[i]).saveOffsets();
        }
        
        public override void close()
        {
            source.close();
        }

        public override void save()
        {
            source.save();
        }

        public void dumpFilesOrdered(TextWriter outs)
        {
            allFiles.Sort();
            foreach (PhysicalFile f in allFiles)
                outs.WriteLine(f.fileBegin.ToString("X8") + " .. " + (f.fileBegin + f.fileSize - 1).ToString("X8") + ":  " + f.getPath());
        }

        public int getFilesystemEnd()
        {
            allFiles.Sort();
            PhysicalFile lastFile = (PhysicalFile) allFiles[allFiles.Count - 1];
            int end = lastFile.fileBegin + lastFile.fileSize;
            return end;
        }
	}
}
