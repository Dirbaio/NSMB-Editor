using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace NSMBe4.DSFileSystem
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
            byte[] data = new byte[ramLen];
            Array.Copy(f.getContents(), fileOffs, data, 0, ramLen);
            Arm9BinSection s = new Arm9BinSection(data, ramAddr, bssSize);
            if(s.len == 0)
                nullSection = s;
            else
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

            while (copyTableBegin != copyTableEnd)
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

        public void saveSections()
        {
            Console.Out.WriteLine("Saving sections...");
            f.beginEdit(this);
            ByteArrayOutputStream o = new ByteArrayOutputStream();
            foreach (Arm9BinSection s in sections)
            {
                o.write(s.data);
                o.align(4);
            }

            int pos = o.getPos();
            bool first = true;
            foreach (Arm9BinSection s in sections)
            {
                Console.Out.WriteLine(String.Format("{0:X8} - {1:X8}", s.ramAddr, s.ramAddr + s.len - 1));
                if(first)
                {
                    first = false;
                    continue;
                }

                o.writeUInt((uint)s.ramAddr);
                o.writeUInt((uint)s.len);
                o.writeUInt((uint)s.bssSize);
            }
            o.writeUInt((uint)nullSection.ramAddr);
            o.writeUInt((uint)nullSection.len);
            o.writeUInt((uint)nullSection.bssSize);

            f.replace(o.getArray(), this);

            f.setUintAt(getCodeSettingsOffs() + 0x00, (uint)pos + 0x02000000);
            f.setUintAt(getCodeSettingsOffs() + 0x04, (uint)o.getPos() + 0x02000000);
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
            Console.Out.WriteLine("OFFS: " + compDatOffs.ToString("X"));
            Console.Out.WriteLine("SIZE: " + compDatSize.ToString("X"));

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


        
        public void writeToRamAddr(int ramAddr, uint val)
        {
            Console.Out.WriteLine(String.Format("WRITETO {0:X8} {1:X8}", ramAddr, val));

            foreach (Arm9BinSection s in sections)
                if(s.containsRamAddr(ramAddr))
                {
                    Console.Out.WriteLine(String.Format("WRITETO {0:X8} {1:X8}: {2:X8}", ramAddr, val, s.ramAddr));
                    makeBinBackup(-1);
                    s.writeToRamAddr(ramAddr, val);
                    return;
                }
            foreach(OverlayFile of in fs.arm9ovs)
                if(of.containsRamAddr(ramAddr))
                {
                    Console.Out.WriteLine(String.Format("WRITETO {0:X8} {1:X8}: ov {2:X8}", ramAddr, val, of.ovId));
                    makeBinBackup((int)of.ovId);
                    of.writeToRamAddr(ramAddr, val);
                    return;
                }
            throw new Exception("WRITE: Addr "+ramAddr+" is not in arm9 binary or overlays");
        }

        public uint readFromRamAddr(int ramAddr)
        {
            foreach (Arm9BinSection s in sections)
                if(s.containsRamAddr(ramAddr))
                    return s.readFromRamAddr(ramAddr);

            foreach(OverlayFile of in fs.arm9ovs)
                if(of.containsRamAddr(ramAddr))
                    return of.readFromRamAddr(ramAddr);

            throw new Exception("READ: Addr "+ramAddr+" is not in arm9 binary or overlays");
        }

        public void makeBinBackup(int file)
        {
            DirectoryInfo dir = new DirectoryInfo(ROM.romfile.Directory.FullName+"/bak");
            Console.Out.WriteLine("Backing up " + file + " "+dir.FullName);
            if (!dir.Exists)
                dir.Create();
            
            
            dir = ROM.romfile.Directory;
            FileStream fs;

            try
            {
                if (file == -1)
                    fs = new FileStream(dir.FullName + "/bak/" + "main.bin", FileMode.CreateNew);
                else
                    fs = new FileStream(dir.FullName + "/bak/" + file + ".bin", FileMode.CreateNew);
            }
            catch (IOException) {return;}

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
            DirectoryInfo dir = new DirectoryInfo(ROM.romfile.Directory.FullName+"/bak");
            if (!dir.Exists) return;

            foreach (FileInfo f in dir.GetFiles())
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
                FileStream fs = f.OpenRead();
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
