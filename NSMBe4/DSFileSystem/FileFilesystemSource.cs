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
    class FileFilesystemSource : FilesystemSource
    {

        File f;
        MemoryStream str;
        bool lz;

        public FileFilesystemSource(File f, bool compressed)
        {
            this.f = f;
            lz = compressed;
        }

        public override Stream load()
        {
            f.beginEdit(this);
            str = new MemoryStream();
            byte[] data = f.getContents();
            if (lz)
                data = ROM.LZ77_Decompress(data);

            str.Write(data, 0, data.Length);

            return str;
        }

        public override void save()
        {
            byte[] data = str.ToArray();

            if (lz)
                data = ROM.LZ77_Compress(data);
            
            f.replace(data, this);
        }

        public override void close()
        {
            save();
            str.Close();
            f.endEdit(this);
        }

        public override string getDescription()
        {
            return f.name;
        }

    }

}
