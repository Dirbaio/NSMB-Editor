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
using NSMBe4.DSFileSystem;
using System.Drawing;

namespace NSMBe4
{
    public class FilePalette : Palette
    {
        File f;
        string name;

        public FilePalette(File f) : this(f, f.name)
        {
        }

        public FilePalette(File f, string name)
        {
            this.f = f;
            this.name = name;

            pal = arrayToPalette(f.getContents());
            if(pal.Length != 0)
                pal[0] = Color.Transparent;
        }

        public static Color[] arrayToPalette(byte[] data)
        {
            ByteArrayInputStream ii = new ByteArrayInputStream(data);
            Color[] pal = new Color[data.Length / 2];
            for (int i = 0; i < pal.Length; i++)
            {
                pal[i] = NSMBTileset.fromRGB15(ii.readUShort());
            }
            return pal;
        }

        public override void beginEdit()
        {
            f.beginEdit(this);
        }

        public override void save()
        {
            ByteArrayOutputStream oo = new ByteArrayOutputStream();
            for (int i = 0; i < pal.Length; i++)
                oo.writeUShort(NSMBTileset.toRGB15(pal[i]));

            f.replace(oo.getArray(), this);

        }

        public override void endEdit()
        {
            f.endEdit(this);
        }

        public override string ToString()
        {
            return name;
        }
    }
}
