using System;
using System.Collections.Generic;
using System.Text;

namespace NSMBe4.DSFileSystem
{
    public class Arm9BinSection
    {
        public int len;
        public int ramAddr;
        public int bssSize;
        public int fileOffs;
        public Arm9Binary binfile;

        public Arm9BinSection(int ramAddr, int ramLen, int fileOffs, int bssSize, Arm9Binary binfile)
        {
            this.len = ramLen;
            this.fileOffs = fileOffs;
            this.ramAddr = ramAddr;
            this.bssSize = bssSize;
            this.binfile =binfile;

        }

        public bool isAddrIn(int addr)
        {
            return addr >= ramAddr && addr < ramAddr + len;
        }

        public uint readFrom(int addr)
        {
            addr -= ramAddr;
            addr += fileOffs;

            return (uint)(binfile.data[addr] |
                binfile.data[addr + 1] << 8 |
                binfile.data[addr + 2] << 16 |
                binfile.data[addr + 3] << 24);
        }
    }
}
