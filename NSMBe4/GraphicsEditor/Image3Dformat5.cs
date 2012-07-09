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
using NSMBe4.NSBMD;

namespace NSMBe4
{
    public class Image3Dformat5 : PalettedImage
    {
        File f;
        File f5;

        int width, height;
        public Image3Dformat5(File f, File f5, int width, int height)
        {
            this.f = f;
            this.f5 = f5;
//            f.beginEdit(this);
//            f5.beginEdit(this);
            this.width = width;
            this.height = height;
        }

        public override int getWidth()
        {
            return width;
        }

        public override int getHeight()
        {
            return height;
        }

        public override Bitmap render(Palette p)
        {
            int w = getWidth();
            int h = getHeight();

            Bitmap b = new Bitmap(w, h);

            ByteArrayInputStream f5data = new ByteArrayInputStream(f5.getContents());
            ByteArrayInputStream data = new ByteArrayInputStream(f.getContents());

            for (uint y = 0; y < h/4; y++)
                for (uint x = 0; x < w/4; x++)
                {
                    ushort palDat = f5data.readUShort();
                    ushort palOffs = (ushort)((palDat & 0x3FFF) * 2);
                    ushort mode = (ushort)((palDat >> 14) & 3);

                    for (uint yy = 0; yy < 4; yy++)
                    {
                        byte row = data.readByte();
                        for (uint xx = 0; xx < 4; xx++)
                        {
                            byte color = (byte)(row >> (byte)(xx * 2));
                            color &= 3;
                            Color col;
                            col = p.getColorSafe(palOffs + color);
                            switch (mode)
                            {
                                case 0:
                                    if (color == 3) col = Color.Transparent;
                                    break;
                                case 1:
                                    if (color == 2) col = ImageTiler.colorMean(p.getColorSafe(palOffs), p.getColorSafe(palOffs + 1), 1, 1);
                                    if (color == 3) col = Color.Transparent;
                                    break;
                                case 3:
                                    if (color == 2) col = ImageTiler.colorMean(p.getColorSafe(palOffs), p.getColorSafe(palOffs + 1), 5, 3);
                                    if (color == 3) col = ImageTiler.colorMean(p.getColorSafe(palOffs), p.getColorSafe(palOffs + 1), 3, 5);
                                    break;
                            }
                            b.SetPixel((int)x * 4 + (int)xx, (int)y * 4 + (int)yy, col);
                        }
                    }
                } 
            return b;
        }

        public override void endEdit()
        {
            f.endEdit(this);
            f5.endEdit(this);
        }

        public override void beginEdit()
        {
            f.beginEdit(this);
            f5.beginEdit(this);
        }

        public override void replaceWithPal(Bitmap b, Palette p)
        {
            throw new InvalidOperationException("Not allowed on these image types!");
        }

        public override void replaceImgAndPal(Bitmap b, Palette p)
        {
            ImageTexeler it = new ImageTexeler(b, (int)p.pal.Length / 4);
            f.replace(it.texdata, this);
            f5.replace(it.f5data, this);

            p.pal = it.finalPalette;
            p.save();
        }

        public override void save()
        {
        }

        public override byte[] getRawData()
        {
            return null;
        }

        public override void setRawData(byte[] data)
        {
        }

        public override string ToString()
        {
            return name;
        }
    }
}
