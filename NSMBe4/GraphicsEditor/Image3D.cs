using System;
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

//        byte[] data;

        public byte bpp
        {
            get { return bpps[format]; }
        }

        public byte mask
        {
            get { return masks[format]; }
        }
        public byte cmask
        {
            get { return masks[format]; }
        }

        //        public uint f5DataOffset;

        //                                  0  1     2  3  4     5    6     7
        static byte[] bpps  = new byte[] { 0, 8, 2, 4, 8, 0, 8, 16 };
        static byte[] masks = new byte[] { 0, 0xFF, 3, 7, 0xFF, 0, 0xFF, 0x0 };
        static byte[] cmsks = new byte[] { 0, 0x1F, 3, 7, 0xFF, 0, 0x07, 0x0 };



        static string[] formatNames = {
                                          "Error?",
                                          "A3I5 Translucent Texture",
                                          "4-Color Palette Texture", 
                                          "16-Color Palette Texture", 
                                          "256-Color Palette Texture",
                                          "4x4-Texel Compressed Texture",
                                          "A5I3 Translucent Texture",
                                          "Direct Color Texture (UNSUPPORTED)"
                                      };

        public Image3D(File f, bool color0, int width, int height, int format)
        {
            this.f = f;
            this.color0Transp = color0;
            this.width = width;
            this.height = height;
            this.format = format;

            f.beginEdit(this);
            data = f.getContents();
        }

        public override int getPixel(int x, int y)
        {
            throw new NotImplementedException();
        }

        public override void setPixel(int x, int y, int c)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public override void setRawData(byte[] data)
        {
            throw new NotImplementedException();
        }

        public override void save()
        {
            throw new NotImplementedException();
        }

        public override void close()
        {
            throw new NotImplementedException();
        }



    }
}
