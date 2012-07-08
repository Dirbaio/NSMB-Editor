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
            imgs = new List<PixelPalettedImage>();
            pals = new List<Palette>();
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
                throw new Exception("Wrong input image size");

            List<Bitmap> bl = new List<Bitmap>();
            for (int i = 0; i < pals.Count; i++)
            {
                Bitmap bbb = new Bitmap(b.Width, tys);
                Graphics bbbgfx = Graphics.FromImage(bbb);
                bbbgfx.DrawImage(b, 0, -i*tys, b.Width, b.Height);
                bl.Add(bbb);
            }
            b.Dispose();

            ImageIndexer ii = new ImageIndexer(bl, 256, true, 0);
            

            int x = 0;
            foreach (PixelPalettedImage i in imgs)
            {
                i.setPixelData(ii.imageData, x, 0);
                x += i.getWidth();
            }

            for (int i = 0; i < pals.Count; i++)
            {
                pals[i].pal = ii.palettes[i];
            }

            return false;
        }
    }
}
