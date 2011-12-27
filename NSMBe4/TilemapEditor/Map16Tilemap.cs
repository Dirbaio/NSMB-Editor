using System;
using System.Collections.Generic;
using System.Text;
using NSMBe4.DSFileSystem;

namespace NSMBe4.TilemapEditor
{
    public class Map16Tilemap : Tilemap
    {
        public Map16Tilemap(File f, int tileWidth, Image2D i, Palette[] pals, int a, int b) :
            base(f, tileWidth, i, pals, a, b)
        { }

        protected override void load()
        {
            height = (f.fileSize / 2 + width - 1) / width;
            tiles = new Tile[width, height];

            ByteArrayInputStream b = new ByteArrayInputStream(f.getContents());

            for (int i = 0; i < f.fileSize / 8; i++)
            {
                int x = (i % (width/2))*2;
                int y = (i / (width/2))*2;
                tiles[x, y] = shortToTile(b.readUShort());
                tiles[x+1, y] = shortToTile(b.readUShort());
                tiles[x, y+1] = shortToTile(b.readUShort());
                tiles[x+1, y+1] = shortToTile(b.readUShort());
            }
        }

        public int getMap16TileCount()
        {
            return tiles.Length / 4;
        }
    }
}
