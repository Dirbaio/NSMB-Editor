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
        public int Selected;
        public int ViewPal;
        public int PalSize;
        public int PalCount;

        public PalettePicker() {
            InitializeComponent();
        }

        public void SetPalette(Color[] colours, int palsize) {
            Palette = colours;
            Selected = 0;
            PalSize = palsize;
            PalCount = colours.Length / palsize;
            SetViewPal(0);
        }

        public void SetViewPal(int idx) {
            ViewPal = idx;
            if (DisplayBuffer == null) {
                DisplayBuffer = new Bitmap(16 * 12, PalSize / 16 * 12);
            }

            Graphics g = Graphics.FromImage(DisplayBuffer);
            int i = ViewPal * PalSize;

            for (int y = 0; y < DisplayBuffer.Height; y += 12) {
                for (int x = 0; x < DisplayBuffer.Width; x += 12) {
                    g.FillRectangle(new SolidBrush(Palette[i]), x, y, 12, 12);
                    i += 1;
                }
            }

            Invalidate();
        }

        private void PalettePicker_Paint(object sender, PaintEventArgs e) {
            if (DisplayBuffer != null) {
                e.Graphics.DrawImage(DisplayBuffer, 0, 0);
            }
        }
    }
}
