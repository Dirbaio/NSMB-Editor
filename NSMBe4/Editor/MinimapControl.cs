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
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NSMBe4
{
    public partial class MinimapControl : UserControl
    {
        float scale = 0.5f;
        private NSMBLevel Level;
        private LevelEditorControl EdControl;
        private Brush UnViewableBlocksBrush;
        private bool loaded = false;

        public MinimapControl()
        {
            InitializeComponent();
        }

        public void loadMinimap(NSMBLevel Level, LevelEditorControl EdControl)
        {
            this.Level = Level;
            this.EdControl = EdControl;
            UnViewableBlocksBrush = new SolidBrush(Color.FromArgb(120, 255, 255, 255));
            loaded = true;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (!loaded) return;

            e.Graphics.ScaleTransform(scale, scale);
            e.Graphics.Clear(SystemColors.ControlDark);
            e.Graphics.FillRectangle(Brushes.DarkSlateGray, 0, 0, 512, 256);
            // Draws object, but draws with background color if it is object 0
            foreach (NSMBObject obj in Level.Objects)
                if (obj.ObjNum == 0 && obj.Tileset == 0)
                    e.Graphics.FillRectangle(Brushes.SlateGray, obj.X, obj.Y, obj.Width, obj.Height);
                else
                    e.Graphics.FillRectangle(Brushes.White, obj.X, obj.Y, obj.Width, obj.Height);

            foreach (NSMBSprite s in Level.Sprites)
                e.Graphics.FillRectangle(Brushes.Chartreuse, s.getRectB());
            foreach (NSMBView v in Level.Views)
                e.Graphics.DrawRectangle(Pens.LightSteelBlue, v.X / 16, v.Y / 16, v.Width / 16, v.Height / 16);
            foreach (NSMBView v in Level.Zones)
                e.Graphics.DrawRectangle(Pens.PaleGreen, v.X / 16, v.Y / 16, v.Width / 16, v.Height / 16);

            // Draw viewable area
            e.Graphics.FillRectangle(UnViewableBlocksBrush, EdControl.ViewableBlocks);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (!loaded) return;
            pictureBox1_MouseMove(sender, e);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!loaded) return;
            if (e.Button == MouseButtons.Left)
            {
                // Calculate new position

                int xx = (int)(e.X / scale);
                int yy = (int)(e.Y / scale);
                Rectangle va = EdControl.ViewableBlocks;
                Rectangle NewArea = new Rectangle(0, 0, va.Width, va.Height);
                NewArea.X = xx - (NewArea.Width / 2);
                NewArea.Y = yy - (NewArea.Height / 2);
                // Make sure it's within bounds
                if (NewArea.X < 0)
                {
                    NewArea.X = 0;
                }
                if (NewArea.Y < 0)
                {
                    NewArea.Y = 0;
                }
                if (NewArea.Right >= 512)
                {
                    NewArea.X = 511 - NewArea.Width;
                }
                if (NewArea.Bottom >= 256)
                {
                    NewArea.Y = 255 - NewArea.Height;
                }
                // Set it
                EdControl.ScrollEditor(NewArea.Location);
                pictureBox1.Invalidate();
                //ScrollEditor(ViewableBlocks.Location);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            return EdControl.ProcessCmdKeyHack(ref msg, keyData);
        }

        private void MinimapControl_Resize(object sender, EventArgs e)
        {
            int width = Width;
            int width2 = Height * 2;
            if (width > width2)
                width = width2;

            scale = width / 512.0f;
        }
    }
}
