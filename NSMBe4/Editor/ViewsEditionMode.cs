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
        private int DragStartX, DragStartY;
        private bool ResizeMode, CloneMode;
        private ViewEditor ve;

        private List<NSMBView> l;

        public ViewsEditionMode(NSMBLevel Level, LevelEditorControl EdControl, bool EdVi)
            : base(Level, EdControl)
        {
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
            DragStartX = x;
            DragStartY = y;
            
        }

        public override void MouseDrag(int x, int y)
        {
            if (v == null)
                return;

            if (CloneMode)
            {
                v = new NSMBView(v);
                v.Number = EdControl.Level.getFreeViewNumber(l);
                l.Add(v);
                CloneMode = false;
                UpdatePanel();
                ve.UpdateList();
            }

            int step = 1;
            if (Control.ModifierKeys == Keys.Alt)
                step = 8;

            bool moved = false;
            int xi = 0, yi = 0;

            if (DragStartX / step != x / step)
            {
                xi = (x / step - DragStartX / step) * step;
                DragStartX = x;
                moved = true;
            }
            if (DragStartY / step != y / step)
            {
                yi = (y / step - DragStartY / step) * step;
                DragStartY = y;
                moved = true;
            }

            if (moved)
            {
                if (ResizeMode)
                {
                    v.Width += xi;
                    v.Height += yi;
                    v.Width = v.Width - v.Width % step;
                    v.Height = v.Height - v.Height % step;
                }
                else
                {
                    v.X += xi;
                    v.Y += yi;
                    v.X = v.X - v.X % step;
                    v.Y = v.Y - v.Y % step;
                }
                if (v.X < 0) v.X = 0;
                if (v.Y < 0) v.Y = 0;
                if (v.isZone)
                {
                    if (v.Width < 16) v.Width = 16;
                    if (v.Height < 16) v.Height = 16;
                }
                else
                {
                    if (v.Width < 16 * 16) v.Width = 16 * 16;
                    if (v.Height < 12 * 16) v.Height = 12 * 16;
                }
                EdControl.repaint();
                UpdatePanel();
                SetDirtyFlag();
                UpdatePanel();
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
                v = o as NSMBView;
                EdControl.repaint();
                UpdatePanel();
            }
        }

        public override void Refresh()
        {
            if (!l.Contains(v))
                SelectObject(null);

            SetPanel(ve);
            UpdatePanel();
        }

        private void UpdatePanel()
        {
            ve.SetView(v);
        }
    }
}
