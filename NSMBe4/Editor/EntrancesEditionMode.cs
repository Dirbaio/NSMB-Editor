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
    public class EntrancesEditionMode:EditionMode
    {
        NSMBEntrance e = null;
        EntranceEditor ed;
        int DragXOff, DragYOff;
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

            DragXOff = x - e.X;
            DragYOff = y - e.Y;

            CloneMode = Control.ModifierKeys == Keys.Control;
        }

        public override void MouseDrag(int x, int y)
        {
            if (e == null) return;

            if (CloneMode)
            {
                e = new NSMBEntrance(e);
                e.Number = Level.getFreeEntranceNumber();
                EdControl.UndoManager.Do(new AddEntranceAction(e));
                UpdatePanel();
                CloneMode = false;
            }

            int step = 1;
            if (Control.ModifierKeys == Keys.Shift)
                step = 8;

            int nx = Math.Max(0, (x - DragXOff) / step * step);
            int ny = Math.Max(0, (y - DragYOff) / step * step);
            if (e.X != nx || e.Y != ny)
                EdControl.UndoManager.Do(new MoveEntranceAction(e, nx, ny));
        }

        public override void Refresh()
        {
            if (!EdControl.Level.Entrances.Contains(e))
                SelectObject(null);

            SetPanel(ed);
            ed.UpdateList();
            ed.UpdateInfo();
        }

        private void UpdatePanel()
        {
            ed.SetEntrance(e);
        }

        public override void MouseUp()
        {
            EdControl.UndoManager.merge = false;
        }

        public override string copy()
        {
            return e.ToStringClip();
        }

        public override void paste(string contents)
        {
            int idx = 0;
            NSMBEntrance newE = NSMBEntrance.FromString(contents.Split(':'), ref idx);
            Rectangle viewableRect = EdControl.ViewableArea;
            newE.X = (viewableRect.Left * 16 + viewableRect.Right * 16) / 2;
            newE.Y = (viewableRect.Top * 16 + viewableRect.Bottom * 16) / 2;
            newE.Number = Level.getFreeEntranceNumber();
            EdControl.UndoManager.Do(new AddEntranceAction(newE));
        }

        public override void DeleteObject()
        {
            ed.delete();
        }
    }
}
