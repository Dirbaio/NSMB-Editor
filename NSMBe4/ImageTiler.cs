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
using System.Drawing.Imaging;

namespace NSMBe4
{
    class ImageTiler
    {
        const int tileCount = 64 * 64;

        Tile[] tiles;
        public int[,] tileMap;
        float[,] tileDiffs;
        public Bitmap tileBuffer;

        public ImageTiler(Bitmap b)
        {
            if(b.Size != new Size(512,512))
                throw new Exception("Wrong image size");

            ProgressWindow p = new ProgressWindow(LanguageManager.Get("BgImport", "Importing"));
            p.Show();

            p.SetMax(tileCount);
            tileMap = new int[64,64];
            tileDiffs = new float[tileCount, tileCount];
            tiles = new Tile[tileCount];

            //LOAD TILES

            int tileNum = 0;
            for (int xt = 0; xt < 64; xt++)
            {
                for (int yt = 0; yt < 64; yt++)
                {
                    //                    Console.Out.WriteLine("Tile " + xt + " " + yt + ", " + tileNum);
                    tiles[tileNum] = new Tile(b, xt * 8, yt * 8);
                    int eqTile = fillDiffs(tileNum);
                    if (eqTile == -1)
                    {
                        eqTile = tileNum;
                        tileNum++;
                    }
                    else
                    {
                        tiles[tileNum] = null;
                        tiles[eqTile].count++;
                    }

                    tileMap[xt, yt] = eqTile;
                    p.setValue(xt * 64 + yt);
                }
                Console.Out.WriteLine(xt);
            }

            //REDUCE TILE COUNT
            int used = countUsedTiles();
            int mustRemove = used - 320;
            if (used > 320)
            {
                p.setValue(0);
                p.SetMax(mustRemove);
            }

            while (used > 320)
            {
                //find the two most similar tiles
                int best1 = 0;
                int best2 = 0;
                float bestdif = 0;
                bool first = true;
                for(int t1 = 0; t1 < tileCount; t1++)
                    for (int t2 = 0; t2 < tileCount; t2++)
                    {
                        if (tiles[t1] == null) continue;
                        if (tiles[t2] == null) continue;
                        if (t1 == t2) continue;

                        if (first || tileDiffs[t1, t2] < bestdif)
                        {
                            best1 = t1;
                            best2 = t2;
                            bestdif = tileDiffs[t1, t2];
//                            Console.Out.WriteLine(tileDiffs[t1, t2]);
                            first = false;
                        }
                    }

//                Console.Out.WriteLine("Used: " + countUsedTiles() + ", replacing " + best2 + " with " + best1 + ", diff " + bestdif);
                tiles[best1].merge(tiles[best2]);
                fillDiffs(best1);
                //fusionate them
                for (int xt = 0; xt < 64; xt++)
                    for (int yt = 0; yt < 64; yt++)
                        if (tileMap[xt,yt] == best2)
                            tileMap[xt,yt] = best1;

                tiles[best2] = null;
                used = countUsedTiles();
                p.setValue(mustRemove - used + 320);
            }


            //DEBUG, DEBUG...
            /*
            for (int yt = 0; yt < 64; yt++)
            {
                for (int xt = 0; xt < 64; xt++)
                    Console.Out.Write(tileMap[xt, yt].ToString("X2") + " ");
                Console.Out.WriteLine();
            }

            Bitmap bb = new Bitmap(512, 512, PixelFormat.Format32bppArgb);
            for (int xt = 0; xt < 64; xt++)
                for (int yt = 0; yt < 64; yt++)
                {
                    for (int x = 0; x < 8; x++)
                        for (int y = 0; y < 8; y++)
                        {
                            Color c = tiles[tileMap[xt, yt]].data[x, y];
//                            Console.Out.WriteLine(c);
                            bb.SetPixel(xt * 8 + x, yt * 8 + y, c);
                        }
                }

            bb.Save("C:\\image2.png");
            new ImagePreviewer(bb).Show();*/

            //COMPACTIFY TILES AND MAKE THE TILE BUFFER!!!
            tileBuffer = new Bitmap(countUsedTiles() * 8, 8, PixelFormat.Format32bppArgb);
            int[] newTileNums = new int[tileCount];
            int nt = 0;
            for (int t = 0; t < tileCount; t++)
            {
                if (tiles[t] != null)
                {
                    newTileNums[t] = nt;
                    for (int x = 0; x < 8; x++)
                        for (int y = 0; y < 8; y++)
                            tileBuffer.SetPixel(x + nt * 8, y, tiles[t].data[x, y]);
                    nt++;
                }
            }
            new ImagePreviewer(tileBuffer).Show();

            for (int xt = 0; xt < 64; xt++)
                for (int yt = 0; yt < 64; yt++)
                    tileMap[xt, yt] = newTileNums[tileMap[xt, yt]];
        }


        private int countUsedTiles()
        {
            int c = 0;
            for (int i = 0; i < tileCount; i++)
                if (tiles[i] != null)
                    c++;

            return c;
        }

        /*
         * Fills the difference table of a tile (row and column)
         * if it finds a very similar tile, returns its number and stops
         * if not, returns -1
         */

        private int fillDiffs(int tile)
        {
            for (int t = 0; t < tileCount; t++)
            {
                if (t == tile) continue;
                if(tiles[t] == null) continue;
                float diff = tiles[tile].difference(tiles[t]);
                if (diff < 0.00000003)
                    return t;
                tileDiffs[tile,t] = diff;
                tileDiffs[t,tile] = diff;
//                Console.Out.WriteLine(t+" "+tile+" "+diff);
            }
            return -1;
        }

        private static float colorDifference(Color a, Color b)
        {
            if (a.A != b.A) return 10f;

            float res = 0;
            res += (float)(a.R - b.R) * (float)(a.R - b.R) / 65536f;
            res += (float)(a.G - b.G) * (float)(a.G - b.G) / 65536f;
            res += (float)(a.B - b.B) * (float)(a.B - b.B) / 65536f;

            return res;
        }

        private static float colorMatrixDiff(Color[,] a, Color[,] b)
        {
            float res = 0;
            for (int x = 0; x < a.GetLength(0); x++)
                for (int y = 0; y < a.GetLength(1); y++)
                    res += colorDifference(a[x, y], b[x, y]);

            return res / a.Length;
        }


        private static float colorMatrixBorderDiff(Color[,] a, Color[,] b)
        {
            int l = a.GetLength(0)-1;
            float res = 0;
            for (int x = 0; x < a.GetLength(0); x++)
            {
                res += colorDifference(a[x, 0], b[x, 0]);
                res += colorDifference(a[x, l], b[x, l]);
                res += colorDifference(a[0, x], b[0, x]);
                res += colorDifference(a[l, x], b[l, x]);
            }

            return res / l*2;
        }

        private static Color[,] colorMatrixReduce(Color[,] m)
        {
            Color[,] r = new Color[m.GetLength(0) / 2, m.GetLength(1) / 2];
            for(int x = 0; x < m.GetLength(0)/2; x++)
                for (int y = 0; y < m.GetLength(1) / 2; y++)
                {
                    r[x, y] = colorMean(
                        colorMean(m[x * 2    , y * 2], m[x * 2    , y * 2 + 1], 1, 1),
                        colorMean(m[x * 2 + 1, y * 2], m[x * 2 + 1, y * 2 + 1], 1, 1), 1, 1
                    );
                }
            return r;
        }
        private static int mean(int a, int b, int wa, int wb)
        {
            return (wa * a + wb * b) / (wa + wb);
        }

        public static Color colorMean(Color a, Color b, int wa, int wb)
        {
            if (a.A == 0) return b;
            if (b.A == 0) return a;

            return Color.FromArgb(255,
                                  mean(a.R, b.R, wa, wb),
                                  mean(a.G, b.G, wa, wb),
                                  mean(a.B, b.B, wa, wb));
        }

        private class Tile
        {
            public Color[,] data, d1, d2;
            public int count = 1;

            public Tile(Bitmap b, int xp, int yp)
            {
                data = new Color[8,8];
                for (int x = 0; x < 8; x++)
                    for (int y = 0; y < 8; y++)
                        data[x,y] = b.GetPixel(x + xp, y + yp);
                makeReductions();
            }

            private void makeReductions()
            {
                d1 = colorMatrixReduce(data);
                d2 = colorMatrixReduce(d1);
            }
            public float difference(Tile b)
            {
                float res = 0;
                res += colorMatrixBorderDiff(data, b.data) * 3;
                res += colorMatrixDiff(d1, b.d1);
                res += colorMatrixDiff(d2, b.d2);
                res *= count + b.count;
                return res;
            }

            public void merge(Tile b)
            {
                for (int x = 0; x < 8; x++)
                    for (int y = 0; y < 8; y++)
                        data[x, y] = colorMean(data[x, y], b.data[x, y], count, b.count);
                count += b.count;
                makeReductions();
            }
        }
    }
}
