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

﻿using System;
using System.Collections.Generic;
using System.Text;
using NSMBe4.DSFileSystem;

namespace NSMBe4.Patcher
{
    public class Arm9BinaryHandler
    {
        public List<Arm9BinSection> sections;
        Arm9BinSection nullSection;
        File f;
        NitroROMFilesystem fs;

        public Arm9BinaryHandler(NitroROMFilesystem fs)
        {
            f = fs.arm9binFile;
            this.fs = fs;
        }

        public void load()
        {
            decompress();
            loadSections();
        }
        public void newSection(int ramAddr, int ramLen, int fileOffs, int bssSize)
        {
            Console.Out.WriteLine(String.Format("SECTION {0:X8} - {1:X8} - {2:X8}", ramAddr, ramAddr + ramLen, ramAddr + ramLen + bssSize));

            byte[] data = new byte[ramLen];
            Array.Copy(f.getContents(), fileOffs, data, 0, ramLen);
            Arm9BinSection s = new Arm9BinSection(data, ramAddr, bssSize);
            sections.Add(s);
        }

        public void loadSections()
        {
            if (isCompressed)
                throw new Exception("Can't load sections of compressed arm9");

            sections = new List<Arm9BinSection>();

            int copyTableBegin = (int)(f.getUintAt(getCodeSettingsOffs() + 0x00) - 0x02000000u);
            int copyTableEnd = (int)(f.getUintAt(getCodeSettingsOffs() + 0x04) - 0x02000000u);
            int dataBegin = (int)(f.getUintAt(getCodeSettingsOffs() + 0x08) - 0x02000000u);

            newSection(0x02000000, dataBegin, 0x0, 0);
            sections[0].real = false;

            while (copyTableBegin < copyTableEnd)
            {
                int start = (int)f.getUintAt(copyTableBegin);
                copyTableBegin += 4;
                int size = (int)f.getUintAt(copyTableBegin);
                copyTableBegin += 4;
                int bsssize = (int)f.getUintAt(copyTableBegin);
                copyTableBegin += 4;

                newSection(start, size, dataBegin, bsssize);
                dataBegin += size;
            }
        }
        //020985f0 02098620
        public void saveSections()
        {
            Console.Out.WriteLine("Saving sections...");
            f.beginEdit(this);
            ByteArrayOutputStream o = new ByteArrayOutputStream();
            foreach (Arm9BinSection s in sections)
            {
                Console.Out.WriteLine(String.Format("{0:X8} - {1:X8} - {2:X8}: {3:X8}",
                    s.ramAddr, s.ramAddr + s.len, s.ramAddr + s.len + s.bssSize, o.getPos()));

                o.write(s.data);
                o.align(4);
            }

            uint sectionTableAddr = 0x02000E00;
            ByteArrayOutputStream o2 = new ByteArrayOutputStream();
            foreach (Arm9BinSection s in sections)
            {
                if (!s.real) continue;
                if (s.len == 0) continue;
                o2.writeUInt((uint)s.ramAddr);
                o2.writeUInt((uint)s.len);
                o2.writeUInt((uint)s.bssSize);
            }

            //Write BSS sections last
            //because they overwrite huge areas with zeros (?)
            foreach (Arm9BinSection s in sections)
            {
                if (!s.real) continue;
                if (s.len != 0) continue;
                o2.writeUInt((uint)s.ramAddr);
                o2.writeUInt((uint)s.len);
                o2.writeUInt((uint)s.bssSize);
            }

            byte[] data = o.getArray();
            byte[] sectionTable = o2.getArray();
            Array.Copy(sectionTable, 0, data, sectionTableAddr - 0x02000000, sectionTable.Length);
            f.replace(data, this);

            f.setUintAt(getCodeSettingsOffs() + 0x00, (uint)sectionTableAddr);
            Console.Out.WriteLine(String.Format("{0:X8} {1:X8}", getCodeSettingsOffs() + 0x04, (uint)o2.getPos() + sectionTableAddr));
            f.setUintAt(getCodeSettingsOffs() + 0x04, (uint)o2.getPos() + sectionTableAddr);
            f.setUintAt(getCodeSettingsOffs() + 0x08, (uint)(sections[0].len + 0x02000000));

            f.endEdit(this);
            Console.Out.WriteLine("DONE");
        }

        private int _codeSettingsOffs = -1;

        public int getCodeSettingsOffs()
        {
            // Find the end of the settings
            // This old method doesn't work with The Legendary Starfy :\ -Treeki
            //return (int)(getUintAt(0x90C) - 0x02000000u);
            if (_codeSettingsOffs == -1)
            {
                for (int i = 0; i < 0x8000; i += 4)
                {
                    if (f.getUintAt(i) == 0xDEC00621 && f.getUintAt(i + 4) == 0x2106C0DE)
                    {
                        _codeSettingsOffs = i - 0x1C;
                        break;
                    }
                }
            }

            return _codeSettingsOffs;
        }


        public int decompressionRamAddr
        {
            get
            {
                return (int)f.getUintAt(getCodeSettingsOffs() + 0x14);
            }

            set
            {
                f.setUintAt(getCodeSettingsOffs() + 0x14, (uint)value);
            }
        }

        public bool isCompressed {get
        {
            return decompressionRamAddr != 0;
        }}

        public void decompress()
        {
            if(!isCompressed) return;

            f.beginEdit(this);

            int decompressionOffs = decompressionRamAddr - 0x02000000;

            int compDatSize = (int)(f.getUintAt(decompressionOffs - 8) & 0xFFFFFF);
            int compDatOffs = decompressionOffs - compDatSize;
            //Console.Out.WriteLine("OFFS: " + compDatOffs.ToString("X"));
            //Console.Out.WriteLine("SIZE: " + compDatSize.ToString("X"));

            byte[] data = f.getContents();
            byte[] compData = new byte[compDatSize];
            Array.Copy(data, compDatOffs, compData, 0, compDatSize);
            byte[] decompData = ROM.DecompressOverlay(compData);
            byte[] newData = new byte[data.Length - compData.Length + decompData.Length];
            Array.Copy(data, newData, data.Length);
            Array.Copy(decompData, 0, newData, compDatOffs, decompData.Length);

            f.replace(newData, this);
            decompressionRamAddr = 0;
            f.endEdit(this);
        }


        
        public void writeToRamAddr(int ramAddr, uint val, int ov)
        {
			if(ov != -1)
			{
		        foreach(OverlayFile of in fs.arm9ovs)
		        	if(of.ovId == ov)
		        	{
				        if(of.containsRamAddr(ramAddr))
						{
				            //Console.Out.WriteLine(String.Format("WRITETO {0:X8} {1:X8}: ov {2:X8}", ramAddr, val, of.ovId));
				            makeBinBackup((int)of.ovId);
				            of.writeToRamAddr(ramAddr, val);
				            return;
						}
			            else throw new Exception("WRITE: Overlay ID "+ov+" doesn't contain addr "+ramAddr+" :(");
			        }
			        
	            throw new Exception("WRITE: Overlay ID "+ov+" not found :(");
			}
			else
			{
		        foreach (Arm9BinSection s in sections)
		            if(s.containsRamAddr(ramAddr))
		            {
		                //Console.Out.WriteLine(String.Format("WRITETO {0:X8} {1:X8}: {2:X8}", ramAddr, val, s.ramAddr));
		                makeBinBackup(-1);
		                s.writeToRamAddr(ramAddr, val);
		                return;
		            }
		        foreach(OverlayFile of in fs.arm9ovs)
		            if(of.containsRamAddr(ramAddr))
		            {
		                //Console.Out.WriteLine(String.Format("WRITETO {0:X8} {1:X8}: ov {2:X8}", ramAddr, val, of.ovId));
		                makeBinBackup((int)of.ovId);
		                of.writeToRamAddr(ramAddr, val);
		                return;
		            }
            }
            throw new Exception("WRITE: Addr "+ramAddr+" is not in arm9 binary or overlays");
        }

        public uint readFromRamAddr(int ramAddr, int ov)
        {
			if(ov != -1)
			{
		        foreach(OverlayFile of in fs.arm9ovs)
		        	if(of.ovId == ov)
		        	{
				        if(of.containsRamAddr(ramAddr))
			                return of.readFromRamAddr(ramAddr);
			            else throw new Exception("READ: Overlay ID "+ov+" doesn't contain addr "+ramAddr+" :(");
			        }
			        
	            throw new Exception("READ: Overlay ID "+ov+" not found :(");
			}
			else
			{
		        foreach (Arm9BinSection s in sections)
		            if(s.containsRamAddr(ramAddr))
		                return s.readFromRamAddr(ramAddr);

		        foreach(OverlayFile of in fs.arm9ovs)
		            if(of.containsRamAddr(ramAddr))
		                return of.readFromRamAddr(ramAddr);

		        throw new Exception("READ: Addr "+ramAddr+" is not in arm9 binary or overlays");
		    }
        }

        public void makeBinBackup(int file)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(ROM.romfile.Directory.FullName+"/bak");
            //Console.Out.WriteLine("Backing up " + file + " "+dir.FullName);
            if (!dir.Exists)
                dir.Create();
            
            dir = ROM.romfile.Directory;
            System.IO.FileStream fs;

            string filename;
            if (file == -1)
                filename = dir.FullName + "/bak/" + "main.bin";
            else
                filename = dir.FullName + "/bak/" + file + ".bin";

            if(System.IO.File.Exists(filename)) return;

            fs = new System.IO.FileStream(filename, System.IO.FileMode.CreateNew);

            File f = ROM.FS.arm9binFile;
            if (file != -1)
            {
                f = ROM.FS.arm9ovs[file];
                ROM.FS.arm9ovs[file].decompress(); //b4ckupz r 41w4ys cmpr3zzd
            }

            fs.Write(f.getContents(), 0, f.fileSize);
            fs.Close();
        }

        public void restoreFromBackup()
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(ROM.romfile.Directory.FullName+"/bak");
            if (!dir.Exists) return;

            foreach (System.IO.FileInfo f in dir.GetFiles())
            {
                string n = f.Name;
                if (!n.EndsWith(".bin")) continue;

                File ff = null;

                n = n.Substring(0, n.Length - 4);
                if (n == "main")
                    ff = ROM.FS.arm9binFile;
                else
                {
                    int num = 0;
                    if (Int32.TryParse(n, out num))
                        ff = ROM.FS.arm9ovs[num];
                }

                if (ff == null) continue;

                Console.Out.WriteLine("Restoring " + f + ", " + ff.name);
                System.IO.FileStream fs = f.OpenRead();
                byte[] data = new byte[fs.Length];
                fs.Read(data, 0, data.Length);
                fs.Close();

                ff.beginEdit(this);
                ff.replace(data, this);
                ff.endEdit(this);

                if (ff is OverlayFile) (ff as OverlayFile).isCompressed = false;
            }
        }

    }
}
