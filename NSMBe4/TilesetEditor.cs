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
        NitroClass ROM;

        public TilesetEditor(NitroClass ROM, ushort TilesetID, string tilesetName) {
            InitializeComponent();
            LanguageManager.ApplyToContainer(this, "TilesetEditor");
            Text = string.Format(LanguageManager.Get("TilesetEditor", "_TITLE"), tilesetName);

            this.ROM = ROM;

            g = new NSMBGraphics(ROM);

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

            objectPickerControl1.Initialise(g);
            objectPickerControl1.CurrentTileset = TilesetNumber;

            tilesetObjectEditor1.load(g, TilesetNumber);
            map16Editor1.load(t);

        }

        private void objectPickerControl1_ObjectSelected()
        {
            if (t.Objects.Length <= objectPickerControl1.SelectedObject)
                return;
            if (t.Objects[objectPickerControl1.SelectedObject] == null)
            {
                t.Objects[objectPickerControl1.SelectedObject] = new NSMBTileset.ObjectDef();
            }

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
