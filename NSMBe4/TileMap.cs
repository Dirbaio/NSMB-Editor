using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace NSMBe4
{
    public class TileMap
    {
        public int tileCount;
        public Bitmap tiles;

        public int palCount;
        public Color[][] pals;

        public int tx, ty;

        public int[,] tileNums;
        public int[,] palNums;
        public bool[,] xFlip, yFlip;

        public TileMap(Bitmap tiles, Color[][] pals, int tx, int ty)
        {

        }
    }
}
