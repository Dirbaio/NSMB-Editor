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
            map16Editor1.reloadTileset();
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

        private void selectFileButton_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog(this);
            filenameTextBox.Text = saveFileDialog1.FileName;
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            t.ExportGFX(filenameTextBox.Text);
        }

        private void importButton_Click(object sender, EventArgs e)
        {
            t.ImportGFX(filenameTextBox.Text, palette2RadioButton.Checked);
            graphicsEditor1.load(t.Palette, false, t.RawGFXData, 256);
            mustRepaintObjects();
        }
    }
}
