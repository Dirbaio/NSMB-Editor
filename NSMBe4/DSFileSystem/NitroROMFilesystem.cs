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
    public class NitroROMFilesystem : NitroFilesystem
    {
        public PhysicalFile arm7binFile, arm7ovFile, arm9ovFile, bannerFile;
        public PhysicalFile arm9binFile;
        public PhysicalFile rsaSigFile;
        public HeaderFile headerFile;

        public NitroROMFilesystem(String n)
            : base(new ExternalFilesystemSource(n))
        {
        }

        public override void load()
        {
            headerFile = new HeaderFile(this, mainDir);

            fntFile = new PhysicalFile(this, mainDir, -1, "fnt.bin", headerFile, 0x40, 0x44, true);
            fatFile = new PhysicalFile(this, mainDir, -1, "fat.bin", headerFile, 0x48, 0x4C, true);

            base.load();

            arm9ovFile = new PhysicalFile(this, mainDir, -1, "arm9ovt.bin", headerFile, 0x50, 0x54, true);
            arm7ovFile = new PhysicalFile(this, mainDir, -1, "arm7ovt.bin", headerFile, 0x58, 0x5C, true);
            //            arm9binFile = new Arm9BinFile(this, mainDir, headerFile);
            //            File arm9binFile2 = new PhysicalFile(this, mainDir, true, -2, "arm9.bin", headerFile, 0x20, 0xC, true);
            arm9binFile = new PhysicalFile(this, mainDir, -1, "arm9.bin", headerFile, 0x20, 0x2C, true);
            arm9binFile.alignment = 0x1000;
            arm9binFile.canChangeOffset = false;
            arm7binFile = new PhysicalFile(this, mainDir, -1, "arm7.bin", headerFile, 0x30, 0x3C, true);
            arm7binFile.alignment = 0x200; //Not sure what should be used here...
            bannerFile = new BannerFile(this, mainDir, headerFile);
            bannerFile.alignment = 0x200; //Not sure what should be used here...

            uint rsaOffs = headerFile.getUintAt(0x1000);

            if (rsaOffs == 0)
            {
                rsaOffs = headerFile.getUintAt(0x80);
                headerFile.setUintAt(0x1000, rsaOffs);
            }

            rsaSigFile = new PhysicalFile(this, mainDir, -1, "rsasig.bin", (int)rsaOffs, 136);
            rsaSigFile.canChangeOffset = false;

            addFile(headerFile);
            mainDir.childrenFiles.Add(headerFile);
            addFile(arm9ovFile);
            mainDir.childrenFiles.Add(arm9ovFile);
            addFile(arm7ovFile);
            mainDir.childrenFiles.Add(arm7ovFile);
            addFile(arm9binFile);
            mainDir.childrenFiles.Add(arm9binFile);
            addFile(arm7binFile);
            mainDir.childrenFiles.Add(arm7binFile);
            addFile(bannerFile);
            mainDir.childrenFiles.Add(bannerFile);
            addFile(rsaSigFile);
            mainDir.childrenFiles.Add(rsaSigFile);

            loadOvTable("overlay7", -99, mainDir, arm7ovFile);
            loadOvTable("overlay9", -98, mainDir, arm9ovFile);
            loadNamelessFiles(mainDir);
        }

        private void loadOvTable(String dirName, int id, Directory parent, File table)
        {
            Directory dir = new Directory(this, parent, true, dirName, id);
            addDir(dir);
            parent.childrenDirs.Add(dir);

            ByteArrayInputStream tbl = new ByteArrayInputStream(table.getContents());

            int i = 0;
            while (tbl.lengthAvailable(32))
            {
                uint ovId = tbl.readUInt();
                uint ramAddr = tbl.readUInt();
                uint ramSize = tbl.readUInt();
                uint bssSize = tbl.readUInt();
                uint staticInitStart = tbl.readUInt();
                uint staticInitEnd = tbl.readUInt();
                ushort fileID = tbl.readUShort();
                tbl.skip(6); //unused 0's

                File f = loadFile(dirName+"_"+ovId+".bin", fileID, dir);
//                f.isSystemFile = true;

                i++;
            }
        }

        public override void fileMoved(File f)
        {
            if (!ROM.dlpMode)
            {
                uint end = (uint)getFilesystemEnd();
                headerFile.setUintAt(0x80, end);
                headerFile.UpdateCRC16();
            }
        }
    }
}
