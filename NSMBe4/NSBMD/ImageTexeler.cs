using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace NSMBe4.NSBMD
{
    public class ImageTexeler
    {
        Bitmap img;

        Color[][] palettes;

        int[] paletteCounts;
        int[,] paletteNumbers;
        float[,] paletteDiffs;

        public byte[] f5data, texdata;
        public Color[] finalPalette;

        public ImageTexeler(Bitmap img, int paletteMaxNum)
        {
            this.img = img;

            int tx = img.Width / 4;
            int ty = img.Height / 4;
            palettes = new Color[tx*ty][];
            paletteCounts = new int[tx*ty];
            paletteNumbers = new int[tx, ty];
            paletteDiffs = new float[tx*ty, tx*ty];

            int palNum = 0;
            for(int x = 0; x < tx; x++)
                for (int y = 0; y < ty; y++)
                {
                    ImageIndexerFast iif = new ImageIndexerFast(img, x * 4, y * 4);
                    palettes[palNum] = iif.palette;
                    paletteNumbers[x, y] = palNum;
                    paletteCounts[palNum] = 1;
                    int similar = calcPaletteDiffs(palNum);
/*                    if (similar != -1)
                    {
                        paletteCounts[palNum] = 0;
                        paletteCounts[similar]++;
                        paletteNumbers[x, y] = similar;
                    }
                    */
                    palNum++;
                }

            while (countUsedPalettes() > paletteMaxNum)
            {
                Console.Out.WriteLine(countUsedPalettes());
                int besta = -1;
                int bestb = -1;
                float bestDif = float.MaxValue;


                //Find the two most similar palettes
                for (int i = 0; i < palettes.Length; i++)
                {
                    if (paletteCounts[i] == 0) continue;
                    for (int j = 0; j < palettes.Length; j++)
                    {
                        if (i == j) continue;
                        if (paletteCounts[j] == 0) continue;

                        if (paletteDiffs[i, j] < bestDif)
                        {
                            bestDif = paletteDiffs[i, j];
                            besta = j;
                            bestb = i;
                        }
                    }
                }

                //Merge the Palettes!!!
                palettes[besta] = palMerge(palettes[besta], palettes[bestb]);
                calcPaletteDiffs(besta);
                paletteCounts[besta] += paletteCounts[bestb];
                paletteCounts[bestb] = 0;

                for (int x = 0; x < tx; x++)
                    for (int y = 0; y < ty; y++)
                        if (paletteNumbers[x, y] == bestb)
                            paletteNumbers[x, y] = besta;
            }

            

            //CREATE THE FINAL PAL
            int currNum = 0;
            finalPalette = new Color[paletteMaxNum*4];
            int[] newPalNums = new int[palettes.Length];
            for(int i = 0; i < palettes.Length; i++)
            {
                if(paletteCounts[i] != 0)
                {
                    //transparentToTheEnd(palettes[i]);
                    newPalNums[i] = currNum;
                    Array.Copy(palettes[i], 0, finalPalette, currNum*4, 4);
                    currNum++;
                }
            }

            ByteArrayOutputStream texDat = new ByteArrayOutputStream();
            ByteArrayOutputStream f5Dat = new ByteArrayOutputStream();
            for (int y = 0; y < ty; y++)
                for (int x = 0; x < tx; x++)
                {
                    //Find out if texel has transparent.

                    bool hasTransparent = false;
                    for (int yy = 0; yy < 4; yy++)
                        for (int xx = 0; xx < 4; xx++)
                        {
                            Color coll = img.GetPixel(x * 4 + xx, y * 4 + yy);
                            if (coll.A < 128)
                                hasTransparent = true;
                        }

                    //WRITE THE IMAGE DATA
                    for (int yy = 0; yy < 4; yy++)
                    {
                        byte b = 0;
                        byte pow = 1;
                        for (int xx = 0; xx < 4; xx++)
                        {
                            Color coll = img.GetPixel(x*4+xx, y*4+yy);
                            byte col;
                            if (coll.A < 128)
                            {
                                col = 3;
                            }
                            else
                            {
                                col = (byte)ImageIndexer.closest(coll, palettes[paletteNumbers[x, y]]);
                                if (col == 3) col = 2;
                            }
                            b |= (byte)(pow * col);
                            pow *= 4;
                        }
                        texDat.writeByte(b);
                    }


                    //WRITE THE FORMAT-5 SPECIFIC DATA
                    ushort dat = (ushort)(newPalNums[paletteNumbers[x, y]] * 2);
                    if(hasTransparent)
                        dat |= 2 << 14;
                    f5Dat.writeUShort(dat);
                }

            f5data = f5Dat.getArray();
            texdata = texDat.getArray();

        }

        /*
        private void transparentToTheEnd(Color[] pal)
        {
            bool transpFound = false;
            for (int i = 0; i < pal.Length; i++)
            {
                if (pal[i] == Color.Transparent)
                {
                    pal[i] = pal[pal.Length - 1];
                    transpFound = true;
                }
            }

            if (transpFound)
                pal[pal.Length - 1] = Color.Transparent;
        }
        */

        public int calcPaletteDiffs(int pal)
        {
            int mostSimilar = -1;
            float bestDiff = int.MaxValue;
            for (int i = 0; i < palettes.Length; i++)
            {
                if (paletteCounts[i] != 0)
                    paletteDiffs[pal, i] = paletteDiffs[i, pal] = 
                        palDif(palettes[pal], palettes[i]);
                if (paletteDiffs[pal, i] < bestDiff)
                {
                    bestDiff = paletteDiffs[pal, i];
                    mostSimilar = i;
                }
            }
            Console.Out.WriteLine(bestDiff);
            return -1;
        }

        public int countUsedPalettes()
        {
            int res = 0;
            for (int i = 0; i < paletteCounts.Length; i++)
                if (paletteCounts[i] != 0)
                    res++;

            return res;
        }

        public float palDif(Color[] a, Color[] b)
        {
            return palDifUni(a, b) + palDifUni(b, a);
        }

        public float palDifUni(Color[] a, Color[] b)
        {
            bool aTransp = a[3] == Color.Transparent;
            bool bTransp = b[3] == Color.Transparent;

            if (aTransp != bTransp) return float.PositiveInfinity;

            float dif = 0;
            int len = aTransp ? 3 : 4;

            bool[] sel = new bool[len];

            for (int i = 0; i < len; i++)
            {
                Color c = a[i];
                float diff = float.PositiveInfinity;
                int i2 = -1;
                for(int j = 0; j < len; j++)
                {
                    if(sel[j]) continue;
                    float diff2 = ImageIndexer.colorDifference(c, b[j]);
                    if (diff2 < diff || i2==-1)
                    {
                        i2 = j;
                        diff = diff2;
                    }
                }
                sel[i2] = true;
                dif += diff;
            }

            return dif;
        }

        public Color[] palMerge(Color[] a, Color[] b)
        {
            return a; //FIXME!!!!

            /*
            //Very ugly hack here. I put the 8 colors in a bitmap
            //and let ImageIndexer find me a good 4-color palette :P

            Bitmap bi = new Bitmap(8, 1);
            for (int i = 0; i < 4; i++)
            {
                bi.SetPixel(i, 0, a[i]);
                bi.SetPixel(i+4, 0, b[i]);
            }

            ImageIndexer ii = new ImageIndexer(bi);
            return ii.palette;*/

            //Haha, it was too slow :)
        }

        public int getClosestColor(Color c, Color[] pal)
        {
            int bestInd = 0;
            float bestDif = ImageIndexer.colorDifferenceWithoutAlpha(pal[0], c);

            for (int i = 0; i < pal.Length; i++)
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
        public int getClosestColorWithAlpha(Color c, Color[] pal)
        {
            int bestInd = 0;
            float bestDif = ImageIndexer.colorDifference(pal[0], c);

            for (int i = 0; i < pal.Length; i++)
            {
                float d = ImageIndexer.colorDifference(pal[i], c);
                if (d < bestDif)
                {
                    bestDif = d;
                    bestInd = i;
                }
            }

            return bestInd;
        }
    }
}
