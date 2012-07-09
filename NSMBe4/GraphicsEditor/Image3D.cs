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

namespace NSMBe4
{
    public class Image3D : PixelPalettedImage
    {
        private File f;
        private byte[] data;

        public bool color0Transp;
        public int width, height;
        public int format;

        public byte bpp
        {
            get { return bpps[format]; }
        }

        //                                 0  1  2  3  4  5  6  7
        public static byte[] bpps  = new byte[] { 0, 8, 2, 4, 8, 2, 8, 16 };

        public static string[] formatNames = {
                                          "Error?", //0
                                          "A3I5 Translucent", //1
                                          "2bpp Paletted",  //2
                                          "4bpp Paletted", //3
                                          "8bpp Paletted",//4
                                          "4x4-Texeled",//5
                                          "A5I3 Translucent",//6
                                          "16-bit Color Texture" //7
                                      };

        public Image3D(File f, bool color0, int width, int height, int format)
        {
            this.f = f;
            this.color0Transp = color0;
            this.width = width;
            this.height = height;
            this.format = format;

//            f.beginEdit(this);
            data = f.getContents();
        }

        private int getPixelVal(int x, int y)
        {
            int i = x + y * width;
            if (bpp == 8) return data[i];
            if (bpp == 16) return data[i * 2] | data[i * 2 + 1] << 8;
            if (bpp == 4)
            {
                int res = data[i / 2];
                res = res >> ((i % 2) * 4);
                res &= 0xF;
                return res;
            }
            if (bpp == 2)
            {
                int res = data[i / 4];
                res = res >> ((i % 4) * 2);
                res &= 0x3;
                return res;
            }
            throw new Exception("Unsupported BPP Value: " + bpp);
        }

        private void setPixelVal(int x, int y, int v)
        {
            int i = x + y * width;
            if (bpp == 8) data[i] = (byte)v;
            else if (bpp == 16)
            {
                data[i * 2] = (byte)(v & 0xFF);
                data[i * 2] = (byte)((v >> 8) & 0xFF);
            }

            else if (bpp == 4)
            {
                int res = data[i / 2];
                res &= ~(0xF << ((i % 2) * 4));
                res |= (v & 0xF) << ((i % 2) * 4);
                data[i / 2] = (byte)res;
            }

            else if (bpp == 2)
            {
                int res = data[i / 4];
                res &= ~(0xF << ((i % 4) * 2));
                res |= (v & 0xF) << ((i % 4) * 2);
                data[i / 4] = (byte)res;
            } 
            else
                throw new Exception("Unsupported BPP Value: " + bpp);
        }
        public override int getPixel(int x, int y)
        {
            if (x < 0 || x >= width) return 0;
            if (y < 0 || y >= height) return 0;
            int val = getPixelVal(x, y);
            if (format == 1) val &= 0x1F;
            if (format == 6) val &= 0x07;
            return val;
        }

        public override void setPixel(int x, int y, int c)
        {
            if (x < 0 || x >= width) return;
            if (y < 0 || y >= height) return;
            if (format == 1)
            {
                c &= 0x1F;
                c |= getPixelVal(x, y) & (~0x1F);
            }
            if (format == 6)
            {
                c &= 0x07;
                c |= getPixelVal(x, y) & (~0x07);
            }

            setPixelVal(x, y, c);
        }

        public override int getWidth()
        {
            return width;
        }

        public override int getHeight()
        {
            return height;
        }

        public override byte[] getRawData()
        {
            return data;
        }

        public override void setRawData(byte[] data)
        {
            this.data = (byte[])data.Clone();
        }

        public override void save()
        {
            f.replace(data, this);
        }

        public override void endEdit()
        {
            f.endEdit(this);
        }
        public override void beginEdit()
        {
            f.beginEdit(this);
        }


        public override string ToString()
        {
            return name ;
        }
    }
}
