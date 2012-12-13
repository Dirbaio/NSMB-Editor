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

namespace NSMBe4.DSFileSystem
{
	//Seriouisy, wtf.
    public class BannerFile : PhysicalFile
    {
        public BannerFile(Filesystem parent, Directory parentDir, File headerFile)
            : base(parent, parentDir, -8, "banner.bin", headerFile, 0x68, 0, true)
        {
            endFile = null;
            fileSizeP = 0x840;
            refreshOffsets();
        }

        //Hack to prevent stack overflow...
        private bool updatingCrc = false;

        public void updateCRC16()
        {
            updatingCrc = true;
            byte[] contents = getContents();
            byte[] checksumArea = new byte[0x820];
            Array.Copy(contents, 0x20, checksumArea, 0, 0x820);
            ushort checksum = ROM.CalcCRC16(checksumArea);
            setUshortAt(2, checksum);
            Console.Out.WriteLine("UPDATING BANNER CHECKSUM!!!!");
            updatingCrc = false;
        }

        public override void editionEnded()
        {
            base.editionEnded();
            if(!updatingCrc)
                updateCRC16();
        }
    }
}
