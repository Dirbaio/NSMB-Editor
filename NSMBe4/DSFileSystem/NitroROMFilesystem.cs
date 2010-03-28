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
            arm9binFile = new File(this, mainDir, true, -6, "arm9.bin", headerFile, 0x20, 0x2C, true);
            arm9binFile.alignment = 0x1000;
            arm9binFile.canChangeOffset = false;
            arm7binFile = new File(this, mainDir, true, -7, "arm7.bin", headerFile, 0x30, 0x3C, true);
            arm7binFile.alignment = 0x200; //Not sure what should be used here...
            bannerFile = new BannerFile(this, mainDir, headerFile);
            bannerFile.alignment = 0x200; //Not sure what should be used here...
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

                loadFile(string.Format(LanguageManager.Get("NitroClass", "OverlayFile"), ovId, ramAddr.ToString("X"), ramSize.ToString("X")), fileID, dir).isSystemFile = true;
            }
        }

        public override void fileMoved(File f)
        {
            uint end = (uint)getFilesystemEnd();
            headerFile.setUintAt(0x80, end);
            headerFile.UpdateCRC16();
        }

        public void disableOverlay0Compression()
        {
            // THIS DISABLES COMPRESSION!!
            // setting 0x1F in the overlay table to 02 is what causes the game
            // to bypass it - I'm not sure if the RAM size needs to be written
            // as well, but I do it anyway just in case.. ~Treeki

            arm9ovFile.setUintAt(8, (uint) getFileById(0).fileSize);
            arm9ovFile.setByteAt(0x1F, 2);
        }

        public bool isOverlay0Compressed()
        {
            return arm9ovFile.getByteAt(0x1F) != 0x02;
        }
    }
}
