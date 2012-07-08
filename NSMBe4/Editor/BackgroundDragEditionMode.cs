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

﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NSMBe4
{
    public class BackgroundDragEditionMode : EditionMode
    {
        public BackgroundDragEditionMode(NSMBLevel l, LevelEditorControl edc) : base(l, edc) { }

        int dx;
        int dy;

        public override void MouseDown(int x, int y)
        {
            dx = EdControl.bgX - x;
            dy = EdControl.bgY - y;
        }

        public override void MouseDrag(int x, int y)
        {
            EdControl.bgX = dx + x;
            EdControl.bgY = dy + y;
            EdControl.repaint();
        }

        public override void RenderSelection(System.Drawing.Graphics g)
        {
        }

        public override void SelectObject(object o)
        {
        }

        public override void Refresh()
        {
        }
    }
}
