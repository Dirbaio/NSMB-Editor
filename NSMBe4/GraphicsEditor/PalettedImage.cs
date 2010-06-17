using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace NSMBe4
{
    public abstract class PalettedImage
    {
        public abstract void replace(Bitmap b, Palette p);
        public abstract Bitmap render(Palette p);
        public abstract void save();
        public abstract int getWidth();
        public abstract int getHeight();
        public abstract void close();
        public int colorsPerPixel = 256;

    }
}
