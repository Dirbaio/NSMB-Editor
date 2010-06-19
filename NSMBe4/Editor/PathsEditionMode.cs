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
        int DragXOff, DragYOff;
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
                    if (x + NSMBPath.XOffs >= nn.X && x + NSMBPath.XOffs <= nn.X + 16)
                        if (y + NSMBPath.YOffs >= nn.Y && y + NSMBPath.YOffs <= nn.Y + 16)
                        {
                            p = pp;
                            n = nn;
                        }
                }
            }


            if (n == null)
            {
                UpdatePanel();
                EdControl.repaint();
                return;
            }

            DragXOff = x - n.X;
            DragYOff = y - n.Y;
            CloneMode = Control.ModifierKeys == Keys.Control;
            if (Control.ModifierKeys == Keys.Alt) //can't make an empty path!
            {
                if (p.points.Count > 1)
                    EdControl.UndoManager.Do(new RemovePathNodeAction(n));
                else
                    EdControl.UndoManager.Do(new RemovePathAction(p));
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
                EdControl.UndoManager.Do(new AddPathNodeAction(n, ind + 1));
                CloneMode = false;
            }

            int step = 1;
            if ((Control.ModifierKeys & Keys.Shift) > 0)
                step = 8;

            bool MovePath = (Control.ModifierKeys & (Keys.Control | Keys.Alt)) == (Keys.Control | Keys.Alt);
            int minx = 0, miny = 0;
            if (MovePath) {
                minx = n.X - p.getMinX();
                miny = n.Y - p.getMinY();
            }
            int nx = Math.Max(minx, (x - DragXOff) / step * step);
            int ny = Math.Max(miny, (y - DragYOff) / step * step);
            if (n.X != nx || n.Y != ny) {
                if (MovePath) {
                    EdControl.UndoManager.Do(new MovePathAction(n, nx, ny));
                } else
                    EdControl.UndoManager.Do(new MovePathNodeAction(n, nx, ny));
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

            if (p == null)
            {
                UpdatePanel();
                return;
            }

            if (!l.Contains(p) || !p.points.Contains(n))
            {
                p = null;
                n = null;
            }
            UpdatePanel();

        }
        public void UpdatePanel()
        {
            pe.UpdateList();
            pe.UpdateInfo();
            pe.setNode(p, n);
        }

        public override void MouseUp()
        {
            EdControl.UndoManager.merge = false;
        }

        public override void DeleteObject()
        {
            if (p != null && p.points.Count > 1)
                EdControl.UndoManager.Do(new RemovePathNodeAction(n));
            else
                EdControl.UndoManager.Do(new RemovePathAction(p));
        }

        public override object copy()
        {
            if (p == null)
                return null;
            return new NSMBPathPoint(n);
        }

        public override void paste(object contents)
        {
            if (contents is NSMBPathPoint)
                EdControl.UndoManager.Do(new AddPathNodeAction(contents as NSMBPathPoint, p.points.IndexOf(n) + 1));
        }
    }
}
