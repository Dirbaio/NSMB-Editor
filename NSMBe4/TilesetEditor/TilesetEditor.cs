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
#if MDI
            this.MdiParent = MdiParentForm.instance;
#endif
            LanguageManager.ApplyToContainer(this, "TilesetEditor");
            Text = string.Format(LanguageManager.Get("TilesetEditor", "_TITLE"), tilesetName);

            g = new NSMBGraphics();

            this.TilesetID = TilesetID;
            if (TilesetID == 65535) {
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
            t.enableWrite();

            objectPickerControl1.Initialise(g);
            objectPickerControl1.CurrentTileset = TilesetNumber;

            tilesetObjectEditor1.load(g, TilesetNumber);
            map16Editor1.load(t);
//            graphicsEditor1.load(t.Palette, false, t.RawGFXData, 256);

            graphicsEditor1.SaveGraphics += new GraphicsEditor.SaveGraphicsHandler(graphicsEditor1_SaveGraphics);
            
            descExists = ROM.descriptions.ContainsKey(TilesetID); //Fild in there are descriptions for the tileset
            deleteDescriptions.Visible = descExists; //Make the appropriate button visible
            createDescriptions.Visible = !descExists;
            tilesetObjectEditor1.descBox.Visible = descExists; //Hide or show the description text box
            tilesetObjectEditor1.descLbl.Visible = descExists;
            if (descExists)
                descriptions = ROM.descriptions[TilesetID]; //Get the descriptions
        }

        private void graphicsEditor1_SaveGraphics() {
//            t.ResetGraphics(graphicsEditor1.GFXData);
            objectPickerControl1.ReRenderAll(TilesetNumber);
            tilesetObjectEditor1.redrawThings();
            map16Editor1.redrawThings();
        }

        private void objectPickerControl1_ObjectSelected()
        {
            if (t.Objects.Length <= objectPickerControl1.SelectedObject)
                return;
            
            tilesetObjectEditor1.setObject(objectPickerControl1.SelectedObject);
            if (tilesetObjectEditor1.descBox.Visible)
                tilesetObjectEditor1.descBox.Text = descriptions[objectPickerControl1.SelectedObject].Substring(descriptions[objectPickerControl1.SelectedObject].IndexOf('=') + 1);
            
        }


        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            // auto save graphics, I always end up forgetting..
            graphicsEditor1_SaveGraphics();

            t.save();
            //Save descriptions
            try {
                if (!descExists) return;
                System.IO.StreamReader s = new System.IO.StreamReader(ROM.DescriptionPath);
                string header = "[" + TilesetID.ToString() + "]"; //The tileset header to search for
                string newTxt = "", line = "";
                while (line != header) { //Find the header in the file
                    line = s.ReadLine();
                    newTxt += line + Environment.NewLine;
                }
                foreach (string str in descriptions) //Write the new data
                    newTxt += str + Environment.NewLine;
                while (!s.EndOfStream && !s.ReadLine().StartsWith("[")) { } //Move to the next tileset
                while (!s.EndOfStream) //Write the rest of the data
                    newTxt += s.ReadLine() + Environment.NewLine;
                s.Close();
                System.IO.StreamWriter w = new System.IO.StreamWriter(ROM.DescriptionPath);
                w.Write(newTxt);
                w.Close();
                ROM.descriptions[TilesetID] = descriptions;
            } catch (Exception ex)  {
                MessageBox.Show("Could not open description file.\n\nThe original error message was:\n" + ex.Message);
            }
        }

        private void mustRepaintObjects()
        {
            objectPickerControl1.ReRenderAll(TilesetNumber);
            tilesetObjectEditor1.redrawThings();
            map16Editor1.redrawThings();
        }

        private void TilesetEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            g.close();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(LanguageManager.Get("TilesetEditor", "sureDelAll"), "NSMB Editor", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                t.Objects = new NSMBTileset.ObjectDef[0];
                foreach (NSMBTileset.Map16Tile tile in t.Map16)
                    tile.makeEmpty();
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
            try {
                string path = ROM.DescriptionPath;
                if (!System.IO.File.Exists(path)) //Create the file if it doesn't exist
                    System.IO.File.Create(path);
                System.IO.StreamWriter s = new System.IO.StreamWriter(new System.IO.FileStream(path, System.IO.FileMode.Append, System.IO.FileAccess.Write));
                s.WriteLine("[" + TilesetID.ToString() + "]"); //Write the header
                descriptions = new List<string>();
                if (TilesetID == 65535) { //Special copying from built in description list
                    List<string> all = LanguageManager.GetList("ObjNotes");
                    int allIndex = 0;
                    for (int l = 0; l <= 255; l++) {
                        string num = l.ToString();
                        if (allIndex < all.Count - 1 && all[allIndex].StartsWith(num)) {
                            num = all[allIndex];
                            allIndex++;
                        } else
                            num += "=";
                        s.WriteLine(num);
                        descriptions.Add(num);
                    }
                } else { //Regular write to file
                    for (int l = 0; l <= 255; l++) {
                        string d = l.ToString() + "=";
                        s.WriteLine(d);
                        descriptions.Add(d);
                    }
                    t.UseNotes = true;
                    t.ObjNotes = new string[256];
                }
                ROM.descriptions.Add(TilesetID, descriptions);
                s.Close();
                descExists = true;
                createDescriptions.Visible = false;
                deleteDescriptions.Visible = true;
                tilesetObjectEditor1.descBox.Visible = true;
                tilesetObjectEditor1.descBox.Text = "";
                tilesetObjectEditor1.descLbl.Visible = true;
            } catch (Exception ex) {
                MessageBox.Show("Could not open description file.\n\nThe original error message was:\n" + ex.Message);
            }
        }

        private void tilesetObjectEditor1_DescriptionChanged()
        {
            int num = objectPickerControl1.SelectedObject;
            descriptions[num] = num.ToString() + "=" + tilesetObjectEditor1.descBox.Text;
            t.ObjNotes[num] = tilesetObjectEditor1.descBox.Text;
            objectPickerControl1.Invalidate(true);
        }

        private void deleteDescriptions_Click(object sender, EventArgs e)
        {
            try {
                if (!descExists) return;
                System.IO.StreamReader s = new System.IO.StreamReader(ROM.DescriptionPath);
                string header = "[" + TilesetID.ToString() + "]"; //The tileset header to search for
                string newTxt = "", line = "";
                while (line != header) { //Find the header in the file
                    line = s.ReadLine();
                    if (line != header)
                        newTxt += line + Environment.NewLine;
                }
                line = "";
                while (!s.EndOfStream && !line.StartsWith("[")) { line = s.ReadLine(); } //Move to the next tileset
                if (!s.EndOfStream) newTxt += line + Environment.NewLine;
                while (!s.EndOfStream) //Write the rest of the data
                    newTxt += s.ReadLine() + Environment.NewLine;
                s.Close();
                System.IO.StreamWriter w = new System.IO.StreamWriter(ROM.DescriptionPath);
                w.Write(newTxt);
                w.Close();
                ROM.descriptions.Remove(TilesetID);
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
            } catch (Exception ex) {
                MessageBox.Show("Could not open description file.\n\nThe original error message was:\n" + ex.Message);
            }
        }

        private void setend_Click(object sender, EventArgs e)
        {
            int i = 0;
            for (; i <= objectPickerControl1.SelectedObject; i++)
            {
                if (t.Objects[i] == null)
                    t.Objects[i] = new NSMBTileset.ObjectDef(t);
            }
            for (; i <= 127; i++)
            {
                t.Objects[i] = null;
            }
            tilesetObjectEditor1.setObject(objectPickerControl1.SelectedObject);
        }

        private void TilesetEditor_Load(object sender, EventArgs e)
        {
            tilesetObjectEditor1.setObject(objectPickerControl1.SelectedObject);
        }
    }
}