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

namespace NSMBe4.NSBMD
{
    public class Palette
    {
        public uint offset, size;
        public int colorCount
        {
            get { return (int)size / 2; }
        }

        public string name;
        NSBTX parent;
        public Color[] pal;

        public Palette(NSBTX p, uint offset)
        {
            parent = p;
            this.offset = offset;
        }

        public byte[] getContents()
        {
            byte[] d = new byte[size];
            parent.str.seek(offset);
            parent.str.read(d);
            return d;
        }

        public void load()
        {
            pal = new Color[size / 2];
            parent.str.seek(offset);
            for (int i = 0; i < pal.Length; i++)
            {
                pal[i] = NSMBTileset.fromRGB15(parent.str.readUShort());
            }
        }

        public Color getColor(int i)
        {
            if (i >= pal.Length)
                return Color.Pink;
            return pal[i];
        }


        public override string ToString()
        {
            return name;
        }

        public void replace(Color[] n)
        {
            pal = n;
            save();
        }

        public void save()
        {
            parent.str.seek(offset);
            parent.str.write(NSMBTileset.paletteToRawData(pal));
            parent.save();
        }

        public int getClosestColor(Color c, int palNum, int palSize)
        {
            int bestInd = 0;
            float bestDif = ImageIndexer.colorDifferenceWithoutAlpha(pal[0], c);

            for(int i = palNum; i < palNum + palSize; i++)
            {
                float d = ImageIndexer.colorDifferenceWithoutAlpha(pal[i], c);
                if (d < bestDif)
                {
                    bestDif = d;
                    bestInd = i;
                }
            }

            return bestInd - palNum;
        }
    }
}
