using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace NSMBe4
{
    public partial class Map16Editor : UserControl
    {
        NSMBTileset t;
        NSMBTileset.Map16Tile selTile = null;
        NSMBTileset.Map16Quarter selQuarter = null;

        public Map16Editor()
        {
            InitializeComponent();
        }

        public void load(NSMBTileset t)
        {
            this.t = t;
            map16Picker1.SetTileset(t);
            tilePicker1.SetTileset(t);
        }

        private void selectTile(NSMBTileset.Map16Tile selTile)
        {
            this.selTile = selTile;
            selectQuarter(selTile.topLeft);
        }

        private void selectQuarter(NSMBTileset.Map16Quarter selQuarter)
        {
            this.selQuarter = selQuarter;
            updateQuarterInfo();
        }

        private void updateQuarterInfo()
        {
            tileNumber.Value = selQuarter.TileNum;
            tileByte.Value = selQuarter.TileByte;
            controlByte.Value = selQuarter.ControlByte;
            tilePicker1.selectTile(selQuarter.TileNum + ((selQuarter.ControlByte & 16) != 0? 448:0));
        }

        private void map16Picker1_TileSelected(int tile)
        {
            selectTile(t.Map16[tile]);
        }

        private void tilePicker1_TileSelected(int tile)
        {

        }

        private void editQ1_Click(object sender, EventArgs e)
        {
            if (selTile == null)
                return;
            selectQuarter(selTile.topLeft);
        }

        private void editQ3_Click(object sender, EventArgs e)
        {

            if (selTile == null)
                return;
            selectQuarter(selTile.topRight);
        }

        private void editQ2_Click(object sender, EventArgs e)
        {

            if (selTile == null)
                return;
            selectQuarter(selTile.bottomLeft);
        }

        private void editQ4_Click(object sender, EventArgs e)
        {

            if (selTile == null)
                return;
            selectQuarter(selTile.bottomRight);
        }

        private void tileNumber_ValueChanged(object sender, EventArgs e)
        {
            if (selQuarter == null)
                return;

//            selQuarter.TileNum = tileNumber.Value;
            updateQuarterInfo(); //in case things change
        }

        private void controlByte_ValueChanged(object sender, EventArgs e)
        {
            if (selQuarter == null)
                return;

            selQuarter.ControlByte = (byte)controlByte.Value;
            updateQuarterInfo(); //in case things change
        }

        private void tileByte_ValueChanged(object sender, EventArgs e)
        {
            if (selQuarter == null)
                return;

            selQuarter.TileByte = (byte)tileByte.Value;
            updateQuarterInfo(); //in case things change
        }
    }
}
