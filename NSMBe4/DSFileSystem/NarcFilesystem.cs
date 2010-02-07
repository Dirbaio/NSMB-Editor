using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace NSMBe4.DSFileSystem
{
    class NarcFilesystem : NitroFilesystem
    {

        public uint fntOffset, fntSize;
        public uint fatOffset, fatSize;

        public NarcFilesystem(File f)
            : base(new FileFilesystemSource(f))
        {
        }

        public override void load()
        {

            //I have to do some tricky offset calculations here ...
            fatOffset= 0x1C;
            s.Seek(0x18, SeekOrigin.Begin); //number of files
            fatSize = readUInt(s) * 8;

            s.Seek(fatSize + fatOffset + 4, SeekOrigin.Begin); //size of FNTB
            fntSize = readUInt(s) - 8; //do not include header
            fntOffset = fatSize + fatOffset + 8;

            fileDataOffset = fntSize + fntOffset + 8;
            fntFile = new File(this, mainDir, true, -2, "fnt.bin", fntOffset, fntSize);
            fatFile = new File(this, mainDir, true, -3, "fat.bin", fatOffset, fatSize);

            base.load();
        }
    }
}
