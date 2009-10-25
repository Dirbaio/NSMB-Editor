using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace NSMBe4
{
    public class ImageIndexer
    {
        private List<Box> boxes;
        private Dictionary<Color, int> freqTable;
        private Dictionary<Color, int> paletteTable;
        private Color[] palette;

        private Bitmap b;
        private int paletteSize;
        public static Bitmap index(Bitmap b, int paletteSize)
        {
            ImageIndexer i = new ImageIndexer();
            i.b = b;
            i.paletteSize = paletteSize;
            return i.start();
        }

        private Bitmap start()
        {
            new ImagePreviewer(b).Show();
            //COMPUTE FREQUENCY TABLE

            freqTable = new Dictionary<Color,int>();
            for(int x = 0; x < b.Width; x++)
                for (int y = 0; y < b.Height; y++)
                {
                    Color c = b.GetPixel(x, y);
                    if (freqTable.ContainsKey(c))
                        freqTable[c]++;
                    else
                        freqTable[c] = 1;
                }

            // NOW CREATE THE PALETTE ZONES
            Box startBox = shrinkBox(new Box(0, 0, 0, 255, 255, 255));
            boxes = new List<Box>();
            boxes.Add(startBox);

            while (boxes.Count < paletteSize)
            {
                Box bo = getDominantBox();
                split(bo);
            }

            //NOW CREATE THE PALETTE COLORS
            palette = new Color[boxes.Count];
            for (int i = 0; i < boxes.Count; i++)
            {
                palette[i] = boxes[i].center();
                Console.Out.WriteLine(i + ": " + boxes[i] + ": "+ palette[i]);
            }

            paletteTable = new Dictionary<Color, int>();
            //NOW MAP ORIGINAL COLORS TO PALETTE ENTRIES
            foreach (Color c in freqTable.Keys)
            {
                paletteTable[c] = closest(c);
            }

            //NOW PALETTE THE IMAGE
            Bitmap res = new Bitmap(b.Width, b.Height);
            for (int x = 0; x < b.Width; x++)
                for (int y = 0; y < b.Height; y++)
                {
                    res.SetPixel(x, y, palette[paletteTable[b.GetPixel(x, y)]]);
                }

            new ImagePreviewer(res).Show();
            return res;
        }

        private int closest(Color c)
        {
            int best = 0;
            ushort bestDif = colorDifference(c, palette[0]);
            for (int i = 1; i < palette.Length; i++)
            {
                ushort dif = colorDifference(c, palette[i]);
                if (dif < bestDif)
                {
                    bestDif = dif;
                    best = i;
                }
            }
            return best;
        }

        private void split(Box b)
        {
            byte dim = b.dominantDimensionNum(); //0, 1, 2 = r, g, b
            List<byteint> values = new List<byteint>();
            int total = 0;
            foreach(Color c in freqTable.Keys)
                if(b.inside(c))
                {
                    values.Add(new byteint(colorDim(c, dim), freqTable[c]));
                    total += freqTable[c];
                }
            values.Sort();

            byte m = median(values, total);
            if (m == values[0].b)
                m++;

            Console.Out.Write("Split: " + b + " ");
            Box nb = new Box(b);
            nb.setDimMax(dim, (byte)(m-1));
            b.setDimMin(dim, m);
            boxes.Add(shrinkBox(nb));
            boxes.Remove(b);
            boxes.Add(shrinkBox(b));
            Console.Out.WriteLine(b + " " + nb);
        }

        private byte median(List<byteint> values, int total)
        {
            int acum = 0;
            foreach(byteint val in values)
            {
                acum += val.i;
                if(acum*2 > total)
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
            Box best = boxes[0];
            int bestDim = best.dominantDimension();

            foreach (Box b in boxes)
            {
                int dim = b.dominantDimension();
                if (dim > bestDim)
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
            foreach(Color c in freqTable.Keys)
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

        private ushort colorDifference(Color a, Color b)
        {
            if (a.A != b.A) return ushort.MaxValue;

            int res = 0;
            res += (a.R - b.R) * (a.R - b.R) / 40;
            res += (a.G - b.G) * (a.G - b.G) / 40;
            res += (a.B - b.B) * (a.B - b.B) / 40;
            
            if(res > ushort.MaxValue)
                return ushort.MaxValue;

            return (ushort) res;
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
                int res = 0;
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

            public Color center()
            {
                return Color.FromArgb((r1 + r2) / 2, (g1 + g2) / 2, (b1 + b2) / 2);
            }

            public override string ToString()
            {
                return "("+r1+"-"+r2+","+g1+"-"+g2+","+b1+"-"+b2+")";
            }
        }
    }
}
