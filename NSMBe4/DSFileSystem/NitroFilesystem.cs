using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace NSMBe4.DSFileSystem
{
    public class NitroFilesystem : Filesystem
    {
        private File headerFile, fatFile, fntFile, arm7binFile, arm9binFile, arm7ovFile, arm9ovFile;
        
        public NitroFilesystem(String n) : base(new ExternalFilesystemSource(n))
        {
            load();
        }

        private void load()
        {
            mainDir = new Directory(this, null, true, "FILESYSTEM", -100);
            addDir(mainDir);
            headerFile = new File(this, mainDir, true, -1, "__NDS ROM HEADER", 0, 0x15D);
            fntFile    = new File(this, mainDir, true, -2, "__NDS ROM FNT", headerFile, 0x40, 0x44, true);
            fatFile = new File(this, mainDir, true, -3, "__NDS ROM FAT", headerFile, 0x48, 0x4C, true);
            arm9ovFile = new File(this, mainDir, true, -4, "__NDS ROM ARM9 OVT", headerFile, 0x50, 0x54, true);
            arm7ovFile = new File(this, mainDir, true, -5, "__NDS ROM ARM7 OVT", headerFile, 0x58, 0x5C, true);

            addFile(headerFile);
            mainDir.childrenFiles.Add(headerFile);
            addFile(fntFile);
            mainDir.childrenFiles.Add(fntFile);
            addFile(fatFile);
            mainDir.childrenFiles.Add(fatFile);
            addFile(arm9ovFile);
            mainDir.childrenFiles.Add(arm9ovFile);
            addFile(arm7ovFile);
            mainDir.childrenFiles.Add(arm7ovFile);

            freeSpaceDelimiter = fntFile;
            mainDir.dumpFiles();

            //read the fnt
            ByteArrayInputStream fnt = new ByteArrayInputStream(fntFile.getContents());
//            fnt.dumpAsciiData();

            loadDir(fnt, "Files", 0xF000, mainDir);
            loadOvTable("ARM7 Overlay Table", -99, mainDir, arm7ovFile);
            loadOvTable("ARM9 Overlay Table", -98, mainDir, arm9ovFile);

        }

        private void loadOvTable(String dirName, int id, Directory parent, File table)
        {
            Directory dir = new Directory(this, parent, true, dirName, id);
            addDir(dir);
            parent.childrenDirs.Add(dir);

            ByteArrayInputStream tbl = new ByteArrayInputStream(table.getContents());

            while (tbl.available(32))
            {
                uint ovId = tbl.readUInt();
                uint ramAddr = tbl.readUInt();
                uint ramSize = tbl.readUInt();
                uint bssSize = tbl.readUInt();
                uint staticInitStart = tbl.readUInt();
                uint staticInitEnd = tbl.readUInt();
                ushort fileID = tbl.readUShort();
                tbl.skip(6); //unused 0's

                loadFile(string.Format(LanguageManager.Get("NitroClass", "OverlayFile"), ovId, ramAddr.ToString("X"), ramSize.ToString("X")), fileID, dir);
            }
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

        private void loadFile(string fileName, int fileID, Directory parent)
        {
            uint beginOffs = (uint)fileID * 8;
            uint endOffs = (uint)fileID * 8 + 4;
            File f = new File(this, parent, false, fileID, fileName, fatFile, beginOffs, endOffs);
            parent.childrenFiles.Add(f);
            addFile(f);
        }
    }
}
