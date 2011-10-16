using System;
using System.Collections.Generic;
using System.Text;

namespace NSMBe4.TilemapEditor
{
    public abstract class Tilemap
    {
        public Tile[] tiles;

        public abstract Tile getTileAtPos();

        private ushort tileToShort(Tile t)
        {
            ushort res = 0;

            res |= (ushort)(t.tileNum & 0x3FF);
            res |= (ushort)((t.hflip ? 1 : 0) << 10);
            res |= (ushort)((t.vflip ? 1 : 0) << 11);
            res |= (ushort)((t.palNum & 0x00F) << 12);

            return res;
        }

        private Tile shortToTile(ushort u)
        {
            Tile res = new Tile();

            res.tileNum = u & 0x3FF;
            res.hflip = ((u >> 10) & 1) == 1;
            res.vflip = ((u >> 11) & 1) == 1;
            res.palNum = (u >> 12) & 0xF;

            return res;
        }

        public class Tile
        {
            public int tileNum;
            public int palNum;
            public bool hflip;
            public bool vflip;
        }
    }
}
