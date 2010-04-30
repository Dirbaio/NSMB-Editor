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
        public OverlayFile[] arm7ovs, arm9ovs;

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
            loadOvTable("ARM7 Overlay Table", -99, mainDir, arm7ovFile, out arm7ovs);
            loadOvTable("ARM9 Overlay Table", -98, mainDir, arm9ovFile, out arm9ovs);
        }

        private void loadOvTable(String dirName, int id, Directory parent, File table, out OverlayFile[] arr)
        {
            Directory dir = new Directory(this, parent, true, dirName, id);
            addDir(dir);
            parent.childrenDirs.Add(dir);

            ByteArrayInputStream tbl = new ByteArrayInputStream(table.getContents());
            arr = new OverlayFile[tbl.available() / 32];

            int i = 0;
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

                OverlayFile f = loadOvFile(fileID, dir, table, tbl.getPos());
                f.isSystemFile = true;
                arr[i] = f;

                i++;

            }
        }

        protected OverlayFile loadOvFile(int fileID, Directory parent, File ovTableFile, uint ovTblOffs)
        {
            int beginOffs = fileID * 8;
            int endOffs = fileID * 8 + 4;
            OverlayFile f = new OverlayFile(this, parent, fileID, fatFile, beginOffs, endOffs,ovTableFile, ovTblOffs);
            parent.childrenFiles.Add(f);
            addFile(f);
            return f;

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

        public bool isArm9BinaryCompressed()
        {
            int codeTableOffs = (int)(arm9binFile.getUintAt(0x90C) - 0x02000000u);
            int decompressionOffs = (int)arm9binFile.getUintAt(codeTableOffs + 0x14);

            return decompressionOffs != 0;
        }

        public void expandArm9Binary()
        {
            arm9binFile.beginEdit(this);

            int codeTableOffs = (int)(arm9binFile.getUintAt(0x90C) - 0x02000000u);
            int decompressionOffs = (int)arm9binFile.getUintAt(codeTableOffs + 0x14);

            if (decompressionOffs != 0)
            {
                decompressionOffs -= 0x02000000;
                int compDatSize = (int)(arm9binFile.getUintAt(decompressionOffs - 8) & 0xFFFFFF);
                int compDatOffs = decompressionOffs - compDatSize;
                Console.Out.WriteLine("OFFS: " + compDatOffs.ToString("X"));
                Console.Out.WriteLine("SIZE: " + compDatSize.ToString("X"));

                byte[] data = arm9binFile.getContents();
                byte[] compData = new byte[compDatSize];
                Array.Copy(data, compDatOffs, compData, 0, compDatSize);
                byte[] decompData = ROM.DecompressOverlay(compData);
                byte[] newData = new byte[data.Length - compData.Length + decompData.Length];
                Array.Copy(data, newData, data.Length);
                Array.Copy(decompData, 0, newData, compDatOffs, decompData.Length);

                arm9binFile.replace(newData, this);
                arm9binFile.setUintAt(codeTableOffs + 0x14, 0);
            }
            arm9binFile.endEdit(this);
        }

        #region Patching the arm9

        public void writeToRamAddr(int ramAddr, uint val)
        {
            File f;
            int offs;
            ramAddr2File(ramAddr, out f, out offs);
            f.setUintAt(offs, val);
        }

        public uint readFromRamAddr(int ramAddr)
        {
            File f;
            int offs;
            ramAddr2File(ramAddr, out f, out offs);
            return f.getUintAt(offs);
        }

        public void ramAddr2File(int ramAddr, out File file, out int offset)
        {
            File arm9 = arm9binFile;
            File header = headerFile;

            int codeTableOffs = (int)(arm9.getUintAt(0x90C) - 0x02000000u);
            int decompressionOffs = (int)arm9.getUintAt(codeTableOffs + 0x14);

            if (decompressionOffs != 0)
                throw new Exception("Need to decompress arm9 bin first");
            
            int copyTableBegin = (int)(arm9.getUintAt(codeTableOffs + 0x00) - 0x02000000u);
            int copyTableEnd = (int)(arm9.getUintAt(codeTableOffs + 0x04) - 0x02000000u);
            uint dataBegin = (uint)(arm9.getUintAt(codeTableOffs + 0x08) - 0x02000000u);

            List<Region> regions = new List<Region>();
            while(copyTableBegin != copyTableEnd)
            {
                uint start = arm9.getUintAt(copyTableBegin);
                copyTableBegin += 4;
                uint size = arm9.getUintAt(copyTableBegin);
                copyTableBegin += 4;
                uint bsssize = arm9.getUintAt(copyTableBegin);
                copyTableBegin += 4;
                //start += dataBegin;
                //end += dataBegin;
                
                regions.Add(new Region(start, size, arm9binFile, dataBegin, "ARM9 bin"));
                dataBegin += size;
            }


            foreach(OverlayFile f in arm9ovs)
            {
                regions.Add(new Region(f.ramAddr, f.ramSize, arm9ovFile, 0, "ARM9 ov " + f.ovId));
            }


            File fi = null;
            int fileOffs = -1;

            foreach (Region r in regions)
            {
                fileOffs = r.ramOffs2FileOffs((uint)ramAddr);
                if (fileOffs != -1)
                {
                    fi = r.f;
                    break;
                }
            }

            file = fi;
            offset = fileOffs;

            if (fi == null)
            {
                Console.Out.WriteLine("Couldnt find");
            }
            else
            {
                Console.Out.WriteLine(fi.name);
                Console.Out.WriteLine(fileOffs.ToString("X"));
            }
        }

        new class Region : IComparable
        {
            public uint ramBegin, ramSize, fileOffs;
            public File f;
            public String desc;

            public Region(uint ramBegin, uint ramSize, File f, uint fileOffs, String desc)
            {
                this.ramBegin = ramBegin;
                this.ramSize = ramSize;
                this.f = f;
                this.fileOffs = fileOffs;
                this.desc = desc;
            }

            public int ramOffs2FileOffs(uint ramAddr)
            {
                if(ramAddr < ramBegin)
                    return -1;

                if(ramAddr >= ramBegin + ramSize)
                    return -1;

                return (int)(ramAddr - ramBegin + fileOffs);
            }

            public void print()
            {/*
                Console.Out.Write(begin.ToString("X8"));
                Console.Out.Write(" - ");
                Console.Out.Write(end.ToString("X8"));
                Console.Out.WriteLine(": "+desc);*/
            }

            public int CompareTo(object obj)
            {
                Region f = obj as Region;
                if (ramBegin == f.ramBegin)
                    return ramSize.CompareTo(f.ramSize);
                return ramBegin.CompareTo(f.ramBegin);                
            }
        }
        #endregion
    }
}
