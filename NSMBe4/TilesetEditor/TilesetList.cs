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

namespace NSMBe4
{
    public partial class TilesetList : UserControl
    {
        public TilesetList()
        {
            InitializeComponent();

            //TODO add this shit to the language file
//            LanguageManager.ApplyToContainer(this, "TilesetList");

            // Add tilesets to list
            int index = 0;
            List<string> parsedlist = new List<string>();

            parsedlist.Add("Tileset 0 (Jyotyu)");
            parsedlist.Add("Tileset 2 (Sub Nohara)");
            foreach (string name in LanguageManager.GetList("Tilesets"))
            {
                string trimmedname = name.Trim();
                if (trimmedname == "") continue;
                parsedlist.Add(trimmedname);
                index += 1;
            }

            tilesetListBox.Items.AddRange(parsedlist.ToArray());
        }

        private ushort getSelectedID()
        {
            int id = tilesetListBox.SelectedIndex - 2;
            if (id == -2) id = 65535;
            if (id == -1) id = 65534;
            return (ushort)id;

        }
        private void editSelectedTileset()
        {
            if (tilesetListBox.SelectedItem == null)
                return;

            string name = (string)tilesetListBox.SelectedItem;
            ushort id = getSelectedID();

            try
            {
                new TilesetEditor(id, name).Show();
            }
            catch (AlreadyEditingException)
            {
                MessageBox.Show(LanguageManager.Get("Errors", "Tileset"));
            }
        }

        private void tilesetListBox_DoubleClick(object sender, EventArgs e)
        {
            editSelectedTileset();
        }

        private void editTilesetBtn_Click(object sender, EventArgs e)
        {
            editSelectedTileset();
        }

        private void importTilesetBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "NMT Files|*.nmt";
            ofd.CheckFileExists = true;

            if (ofd.ShowDialog() != DialogResult.OK) return;

            NSMBTileset t = loadTileset(getSelectedID());
            t.importTileset(ofd.FileName);
        }

        private void exportTilesetBtn_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "NMT Files|*.nmt";
            if (sfd.ShowDialog() != DialogResult.OK) return;

            NSMBTileset t = loadTileset(getSelectedID());
            t.exportTileset(sfd.FileName);
        }

        private NSMBTileset loadTileset(ushort TilesetID)
        {
            NSMBGraphics g = new NSMBGraphics();
            int TilesetNumber;

            if (TilesetID == 65535)
            {
                // load Jyotyu
                g.LoadTilesets(0);
                TilesetNumber = 0;
            }
            else if (TilesetID == 65534)
            {
                // load Nohara_sub
                g.LoadTilesets(2);
                TilesetNumber = 2;
            }
            else
            {
                // load a normal tileset
                g.LoadTilesets(TilesetID);
                TilesetNumber = 1;
            }

            return g.Tilesets[TilesetNumber];
        }
    }
}
