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

        public ImageTexeler(Bitmap img, int paletteMaxNum)
        {
            this.img = img;

            Bitmap texel = new Bitmap(4, 4);
            int tx = img.Width / 4;
            int ty = img.Height / 4;
            palettes = new Color[tx*ty][];
            paletteCounts = new int[tx*ty];
            paletteNumbers = new int[tx, ty];
            paletteDiffs = new float[tx*ty, tx*ty];

            Graphics texelGfx = Graphics.FromImage(texel);
            int palNum = 0;
            for(int x = 0; x < img.Width / 4; x++)
                for (int y = 0; y < img.Height / 4; y++)
                {
                    texelGfx.DrawImage(img, new Rectangle(0, 0, 4, 4),
                        new Rectangle(x * 4, y * 4, 4, 4), GraphicsUnit.Pixel);

                    ImageIndexer ii = new ImageIndexer(texel, 4);
                    palettes[palNum] = ii.palette;
                    paletteNumbers[x, y] = palNum;
                    paletteCounts[palNum] = 1;
                    calcPaletteDiffs(palNum);
                    palNum++;
                }

            while (countUsedPalettes() > paletteMaxNum)
            {
                int besta = -1;
                int bestb = -1;
                float bestDif = float.MaxValue;

                for(int i = 0; i < palettes.Length; i++)
                    for (int j = 0; j < palettes.Length; j++)
                    {
                        if (i == j) continue;
                        if (paletteDiffs[i, j] < bestDif)
                        {
                            bestDif = paletteDiffs[i, j];
                            besta = j;
                            bestb = i;
                        }
                    }

            }
        }

        public void calcPaletteDiffs(int pal)
        {
            for (int i = 0; i < palettes.Length; i++)
            {
                if (paletteCounts[i] != 0)
                    paletteDiffs[pal, i] = palDif(palettes[pal], palettes[i]);
            }
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
            float dif = 0;
            for (int i = 0; i < a.Length; i++)
            {
                int ind = getClosestColor(a[i], b);
                Color c = b[ind];
                dif += ImageIndexer.colorDifference(c, a[i]);
            }

            return dif;
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
    }
}
