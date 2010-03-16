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
    public abstract class EditionMode
    {
        public NSMBLevel Level;
        public LevelEditorControl EdControl;

        public EditionMode(NSMBLevel Level, LevelEditorControl EdControl)
        {
            this.Level = Level;
            this.EdControl = EdControl;
        }


        public void SetDirtyFlag()
        {
            EdControl.FireSetDirtyFlag();
        }

        public void SetPanel(UserControl p)
        {
            EdControl.editor.SetPanel(p);
        }

        public abstract void MouseDown(int x, int y);
        public abstract void MouseDrag(int x, int y);
        public virtual void MouseUp() { }
        public abstract void RenderSelection(Graphics g);
        public abstract void SelectObject(Object o);
        public abstract void Refresh();

        public virtual void DeleteObject()
        {
        }

        public virtual object copy()
        {
            return null;
        }

        public virtual void paste(object contents)
        {
        }
    }
}
