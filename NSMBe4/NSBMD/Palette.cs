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
            parent.str.seek(offset);
            pal = n;
            save();
        }

        public void save()
        {
            parent.str.write(NSMBTileset.paletteToRawData(pal));
            parent.save();
        }

        public int getClosestColor(Color c, int palNum, int palSize)
        {
            int bestInd = 0;
            ushort bestDif = ImageIndexer.colorDifference(pal[0], c);

            for(int i = palNum; i < palNum + palSize; i++)
            {
                ushort d = ImageIndexer.colorDifference(pal[i], c);
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
