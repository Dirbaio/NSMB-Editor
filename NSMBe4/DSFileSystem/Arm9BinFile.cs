using System;
using System.Collections.Generic;
using System.Text;

namespace NSMBe4.DSFileSystem
{
    public class Arm9BinFile : File
    {
        public List<Arm9BinSection> sections;
        Arm9BinSection nullSection;

        public Arm9BinFile(Filesystem parent, Directory parentDir, File headerFile)
            :base(parent, parentDir, true, -1, "arm9.bin", headerFile, 0x20, 0x2C, true)
        {
            loadSections();
        }

        public void newSection(int ramAddr, int ramLen, int fileOffs, int bssSize)
        {
            byte[] data = new byte[ramLen];
            Array.Copy(getContents(), fileOffs, data, 0, ramLen);
            Arm9BinSection s = new Arm9BinSection(data, ramAddr, bssSize);
            if(s.len == 0)
                nullSection = s;
            else
                sections.Add(s);
        }

        public void loadSections()
        {
            if (isCompressed)
                return;

            sections = new List<Arm9BinSection>();

            int copyTableBegin = (int)(getUintAt(codeSettingsOffs + 0x00) - 0x02000000u);
            int copyTableEnd = (int)(getUintAt(codeSettingsOffs + 0x04) - 0x02000000u);
            int dataBegin = (int)(getUintAt(codeSettingsOffs + 0x08) - 0x02000000u);

            newSection(0x02000000, dataBegin, 0x0, 0);

            while (copyTableBegin != copyTableEnd)
            {
                int start = (int)getUintAt(copyTableBegin);
                copyTableBegin += 4;
                int size = (int)getUintAt(copyTableBegin);
                copyTableBegin += 4;
                int bsssize = (int)getUintAt(copyTableBegin);
                copyTableBegin += 4;

                newSection(start, size, dataBegin, bsssize);
                dataBegin += size;
            }
        }

        public void saveSections()
        {
            Console.Out.WriteLine("Saving sections...");
            beginEdit(this);
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
            if (nullSection != null)
            {
                o.writeUInt((uint)nullSection.ramAddr);
                o.writeUInt((uint)nullSection.len);
                o.writeUInt((uint)nullSection.bssSize);
            }
            replace(o.getArray(), this);

            setUintAt(codeSettingsOffs + 0x00, (uint)pos + 0x02000000);
            setUintAt(codeSettingsOffs + 0x04, (uint)o.getPos() + 0x02000000);
            setUintAt(codeSettingsOffs + 0x08, (uint)(sections[0].len + 0x02000000));

            endEdit(this);
            Console.Out.WriteLine("DONE");
        }

        public int codeSettingsOffs {get
        {
            return (int)(getUintAt(0x90C) - 0x02000000u);
        }}

        public int decompressionRamAddr
        {
            get
            {
                return (int)getUintAt(codeSettingsOffs + 0x14);
            }

            set
            {
                setUintAt(codeSettingsOffs + 0x14, (uint)value);
            }
        }

        public bool isCompressed {get
        {
            return decompressionRamAddr != 0;
        }}

        public void decompress()
        {
            if(!isCompressed) return;

            beginEdit(this);

            int decompressionOffs = decompressionRamAddr - 0x02000000;

            int compDatSize = (int)(getUintAt(decompressionOffs - 8) & 0xFFFFFF);
            int compDatOffs = decompressionOffs - compDatSize;
            Console.Out.WriteLine("OFFS: " + compDatOffs.ToString("X"));
            Console.Out.WriteLine("SIZE: " + compDatSize.ToString("X"));

            byte[] data = getContents();
            byte[] compData = new byte[compDatSize];
            Array.Copy(data, compDatOffs, compData, 0, compDatSize);
            byte[] decompData = ROM.DecompressOverlay(compData);
            byte[] newData = new byte[data.Length - compData.Length + decompData.Length];
            Array.Copy(data, newData, data.Length);
            Array.Copy(decompData, 0, newData, compDatOffs, decompData.Length);

            replace(newData, this);
            decompressionRamAddr = 0;
            endEdit(this);

            loadSections();
        }
    }
}
