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
using NSMBe4.DSFileSystem;

namespace NSMBe4
{
    public class EnpgImage2D : Image2D
    {
        
        public EnpgImage2D(File f)
            :base(f, 256, false, false)
        {
        }

        public override int getPixel(int x, int y)
        {
            int offs = x + y * 256;
            if (offs >= data.Length) return 0;
            return data[offs];
        }

        public override void setPixel(int x, int y, int c)
        {
            int offs = x + y * 256;

            if (offs >= data.Length) return;
            data[offs] = (byte)c;
        }

        /*
        public override int getPixel(int x, int y)
        {
            int bx = x / 32;
            int by = y / 32;

            int offs = bx + by * (width / 32);
            if (offs < 0) return 0;
            offs *= 32 * 32;
            offs += x % 32 + 32 * (y % 32);
            if (offs >= data.Length) return 0;
            return data[offs];
        }

        public override void setPixel(int x, int y, int c)
        {
            int bx = x / 32;
            int by = y / 32;

            int offs = bx + by * (width / 32) - tileOffset;
            if (offs < 0) return;

            offs *= 32 * 32;
            offs += x % 32 + 32 * (y % 32);
            if (offs >= data.Length) return;
            data[offs] = (byte)c;
        }*/
    }
}
