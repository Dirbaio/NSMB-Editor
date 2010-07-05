using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace NSMBe4
{
    public class GraphicsSet
    {
        public List<PixelPalettedImage> imgs;
        public List<Palette> pals;

        public IWin32Window win;
        private int tx, ty, tys;

        public GraphicsSet()
        {

        }

        private void calcSizes()
        {
            tx = 0;
            tys = 0;
            foreach (PixelPalettedImage img in imgs)
            {
                tx += img.getWidth();
                if (img.getHeight() > tys)
                    tys = img.getHeight();
            }

            ty = tys * pals.Count;
        }

        public bool export()
        {
            checkStuff();

            SaveFileDialog ofd = new SaveFileDialog();
            ofd.Filter = "PNG Files|*.png";
            if (ofd.ShowDialog(win) == DialogResult.Cancel) return false;
            calcSizes();


            Bitmap b = new Bitmap(tx, ty);
            Graphics bgfx = Graphics.FromImage(b);
            int x = 0;
            foreach (PixelPalettedImage img in imgs)
            {
                int y = 0;
                foreach (Palette pal in pals)
                {
                    Bitmap bb = img.render(pal);
                    bgfx.DrawImage(bb, x, y, bb.Width, bb.Height);
                    bb.Dispose();
                    y += tys;
                }
                x += img.getWidth();
            }

            b.Save(ofd.FileName);
            b.Dispose();
            return true;
        }

        private void checkStuff()
        {
            if (pals.Count == 0)
                throw new Exception("No palettes selected");
            if (imgs.Count == 0)
                throw new Exception("No images selected");

            int palss = pals[0].pal.Length;
            foreach (Palette p in pals)
            {
                if (p.pal.Length != palss)
                    throw new Exception("All palettes must be the same size");
            }
        }
        public bool import()
        {
            checkStuff();

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "PNG Files|*.png";
            if (ofd.ShowDialog() == DialogResult.Cancel) return false;

            calcSizes();

            Bitmap b = new Bitmap(ofd.FileName);
            if (b.Width != tx || b.Height != ty)
                throw new Exception("Wrong image size");

            Bitmap bbb = new Bitmap(b.Width, tys);
            Graphics bbbgfx = Graphics.FromImage(bbb);
            bbbgfx.DrawImage(b, 0, 0, b.Width, b.Height);
            Color[] firstPal = ImageIndexer.createPaletteForImage(bbb, pals[0].pal.Length);
            pals[0].pal = firstPal;

            bbb.Dispose();

            int x = 0;
            foreach (PixelPalettedImage i in imgs)
            {
                Bitmap bb = new Bitmap(i.getWidth(), i.getHeight());
                Graphics bbgfx = Graphics.FromImage(bb);
                bbgfx.DrawImage(b, -x, 0, b.Width, b.Height);

                i.replaceWithPal(bb, pals[0]);
                bb.Dispose();
            }

            for (int i = 1; i < pals.Count; i++)
            {
                Color[] newPal = new Color[firstPal.Length];
                x = 0;
                foreach(PixelPalettedImage img in imgs)
                {
                    for (int xx = 0; xx < img.getWidth(); xx++)
                    {
                        for (int y = 0; y < img.getHeight(); y++)
                        {
                            Color c = b.GetPixel(x + xx, y + i * tys);
                            int ind = img.getPixel(xx, y);
                            if (newPal[ind] == null)
                                newPal[ind] = c;
                            else
                                newPal[ind] = ImageTiler.colorMean(newPal[ind], c, 6, 1);
                        }
                    }
                    x += img.getWidth();
                }
                pals[i].pal = newPal;
            }

            return false;
        }
    }
}
