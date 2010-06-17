using System;
using System.Collections.Generic;
using System.Text;
using NSMBe4.DSFileSystem;

namespace NSMBe4
{
    public class Image2D : PixelPalettedImage
    {
        File f;
        byte[] data;
        int width;
        bool is4bpp;

        public Image2D(File f, int width, bool is4bpp)
        {
            this.f = f;
            this.is4bpp = is4bpp;
            this.width = width;
            f.beginEdit(this);
            reload();
        }

        public void reload()
        {
            data = f.getContents();
            data = ROM.LZ77_Decompress(data);

            if (is4bpp)
            {
                byte[] newdata = new byte[data.Length * 2];
                for (int i = 0; i < data.Length; i++)
                {
                    newdata[i * 2] = (byte)(data[i] & 0xF);
                    newdata[i * 2 + 1] = (byte)(data[i] >> 4);
                }
                data = newdata;
            }
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
            return data.Length / width;
        }

        public override int getPixel(int x, int y)
        {
            int bx = x / 8;
            int by = y / 8;

            int offs = bx + by * (width / 8);
            offs *= 64;
            offs += x % 8 + 8 * (y % 8);
            return data[offs];
        }

        public override void setPixel(int x, int y, int c)
        {
            int bx = x / 8;
            int by = y / 8;

            int offs = bx + by * (width / 8);
            offs *= 64;
            offs += x % 8 + 8 * (y % 8);
            data[offs] = (byte)c;
        }

        public override void save()
        {
            byte[] newdata = data;
            if (is4bpp)
            {
                newdata = new byte[data.Length / 2];

                for(int i = 0; i < newdata.Length; i++)
                    newdata[i] = (byte)(
                        data[i*2] & 0xF |
                        (data[i*2+1] & 0xF)<<4);
            }

            f.replace(newdata, this);
        }
    }
}
