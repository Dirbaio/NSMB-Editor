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
    public class Image2D : PixelPalettedImage
    {
        private File f;
        protected byte[] data;
        private byte[] rawdata;
        public int width;
        public int tileOffset;

        private bool is4bppI;

        //TODO: Ask on file opening whether LZ Compresed?
        public bool isLZCompressed = true;

        public bool is4bpp
        {
            get{return is4bppI;}
            set{
                if(value == is4bppI) return;
                saveImageData();
                is4bppI = value;
                loadImageData();
            }
        }

        public Image2D(File f, int width, bool is4bpp)
            :this(f, width, is4bpp, true)
        {
        }

		//TODO: Take the LZ flag out of here and use LZFile.
        public Image2D(File f, int width, bool is4bpp, bool isLZCompressed)
        {
            this.f = f;
            this.is4bppI = is4bpp;
            this.width = width;
            this.isLZCompressed = isLZCompressed;
            reload();
        }

        public void reload()
        {
            rawdata = f.getContents();
            if(isLZCompressed)
                rawdata = ROM.LZ77_Decompress(rawdata);
            loadImageData();
        }

        public override void beginEdit()
        {
            f.beginEdit(this);
        }

        public override void endEdit()
        {
            f.endEdit(this);
        }

        public override int getWidth()
        {
            return width;
        }

        public override int getHeight()
        {
            int tileCount = data.Length / 64 + tileOffset;

            int tileWidth = (width / 8);
            int rowCount = tileCount / tileWidth;
            if (tileCount % tileWidth != 0) rowCount++;

            return rowCount * 8;
        }


        public override int getPixel(int x, int y)
        {
            int bx = x / 8;
            int by = y / 8;

            int offs = bx + by * (width / 8) - tileOffset;
            if (offs < 0) return 0;
            offs *= 64;
            offs += x % 8 + 8 * (y % 8);
            if (offs < 0) return 0;
            if (offs >= data.Length) return 0;
            return data[offs];
        }

        public override void setPixel(int x, int y, int c)
        {
            int bx = x / 8;
            int by = y / 8;

            int offs = bx + by * (width / 8) - tileOffset;
            if (offs < 0) return;

            offs *= 64;
            offs += x % 8 + 8 * (y % 8);
            if (offs < 0) return;
            if (offs >= data.Length) return;
            data[offs] = (byte)c;
        }

        private void saveImageData()
        {
            if (is4bpp)
            {
                rawdata = new byte[data.Length / 2];

                for (int i = 0; i < rawdata.Length; i++)
                    rawdata[i] = (byte)(
                        data[i * 2] & 0xF |
                        (data[i * 2 + 1] & 0xF) << 4);
            }
            else
                rawdata = (byte[])data.Clone();
        }

        private void loadImageData()
        {
            if (is4bpp)
            {
                byte[] newdata = new byte[rawdata.Length * 2];
                for (int i = 0; i < rawdata.Length; i++)
                {
                    newdata[i * 2] = (byte)(rawdata[i] & 0xF);
                    newdata[i * 2 + 1] = (byte)(rawdata[i] >> 4);
                }
                data = newdata;
            }
            else
                data = (byte[])rawdata.Clone();
        }

        public override void save()
        {
            saveImageData();
            if (isLZCompressed)
                f.replace(ROM.LZ77_Compress(rawdata), this);
            else
                f.replace(rawdata, this);
        }

        public override byte[] getRawData()
        {
            return data;
        }

        public override void setRawData(byte[] data)
        {
            this.data = (byte[])data.Clone();
        }

        public static Rectangle getTileRectangle(Bitmap b, int tileSize, int tilenum)
        {
            int tileCountX = b.Width / tileSize;
            int tileCountY = b.Height / tileSize;
            int tileCount = tileCountX * tileCountY;

            if (tilenum >= tileCount)
                throw new Exception("Tile number out of bounds!");

            int x = (tilenum % tileCountX)*tileSize;
            int y = (tilenum / tileCountX)*tileSize;

            return new Rectangle(x, y, tileSize, tileSize);
        }

        public static Bitmap CutImage(Image im, int width, int blockrows)
        {
            int blocksize = im.Height / blockrows;
            int blockcount = im.Width / blocksize;

            int cols = width / blocksize;
            int rows = blockcount / cols;

            Bitmap b = new Bitmap(cols * blocksize, rows * blocksize * blockrows);
            Graphics g = Graphics.FromImage(b);
            g.Clear(Color.Gray);

            Rectangle SourceRect = new Rectangle(0, 0, cols * blocksize, blocksize);
            Rectangle DestRect = new Rectangle(0, 0, cols * blocksize, blocksize);


            for (int r = 0; r < blockrows; r++)
            {
                SourceRect.Y = r * blocksize;
                for (int i = 0; i < rows; i++)
                {
                    SourceRect.X = i * cols * blocksize;
                    DestRect.Y = i * blocksize + r * rows * blocksize;

                    g.DrawImage(im, DestRect, SourceRect, GraphicsUnit.Pixel);
                }
            }

            return b;
        }
        public override string ToString()
        {
            return f.name;
        }
    }
}
