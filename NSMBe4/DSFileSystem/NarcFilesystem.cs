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
    class NarcFilesystem : NitroFilesystem
    {

        public int fntOffset, fntSize;
        public int fatOffset, fatSize;

        public NarcFilesystem(File f)
            : base(new FileFilesystemSource(f, false))
        {
        }

        public NarcFilesystem(File f, bool compressed)
            : base(new FileFilesystemSource(f, compressed))
        {
        }

        public override void load()
        {

            //I have to do some tricky offset calculations here ...
            fatOffset= 0x1C;
            s.Seek(0x18, SeekOrigin.Begin); //number of files
            fatSize = (int)readUInt(s) * 8;

            s.Seek(fatSize + fatOffset + 4, SeekOrigin.Begin); //size of FNTB
            fntSize = (int)readUInt(s) - 8; //do not include header
            fntOffset = fatSize + fatOffset + 8;

            fileDataOffset = fntSize + fntOffset + 8;
            fntFile = new File(this, mainDir, true, -2, "fnt.bin", fntOffset, fntSize);
            fatFile = new File(this, mainDir, true, -3, "fat.bin", fatOffset, fatSize);

            base.load();
            loadNamelessFiles(mainDir);
        }


        public override void fileMoved(File f)
        {
            source.save();
        }
    }
}
