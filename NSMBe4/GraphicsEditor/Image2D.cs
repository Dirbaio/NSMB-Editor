using System;
using System.Collections.Generic;
using System.Text;
using NSMBe4.DSFileSystem;

namespace NSMBe4
{
    public class Image2D : PixelPalettedImage
    {
        private File f;
        private byte[] data;
        private byte[] rawdata;
        public int width;
        public int tileOffset;

        private bool is4bppI;
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
        {
            this.f = f;
            this.is4bppI = is4bpp;
            this.width = width;
            f.beginEdit(this);
            reload();
        }

        public void reload()
        {
            rawdata = f.getContents();
            rawdata = ROM.LZ77_Decompress(rawdata);
            loadImageData();
        }

        public override void close()
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
    }
}
