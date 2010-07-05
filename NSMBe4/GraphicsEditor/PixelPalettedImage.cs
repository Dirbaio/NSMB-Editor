using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace NSMBe4
{
    public abstract class PixelPalettedImage : PalettedImage
    {
        public override void replaceImgAndPal(Bitmap b, Palette p)
        {
            p.pal = ImageIndexer.createPaletteForImage(b, p.pal.Length);
            replaceWithPal(b, p);
        }

        public override void replaceWithPal(Bitmap b, Palette p)
        {
            for (int x = 0; x < getWidth(); x++)
                for (int y = 0; y < getHeight(); y++)
                {
                    Color c = b.GetPixel(x, y);
                    int i = p.getClosestColor(c);
                    setPixel(x, y, i);
                }
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
