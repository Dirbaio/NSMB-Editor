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

        public TilesetEditor(ushort TilesetID, string tilesetName) {
            InitializeComponent();
            LanguageManager.ApplyToContainer(this, "TilesetEditor");
            Text = string.Format(LanguageManager.Get("TilesetEditor", "_TITLE"), tilesetName);

            g = new NSMBGraphics();

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
            graphicsEditor1.load(t.Palette, false, t.RawGFXData, 256);

            graphicsEditor1.SaveGraphics += new GraphicsEditor.SaveGraphicsHandler(graphicsEditor1_SaveGraphics);
        }

        private void graphicsEditor1_SaveGraphics() {
            t.ResetGraphics(graphicsEditor1.GFXData);
            objectPickerControl1.ReRenderAll(TilesetNumber);
            tilesetObjectEditor1.redrawThings();
            map16Editor1.redrawThings();
        }

        private void objectPickerControl1_ObjectSelected()
        {
            if (t.Objects.Length <= objectPickerControl1.SelectedObject)
                return;
            if (t.Objects[objectPickerControl1.SelectedObject] == null)
            {
                t.Objects[objectPickerControl1.SelectedObject] = new NSMBTileset.ObjectDef(t);
            }

            tilesetObjectEditor1.setObject(objectPickerControl1.SelectedObject);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            // auto save graphics, I always end up forgetting..
            graphicsEditor1_SaveGraphics();

            t.save();
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
    }
}
