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
    public class ImageIndexerFast
    {
        private List<Box> boxes;
        private Dictionary<Color, int> freqTable;
        public Color[] palette;

        public ImageIndexerFast(Bitmap b, int xx, int yy)
        {
            int paletteCount = 4;
            //COMPUTE FREQUENCY TABLE

            bool haveTransparent = false;

            freqTable = new Dictionary<Color, int>();
            for (int x = 0; x < 4; x++)
                for (int y = 0; y < 4; y++)
                {
                    Color c = b.GetPixel(x+xx, y+yy);
                    if (c.A < 128)
                    {
                        haveTransparent = true;
                        continue;
                    }
                    c = Color.FromArgb(c.R, c.G, c.B);

                    if (freqTable.ContainsKey(c))
                        freqTable[c]++;
                    else
                        freqTable[c] = 1;
                }

            // NOW CREATE THE PALETTE ZONES
            Box startBox = shrinkBox(new Box(0, 0, 0, 255, 255, 255));
            boxes = new List<Box>();
            boxes.Add(startBox);

//            if(haveTransparent) paletteCount--;

            while (boxes.Count < paletteCount)
            {
                Box bo = getDominantBox();
                if (bo == null)
                    break;

                split(bo);
            }


            //NOW CREATE THE PALETTE COLORS
            palette = new Color[4];

            for (int i = 0; i < paletteCount; i++)
            {
                if (i >= boxes.Count)
                    palette[i] = boxes[0].center(); //Do not use fuchsia, it will make palettes compare bad.
                else
                    palette[i] = boxes[i].center();
            }
        }


        private void split(Box b)
        {
            byte dim = b.dominantDimensionNum(); //0, 1, 2 = r, g, b
            List<byteint> values = new List<byteint>();
            int total = 0;
            foreach (Color c in freqTable.Keys)
                if (b.inside(c))
                {
                    values.Add(new byteint(colorDim(c, dim), freqTable[c]));
                    total += freqTable[c];
                }
            values.Sort();

            if (values.Count == 0)
                Console.Out.WriteLine("iijiji");

            byte m = median(values, total);
            if (m == values[0].b)
                m++;

            //            Console.Out.Write("Split: " + b + " ");
            Box nb = new Box(b);
            nb.setDimMax(dim, (byte)(m - 1));
            b.setDimMin(dim, m);
            boxes.Add(shrinkBox(nb));
            boxes.Remove(b);
            boxes.Add(shrinkBox(b));
            //            Console.Out.WriteLine(b + " " + nb);
        }

        private byte median(List<byteint> values, int total)
        {
            int acum = 0;
            foreach (byteint val in values)
            {
                acum += val.i;
                if (acum * 2 > total)
                    return val.b;
            }
            //median  is best, not mean!
            /*
            int totalval = 0;
            foreach (byteint val in values)
            {
                totalval += val.b;
            }
            return (byte)(totalval / total);*/

            throw new Exception("Bad, bad, bad!");
        }

        private class byteint : IComparable
        {
            public byte b;
            public int i;
            public byteint(byte b, int i)
            {
                this.b = b;
                this.i = i;
            }

            public int CompareTo(object obj)
            {
                byteint bi = obj as byteint;
                return b.CompareTo(bi.b);
            }

            public static bool operator <(byteint a, byteint b)
            {
                return a.b < b.b;
            }
            public static bool operator >(byteint a, byteint b)
            {
                return a.b > b.b;
            }
        }

        private byte colorDim(Color c, byte d)
        {
            if (d == 0) return c.R;
            if (d == 1) return c.G;
            return c.B;
        }

        private Box getDominantBox()
        {
            Box best = null;
            int bestDim = 0;

            foreach (Box b in boxes)
            {
                int dim = b.dominantDimension();
                if ((dim > bestDim || best == null) && b.canSplit(freqTable))
                {
                    bestDim = dim;
                    best = b;
                }
            }
            return best;
        }

        private Box shrinkBox(Box b)
        {
            Box r = null;
            foreach (Color c in freqTable.Keys)
                if (b.inside(c))
                {
                    if (r == null)
                        r = new Box(c.R, c.G, c.B, c.R, c.G, c.B);
                    else
                        r.expand(c);
                }

            if (r == null)
                return new Box(b);

            return r;
        }

        public static float colorDifference(Color a, Color b)
        {
            if (a.A != b.A) return ushort.MaxValue;

            int res = 0;
            res += (a.R - b.R) * (a.R - b.R) / 40;
            res += (a.G - b.G) * (a.G - b.G) / 40;
            res += (a.B - b.B) * (a.B - b.B) / 40;

            if (res > float.MaxValue)
                return float.MaxValue;

            return (ushort)res;
        }

        public static float colorDifferenceWithoutAlpha(Color a, Color b)
        {
            int res = 0;
            res += (a.R - b.R) * (a.R - b.R) / 40;
            res += (a.G - b.G) * (a.G - b.G) / 40;
            res += (a.B - b.B) * (a.B - b.B) / 40;

            if (res > float.MaxValue)
                return float.MaxValue;

            return (ushort)res;
        }

        private class Box
        {
            public byte r1, r2, g1, g2, b1, b2;
            public Box(byte r1, byte g1, byte b1, byte r2, byte g2, byte b2)
            {
                this.r1 = r1;
                this.g1 = g1;
                this.b1 = b1;
                this.r2 = r2;
                this.g2 = g2;
                this.b2 = b2;
            }

            public Box(Box b)
            {
                this.r1 = b.r1;
                this.g1 = b.g1;
                this.b1 = b.b1;
                this.r2 = b.r2;
                this.g2 = b.g2;
                this.b2 = b.b2;
            }

            public bool inside(Color c)
            {
                return
                    r1 <= c.R && r2 >= c.R &&
                    g1 <= c.G && g2 >= c.G &&
                    b1 <= c.B && b2 >= c.B;
            }

            public void expand(Color c)
            {
                if (r1 > c.R) r1 = c.R;
                if (r2 < c.R) r2 = c.R;
                if (g1 > c.G) g1 = c.G;
                if (g2 < c.G) g2 = c.G;
                if (b1 > c.B) b1 = c.B;
                if (b2 < c.B) b2 = c.B;
            }

            public int dominantDimension()
            {
                int res = -1;
                if (r2 - r1 > res) res = r2 - r1;
                if (g2 - g1 > res) res = g2 - g1;
                if (b2 - b1 > res) res = b2 - b1;

                return res;
            }

            public byte dominantDimensionNum()
            {
                int d = dominantDimension();
                if (d == r2 - r1) return 0;
                if (d == g2 - g1) return 1;
                return 2;
            }

            public bool isColorIn(Color c, int dim)
            {
                if (dim == 0)
                    return c.R >= r1 && c.R <= r2;
                if (dim == 1)
                    return c.G >= g1 && c.G <= g2;
                return c.B >= b1 && c.B <= b2;
            }

            public void setDimMin(byte d, byte a)
            {
                if (d == 0)
                    r1 = a;
                else if (d == 1)
                    g1 = a;
                else
                    b1 = a;
            }
            public void setDimMax(byte d, byte a)
            {
                if (d == 0)
                    r2 = a;
                else if (d == 1)
                    g2 = a;
                else
                    b2 = a;
            }

            public bool canSplit(Dictionary<Color, int> freqTable)
            {
                if (r1 == r2 && g1 == g2 && b1 == b2) return false;

                int count = 0;
                foreach (Color c in freqTable.Keys)
                {
                    if (inside(c))
                        count++;

                    if (count <= 2)
                        return true;
                }
                return false;
            }

            public Color center()
            {
                return Color.FromArgb((r1 + r2) / 2, (g1 + g2) / 2, (b1 + b2) / 2);
            }

            public override string ToString()
            {
                return "(" + r1 + "-" + r2 + "," + g1 + "-" + g2 + "," + b1 + "-" + b2 + ")";
            }
        }
    }
}
