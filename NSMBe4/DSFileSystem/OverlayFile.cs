using System;
using System.Collections.Generic;
using System.Text;

namespace NSMBe4.DSFileSystem
{
    public class OverlayFile : File
    {

        public File ovTableFile;
        public uint ovTableOffs;
        public uint ovId, ramAddr, ramSize, bssSize, staticInitStart, staticInitEnd;

        public bool isCompressed
        {
            get
            {
                byte b = ovTableFile.getByteAt((int)ovTableOffs + 0x1F);
                return (b & 0x1) == 0x1;
            }
            set
            {
                byte b = ovTableFile.getByteAt((int)ovTableOffs + 0x1F);
                b &= 0xFE; // clear bit
                if(value)
                    b |= 0x1;
                ovTableFile.setByteAt((int)ovTableOffs + 0x1F, b);
            }
        }

        public OverlayFile(Filesystem parent, Directory parentDir,
            int id,  File alFile, int alBeg, int alEnd,
            File ovTableFile, uint ovTableOffs)
            : base(parent, parentDir, true, id, ":::", alFile, alBeg, alEnd)
        {
            this.nameP = string.Format(LanguageManager.Get("NitroClass", "OverlayFile"), 
                id, ramAddr.ToString("X"), ramSize.ToString("X"));

            this.ovTableFile = ovTableFile;
            this.ovTableOffs = ovTableOffs;

            ovId = ovTableFile.getUintAt((int)ovTableOffs + 0x00);
            ramAddr = ovTableFile.getUintAt((int)ovTableOffs + 0x04);
            ramSize = ovTableFile.getUintAt((int)ovTableOffs + 0x08);
            bssSize = ovTableFile.getUintAt((int)ovTableOffs + 0x0C);
            staticInitStart = ovTableFile.getUintAt((int)ovTableOffs + 0x10);
            staticInitEnd = ovTableFile.getUintAt((int)ovTableOffs + 0x14);

            nameP = String.Format("{0:X8} - {1:X8}, OV {2}, FILE {3}", ramAddr, ramAddr + ramSize - 1, ovId, id);
        }

        public void decompress()
        {
            if (isCompressed)
            {
                byte[] data = getContents();
                data = ROM.DecompressOverlay(data);
                beginEdit(this);
                replace(data, this);
                endEdit(this);
                isCompressed = false;
            }
        }

        public override void enableEdition()
        {
//            decompress();
            base.enableEdition();
        }

        public bool containsRamAddr(int addr)
        {
            return addr >= ramAddr && addr < ramAddr + ramSize;
        }

        public uint readFromRamAddr(int addr)
        {
            decompress();

            addr -= (int)ramAddr;
            return getUintAt(addr);
        }

        public void writeToRamAddr(int addr, uint val)
        {
            decompress();
            addr -= (int)ramAddr;
            setUintAt(addr, val);
        }
    }
}
