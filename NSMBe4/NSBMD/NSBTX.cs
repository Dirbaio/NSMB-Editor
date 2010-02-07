using System;
using System.Collections.Generic;
using System.Text;
using NSMBe4.DSFileSystem;

namespace NSMBe4.NSBMD
{
    public class NSBTX
    {
        File f;
        byte[] data;
        uint texDataOffset;
        uint palDataOffset;
        uint palDefOffset;
        uint palDataSize;
        uint f5texDataOffset;
        uint f5dataOffset;

        public Texture[] textures;
        public Palette[] palettes;
        public ByteArrayInputStream str;

        public NSBTX(File f)
        {
            this.f = f;
            f.beginEdit(this);
            data = f.getContents();
            str = new ByteArrayInputStream(data);
            if (str.readUInt() == 0x37375A4C) //LZ77
            {
                byte[] ndata = new byte[data.Length-4];
                Array.Copy(data, 4, ndata, 0, ndata.Length);

                data = ROM.LZ77_Decompress(ndata);
                str = new ByteArrayInputStream(data);
            }

            //look for TEX0 block
            //ugly, but i'm lazy to implement it properly.
            bool found = false;
            while (str.available(4))
            {
                uint v = str.readUInt();
                if (v == 0x30584554) // "TEX0"
                {
                    str.setOrigin(str.getPos()-4);
                    found = true;
                    break;
                }
                else
                    str.skipback(3); //just in case its not word-aligned
            }
            str.seek(0);
            if (!found)
            {
                textures = new Texture[0];
                palettes = new Palette[0];
                return;
            }

            //Read stuff
            str.seek(0x14);
            texDataOffset = str.readUInt();
            str.seek(0x24);
            f5texDataOffset = str.readUInt();
            f5dataOffset = str.readUInt();
            str.seek(0x30);
            palDataSize = str.readUInt() * 8;
            str.seek(0x34);
            palDefOffset = str.readUInt();
            palDataOffset = str.readUInt(); 

            //Read texture definitions
            str.seek(0x3D);
            textures = new Texture[str.readByte()];
            str.skip((uint)(0xE + textures.Length * 4));

            for (int i = 0; i < textures.Length; i++)
            {
                uint offset = (uint)(8 * str.readUShort());
                ushort param = str.readUShort();
                int format = (param >> 10) & 7;
                if (format == 5)
                    offset += f5texDataOffset;
                else
                    offset += texDataOffset;
                int width = 8 << ((param >> 4) & 7);
                int height = 8 << ((param >> 7) & 7);
                bool color0 = ((param >> 13) & 1) != 0;
                str.readUInt(); // unused

                textures[i] = new Texture(this, color0, width, height, format, offset, "");
                if (format == 5)
                {
                    textures[i].f5DataOffset = f5dataOffset;
                    f5dataOffset += (uint)(width * height) / 16 * 2;
                }
            }

            for (int i = 0; i < textures.Length; i++)
                textures[i].name = str.ReadString(16);



            //Read palette definitions
            str.seek(palDefOffset+1);
            palettes = new Palette[str.readByte()];
            str.skip((uint)(0xE + palettes.Length * 4));

            for (int i = 0; i < palettes.Length; i++)
            {
                uint offset = (uint)(8 * str.readUShort() + palDataOffset);
                str.readUShort();
                palettes[i] = new Palette(this, offset);
            }

            for (int i = 0; i < palettes.Length; i++)
            {
                palettes[i].name = str.ReadString(16);
                if (i != palettes.Length - 1)
                    palettes[i].size = palettes[i + 1].offset - palettes[i].offset;
            }

            palettes[palettes.Length - 1].size = palDataOffset + palDataSize - palettes[palettes.Length - 1].offset;
            foreach (Palette p in palettes)
                p.load();

//            new ImagePreviewer(textures[0].render(palettes[0])).Show();
        }

        public void close()
        {
            f.endEdit(this);
        }

        public void save()
        {
            f.replace(data, this);
        }
    }
}
