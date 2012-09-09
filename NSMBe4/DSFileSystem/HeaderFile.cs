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
    public class HeaderFile : PhysicalFile
    {
        public HeaderFile(Filesystem parent, Directory parentDir)
            : base(parent, parentDir, -1, "header.bin", 0, 0x15F)
        {

        }

        public void UpdateCRC16()
        {
        	byte[] contents = getContents();
            byte[] header = new byte[0x15E];
            Array.Copy(contents, 0, header, 0, 0x15E);
            
            ushort crc16 = ROM.CalcCRC16(header);
            setUshortAt(0x15E, crc16);
        }
    }
}
