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
using NSMBe4.DSFileSystem;

namespace NSMBe4
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

        public override void  save()
        {
            ByteArrayOutputStream os = new ByteArrayOutputStream();

            for (int i = 0; i < f.fileSize / 8; i++)
            {
                int x = (i % (width/2))*2;
                int y = (i / (width/2))*2;
                os.writeUShort(tileToShort(tiles[x, y]));
                os.writeUShort(tileToShort(tiles[x+1, y]));
                os.writeUShort(tileToShort(tiles[x, y+1]));
                os.writeUShort(tileToShort(tiles[x+1, y+1]));
            }

            f.replace(os.getArray(), this);
        }

        public int getMap16TileCount()
        {
            return tiles.Length / 4;
        }
    }
}
