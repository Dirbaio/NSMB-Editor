using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace NSMBe4
{
    public class PathsEditionMode:EditionMode
    {
        NSMBPath p;
        NSMBPathPoint n;
        int DragStartX, DragStartY;
        bool CloneMode;

        PathEditor pe;

        public PathsEditionMode(NSMBLevel Level, LevelEditorControl EdControl)
            : base(Level, EdControl)
        {
            pe = new PathEditor(EdControl);
            pe.setNode(null, null);
        }

        public override void MouseDown(int x, int y)
        {
            p = null;
            n = null;
            foreach (NSMBPath pp in Level.Paths)
            {
                foreach (NSMBPathPoint nn in pp.points)
                {
                    if(x + NSMBPath.XOffs >= nn.X && x + NSMBPath.XOffs <= nn.X + 16)
                        if (y + NSMBPath.YOffs >= nn.Y && y + NSMBPath.YOffs <= nn.Y + 16)
                        {
                            p = pp;
                            n = nn;
                        }
                }
            }

            UpdatePanel();
            EdControl.repaint();

            if (n == null)
                return;

            DragStartX = x;
            DragStartY = y;
            CloneMode = Control.ModifierKeys == Keys.Control;
            if (Control.ModifierKeys == Keys.Alt) //can't make an empty path!
            {
                if (p.points.Count > 1)
                    p.points.Remove(n);
                else
                    Level.Paths.Remove(p);

                SetDirtyFlag();
                n = null;
                p = null;
            }

            UpdatePanel();
            EdControl.repaint();
        }

        public override void MouseDrag(int x, int y)
        {
            if (n == null) return;

            if (CloneMode)
            {
                int ind = p.points.IndexOf(n);
                n = new NSMBPathPoint(n);
                p.points.Insert(ind + 1, n);
                CloneMode = false;
            }

            int step = 1;
            if (Control.ModifierKeys == Keys.Shift)
                step = 8;

            bool moved = false;

            if (DragStartX / step != x / step)
            {
                n.X += (x / step - DragStartX / step) * step;
                if (n.X < 0)
                    n.X = 0;

                DragStartX = x;
                moved = true;
            }
            if (DragStartY / step != y / step)
            {
                n.Y += (y / step - DragStartY / step) * step;
                if (n.Y < 0)
                    n.Y = 0;

                DragStartY = y;
                moved = true;
            }

            if (moved)
            {
                n.X = n.X - n.X % step;
                n.Y = n.Y - n.Y % step;
                EdControl.repaint();
                UpdatePanel();
                SetDirtyFlag();
            }
        }

        public override void RenderSelection(Graphics g)
        {
            if(n != null)
                g.DrawRectangle(Pens.White, n.X + NSMBPath.XOffs, n.Y + NSMBPath.YOffs, 16, 16);
        }
        public override void SelectObject(Object o)
        {
            if (o == null)
            {
                n = null;
                o = null;
                UpdatePanel();
                EdControl.repaint();
            }
            else if (o is NSMBPathPoint)
            {
                n = o as NSMBPathPoint;
                p = n.parent;
                UpdatePanel();
                EdControl.repaint();
            }
        }
        public override void Refresh()
        {
            SetPanel(pe);

            //some checks to keep consistency...
            if(p == null)
                n = null;

            if(n == null)
                p = null;

            UpdatePanel();
            if (p == null) return;

            if (!Level.Paths.Contains(p) || !p.points.Contains(n))
            {
                p = null;
                n = null;
            }
            UpdatePanel();

        }
        public void UpdatePanel()
        {
            pe.setNode(p, n);
        }
    }
}
