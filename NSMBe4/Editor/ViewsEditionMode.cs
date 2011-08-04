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
using System.Windows.Forms;
using System.Drawing;

namespace NSMBe4
{
    public class ViewsEditionMode:EditionMode
    {
        private NSMBView v;
        private int DragXOff, DragYOff;
        private bool ResizeMode, CloneMode, EdVi;
        private ViewEditor ve;

        private List<NSMBView> l;

        public ViewsEditionMode(NSMBLevel Level, LevelEditorControl EdControl, bool EdVi)
            : base(Level, EdControl)
        {
            this.EdVi = EdVi;
            if (EdVi)
                l = Level.Views;
            else
                l = Level.Zones;
            ve = new ViewEditor(EdControl, l, EdVi);
        }

        
        public override void MouseDown(int x, int y)
        {
            v = null;
            foreach(NSMBView vv in l)
                if (vv.X <= x && vv.X + vv.Width >= x)
                    if (vv.Y <= y && vv.Y + vv.Height >= y)
                        v = vv;

            EdControl.repaint();
            UpdatePanel();
            if (v == null)
                return;

            ResizeMode = Control.ModifierKeys == Keys.Shift;
            CloneMode = Control.ModifierKeys == Keys.Control;
            if (ResizeMode) {
                DragXOff = (v.X + v.Width) - x;
                DragYOff = (v.Y + v.Height) - y;
            } else {
                DragXOff = x - v.X;
                DragYOff = y - v.Y;
            }
        }

        public override void MouseDrag(int x, int y)
        {
            if (v == null)
                return;

            if (CloneMode)
            {
                v = new NSMBView(v);
                EdControl.UndoManager.Do(new AddViewAction(v));
                CloneMode = false;
            }

            int step = 1;
            if ((Control.ModifierKeys & Keys.Alt) != 0)
                step = 8;
            int nx, ny;
            if (ResizeMode) {
                int xmin = 16;
                int ymin = 16;
                if (EdVi)
                {
                    xmin = 16 * 16;
                    ymin = 12 * 16;
                }

                nx = Math.Max(xmin, (x + DragXOff - v.X) / step * step);
                ny = Math.Max(ymin, (y + DragYOff - v.Y) / step * step);
                if (v.Width != nx || v.Height != ny)
                    EdControl.UndoManager.Do(new SizeViewAction(v, nx, ny));
            } else {
                nx = Math.Max(0, (x - DragXOff) / step * step);
                ny = Math.Max(0, (y - DragYOff) / step * step);
                if (v.X != nx || v.Y != ny)
                    EdControl.UndoManager.Do(new MoveViewAction(v, nx, ny));
            }
        }

        public override void RenderSelection(Graphics g)
        {
            if(v != null)
                v.renderSelected(g);
        }

        public override void SelectObject(Object o)
        {
            if (o is NSMBView || o == null)
            {
                if (o != null && (o as NSMBView).isZone != EdVi)
                    v = o as NSMBView;
                if (o == null)
                    v = null;
                EdControl.repaint();
                UpdatePanel();
            }
        }

        public override void Refresh()
        {
            if (!l.Contains(v))
                SelectObject(null);

            SetPanel(ve);
            ve.UpdateList();
            UpdatePanel();
        }

        private void UpdatePanel()
        {
            ve.SetView(v);
        }

        public override void MouseUp()
        {
            EdControl.UndoManager.merge = false;
        }

        public override string copy()
        {
            return v.ToStringClip();
        }

        public override void paste(string contents)
        {
            int idx = 0;
            NSMBView newV = NSMBView.FromString(contents.Split(':'), ref idx);
            newV.isZone = !EdVi;
            if (newV.isZone)
                newV.Number = Level.getFreeViewNumber(Level.Zones);
            else
                newV.Number = Level.getFreeViewNumber(Level.Views);
            EdControl.UndoManager.Do(new AddViewAction(newV));
        }

        public override void DeleteObject()
        {
            ve.delete();
        }
    }
}
