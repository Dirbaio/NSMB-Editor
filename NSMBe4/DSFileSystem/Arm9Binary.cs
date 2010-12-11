using System;
using System.Collections.Generic;
using System.Text;
 
namespace  NSMBe4.DSFileSystem
{ 
    public class Arm9Binary 
    { 
        public List<Arm9BinSection> sections;

        File f;
        public byte[] data;
        ByteArrayInputStream datain;
        public Arm9Binary(File parent)
        {
            f = parent;
            load();
        }


        public void loadSections()
        {
            datain = new ByteArrayInputStream(data);
            sections = new List<Arm9BinSection>();

            int ofs = getCodeSettingsOffs();
            int copyTableBegin = (int)(f.getUintAt(ofs + 0x00) - 0x02000000u);
            int copyTableEnd = (int)(f.getUintAt(ofs + 0x04) - 0x02000000u);
            int dataBegin = (int)(f.getUintAt(ofs + 0x08) - 0x02000000u);

            sections.Add(new Arm9BinSection(0x02000000, dataBegin, 0x0, 0, this));

            datain.seek(copyTableBegin);
            while (copyTableBegin < copyTableEnd)
            {
                int start = datain.readInt();
                int size = datain.readInt();
                int bsssize = datain.readInt();
                copyTableBegin += 12;

                sections.Add(new Arm9BinSection(start, size, dataBegin, bsssize, this));
                dataBegin += size;
            }
        }
  
        private int _codeSettingsOffs = -1;
        
        public int getCodeSettingsOffs()
        {
            // Find the end of the settings
            // This old method doesn't work with The Legendary Starfy :\ -Treeki
            //return (int)(getUintAt(0x90C) - 0x02000000u);
            if (_codeSettingsOffs == -1) {
                for (int i = 0; i < 0x8000; i += 4) {
                    if (f.getUintAt(i) == 0xDEC00621 && f.getUintAt(i+4) == 0x2106C0DE) {
                        _codeSettingsOffs = i - 0x1C;
                        break;
                    }
                }
            }
                
            return _codeSettingsOffs;
        }

        public int getDecompressionRamAddr()
        {
            return (int)f.getUintAt(getCodeSettingsOffs() + 0x14);
        }

        public void load()
        {
            byte[] data = f.getContents();

            int decompressionOffs = getDecompressionRamAddr() - 0x02000000;
            if (decompressionOffs == 0) //For compatibility with the old editor (?)
            {
                this.data = data;
            }
            else
            {
                int compDatSize = (int)(f.getUintAt(decompressionOffs - 8) & 0xFFFFFF);
                int compDatOffs = decompressionOffs - compDatSize;
                Console.Out.WriteLine("OFFS: " + compDatOffs.ToString("X"));
                Console.Out.WriteLine("SIZE: " + compDatSize.ToString("X"));

                byte[] compData = new byte[compDatSize];
                Array.Copy(data, compDatOffs, compData, 0, compDatSize);
                byte[] decompData = ROM.DecompressOverlay(compData);
                byte[] newData = new byte[data.Length - compData.Length + decompData.Length];
                Array.Copy(data, newData, data.Length);
                Array.Copy(decompData, 0, newData, compDatOffs, decompData.Length);

                this.data = newData;
            }

            loadSections();
        }


        public uint readFromRamAddr(int ramAddr, int ovId)
        {
            Console.Out.WriteLine(String.Format("READFROM {0:X8}", ramAddr));
            
            if (ovId != -1)
            {
                foreach (OverlayFile of in ROM.FS.arm9ovs)
                    if (of.ovId == ovId)
                        return of.getUintAt((int)(ramAddr - of.ramAddr));

                throw new Exception("ERROR: WRONG OVERLAY");
            }
            
            foreach (Arm9BinSection s in sections)
                if (s.isAddrIn(ramAddr))
                    return s.readFrom(ramAddr);

            throw new Exception("ERROR: ADDR NOT FOUND");
        }
    }
}
