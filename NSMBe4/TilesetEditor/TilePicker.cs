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
    public partial class TilePicker : UserControl
    {
        Image tilesetImage;
        int selx = -1, sely = -1;
        int hovx = -1, hovy = -1;
        int tileCount;

        public TilePicker()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.Selectable, true);
        }

        public delegate void TileSelectedd(int tile, bool second);
        public event TileSelectedd TileSelected;
        public delegate void QuarterChangedd(int dx, int dy);
        public event QuarterChangedd QuarterChanged;

        NSMBTileset t;

        public void SetTileset(NSMBTileset t)
        {
            this.t = t;
            tileCount = t.TilesetBuffer.Width / 8;
            tilesetImage = Image2D.CutImage(t.TilesetBuffer, 256, 2);
            pictureBox1.Size = tilesetImage.Size;
            pictureBox1.Invalidate(true);
        }

        public void selectTile(int tile, bool second)
        {
            if (second)
                tile += tileCount;
            selx = tile % 32;
            sely = tile / 32;
            pictureBox1.Invalidate(true);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            int tx = e.X / 8;
            int ty = e.Y / 8;
            int t = ty * 32 + tx;
            if (t >= 0 && t < tileCount*2)
            {
                selx = tx;
                sely = ty;
                bool second = false;
                if (t > tileCount)
                {
                    second = true;
                    t -= tileCount;
                }
                if (TileSelected != null)
                    TileSelected(t, second);
            }
            this.Focus();
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

        private void TilePicker_KeyPress(object sender, KeyPressEventArgs e)
        {
//            Console.Out.WriteLine("Key pressed: " + e.KeyChar);
            if(QuarterChanged == null) return;

            if (e.KeyChar == 'a' || e.KeyChar == 'A')
                QuarterChanged(-1, 0);
            if (e.KeyChar == 'w' || e.KeyChar == 'W')
                QuarterChanged(0, -1);
            if (e.KeyChar == 's' || e.KeyChar == 'S')
                QuarterChanged(0, 1);
            if (e.KeyChar == 'd' || e.KeyChar == 'D')
                QuarterChanged(1, 0);
        }

    }
}
