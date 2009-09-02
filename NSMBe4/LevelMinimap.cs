using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NSMBe4 {
    public partial class LevelMinimap : Form {
        public LevelMinimap(NSMBLevel Level) {
            InitializeComponent();
            this.Level = Level;
            UnviewableAreaBrush = new SolidBrush(Color.FromArgb(120, Color.DarkSlateGray.R, Color.DarkSlateGray.G, Color.DarkSlateGray.B));
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e) {
            e.Graphics.Clear(Color.LightSlateGray);
            for (int ObjIdx = 0; ObjIdx < Level.Objects.Count; ObjIdx++) {
                e.Graphics.FillRectangle(Brushes.White, Level.Objects[ObjIdx].X, Level.Objects[ObjIdx].Y, Level.Objects[ObjIdx].Width, Level.Objects[ObjIdx].Height);
            }

            for (int SpriteIdx = 0; SpriteIdx < Level.Sprites.Count; SpriteIdx++) {
                e.Graphics.FillRectangle(Brushes.Chartreuse, Level.Sprites[SpriteIdx].X, Level.Sprites[SpriteIdx].Y, 1, 1);
            }

            // Draw viewable area
            e.Graphics.FillRectangle(UnviewableAreaBrush, ViewableArea);
        }

        public Rectangle ViewableArea;
        private NSMBLevel Level;
        private Brush UnviewableAreaBrush;

        public delegate void ScrollEditorDelegate(Point NewPosition);
        public event ScrollEditorDelegate ScrollEditor;

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e) {
            pictureBox1_MouseMove(sender, e);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                // Calculate new position
                Rectangle NewArea = new Rectangle(0, 0, ViewableArea.Width, ViewableArea.Height);
                NewArea.X = e.X - (NewArea.Width / 2);
                NewArea.Y = e.Y - (NewArea.Height / 2);
                // Make sure it's within bounds
                if (NewArea.X < 0) {
                    NewArea.X = 0;
                }
                if (NewArea.Y < 0) {
                    NewArea.Y = 0;
                }
                if (NewArea.Right >= 512) {
                    NewArea.X = 512 - NewArea.Width;
                }
                if (NewArea.Bottom >= 256) {
                    NewArea.Y = 256 - NewArea.Height;
                }
                // Set it
                ViewableArea = NewArea;
                pictureBox1.Invalidate();
                ScrollEditor(ViewableArea.Location);
            }
        }
    }
}
