using System;
using System.Collections.Generic;
using System.Text;
using NSMBe4.DSFileSystem;

namespace NSMBe4
{
    public class EnpgImage2D : Image2D
    {
        
        public EnpgImage2D(File f)
            :base(f, 256, false, false)
        {
        }

        public override int getPixel(int x, int y)
        {
            int offs = x + y * 256;
            if (offs >= data.Length) return 0;
            return data[offs];
        }

        public override void setPixel(int x, int y, int c)
        {
            int offs = x + y * 256;

            if (offs >= data.Length) return;
            data[offs] = (byte)c;
        }

        /*
        public override int getPixel(int x, int y)
        {
            int bx = x / 32;
            int by = y / 32;

            int offs = bx + by * (width / 32);
            if (offs < 0) return 0;
            offs *= 32 * 32;
            offs += x % 32 + 32 * (y % 32);
            if (offs >= data.Length) return 0;
            return data[offs];
        }

        public override void setPixel(int x, int y, int c)
        {
            int bx = x / 32;
            int by = y / 32;

            int offs = bx + by * (width / 32) - tileOffset;
            if (offs < 0) return;

            offs *= 32 * 32;
            offs += x % 32 + 32 * (y % 32);
            if (offs >= data.Length) return;
            data[offs] = (byte)c;
        }*/
    }
}
