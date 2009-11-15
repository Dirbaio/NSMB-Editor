using System;
using System.Collections.Generic;
using System.Text;

namespace NSMBe4.DSFileSystem
{
    class HeaderFile : File
    {
        public HeaderFile(Filesystem parent, Directory parentDir)
            : base(parent, parentDir, true, -1, "__NDS ROM HEADER", 0, 0x15F)
        {

        }
    }
}
