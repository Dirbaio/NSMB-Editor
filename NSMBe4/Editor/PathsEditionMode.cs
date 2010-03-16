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
        List<NSMBPath> l;

        public PathsEditionMode(NSMBLevel Level, LevelEditorControl EdControl, List<NSMBPath> l)
            : base(Level, EdControl)
        {
            this.l = l;
            pe = new PathEditor(EdControl, l);
            pe.setNode(null, null);
        }

        public override void MouseDown(int x, int y)
        {
            p = null;
            n = null;
            foreach (NSMBPath pp in l)
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
                    l.Remove(p);

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

            if (!l.Contains(p) || !p.points.Contains(n))
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
