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
        NitroClass ROM;
        public TilesetEditor(NitroClass ROM, ushort TilesetID, string tilesetName)
        {
            InitializeComponent();
            Text = "Editing Tileset - " + tilesetName;

            this.ROM = ROM;
            g = new NSMBGraphics(ROM);
            g.LoadTilesets(TilesetID);
            t = g.Tilesets[1];

            objectPickerControl1.Initialise(g);
            objectPickerControl1.CurrentTileset = 1;

            tilesetObjectEditor1.load(g);
            map16Editor1.load(t);
            
        }

        private void objectPickerControl1_ObjectSelected()
        {
            if (t.Objects.Length <= objectPickerControl1.SelectedObject)
                return;
            if (t.Objects[objectPickerControl1.SelectedObject] == null)
                return;

            tilesetObjectEditor1.setObject(objectPickerControl1.SelectedObject);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            t.save();
        }

        private void mustRepaintObjects()
        {
            objectPickerControl1.ReRenderAll(1);
            tilesetObjectEditor1.redrawThings();
            map16Editor1.redrawThings();
        }
    }
}
