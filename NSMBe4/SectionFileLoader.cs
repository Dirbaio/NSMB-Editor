using System;
using System.Collections.Generic;
using System.Text;
using NSMBe4.DSFileSystem;
using NSMBe4.TilemapEditor;

namespace NSMBe4
{
    public static class SectionFileLoader
    {
        public static void gotSection(File f, int offs, int size)
        {
            uint magic = f.getUintAt(offs);

            if (magic == 0x504C5454) //PLTT
                new PaletteViewer(new InlineFile(f, offs + 0x18, size - 0x18, f.name)).Show();
            else if (magic == 0x43484152) //CHAR
            {
                LevelChooser.showImgMgr();
                int tileWidth = f.getUshortAt(offs + 0xA);
                if (tileWidth == 0xFFFF)
                    tileWidth = 8;
                LevelChooser.imgMgr.m.addImage(new Image2D(new InlineFile(f, offs + 0x20, size - 0x20, f.name), 8*tileWidth, true, false));
            }
            else if (magic == 0x5343524E) //SCRN
            {
                if (LevelChooser.imgMgr == null) return;
                Image2D img = LevelChooser.imgMgr.m.getSelectedImage();
                Palette[] pals = LevelChooser.imgMgr.m.getPalettes();
                if (img == null) return;
                if (pals == null) return;
                if (pals.Length == 0) return;
                InlineFile ff = new InlineFile(f, offs + 0x14, size - 0x14, f.name);
                Tilemap t = new Tilemap(ff, 32, img, pals, 0, 0);
                new TilemapEditorWindow(t).Show();
            }
            else
                Console.WriteLine(String.Format("Unknown magic: {0:X8}", magic));
        }
        public static void load(File f)
        {
            int size = f.fileSize;

            int offs = 0x10; //Asisuming always same header size.
            while (offs+8 <= size)
            {
                uint magic = f.getUintAt(offs);
                uint sectionSize = f.getUintAt(offs + 4);
                if (sectionSize == 0) break; //Some files appear to have extra 0's at the end !?
                gotSection(f, offs, (int)sectionSize);
                offs += (int) sectionSize;
            }
        }

    }
}
