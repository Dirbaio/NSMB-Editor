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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NSMBe4
{
    public partial class TilesetEditor : Form
    {
        NSMBTileset t;
        NSMBGraphics g;
        int TilesetNumber;
        ushort TilesetID;
        List<string> descriptions;
        bool descExists;

        public TilesetEditor(ushort TilesetID, string tilesetName) {
            InitializeComponent();
            if (Properties.Settings.Default.mdi)
                this.MdiParent = MdiParentForm.instance;
            LanguageManager.ApplyToContainer(this, "TilesetEditor");
            Text = string.Format(LanguageManager.Get("TilesetEditor", "_TITLE"), tilesetName);

            g = new NSMBGraphics();

            this.TilesetID = TilesetID;
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

            t = g.Tilesets[TilesetNumber];
            t.beginEdit();

            objectPickerControl1.Initialise(g);
            objectPickerControl1.CurrentTileset = TilesetNumber;

            tilesetObjectEditor1.load(g, TilesetNumber);
            tilemapEditor1.load(t.map16);

            imageManager1.addImage(t.graphics);
            imageManager1.addPalette(t.palette1);
            imageManager1.addPalette(t.palette2);

            tileBehaviorPicker.init(new Bitmap[] { t.Map16Buffer }, 16);

            //FIXME
//            graphicsEditor1.SaveGraphics += new GraphicsEditor.SaveGraphicsHandler(graphicsEditor1_SaveGraphics);
            
            descExists = ROM.UserInfo.descriptions.ContainsKey(TilesetID); //Fild in there are descriptions for the tileset
            deleteDescriptions.Visible = descExists; //Make the appropriate button visible
            createDescriptions.Visible = !descExists;
            tilesetObjectEditor1.descBox.Visible = descExists; //Hide or show the description text box
            tilesetObjectEditor1.descLbl.Visible = descExists;
            if (descExists) {
                descriptions = ROM.UserInfo.descriptions[TilesetID]; //Get the descriptions
                tilesetObjectEditor1.descBox.Text = descriptions[0]; //Fill the description box with that of the first object
            }
            this.Icon = Properties.Resources.nsmbe;
        }

        private void objectPickerControl1_ObjectSelected()
        {
            int sel = objectPickerControl1.SelectedObject;
            if (t.Objects.Length <= sel)
                return;

            for (int i = 0; i <= sel; i++)
                if (t.Objects[i] == null)
                    t.Objects[i] = new NSMBTileset.ObjectDef(t);

            tilesetObjectEditor1.setObject(objectPickerControl1.SelectedObject);
            if (tilesetObjectEditor1.descBox.Visible)
                tilesetObjectEditor1.descBox.Text = descriptions[objectPickerControl1.SelectedObject];
            objectPickerControl1.ReRenderAll(TilesetNumber);
        }


        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            // auto save graphics, I always end up forgetting..
            imageManager1.saveAll();

            t.save();
            ROM.UserInfo.SaveFile();
        }

        private void mustRepaintObjects()
        {
            t.map16.reRenderAll();
            tilemapEditor1.reload();
        }

        private void TilesetEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            t.endEdit();
            g.close();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(LanguageManager.Get("TilesetEditor", "sureDelAll"), "NSMB Editor", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                t.Objects = new NSMBTileset.ObjectDef[256];
                for(int x = 0; x < t.map16.width; x++)
                    for (int y = 0; y < t.map16.height; y++)
                    {
                        t.map16.tiles[x, y].tileNum = -1;
                        t.map16.tiles[x, y].hflip = false;
                        t.map16.tiles[x, y].vflip = false;
                        t.map16.tiles[x, y].palNum = 0;
                    }

                for (int i = 0; i < t.TileBehaviors.Length; i++)
                    t.TileBehaviors[i] = 0;

                mustRepaintObjects();
            }
        }

        private void exportButton_Click_1(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;
            t.ExportGFX(saveFileDialog1.FileName);
        }

        private void importButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            t.ImportGFX(openFileDialog1.FileName, false);
            mustRepaintObjects();
        }

        private void exportTilesetButton_Click(object sender, EventArgs e)
        {
            if (saveFileDialog2.ShowDialog() != DialogResult.OK)
                return;

            t.exportTileset(saveFileDialog2.FileName);
        }

        private void importTilesetButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog() != DialogResult.OK)
                return;

            t.importTileset(openFileDialog2.FileName);
            mustRepaintObjects();
        }

        private void createDescriptions_Click(object sender, EventArgs e)
        {
            ROM.UserInfo.createDescriptions(TilesetID);
            descriptions = ROM.UserInfo.descriptions[TilesetID];
            t.UseNotes = true;
            t.ObjNotes = descriptions.ToArray();
            descExists = true;
            createDescriptions.Visible = false;
            deleteDescriptions.Visible = true;
            tilesetObjectEditor1.descBox.Visible = true;
            tilesetObjectEditor1.descBox.Text = "";
            tilesetObjectEditor1.descLbl.Visible = true;
        }

        private void tilesetObjectEditor1_DescriptionChanged()
        {
            int num = objectPickerControl1.SelectedObject;
            descriptions[num] = num.ToString() + "=" + tilesetObjectEditor1.descBox.Text;
            t.ObjNotes[num] = tilesetObjectEditor1.descBox.Text;
            ROM.UserInfo.descriptions[TilesetID][num] = tilesetObjectEditor1.descBox.Text;
            objectPickerControl1.Invalidate(true);
        }

        private void deleteDescriptions_Click(object sender, EventArgs e)
        {
            if (!descExists) return;
            if (MessageBox.Show("Are you sure you want to delete all descriptions for this tileset?", "Delete descriptions?", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                return;
            ROM.UserInfo.descriptions.Remove(TilesetID);
            createDescriptions.Visible = true;
            deleteDescriptions.Visible = false;
            tilesetObjectEditor1.descBox.Visible = false;
            tilesetObjectEditor1.descLbl.Visible = false;
            if (TilesetID != 65535)
                t.UseNotes = false;
            else //Restore the original notes
                t.ObjNotes = NSMBGraphics.GetDescriptions(LanguageManager.GetList("ObjNotes"));
            descExists = false;
            objectPickerControl1.Invalidate(true);
        }

        private void setend_Click(object sender, EventArgs e)
        {
            int i = 0;
            for (; i <= objectPickerControl1.SelectedObject; i++)
            {
                if (t.Objects[i] == null)
                    t.Objects[i] = new NSMBTileset.ObjectDef(t);
            }
            for (; i <= 255; i++)
            {
                t.Objects[i] = null;
            }
            tilesetObjectEditor1.setObject(objectPickerControl1.SelectedObject);
        }

        private void TilesetEditor_Load(object sender, EventArgs e)
        {
            tilesetObjectEditor1.setObject(objectPickerControl1.SelectedObject);
        }

        byte[] tileBehavior = new byte[4];

        private void tileBehaviorPicker_TileSelected(int selTileNum, int selTilePal, int selTileWidth, int selTileHeight)
        {
            for(int i = 0; i < 4; i++)
                tileBehavior[i] = (byte)((t.TileBehaviors[selTileNum] >> (i*8)) & 0xFF);
            tileBehaviorEditor.setArray(tileBehavior);
        }

        private void tileBehaviorEditor_ValueChanged(byte[] val)
        {
            int newBehavior = 0;
            for (int i = 0; i < 4; i++)
                newBehavior |= val[i] << (i * 8);

            for(int x = 0; x < tileBehaviorPicker.selTileWidth; x++)
                for(int y = 0; y < tileBehaviorPicker.selTileHeight; y++)
                    t.TileBehaviors[tileBehaviorPicker.selTileNum + x + y*tileBehaviorPicker.bufferWidth] = newBehavior;
        }

        private void imageManager1_SomethingSaved()
        {
            mustRepaintObjects();
        }

    }
}