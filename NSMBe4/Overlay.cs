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

namespace NSMBe4
{
    public class Overlay
    {
        public File f;
        private File ovTableFile;
        private uint ovTableOffs;

        public uint ovId { get { return ovTableFile.getUintAt((int)ovTableOffs + 0x00); } }
        public uint ramAddr { get { return ovTableFile.getUintAt((int)ovTableOffs + 0x04); } }
        public uint ramSize { get { return ovTableFile.getUintAt((int)ovTableOffs + 0x08); } }
        public uint bssSize { get { return ovTableFile.getUintAt((int)ovTableOffs + 0x0C); } }
        public uint staticInitStart { get { return ovTableFile.getUintAt((int)ovTableOffs + 0x10); } }
        public uint staticInitEnd { get { return ovTableFile.getUintAt((int)ovTableOffs + 0x14); } }

        public bool isCompressed
        {
            get
            {
                byte b = ovTableFile.getByteAt((int)ovTableOffs + 0x1F);
                return (b & 0x1) != 0;
            }
            set
            {
                byte b = ovTableFile.getByteAt((int)ovTableOffs + 0x1F);
                b &= 0xFE; // clear bit
                if (value)
                    b |= 0x1;
                ovTableFile.setByteAt((int)ovTableOffs + 0x1F, b);
            }
        }

        public Overlay(File file, File ovTableFile, uint ovTableOffs)
        {
            this.f = file;
            this.ovTableFile = ovTableFile;
            this.ovTableOffs = ovTableOffs;
        }

        public byte[] getDecompressedContents()
        {
            byte[] data = f.getContents();
            if (isCompressed)
                data = ROM.DecompressOverlay(data);
            return data;
        }

        public void decompress()
        {
            if (isCompressed)
            {
                byte[] data = f.getContents();
                data = ROM.DecompressOverlay(data);
                f.beginEdit(this);
                f.replace(data, this);
                f.endEdit(this);
                isCompressed = false;
            }
        }

        public bool containsRamAddr(int addr)
        {
            return addr >= ramAddr && addr < ramAddr + ramSize;
        }

        public uint readFromRamAddr(int addr)
        {
            decompress();

            addr -= (int)ramAddr;
            return f.getUintAt(addr);
        }

        public void writeToRamAddr(int addr, uint val)
        {
            decompress();
            addr -= (int)ramAddr;
            f.setUintAt(addr, val);
        }
    }
}
