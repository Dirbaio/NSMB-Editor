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
        private Color[] Palette;
        public int SelectedFG;
        public int SelectedBG;
        public int ViewPal;
        public int PalSize;
        public int PalCount;

        private bool DragChoice = false;

        public delegate void EditColourDelegate(int idx);
        public event EditColourDelegate EditColour;

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
            if (PalBuffer == null) {
                PalBuffer = new Bitmap(16 * 12 + 2, PalSize / 16 * 12 + 2);
            }

            Graphics g = Graphics.FromImage(PalBuffer);
            int i = ViewPal * PalSize;

            g.Clear(Color.Black);

            for (int y = 2; y < PalBuffer.Height; y += 12) {
                for (int x = 2; x < PalBuffer.Width; x += 12) {
                    g.FillRectangle(new SolidBrush(Palette[i]), x, y, 10, 10);
                    i += 1;
                }
            }

            drawingBox.Invalidate();
        }

        private void drawingBox_Paint(object sender, PaintEventArgs e) {
            if (PalBuffer != null) {
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
                e.Graphics.FillRectangle(new SolidBrush(Palette[(ViewPal * PalSize) + SelectedFG]), 23, 2, 60, 16);

                e.Graphics.FillRectangle(Brushes.Black, 91, 4, 12, 12);

                e.Graphics.FillRectangle(Brushes.Black, 109, 0, 64, 20);
                e.Graphics.FillRectangle(new SolidBrush(Palette[(ViewPal * PalSize) + SelectedBG]), 111, 2, 60, 16);
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

        private void drawingBox_MouseUp(object sender, MouseEventArgs e) {
            DragChoice = false;
        }
    }
}
