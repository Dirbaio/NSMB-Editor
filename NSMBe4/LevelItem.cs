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
using System.Drawing;

namespace NSMBe4
{
    public interface LevelItem
    {
        int x { get; set; }
        int y { get; set; }
        int width { get; set; }
        int height { get; set; }

        //These are the "real" object rectangle.
        //It is useful for knowing the real position and size,
        //since the real position shouldn't go out of the level space.
        //Though, the "drawn" position can. For example, see sprite End-of-level Flag.
        int rx { get; }
        int ry { get; }
        int rwidth { get; }
        int rheight { get; }

        //If it's not resizable, width.set and height.set just do nothing.
        bool isResizable { get; }

        //Objects and sprites have snap 16, the others have snap 1.
        //x, y, width, height should always be multiples of snap.
        //Setting them to something that's not multiple of snap is OK, though.
        int snap { get; }

        //Renders the object itself.
        void render(Graphics g, LevelEditorControl ed);

        //Renders the white selection box around the object.
//        void render(Graphics g, LevelEditorControl ed);

    }
}
