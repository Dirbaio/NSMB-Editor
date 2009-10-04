using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace NSMBe4
{
    public partial class TilePicker : UserControl
    {
        Image tilesetImage;
        int selx = -1, sely = -1;
        int hovx = -1, hovy = -1;

        public TilePicker()
        {
            InitializeComponent();
        }
        public delegate void TileSelectedd(int tile);
        public event TileSelectedd TileSelected;

        NSMBTileset t;

        public void SetTileset(NSMBTileset t)
        {
            this.t = t;
            tilesetImage = GraphicsViewer.CutImage(t.TilesetBuffer, 256, 2);
            pictureBox1.Invalidate(true);
        }

        public void selectTile(int tile)
        {
            selx = tile % 32;
            sely = tile / 32;
            pictureBox1.Invalidate(true);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            int tx = e.X / 8;
            int ty = e.Y / 8;
            int t = ty * 16 + tx;
            if (t >= 0 && t < 256*224/64)
            {
                selx = tx;
                sely = ty;
                if (TileSelected != null)
                    TileSelected(t);
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (tilesetImage == null)
                return;

            e.Graphics.DrawImage(tilesetImage, 0, 0);
            e.Graphics.DrawRectangle(Pens.White, selx * 8, sely * 8, 8, 8);
            e.Graphics.DrawRectangle(Pens.White, hovx * 8, hovy * 8, 8, 8);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            hovx = e.X / 8;
            hovy = e.Y / 8;
            pictureBox1.Invalidate(true);
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            hovx = -1;
            hovy = -1;
            pictureBox1.Invalidate(true);
        }
    }
}
