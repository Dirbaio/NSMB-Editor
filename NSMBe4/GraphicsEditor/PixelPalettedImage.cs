using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace NSMBe4
{
    public abstract class PixelPalettedImage : PalettedImage
    {
        public override void replace(Bitmap b, Palette p)
        {
            int w = getWidth();
            int h = getHeight();

            for (int x = 0; x < w; x++)
                for (int y = 0; y < w; y++)
                    setPixel(x, y, p.getClosestColor(b.GetPixel(x, y), colorsPerPixel));
        }

        public override Bitmap render(Palette p)
        {
            int w = getWidth();
            int h = getHeight();

            Bitmap b = new Bitmap(w, h);
            for (int x = 0; x < w; x++)
                for (int y = 0; y < h; y++)
                    b.SetPixel(x, y, p.getColorSafe(getPixel(x, y)));

            return b;
        }
        public abstract void setPixel(int x, int y, int c);
        public abstract int getPixel(int x, int y);
    }
}
