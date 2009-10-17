using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace NSMBe4 {
    public partial class PalettePicker : UserControl {
        private Bitmap DisplayBuffer = null;
        private Color[] Palette;
        public int SelectedFG;
        public int SelectedBG;
        public int ViewPal;
        public int PalSize;
        public int PalCount;

        private bool DragChoice = false;

        public PalettePicker() {
            InitializeComponent();
        }

        public void SetPalette(Color[] colours, int palsize) {
            Palette = colours;
            SelectedFG = 1;
            SelectedBG = 0;
            PalSize = palsize;
            PalCount = colours.Length / palsize;
            Height = PalSize / 16 * 12 + 26;
            SetViewPal(0);
        }

        public void SetViewPal(int idx) {
            ViewPal = idx;
            if (DisplayBuffer == null) {
                DisplayBuffer = new Bitmap(16 * 12 + 2, PalSize / 16 * 12 + 2);
            }

            Graphics g = Graphics.FromImage(DisplayBuffer);
            int i = ViewPal * PalSize;

            g.Clear(Color.Black);

            for (int y = 2; y < DisplayBuffer.Height; y += 12) {
                for (int x = 2; x < DisplayBuffer.Width; x += 12) {
                    g.FillRectangle(new SolidBrush(Palette[i]), x, y, 10, 10);
                    i += 1;
                }
            }

            Invalidate();
        }

        private void PalettePicker_Paint(object sender, PaintEventArgs e) {
            if (DisplayBuffer != null) {
                e.Graphics.DrawImage(DisplayBuffer, 0, 24);

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
                e.Graphics.FillRectangle(new SolidBrush(Palette[(ViewPal * PalSize) + SelectedFG]), 23, 2, 60, 16);

                e.Graphics.FillRectangle(Brushes.Black, 91, 4, 12, 12);

                e.Graphics.FillRectangle(Brushes.Black, 109, 0, 64, 20);
                e.Graphics.FillRectangle(new SolidBrush(Palette[(ViewPal * PalSize) + SelectedBG]), 111, 2, 60, 16);
            }
        }

        private void PalettePicker_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right) return;

            if (e.Y < 24) {
                if (e.X >= 91 && e.Y >= 4 && e.X <= 102 && e.Y <= 15) {
                    int swap = SelectedFG;
                    SelectedFG = SelectedBG;
                    SelectedBG = SelectedFG;
                    Invalidate();
                }
            } else {
                DragChoice = true;
                PalettePicker_MouseMove(sender, e);
            }
        }

        private void PalettePicker_MouseMove(object sender, MouseEventArgs e) {
            if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right) return;
            if (!DragChoice) return;

            int X = e.X, Y = e.Y;
            if (X < 0) X = 0;
            if (X >= Width) X = Width - 1;
            if (Y < 24) Y = 24;
            if (Y >= Height) Y = Height - 1;

            int picked = (((Y - 24) / 12) * 16) + (X / 12);
            //while (picked < 0) picked += 16;
            //while (picked >= PalSize) picked -= 16;

            if (e.Button == MouseButtons.Left) {
                SelectedFG = picked;
            } else {
                SelectedBG = picked;
            }

            Refresh();
        }

        private void PalettePicker_MouseUp(object sender, MouseEventArgs e) {
            DragChoice = false;
        }
    }
}
