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

namespace NSMBe4 {
    public partial class PalettePicker : UserControl {
        private Bitmap PalBuffer = null;
        private Palette pal;
        public int SelectedFG;
        public int SelectedBG;
        public int PalSize;

        private bool DragChoice = false;

        public delegate void EditColourDelegate(int idx);
        public event EditColourDelegate EditColour;

        int rows;

        public PalettePicker() {
            InitializeComponent();
        }

        public void SetPalette(Palette pal) {
            this.pal = pal;
            SelectedFG = 1;
            SelectedBG = 0;
            PalSize = pal.pal.Length;
            rows = (PalSize + 15) / 16;
            Height = rows * 12 + 26;

            if (PalBuffer == null) {
                PalBuffer = new Bitmap(16 * 12 + 2, rows * 12 + 2);
            }

            Graphics g = Graphics.FromImage(PalBuffer);

            g.Clear(Color.Black);

            for (int i = 0; i < pal.pal.Length; i++)
            {
                int x = 2 + (i % 16) * 12;
                int y = 2 + (i / 16) * 12;
                g.FillRectangle(new SolidBrush(pal.pal[i]), x, y, 10, 10);
            }

            drawingBox.Invalidate();
        }

        private void drawingBox_Paint(object sender, PaintEventArgs e) 
        {
            if (PalBuffer != null) 
            {
                e.Graphics.DrawImage(PalBuffer, 0, 24);

                Point FGPos = new Point(SelectedFG % 16 * 12 + 1, SelectedFG / 16 * 12 + 25);
                e.Graphics.DrawRectangle(Pens.White, FGPos.X, FGPos.Y, 11, 11);
                e.Graphics.FillRectangle(Brushes.White, FGPos.X + 1, FGPos.Y + 1, 3, 1);
                e.Graphics.FillRectangle(Brushes.White, FGPos.X + 1, FGPos.Y + 1, 2, 2);
                e.Graphics.FillRectangle(Brushes.White, FGPos.X + 1, FGPos.Y + 1, 1, 3);

                Point BGPos = new Point(SelectedBG % 16 * 12 + 1, SelectedBG / 16 * 12 + 25);
                e.Graphics.DrawRectangle(Pens.White, BGPos.X, BGPos.Y, 11, 11);
                e.Graphics.FillRectangle(Brushes.White, BGPos.X + 8, BGPos.Y + 10, 3, 1);
                e.Graphics.FillRectangle(Brushes.White, BGPos.X + 9, BGPos.Y + 9, 2, 2);
                e.Graphics.FillRectangle(Brushes.White, BGPos.X + 10, BGPos.Y + 8, 1, 3);

                e.Graphics.FillRectangle(Brushes.Black, 21, 0, 64, 20);
                if (SelectedFG < pal.pal.Length)
                    e.Graphics.FillRectangle(new SolidBrush(pal.pal[SelectedFG]), 23, 2, 60, 16);

                e.Graphics.FillRectangle(Brushes.Black, 91, 4, 12, 12);

                e.Graphics.FillRectangle(Brushes.Black, 109, 0, 64, 20);
                if (SelectedBG < pal.pal.Length)
                    e.Graphics.FillRectangle(new SolidBrush(pal.pal[SelectedBG]), 111, 2, 60, 16);
            }
        }

        private void drawingBox_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right) return;

            if (e.Y < 24) {
                if (e.X >= 91 && e.Y >= 4 && e.X <= 102 && e.Y <= 15) {
                    int swap = SelectedFG;
                    SelectedFG = SelectedBG;
                    SelectedBG = swap;
                    drawingBox.Invalidate();
                }
            } else {
                DragChoice = true;
                drawingBox_MouseMove(sender, e);
            }
        }

        private void drawingBox_MouseMove(object sender, MouseEventArgs e) {
            if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right) return;
            if (!DragChoice) return;

            int X = e.X, Y = e.Y;
            if (X < 0) X = 0;
            if (X >= Width - 1) X = Width - 2;
            if (Y < 24) Y = 24;
            if (Y >= Height - 1) Y = Height - 2;

            X -= 1;
            Y -= 25;

            int picked = ((Y / 12) * 16) + (X / 12);
            int old = -1;

            if (ModifierKeys == Keys.Control) {
                EditColour(picked);
            } else {
                if (e.Button == MouseButtons.Left) {
                    old = SelectedFG;
                    SelectedFG = picked;
                } else {
                    old = SelectedBG;
                    SelectedBG = picked;
                }

                if (old != picked) {
                    drawingBox.Invalidate();
                }
            }
        }

        private void drawingBox_MouseUp(object sender, MouseEventArgs e)
        {
            DragChoice = false;
        }

        private void drawingBox_Click(object sender, EventArgs e)
        {

        }
    }
}
