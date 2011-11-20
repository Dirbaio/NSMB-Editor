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
        public int qx = -1;
        public int qy = -1;
        int tileCount;
        public bool map16Editing = false;
        public float zoom = 1f;
        private bool zoomUpdate = false;

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

        public void SetZoom(float zoom)
        {
            this.zoom = zoom;
            zoomUpdate = true;
            pictureBox1.Size = new Size((int)(map16Image.Width * zoom), (int)(map16Image.Height * zoom));
            zoomUpdate = false;
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
            
            int tx = (int)(e.X / 16 / zoom);
            int ty = (int)(e.Y / 16 / zoom);
            int t = ty * 16 + tx;
            if (t >= 0 && t < tileCount)
            {
                if (Control.ModifierKeys == Keys.Control && map16Editing)
                {
                    int ot = selx + sely * 16;
                    Array.Copy(this.t.TileBehaviors[ot], this.t.TileBehaviors[t], 4);
                }
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
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            e.Graphics.DrawImage(map16Image, 0f, 0f, map16Image.Width * zoom, map16Image.Height * zoom);
            e.Graphics.DrawRectangle(Pens.White, selx * 16 * zoom, sely * 16 * zoom, 16 * zoom, 16 * zoom);
            e.Graphics.DrawRectangle(Pens.White, hovx * 16 * zoom, hovy * 16 * zoom, 16 * zoom, 16 * zoom);
            if (qx != -1)
                e.Graphics.DrawRectangle(Pens.Yellow, selx * 16 * zoom + qx * 8 * zoom, sely * 16 * zoom + qy * 8 * zoom, 8 * zoom, 8 * zoom);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            hovx = (int)(e.X / 16 / zoom);
            hovy = (int)(e.Y / 16 / zoom);
            pictureBox1.Invalidate(true);
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            hovx = -1;
            hovy = -1;
            pictureBox1.Invalidate(true);
        }

        private void zoomIn_Click(object sender, EventArgs e)
        {
            if (zoom < 8)
            {
                SetZoom(zoom + 1f);
                zoomOut.Enabled = true;
                zoomActualSize.Enabled = true;
                if (zoom == 8)
                    zoomIn.Enabled = false;
            }
        }

        private void zoomActualSize_Click(object sender, EventArgs e)
        {
            SetZoom(1f);
            zoomIn.Enabled = true;
            zoomActualSize.Enabled = false;
            zoomOut.Enabled = false;
        }

        private void zoomOut_Click(object sender, EventArgs e)
        {
            if (zoom > 1)
            {
                SetZoom(zoom - 1f);
                zoomIn.Enabled = true;
                if (zoom == 1) {
                    zoomOut.Enabled = false;
                    zoomActualSize.Enabled = false;
                }
            }
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            if (zoomUpdate) return;
            pictureBox1.Size = new Size((int)(map16Image.Width * zoom), (int)(map16Image.Height * zoom));
        }
    }
}
