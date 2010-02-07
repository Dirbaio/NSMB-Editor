using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace NSMBe4.DSFileSystem
{
    public class NitroROMFilesystem : NitroFilesystem
    {
        public File arm7binFile, arm9binFile, arm7ovFile, arm9ovFile, bannerFile;
        public HeaderFile headerFile;

        public NitroROMFilesystem(String n)
            : base(new ExternalFilesystemSource(n))
        {
        }

        public override void load()
        {
            headerFile = new HeaderFile(this, mainDir);

            fntFile = new File(this, mainDir, true, -2, "fnt.bin", headerFile, 0x40, 0x44, true);
            fatFile = new File(this, mainDir, true, -3, "fat.bin", headerFile, 0x48, 0x4C, true);

            base.load();

            arm9ovFile = new File(this, mainDir, true, -4, "arm9ovt.bin", headerFile, 0x50, 0x54, true);
            arm7ovFile = new File(this, mainDir, true, -5, "arm7ovt.bin", headerFile, 0x58, 0x5C, true);
            bannerFile = new BannerFile(this, mainDir, headerFile);
            addFile(headerFile);
            mainDir.childrenFiles.Add(headerFile);
            addFile(arm9ovFile);
            mainDir.childrenFiles.Add(arm9ovFile);
            addFile(arm7ovFile);
            mainDir.childrenFiles.Add(arm7ovFile);
            addFile(bannerFile);
            mainDir.childrenFiles.Add(bannerFile);
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

        public override void fileMoved(File f)
        {
            uint end = getFilesystemEnd();
            headerFile.setUintAt(0x80, end);
            headerFile.UpdateCRC16();
        }

        public void disableOverlay0Compression()
        {
            // THIS DISABLES COMPRESSION!!
            // setting 0x1F in the overlay table to 02 is what causes the game
            // to bypass it - I'm not sure if the RAM size needs to be written
            // as well, but I do it anyway just in case.. ~Treeki

            arm9ovFile.setUintAt(8, getFileById(0).fileSize);
            arm9ovFile.setByteAt(0x1F, 2);
        }

        public bool isOverlay0Compressed()
        {
            return arm9ovFile.getByteAt(0x1F) != 0x02;
        }
    }
}
