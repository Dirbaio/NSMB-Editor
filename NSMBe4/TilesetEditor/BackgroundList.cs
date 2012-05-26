using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using NSMBe4.DSFileSystem;
using NSMBe4.TilemapEditor;

namespace NSMBe4
{
    public partial class BackgroundList : UserControl
    {
        private class BackgroundEntry
        {
            public bool topLayer;
            public int id;
            public string name;

            public BackgroundEntry(bool topLayer, int id, string name)
            {
                this.topLayer = topLayer;
                this.id = id;
                this.name = name;
            }

            public override string ToString()
            {
                return (topLayer?"TOP":"BOTTOM")+" "+name;
            }
        }


        public BackgroundList()
        {
            InitializeComponent();

            //TODO add this shit to the language file
//            LanguageManager.ApplyToContainer(this, "BackgroundList");

            int id = 0;
            List<string> list = LanguageManager.GetList("Foregrounds");
            foreach (string name in list)
            {
                if (name == list[list.Count - 1]) continue;
                string trimmedname = name.Trim();
                if (trimmedname == "") continue;
                tilesetListBox.Items.Add(new BackgroundEntry(true, id, trimmedname));
                id++;
            }
            id = 0;
            list = LanguageManager.GetList("Backgrounds");
            foreach (string name in list)
            {
                if (name == list[list.Count - 1]) continue;
                string trimmedname = name.Trim();
                if (trimmedname == "") continue;
                tilesetListBox.Items.Add(new BackgroundEntry(false, id, trimmedname));
                id++;
            }
        }

        private void editSelectedBG()
        {
            if (tilesetListBox.SelectedItem == null)
                return;

            BackgroundEntry bg = (BackgroundEntry)tilesetListBox.SelectedItem;

            ushort GFXFileID = ROM.GetFileIDFromTable(bg.id, bg.topLayer ? ROM.Data.Table_FG_NCG : ROM.Data.Table_BG_NCG);
            ushort PalFileID = ROM.GetFileIDFromTable(bg.id, bg.topLayer ? ROM.Data.Table_FG_NCL : ROM.Data.Table_BG_NCL);
            ushort LayoutFileID = ROM.GetFileIDFromTable(bg.id, bg.topLayer ? ROM.Data.Table_FG_NSC : ROM.Data.Table_BG_NSC);

            File GFXFile = ROM.FS.getFileById(GFXFileID);
            File PalFile = ROM.FS.getFileById(PalFileID);
            File LayoutFile = ROM.FS.getFileById(LayoutFileID);
            LayoutFile = new InlineFile(LayoutFile, 0, 2 * 64 * 64, LayoutFile.name, null, InlineFile.CompressionType.LZComp);

            if (GFXFile == null) return;
            if (PalFile == null) return;
            if (LayoutFile == null) return;
            //Palettes
            //BACK: A and B
            //FREE: 8 and 9

            Image2D i = new Image2D(GFXFile, 256, false);
            Palette pal1 = new FilePalette(new InlineFile(PalFile, 0, 512, PalFile.name, null, InlineFile.CompressionType.LZComp));
            Palette pal2 = new FilePalette(new InlineFile(PalFile, 512, 512, PalFile.name, null, InlineFile.CompressionType.LZComp));

            Tilemap t = new Tilemap(LayoutFile, 64, i, new Palette[] { pal1, pal2 }, bg.topLayer ? 256 : 576, bg.topLayer ? 8 : 10);
            t.render();

            new TilemapEditorWindow(t).Show();
        }

        private void tilesetListBox_DoubleClick(object sender, EventArgs e)
        {
            editSelectedBG();
        }

        private void editTilesetBtn_Click(object sender, EventArgs e)
        {
            editSelectedBG();
        }

        private void importTilesetBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "NMT Files|*.nmt";
            ofd.CheckFileExists = true;

            if (ofd.ShowDialog() != DialogResult.OK) return;


        }

        private void exportTilesetBtn_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "NMT Files|*.nmt";
            if (sfd.ShowDialog() != DialogResult.OK) return;


        }
    }
}
