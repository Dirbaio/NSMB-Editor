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

        protected void loadFile(string fileName, int fileID, Directory parent)
        {
            uint beginOffs = (uint)fileID * 8;
            uint endOffs = (uint)fileID * 8 + 4;
            File f = new File(this, parent, false, fileID, fileName, fatFile, beginOffs, endOffs);
            parent.childrenFiles.Add(f);
            addFile(f);
        }

    }
}
