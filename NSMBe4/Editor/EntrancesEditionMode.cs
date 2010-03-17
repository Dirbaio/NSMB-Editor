﻿/*
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
    public class EntrancesEditionMode:EditionMode
    {

        NSMBEntrance e = null;
        EntranceEditor ed;
        int step = 1;
        int DragStartX, DragStartY;
        bool CloneMode;

        public EntrancesEditionMode(NSMBLevel Level, LevelEditorControl EdControl)
            : base(Level, EdControl)
        {
            ed = new EntranceEditor(null, EdControl);
        }
        public override void SelectObject(Object o)
        {
            if (o is NSMBEntrance || o == null)
            {
                e = o as NSMBEntrance;
            }
            UpdatePanel();
        }

        public override void RenderSelection(Graphics g)
        {
            if(e != null)
                g.DrawRectangle(Pens.White, e.X, e.Y, 16, 16);
        }

        public override void MouseDown(int x, int y)
        {
            e = null;

            foreach(NSMBEntrance ee in EdControl.Level.Entrances)
            {
                if (x >= ee.X && x <= ee.X + 16 &&
                    y >= ee.Y && y <= ee.Y + 16)
                    e = ee;
            }

            EdControl.repaint();
            UpdatePanel();

            if (e == null) return;

            DragStartX = x;
            DragStartY = y;

            CloneMode = Control.ModifierKeys == Keys.Control;
        }

        public override void MouseDrag(int x, int y)
        {
            if (e == null) return;

            if (CloneMode)
            {
                e = new NSMBEntrance(e);
                Level.Entrances.Add(e);
                e.Number = Level.getFreeEntranceNumber();
                EdControl.repaint();
                EdControl.FireSetDirtyFlag();
                ed.UpdateList();
                UpdatePanel();
                CloneMode = false;
            }

            step = 1;
            if (Control.ModifierKeys == Keys.Shift)
                step = 8;

            bool moved = false;

            if (DragStartX/step != x/step)
            {
                e.X += (x / step - DragStartX / step) * step;
                if (e.X < 0)
                    e.X = 0;

                DragStartX = x;
                moved = true;
            }
            if (DragStartY / step != y / step)
            {
                e.Y += (y / step - DragStartY / step) * step;
                if (e.Y < 0)
                    e.Y = 0;

                DragStartY = y;
                moved = true;
            }

            if (moved)
            {
                e.X = e.X - e.X % step;
                e.Y = e.Y - e.Y % step;
                EdControl.repaint();
                UpdatePanel();
                SetDirtyFlag();
            }
        }

        public override void Refresh()
        {
            if (!EdControl.Level.Entrances.Contains(e))
                SelectObject(null);

            SetPanel(ed);
        }

        private void UpdatePanel()
        {
            ed.SetEntrance(e);
        }
    }
}