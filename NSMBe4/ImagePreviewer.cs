using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NSMBe4 {
    public partial class ImagePreviewer : Form {
        public ImagePreviewer(Image image) {
            InitializeComponent();
            this.Size = image.Size;
            Console.Out.WriteLine("Width: " + image.Width + ", Height: " + image.Height);
            this.Width += 8;
            this.Height += 24;
            this.Image = image;
            pictureBox1.Image = image;
        }


        private Image Image;

        public static void ShowCutImage(Image im, int width, int blockrows)
        {
            int blocksize = im.Height / blockrows;
            int blockcount = im.Width/blocksize;

            int cols = width / blocksize;
            int rows = blockcount / cols;

            Bitmap b = new Bitmap(cols * blocksize, rows * blocksize * blockrows);
            Graphics g = Graphics.FromImage(b);
            g.Clear(Color.Gray);

            Rectangle SourceRect = new Rectangle(0, 0, cols * blocksize, blocksize);
            Rectangle DestRect = new Rectangle(0, 0, cols * blocksize, blocksize);


#if !USE_GDIPLUS
            IntPtr pTarget = g.GetHdc();
            Graphics srcGr = Graphics.FromImage(im);
            IntPtr pSource = srcGr.GetHdc();
#endif
            
            for (int r = 0; r < blockrows; r++)
            {
                SourceRect.Y = r * blocksize;
                for (int i = 0; i < rows; i++)
                {
                    SourceRect.X = i * cols * blocksize;
                    DestRect.Y = i * blocksize + r * rows * blocksize;

#if USE_GDIPLUS
                    g.DrawImage(im, DestRect, SourceRect, GraphicsUnit.Pixel);
#else
                    GDIImports.BitBlt(pTarget, DestRect.X, DestRect.Y, DestRect.Width, DestRect.Height, pSource, SourceRect.X, SourceRect.Y, GDIImports.TernaryRasterOperations.SRCCOPY);
#endif
                }
            }

#if !USE_GDIPLUS
            g.ReleaseHdc(pTarget);
            srcGr.ReleaseHdc(pSource);
#endif

            new ImagePreviewer(b).Show();
        }
    }
}
