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
    public class ImageIndexer
    {
        private List<Box> boxes;
        private Dictionary<MultiColor, int> freqTable;
        public Color[][] palettes;

        public ImageIndexer(List<Bitmap> bl, bool useAlpha)
            : this(bl, 256, useAlpha)
        {
        }

        public ImageIndexer(List<Bitmap> bl)
            : this(bl, 256, true)
        {
        }

        public ImageIndexer(List<Bitmap> bl, int paletteCount, bool useAlpha)
        {
            int boxColorCount = bl.Count * 4;
            //COMPUTE FREQUENCY TABLE

            freqTable = new Dictionary<MultiColor,int>();

            //Quick check just in case...
            int width = bl[0].Width;
            int height = bl[0].Height;
            foreach(Bitmap b in bl)
            {
                if (b.Width != width || b.Height != height)
                    throw new Exception("Not all images have the same size!!");
            }


            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                {
                    MultiColor c = new MultiColor(boxColorCount);
                    for(int i = 0; i < bl.Count; i++)
                        c.setColor(i, bl[i].GetPixel(x, y));
                    c.calcHash();
                    if(!c.allTransparent())
                        if (freqTable.ContainsKey(c))
                            freqTable[c]++;
                        else
                            freqTable[c] = 1;
                }

            // NOW CREATE THE PALETTE ZONES
            Box startBox = shrinkBox(new Box(boxColorCount));
            boxes = new List<Box>();
            boxes.Add(startBox);

            while (boxes.Count < (useAlpha ? paletteCount - 1 : paletteCount))
            {
                Console.Out.WriteLine(boxes.Count);
                Box bo = getDominantBox();
                if (bo == null)
                    break;

                split(bo);
            }


            //NOW CREATE THE PALETTE COLORS
            palettes = new Color[bl.Count][];
            for (int i = 0; i < bl.Count; i++)
            {
                palettes[i] = new Color[paletteCount];
                for (int j = useAlpha ? 1 : 0; j < paletteCount; j++)
                {
                    if ((useAlpha ? j : j + 1) > boxes.Count)
                        palettes[i][j] = Color.Fuchsia;
                    else
                        palettes[i][j] = boxes[useAlpha ? j - 1 : j].center().getColor(i);
                    //                Console.Out.WriteLine(i + ": " + boxes[i] + ": "+ palette[i]);
                }
                if (useAlpha)
                    palettes[i][0] = Color.Transparent;
                
            }
            Console.Out.WriteLine("DONE");
            /*

                }*/
        }

        public static byte closest(Color c, Color[] palette)
        {
            int best = 0;
            float bestDif = colorDifference(c, palette[0]);
            for (int i = 0; i < palette.Length; i++)
            {
                float dif = colorDifference(c, palette[i]);
                if (dif < bestDif)
                {
                    bestDif = dif;
                    best = i;
                }
            }
            if (best >= 256)
                Console.Out.WriteLine("GRAAH");
            return (byte)best;
        }

        private void split(Box b)
        {
            byte dim = b.dominantDimensionNum(); //0, 1, 2 = r, g, b
            List<byteint> values = new List<byteint>();
            int total = 0;
            foreach(MultiColor c in freqTable.Keys)
                if(b.inside(c))
                {
                    values.Add(new byteint(c.data[dim], freqTable[c]));
                    total += freqTable[c];
                }
            values.Sort();

            if (values.Count == 0)
                throw new Exception("WTF?!");

            byte m = median(values, total);
            if (m == values[0].b)
                m++;

//            Console.Out.Write("Split: " + b + " ");
            Box nb = new Box(b);
            nb.setDimMax(dim, (byte)(m-1));
            b.setDimMin(dim, m);
            boxes.Add(shrinkBox(nb));
            boxes.Remove(b);
            boxes.Add(shrinkBox(b));
//            Console.Out.WriteLine(b + " " + nb);
        }

        private byte median(List<byteint> values, int total)
        {
            //Naive median algorithm
            //Binary search would be better?
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
            foreach(MultiColor c in freqTable.Keys)
                if (b.inside(c))
                {
                    if (r == null)
                        r = new Box(c);
                    else
                        r.expand(c);
                }

            if (r == null)
                return new Box(b);

            return r;
        }

        public static float colorDifference(Color a, Color b)
        {
            if (a.A != b.A) return float.MaxValue;

            float res = 0;
            res += (a.R - b.R) * (a.R - b.R) / 40;
            res += (a.G - b.G) * (a.G - b.G) / 40;
            res += (a.B - b.B) * (a.B - b.B) / 40;

            if (res > float.MaxValue)
                return float.MaxValue;

            return res;
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

        public static byte reduce(int c)
        {
            return (byte)((c >> 3) << 3);
        }

        private class MultiColor
        {
            public byte[] data;
            public bool[] transp;
            public MultiColor(int count)
            {
                data = new byte[count];
                transp = new bool[count];
            }

            public void setColor(int i, Color c)
            {
                transp[i * 3 + 0] = c.A < 128;
                transp[i * 3 + 1] = c.A < 128;
                transp[i * 3 + 2] = c.A < 128;
                if (c.A >= 128)
                {
                    data[i * 3 + 0] = reduce(c.R);
                    data[i * 3 + 1] = reduce(c.G);
                    data[i * 3 + 2] = reduce(c.B);
                }
            }

            public Color getColor(int i)
            {
                if (transp[i * 3]) return Color.Transparent;
                return Color.FromArgb(data[i * 3], data[i * 3 + 1], data[i * 3 + 2]);
            }

            public bool allTransparent()
            {
                for (int i = 0; i < transp.Length; i+=3)
                    if (!transp[i]) return false;
                return true;
            }

            private int thehash;
            public void calcHash()
            {
                unchecked
                {
                    const int p = 16777619;
                    int hash = (int)2166136261;

                    for (int i = 0; i < data.Length; i++)
                    {
                        hash = (hash ^ data[i]) * p;
                        if (transp[i]) hash++;
                    }

                    hash += hash << 13;
                    hash ^= hash >> 7;
                    hash += hash << 3;
                    hash ^= hash >> 17;
                    hash += hash << 5;

                    thehash = hash;
                }
            }

            public override int GetHashCode()
            {
                return thehash;
            }

            public override bool Equals(object obj)
            {
                if (obj is MultiColor)
                {
                    MultiColor c = obj as MultiColor;
                    if(data.Length != c.data.Length)
                        return false;

                    for (int i = 0; i < data.Length; i++)
                    {
                        if (data[i] != c.data[i]) return false;
                        if (transp[i] != c.transp[i]) return false;
                    }
                    return true;
                }
                else return false;
            }
        }

        private class Box
        {
            public byte[] min, max;
            private bool splittable = false;
            private bool splittablecached = false;
            public Box(int count)
            {
                min = new byte[count];
                max = new byte[count];
                for (int i = 0; i < count; i++)
                {
                    min[i] = 0;
                    max[i] = 255;
                }
            }

            public Box(Box b)
            {
                this.min = (byte[])b.min.Clone();
                this.max = (byte[])b.max.Clone();
            }

            public Box(MultiColor c)
            {
                //FIXME!!!!! NOW!!!
                //Transparent colors are made into black...
                this.min = (byte[])c.data.Clone();
                this.max = (byte[])c.data.Clone();
            }

            public bool inside(MultiColor c)
            {
                for (int i = 0; i < min.Length; i++)
                {
                    if (c.transp[i]) continue;
                    if (c.data[i] < min[i]) return false;
                    if (c.data[i] > max[i]) return false;
                }
                return true;
            }

            public void expand(MultiColor c)
            {
                for (int i = 0; i < min.Length; i++)
                {
                    if (c.transp[i]) continue;
                    if (min[i] > c.data[i]) min[i] = c.data[i];
                    if (max[i] < c.data[i]) max[i] = c.data[i];
                }
                splittablecached = false;
            }

            public int dominantDimension()
            {
                int d = dominantDimensionNum();
                return max[d] - min[d];
            }

            public byte dominantDimensionNum()
            {
                int d = -1;
                int dl = -1;
                for (int i = 0; i < min.Length; i++)
                {
                    int il = max[i] - min[i];
                    if (il > dl)
                    {
                        dl = il;
                        d = i;
                    }
                }
                return (byte)d;
            }

            public bool isColorIn(MultiColor c, int i)
            {
                return c.data[i] >= min[i] && c.data[i] <= max[i];
            }

            public void setDimMin(byte d, byte a)
            {
                min[d] = a;
                splittablecached = false;
            }
            public void setDimMax(byte d, byte a)
            {
                max[d] = a;
                splittablecached = false;
            }

            public bool canSplit(Dictionary<MultiColor, int> freqTable)
            {
                if (splittablecached) return splittable;
                else
                {
                    splittablecached = true;
                    splittable = canSplit2(freqTable);
                    return splittable;
                }
            }
            public bool canSplit2(Dictionary<MultiColor, int> freqTable)
            {
                int count = 0;
                foreach (MultiColor c in freqTable.Keys)
                {
                    if (inside(c))
                        count++;

                    if (count >= 2)
                        return true;
                }
                return false;
            }

            public MultiColor center()
            {
                MultiColor res = new MultiColor(min.Length);
                for (int i = 0; i < min.Length; i++)
                    res.data[i] = (byte)((min[i] + max[i]) / 2);
                return res;
            }

            public override string ToString()
            {
                return "shit";
//                return "("+r1+"-"+r2+","+g1+"-"+g2+","+b1+"-"+b2+")";
            }
        }


        public static Color[] createPaletteForImage(Bitmap b)
        {
            return createPaletteForImage(b, 256);
        }

        public static Color[] createPaletteForImage(Bitmap b, int palLen)
        {
            List<Bitmap> bl = new List<Bitmap>();
            bl.Add(b);

            ImageIndexer i = new ImageIndexer(bl, palLen, true);

            return i.palettes[0];
        }

        public static void previewPalettedBitmap(Bitmap b, Color[] palette)
        {
            for(int x = 0; x < b.Width; x++)
                for (int y = 0; y < b.Height; y++)
                {
                    Color c = b.GetPixel(x, y);
                    if (c.A < 128) c = Color.Transparent;
                    else c = palette[closest(c, palette)];
                    b.SetPixel(x, y, c);
                }
        }
        public static byte[] indexImageWithPalette(Bitmap b, Color[] palette)
        {
            Dictionary<Color, byte> paletteTable = new Dictionary<Color, byte>();
            //NOW MAP ORIGINAL COLORS TO PALETTE ENTRIES
            for (int x = 0; x < b.Width; x++)
                for (int y = 0; y < b.Height; y++)
                {
                    Color c = b.GetPixel(x, y);
                    if (c.A == 0) continue;
                        
                    c = Color.FromArgb(c.R, c.G, c.B);
                    paletteTable[c] = closest(c, palette);
                }

            paletteTable[Color.Transparent] = 0;

            
            //NOW INDEX THE IMAGE

            byte[] palettedImage = new byte[b.Width * b.Height];
            int tileCount = b.Width * b.Height / 64;
            int tileWidth = b.Width / 8;

            for (int t = 0; t < tileCount; t++)
                for (int y = 0; y < 8; y++)
                    for (int x = 0; x < 8; x++)
                    {
                        int tx = (t % tileWidth) * 8;
                        int ty = (int)(t / tileWidth) * 8;
                        Color c = b.GetPixel(tx + x, ty + y);
                        if (c.A != 0)
                        {
                            c = Color.FromArgb(c.R, c.G, c.B);

                            palettedImage[t * 64 + y * 8 + x] =
                                paletteTable[c];
                        }
                        else
                            palettedImage[t * 64 + y * 8 + x] = 0;
                    }

            return palettedImage;
        }
    }
}
