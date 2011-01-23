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
    public partial class ColourPickerControl : UserControl {
        public int R = 0;
        public int G = 0;
        public int B = 0;

        public int Value {
            get { return (B << 10) | (G << 5) | R; }
            set { R = value & 31; G = (value >> 5) & 31; B = (value >> 10) & 31; }
        }

        public ColourPickerControl() {
            InitializeComponent();
        }

        private string _redlabel = LanguageManager.Get("ColourPicker", "Red");
        private string _greenlabel = LanguageManager.Get("ColourPicker", "Green");
        private string _bluelabel = LanguageManager.Get("ColourPicker", "Blue");
        private string _previewlabel = LanguageManager.Get("ColourPicker", "Preview");

        private void renderer_Paint(object sender, PaintEventArgs e) {
            RenderSlider(0, 2, 0, e.Graphics);
            RenderSlider(0, 30, 1, e.Graphics);
            RenderSlider(0, 58, 2, e.Graphics);

            // show a preview
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(R << 3, G << 3, B << 3)), 0, 86, 256, 16);
            e.Graphics.FillRectangle(SystemBrushes.Control, 0, 86, 1, 1);
            e.Graphics.FillRectangle(SystemBrushes.Control, 255, 86, 1, 1);
            e.Graphics.FillRectangle(SystemBrushes.Control, 0, 101, 1, 1);
            e.Graphics.FillRectangle(SystemBrushes.Control, 255, 101, 1, 1);

            TextRenderer.DrawText(e.Graphics, "Red", SystemFonts.DialogFont, new Point(0, 2), Color.White);
            TextRenderer.DrawText(e.Graphics, "Green", SystemFonts.DialogFont, new Point(0, 30), Color.White);
            TextRenderer.DrawText(e.Graphics, "Blue", SystemFonts.DialogFont, new Point(0, 58), Color.White);
            TextRenderer.DrawText(e.Graphics, "Preview", SystemFonts.DialogFont, new Point(0, 86), Color.White);
        }

        private int LastActivated = -1;

        private void renderer_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                if (e.Y >= 2 && e.Y <= 17) {
                    LastActivated = 0;
                    HandleSliderClick(0, 2, 0, e);
                } else if (e.Y >= 30 && e.Y <= 45) {
                    LastActivated = 1;
                    HandleSliderClick(0, 30, 1, e);
                } else if (e.Y >= 58 && e.Y <= 73) {
                    LastActivated = 2;
                    HandleSliderClick(0, 58, 2, e);
                } else {
                    LastActivated = -1;
                }
            }
        }

        private void renderer_MouseMove(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                if (LastActivated == 0) {
                    HandleSliderClick(0, 2, 0, e);
                } else if (LastActivated == 1) {
                    HandleSliderClick(0, 30, 1, e);
                } else if (LastActivated == 2) {
                    HandleSliderClick(0, 58, 2, e);
                }
            }
        }

        private void RenderSlider(int x, int y, int target, Graphics g) {
            int rx = x;

            for (int v = 0; v < 32; v++) {
                Color show = Color.FromArgb(((target == 0) ? v : R) << 3, ((target == 1) ? v : G) << 3, ((target == 2) ? v : B) << 3);
                g.FillRectangle(new SolidBrush(show), rx, y, 8, 16);
                rx += 8;
            }

            // rounded corners
            g.FillRectangle(SystemBrushes.Control, x, y, 1, 1);
            g.FillRectangle(SystemBrushes.Control, x + 255, y, 1, 1);
            g.FillRectangle(SystemBrushes.Control, x, y + 15, 1, 1);
            g.FillRectangle(SystemBrushes.Control, x + 255, y + 15, 1, 1);

            int selected = 0;
            if (target == 0) selected = R;
            if (target == 1) selected = G;
            if (target == 2) selected = B;

            g.DrawImage(Properties.Resources.selectedarrows, x + (selected << 3), y - 2);
        }

        private void HandleSliderClick(int x, int y, int target, MouseEventArgs e) {
            int newval = (e.X - x) / 8;
            if (newval < 0) newval = 0;
            if (newval > 31) newval = 31;

            int oldval = 0;
            if (target == 0) oldval = R;
            if (target == 1) oldval = G;
            if (target == 2) oldval = B;

            if (oldval != newval) {
                if (target == 0) R = newval;
                if (target == 1) G = newval;
                if (target == 2) B = newval;
                renderer.Invalidate();
            }
        }

        private void renderer_Click(object sender, EventArgs e)
        {

        }
    }
}
