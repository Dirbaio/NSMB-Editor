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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace NSMBe4
{
    public partial class Map16Picker : UserControl
    {
        Image map16Image;
        int selx = -1, sely = -1;
        int hovx = -1, hovy = -1;
        int tileCount;

        public Map16Picker()
        {
            InitializeComponent();
        }

        public delegate void TileSelectedd(int tile);
        public event TileSelectedd TileSelected;

        NSMBTileset t;

        public void SetTileset(NSMBTileset t)
        {
            this.t = t;
            tileCount = t.Map16Buffer.Width / 16;
            map16Image = GraphicsViewer.CutImage(t.Map16Buffer, 256, 1);
            pictureBox1.Size = map16Image.Size;
            pictureBox1.Invalidate(true);
        }

        public void selectTile(int tile)
        {
            selx = tile % 16;
            sely = tile / 16;
            pictureBox1.Invalidate(true);

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            int tx = e.X / 16;
            int ty = e.Y / 16;
            int t = ty * 16 + tx;
            if (t >= 0 && t < tileCount)
            {
                selx = tx;
                sely = ty;
                if (TileSelected != null)
                    TileSelected(t);
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (map16Image == null)
                return;

            e.Graphics.DrawImage(map16Image, 0, 0);
            e.Graphics.DrawRectangle(Pens.White, selx * 16, sely * 16, 16, 16);
            e.Graphics.DrawRectangle(Pens.White, hovx * 16, hovy * 16, 16, 16);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            hovx = e.X / 16;
            hovy = e.Y / 16;
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
