using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace NSMBe4 {
    public class NSMBSprite {
        public int X;
        public int Y;
        public int Type;
        public byte[] Data;
        private NSMBLevel Level;

        public NSMBSprite(NSMBLevel Level)
        {
            this.Level = Level;
        }

        public NSMBSprite(NSMBSprite s)
        {
            this.X = s.X;
            this.Y = s.Y;
            this.Type = s.Type;
            this.Level = s.Level;

            this.Data = new byte[6];
            Array.Copy(s.Data, Data, 6);
        }

        public void Render(Graphics g) {
            int RenderX = X * 16;
            int RenderY = Y * 16;

            if (Level.ValidSprites[Type]) {
                g.DrawImage(Properties.Resources.sprite, RenderX, RenderY);
            } else {
                g.DrawImage(Properties.Resources.sprite_invalid, RenderX, RenderY);
            }
            g.DrawString(Type.ToString(), NSMBGraphics.SmallInfoFont, Brushes.White, (float)RenderX, (float)RenderY);
        }
    }
}
