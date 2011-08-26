using System;
using System.Collections.Generic;
using System.Text;

namespace NSMBe4
{
    public class MoveAllEditionMode : EditionMode
    {
        public int minX = int.MaxValue;
        public int minY = int.MaxValue;
        public int DragXOff, DragYOff;

        public MoveAllEditionMode(NSMBLevel l, LevelEditorControl edc) : base(l, edc) { }

        public override void Refresh()
        {
            foreach (NSMBObject o in Level.Objects) {
                if (o.X < minX)
                    minX = o.X;
                if (o.Y < minY)
                    minY = o.Y;
            }
            foreach (NSMBSprite s in Level.Sprites) {
                if (s.X < minX)
                    minX = s.X;
                if (s.Y < minY)
                    minY = s.Y;
            }
            foreach (NSMBEntrance e in Level.Entrances) {
                if (e.X / 16 < minX)
                    minX = e.X / 16;
                if (e.Y / 16 < minY)
                    minY = e.Y / 16;
            }
            foreach (NSMBView v in Level.Views) {
                if (v.X / 16 < minX)
                    minX = v.X / 16;
                if (v.Y / 16 < minY)
                    minY = v.Y / 16;
            }
            foreach (NSMBView v in Level.Zones) {
                if (v.X / 16 < minX)
                    minX = v.X / 16;
                if (v.Y / 16 < minY)
                    minY = v.Y / 16;
            }
            List<NSMBPath> pl = Level.Paths;
            for (int l = 0; l < 2; l++) {
                foreach (NSMBPath p in pl) {
                    foreach (NSMBPathPoint pp in p.points) {
                        if (pp.X / 16 < minX)
                            minX = pp.X / 16;
                        if (pp.Y / 16 < minY)
                            minY = pp.Y / 16;
                    }
                }
                pl = Level.ProgressPaths;
            }
        }

        public override void MouseDown(int x, int y)
        {
            DragXOff = x;
            DragYOff = y;
        }

        public override void MouseDrag(int x, int y)
        {
            int dx = (x - DragXOff) / 16;
            int dy = (y - DragYOff) / 16;
            if (minX + dx < 0)
                dx = 0;
            if (minY + dy < 0)
                dy = 0;
            if (dx != 0 || dy != 0) {
                foreach (NSMBObject o in Level.Objects) {
                    o.X += dx;
                    o.Y += dy;
                }
                foreach (NSMBSprite s in Level.Sprites) {
                    s.X += dx;
                    s.Y += dy;
                }
                foreach (NSMBEntrance e in Level.Entrances) {
                    e.X += dx * 16;
                    e.Y += dy * 16;
                }
                foreach (NSMBView v in Level.Views) {
                    v.X += dx * 16;
                    v.Y += dy * 16;
                }
                foreach (NSMBView v in Level.Zones) {
                    v.X += dx * 16;
                    v.Y += dy * 16;
                }
                List<NSMBPath> pl = Level.Paths;
                for (int l = 0; l < 2; l++) {
                    foreach (NSMBPath p in pl) {
                        foreach (NSMBPathPoint pp in p.points) {
                            pp.X += dx * 16;
                            pp.Y += dy * 16;
                        }
                    }
                    pl = Level.ProgressPaths;
                }
                minX += dx;
                minY += dy;
                DragXOff += dx * 16;
                DragYOff += dy * 16;
                EdControl.repaint();
            }
        }

        public override void SelectObject(object o)
        {
            
        }

        public override void RenderSelection(System.Drawing.Graphics g)
        {
            
        }
    }
}
