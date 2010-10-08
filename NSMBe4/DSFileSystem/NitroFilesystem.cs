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
    public class NitroFilesystem : Filesystem
    {
        public File fatFile, fntFile;

        public NitroFilesystem(FilesystemSource s)
            : base(s)
        {
            mainDir = new Directory(this, null, true, "FILESYSTEM ["+s.getDescription()+"]", -100);
            load();
        }

        public virtual void load()
        {
            
            addDir(mainDir);


            addFile(fntFile);
            mainDir.childrenFiles.Add(fntFile);
            addFile(fatFile);
            mainDir.childrenFiles.Add(fatFile);

            freeSpaceDelimiter = fntFile;
            //read the fnt
            ByteArrayInputStream fnt = new ByteArrayInputStream(fntFile.getContents());
            //            fnt.dumpAsciiData();

            loadDir(fnt, "Files", 0xF000, mainDir);
            
        }


        private void loadDir(ByteArrayInputStream fnt, string dirName, int dirID, Directory parent)
        {
            fnt.savePos();
            fnt.seek(8 * (dirID & 0xFFF));
            uint subTableOffs = fnt.readUInt();

            int fileID = fnt.readUShort();

            //Crappy hack for MKDS course .carc's. 
            //Their main dir starting ID is 2, which is weird...
            if (parent == mainDir) fileID = 0; 

            Directory thisDir = new Directory(this, parent, false, dirName, dirID);
            addDir(thisDir);
            parent.childrenDirs.Add(thisDir);

            fnt.seek((int)subTableOffs);
            while (true)
            {
                byte data = fnt.readByte();
                int len = data & 0x7F;
                bool isDir = (data & 0x80) != 0;
                if (len == 0)
                    break;
                String name = fnt.ReadString(len);

                if (isDir)
                {
                    int subDirID = fnt.readUShort();
                    loadDir(fnt, name, subDirID, thisDir);
                }
                else
                    loadFile(name, fileID, thisDir);

                fileID++;
            }
            fnt.loadPos();
        }

        protected virtual void loadNamelessFiles(Directory parent)
        {
            bool ok = true;
            for (int i = 0; i < fatFile.fileSize / 8; i++)
            {
                if (getFileById(i) == null)
                    ok = false;
            }

            if (ok) return;

            Directory d = new Directory(this, parent, true, "Unnamed files", -94);
            parent.childrenDirs.Add(d);
            allDirs.Add(d);

            for (int i = 0; i < fatFile.fileSize / 8; i++)
            {
                if (getFileById(i) == null)
                    loadFile("File " + i, i, d);
            }
        }

        protected File loadFile(string fileName, int fileID, Directory parent)
        {
            int beginOffs = fileID * 8;
            int endOffs = fileID * 8 + 4;
            File f = new File(this, parent, false, fileID, fileName, fatFile, beginOffs, endOffs);
            parent.childrenFiles.Add(f);
            addFile(f);
            return f;

        }

    }
}
