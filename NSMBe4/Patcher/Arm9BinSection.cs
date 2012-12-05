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

﻿
namespace NSMBe4.Patcher
{
    public class Arm9BinSection
    {
        public byte[] data;
        public int len;
        public int ramAddr;
        public int bssSize;
        public bool real = true;

        public Arm9BinSection(byte[] data, int ramAddr, int bssSize)
        {
            this.data = data;
            len = data.Length;
            this.ramAddr = ramAddr;
            this.bssSize = bssSize;
        }

        public bool containsRamAddr(int addr)
        {
            return addr >= ramAddr && addr < ramAddr + len;
        }

        public uint readFromRamAddr(int addr)
        {
            addr -= ramAddr;

            return (uint)(data[addr] |
                data[addr + 1] << 8 |
                data[addr + 2] << 16 |
                data[addr + 3] << 24);
        }

        public void writeToRamAddr(int addr, uint val)
        {
            addr -= ramAddr;

            data[addr] = (byte)val;
            data[addr + 1] = (byte)(val >> 8);
            data[addr + 2] = (byte)(val >> 16);
            data[addr + 3] = (byte)(val >> 24);
        }
    }
}
