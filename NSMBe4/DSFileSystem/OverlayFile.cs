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

namespace NSMBe4.DSFileSystem
{
	//This one needs to die too.
	//Should be an adapter over a PhysicalFile.
    public class OverlayFile : PhysicalFile
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
            : base(parent, parentDir, id, ":::", alFile, alBeg, alEnd)
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
