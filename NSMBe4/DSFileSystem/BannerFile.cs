using System;
using System.Collections.Generic;
using System.Text;

namespace NSMBe4.DSFileSystem
{
    public class BannerFile:File
    {
        public BannerFile(Filesystem parent, Directory parentDir, File headerFile)
            : base(parent, parentDir, true, -8, "banner.bin", headerFile, 0x68, 0, true)
        {
            endFile = null;
            fileSize = 0x840;
            refreshOffsets();
        }

        public void updateCRC16()
        {
            byte[] contents = getContents();
            byte[] checksumArea = new byte[0x820];
            Array.Copy(contents, 0x20, checksumArea, 0, 0x820);
            ushort checksum = ROM.CalcCRC16(checksumArea);
            setUshortAt(2, checksum);
        }
    }
}
