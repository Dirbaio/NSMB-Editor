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

        public TextInputForm textForm = new TextInputForm();

        public BackgroundList()
        {
            InitializeComponent();

            //TODO add this shit to the language file
//            LanguageManager.ApplyToContainer(this, "BackgroundList");

            if (ROM.UserInfo == null) return;
            int id = 0;
            List<string> list = ROM.UserInfo.getFullList("Foregrounds");
            foreach (string name in list)
            {
                if (name == list[list.Count - 1]) continue;
                string trimmedname = name.Trim();
                if (trimmedname == "") continue;
                tilesetListBox.Items.Add(new BackgroundEntry(true, id, trimmedname));
                id++;
            }
            id = 0;
            list = ROM.UserInfo.getFullList("Backgrounds");
            foreach (string name in list)
            {
                if (name == list[list.Count - 1]) continue;
                string trimmedname = name.Trim();
                if (trimmedname == "") continue;
                tilesetListBox.Items.Add(new BackgroundEntry(false, id, trimmedname));
                id++;
            }
        }

        ushort GFXFileID;
        ushort PalFileID;
        ushort LayoutFileID;
        File GFXFile;
        File PalFile;
        File LayoutFile;
        BackgroundEntry bg;

        private void getFiles()
        {
            bg = (BackgroundEntry)tilesetListBox.SelectedItem;

            GFXFileID = ROM.GetFileIDFromTable(bg.id, bg.topLayer ? ROM.Data.Table_FG_NCG : ROM.Data.Table_BG_NCG);
            PalFileID = ROM.GetFileIDFromTable(bg.id, bg.topLayer ? ROM.Data.Table_FG_NCL : ROM.Data.Table_BG_NCL);
            LayoutFileID = ROM.GetFileIDFromTable(bg.id, bg.topLayer ? ROM.Data.Table_FG_NSC : ROM.Data.Table_BG_NSC);

            GFXFile = ROM.FS.getFileById(GFXFileID);
            PalFile = ROM.FS.getFileById(PalFileID);
            LayoutFile = ROM.FS.getFileById(LayoutFileID);
        }

        private Tilemap getTilemap()
        {

            getFiles();
            if (GFXFile == null) return null;
            if (PalFile == null) return null;
            if (LayoutFile == null) return null;

            LayoutFile = new InlineFile(LayoutFile, 0, 2 * 64 * 64, LayoutFile.name, null, InlineFile.CompressionType.LZComp);

            Image2D i = new Image2D(GFXFile, 256, false);
            Palette pal1 = new FilePalette(new InlineFile(PalFile, 0, 512, PalFile.name, null, InlineFile.CompressionType.LZComp));
            Palette pal2 = new FilePalette(new InlineFile(PalFile, 512, 512, PalFile.name, null, InlineFile.CompressionType.LZComp));

            Tilemap t = new Tilemap(LayoutFile, 64, i, new Palette[] { pal1, pal2 }, bg.topLayer ? 256 : 576, bg.topLayer ? 8 : 10);
            return t;
        }

        private void editSelectedBG()
        {
            if (tilesetListBox.SelectedItem == null)
                return;
            Tilemap t = getTilemap();
            if (t == null) return;
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
            getFiles();

            if (GFXFile == null) return;
            if (PalFile == null) return;
            if (LayoutFile == null) return;

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "NMB Files|*.nmb";
            ofd.CheckFileExists = true;

            if (ofd.ShowDialog() != DialogResult.OK) return;

            System.IO.BinaryReader br = new System.IO.BinaryReader(
                new System.IO.FileStream(ofd.FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read));
            string header = br.ReadString();
            if (header != "NSMBe Exported Background")
            {
                MessageBox.Show(
                    LanguageManager.Get("NSMBLevel", "InvalidFile"),
                    LanguageManager.Get("NSMBLevel", "Unreadable"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            readFileContents(GFXFile, br);
            readFileContents(PalFile, br);
            readFileContents(LayoutFile, br);
            br.Close();
        }

        private void exportTilesetBtn_Click(object sender, EventArgs e)
        {
            getFiles();

            if (GFXFile == null) return;
            if (PalFile == null) return;
            if (LayoutFile == null) return;

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "NMB Files|*.nmb";
            if (sfd.ShowDialog() != DialogResult.OK) return;

            System.IO.BinaryWriter bw = new System.IO.BinaryWriter(
                new System.IO.FileStream(sfd.FileName, System.IO.FileMode.Create, System.IO.FileAccess.Write));
            bw.Write("NSMBe Exported Background");
            writeFileContents(GFXFile, bw);
            writeFileContents(PalFile, bw);
            writeFileContents(LayoutFile, bw);
            bw.Close();
        }

        private void writeFileContents(File f, System.IO.BinaryWriter bw)
        {
            bw.Write((int)f.fileSize);
            bw.Write(f.getContents());
        }

        void readFileContents(File f, System.IO.BinaryReader br)
        {
            int len = br.ReadInt32();
            byte[] data = new byte[len];
            br.Read(data, 0, len);
            f.beginEdit(this);
            f.replace(data, this);
            f.endEdit(this);
        }

        private void importPNGButton_Click(object sender, EventArgs e)
        {
            getFiles();

            if (GFXFile == null) return;
            if (PalFile == null) return;
            if (LayoutFile == null) return;

            int offs = bg.topLayer ? 256 : 576;
            int palOffs = bg.topLayer ? 8 : 10;

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "PNG Files|*.png";
            ofd.CheckFileExists = true;

            if (ofd.ShowDialog() != DialogResult.OK) return;
            string filename = ofd.FileName;

            Bitmap b = new Bitmap(filename);

            ImageTiler ti = new ImageTiler(b);
            Color[] palette = ImageIndexer.createPaletteForImage(b);

            ByteArrayOutputStream oo = new ByteArrayOutputStream();
            for (int i = 0; i < palette.Length; i++)
                oo.writeUShort(NSMBTileset.toRGB15(palette[i]));
            for (int i = 0; i < 256; i++)
                oo.writeUShort(0); 

            PalFile.beginEdit(this);
            PalFile.replace(ROM.LZ77_Compress(oo.getArray()), this);
            PalFile.endEdit(this);
            GFXFile.beginEdit(this);
            GFXFile.replace(ROM.LZ77_Compress(ImageIndexer.indexImageWithPalette(ti.tileBuffer, palette)), this);
            GFXFile.endEdit(this);
            b.Dispose();

            ByteArrayOutputStream layout = new ByteArrayOutputStream();
            for (int y = 0; y < 64; y++)
                for (int x = 0; x < 64; x++)
                    layout.writeUShort((ushort)((ti.tileMap[x, y] + offs) | (palOffs<<12)));

            LayoutFile.beginEdit(this);
            LayoutFile.replace(ROM.LZ77_Compress(layout.getArray()), this);
            LayoutFile.endEdit(this);
        }

        private void exportPNGButton_Click(object sender, EventArgs e)
        {
            Tilemap t = getTilemap();
            if (t == null) return;

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PNG Files|*.png";
            if (sfd.ShowDialog() != DialogResult.OK) return;

            t.render();
            t.buffer.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
        }

        private void RenameBtn_Click(object sender, EventArgs e)
        {
            string newName;
            BackgroundEntry bg = tilesetListBox.SelectedItem as BackgroundEntry;
            string listName = bg.topLayer ? "Foregrounds" : "Backgrounds";
            if (textForm.ShowDialog("Enter new background name:", bg.name, out newName) == DialogResult.OK)
            {
                if (newName == string.Empty)
                {
                    ROM.UserInfo.removeListItem(listName, bg.id, true);
                    tilesetListBox.Items[tilesetListBox.SelectedIndex] = new BackgroundEntry(bg.topLayer, bg.id, LanguageManager.GetList(listName)[bg.id]);
                    return;
                }
                ROM.UserInfo.setListItem(listName, bg.id, newName, true);
                tilesetListBox.Items[tilesetListBox.SelectedIndex] = new BackgroundEntry(bg.topLayer, bg.id, newName);
            }
        }
    }
}
