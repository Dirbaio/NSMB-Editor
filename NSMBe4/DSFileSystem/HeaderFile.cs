using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace NSMBe4.DSFileSystem
{
    public class HeaderFile : File
    {
        public HeaderFile(Filesystem parent, Directory parentDir)
            : base(parent, parentDir, true, -1, "__NDS ROM HEADER", 0, 0x15F)
        {

        }

        public void UpdateCRC16()
        {
            Console.Out.WriteLine("UPDATING CRC...");

            byte[] header = new byte[0x15E];
            parent.s.Seek(0, SeekOrigin.Begin);
            parent.s.Read(header, 0, 0x15E);
            
            ushort crc16 = ROM.CalcCRC16(header);
            setUshortAt(0x15E, crc16);
        }
    }
}
