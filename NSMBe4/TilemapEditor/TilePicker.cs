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

        public delegate void TileSelectedd(int selTileNum, int selTilePal, int selTileWidth, int selTileHeight);
        public event TileSelectedd TileSelected;


        int hovertx = -1;
        int hoverty = -1;


        int downTileNum;

        public int selTileNum = 0;
        public int selTilePal = 0;
        public int selTileWidth = 1;
        public int selTileHeight = 1;

        public Bitmap[] buffers;
        public int bufferWidth, bufferHeight;
        public int bufferCount;
        public int tileSize;

        public bool allowsRectangle = true;
        public bool allowNoTile = true;

        public TilePicker()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.Selectable, true);
        }

        public void init(Bitmap[] buffers, int tileSize)
        {
            this.buffers = buffers;
            this.bufferCount = buffers.Length;

            this.tileSize = tileSize;
            this.bufferHeight = buffers[0].Height / tileSize;
            this.bufferWidth = buffers[0].Width / tileSize;
            
            pictureBox1.Size = pictureBox1.MinimumSize = new Size(bufferWidth * tileSize, bufferHeight * bufferCount * tileSize);
        }

        public void SetTileset(NSMBTileset t)
        {
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (buffers == null)
                return;

            e.Graphics.FillRectangle(Brushes.DarkSlateGray,
                0, 0, bufferWidth * tileSize, bufferHeight * tileSize * bufferCount);

            for (int i = 0; i < bufferCount; i++)
                e.Graphics.DrawImage(buffers[i], 0, i * bufferHeight * tileSize);

            e.Graphics.DrawRectangle(Pens.White,
                (selTileNum % bufferWidth) * tileSize, (selTileNum / bufferWidth + selTilePal * bufferHeight) * tileSize,
                selTileWidth * tileSize, selTileHeight * tileSize);

            if(!down && hovertx != -1)
                e.Graphics.DrawRectangle(Pens.White,
                    hovertx * tileSize, hoverty*tileSize,
                    tileSize, tileSize);
        }

        bool down = false;

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            down = true;

            int tx = e.X / tileSize;
            if (tx >= bufferWidth) tx = bufferWidth-1;
            if (tx < 0) tx = 0;
            int ty = e.Y / tileSize;
            if (ty >= bufferHeight * bufferCount) ty = bufferHeight * bufferCount - 1;
            if (ty < 0) ty = 0;

            selTilePal = ty / bufferHeight;
            ty %= bufferHeight;

            downTileNum = ty * bufferWidth + tx;

            pictureBox1_MouseMove(sender, e);

            this.Focus();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (down)
            {
                int dx = downTileNum % bufferWidth;
                int dy = downTileNum / bufferWidth;

                int tx = e.X / tileSize;
                if (tx >= bufferWidth) tx = bufferWidth - 1;
                if (tx < 0) tx = 0;

                int ty = e.Y / tileSize;
                ty -= bufferHeight * selTilePal;
                if (ty >= bufferHeight) ty = bufferHeight - 1;
                if (ty < 0) ty = 0;

                int xmin = Math.Min(dx, tx);
                int ymin = Math.Min(dy, ty);
                int xmax = Math.Max(dx, tx);
                int ymax = Math.Max(dy, ty);

                selTileNum = xmin + ymin * bufferWidth;
                selTileWidth = xmax - xmin + 1;
                selTileHeight = ymax - ymin + 1;
            }
            else
            {
                int tx = e.X / tileSize;
                if (tx >= bufferWidth) tx = bufferWidth - 1;
                if (tx < 0) tx = 0;
                int ty = e.Y / tileSize;
                if (ty >= bufferHeight * bufferCount) ty = bufferHeight * bufferCount - 1;
                if (ty < 0) ty = 0;

                hovertx = tx;
                hoverty = ty;
            }
            pictureBox1.Invalidate(true);
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (down)
            {
                if (TileSelected != null)
                    TileSelected(selTileNum, selTilePal, selTileWidth, selTileHeight);
            }
            down = false;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            hovertx = -1;
            hoverty = -1;
            pictureBox1.Invalidate(true);
        }


        private void TilePicker_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

    }
}
