using System;
using System.Collections.Generic;
using System.Text;
using NSMBe4.DSFileSystem;
using System.Drawing;

namespace NSMBe4
{
    public class FilePalette : Palette
    {
        File f;
        public FilePalette(File f)
        {
            f.beginEdit(this);
            this.f = f;
            byte[] data = f.getContents();
            data = ROM.LZ77_Decompress(data);
            ByteArrayInputStream ii = new ByteArrayInputStream(data);
            pal = new Color[data.Length / 2];
            for (int i = 0; i < pal.Length; i++)
            {
                pal[i] = NSMBTileset.fromRGB15(ii.readUShort());
            }
 
        }
        public override void save()
        {
            ByteArrayOutputStream oo = new ByteArrayOutputStream();
            for (int i = 0; i < pal.Length; i++)
                oo.writeUShort(NSMBTileset.toRGB15(pal[i]));

            f.replace(oo.getArray(), this);

        }

        public override void close()
        {
            f.endEdit(this);
        }

        public override string ToString()
        {
            return f.name;
        }
    }
}
