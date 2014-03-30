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
    public abstract class Palette
    {
        public Color[] pal;

        public abstract void beginEdit();
        public abstract void save();
        public abstract void endEdit();

        public int getClosestColor(Color c)
        {
            if (c.A == 0)
                return 0;

            int bestInd = 1;
            float bestDif = ImageIndexer.colorDifferenceWithoutAlpha(pal[1], c);

            for (int i = 1; i < pal.Length; i++)
            {
                float d = ImageIndexer.colorDifferenceWithoutAlpha(pal[i], c);
                if (d < bestDif)
                {
                    bestDif = d;
                    bestInd = i;
                }
            }

            return bestInd;
        }

        public Color getColorSafe(int ind)
        {
            if (ind >= pal.Length)
                return Color.Fuchsia;

            return pal[ind];
        }
    }
}
