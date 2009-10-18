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
        int selTileNum = -1;
        bool DataUpdateFlag = false;

        public delegate void mustRepaintObjectsD();
        public event mustRepaintObjectsD mustRepaintObjects;

        public Map16Editor()
        {
            InitializeComponent();
            LanguageManager.ApplyToContainer(this, "Map16Editor");
        }

        public void load(NSMBTileset t)
        {
            this.t = t;
            map16Picker1.SetTileset(t);
            tilePicker1.SetTileset(t);
        }

        private void selectTile(int tile)
        {
            this.selTile = t.Map16[tile];
            this.selTileNum = tile;
            if(t.TileBehaviors != null)
                tileBehavior.setArray(t.TileBehaviors[tile]);
            selectQuarter(selTile.topLeft);
        }

        private void selectQuarter(NSMBTileset.Map16Quarter selQuarter)
        {
            this.selQuarter = selQuarter;
            updateQuarterInfo();
        }

        private void updateQuarterInfo()
        {
            DataUpdateFlag = true;
            int val = selQuarter.TileNum;
            if (val < -1)
                val = -1;
            tileNumber.Value = val;
            tileByte.Value = selQuarter.TileByte;
            controlByte.Value = selQuarter.ControlByte;
            tilePicker1.selectTile(selQuarter.TileNum, selQuarter.secondPalette);
            secPal.Checked = selQuarter.secondPalette;
            xFlip.Checked = selQuarter.xFlip;
            yFlip.Checked = selQuarter.yFlip;
            DataUpdateFlag = false;
        }

        private void map16Picker1_TileSelected(int tile)
        {
            selectTile(tile);
            if (Control.ModifierKeys == Keys.Alt)
            {
                XFlipTile(selTile);
                repaint();
            }
            if (Control.ModifierKeys == Keys.Shift)
            {
                YFlipTile(selTile);
                repaint();
            }
        }

        private void XFlipTile(NSMBTileset.Map16Tile t)
        {
            NSMBTileset.Map16Quarter aux = t.topLeft;
            t.topLeft = t.topRight;
            t.topRight = aux;

            t.topRight.xFlip = !t.topRight.xFlip;
            t.topLeft.xFlip = !t.topLeft.xFlip;

            aux = t.bottomLeft;
            t.bottomLeft = t.bottomRight;
            t.bottomRight = aux;

            t.bottomRight.xFlip = !t.bottomRight.xFlip;
            t.bottomLeft.xFlip = !t.topLeft.xFlip;
        }
        private void YFlipTile(NSMBTileset.Map16Tile t)
        {
            NSMBTileset.Map16Quarter aux = t.topLeft;
            t.topLeft = t.bottomLeft;
            t.bottomLeft = aux;

            aux = t.topRight;
            t.topRight = t.bottomRight;
            t.bottomRight = aux;

            t.bottomRight.yFlip = !t.bottomRight.yFlip;
            t.bottomLeft.yFlip = !t.topLeft.yFlip;
            t.topRight.yFlip = !t.topRight.yFlip;
            t.topLeft.yFlip = !t.topLeft.yFlip;
        }

        private NSMBTileset.Map16Quarter createQuarter(int tile, bool secondPalette)
        {
            NSMBTileset.Map16Quarter q = new NSMBTileset.Map16Quarter(t);
            q.secondPalette = secondPalette;
            q.TileNum = tile;

            return q;

        }
        private void tilePicker1_TileSelected(int tile, bool secondPalette)
        {
            if (DataUpdateFlag)
                return;

            if (Control.ModifierKeys == Keys.Control)
            {
                if (selTile == null)
                    return;

                selTile.topLeft = createQuarter(tile, secondPalette);
                selTile.topRight = createQuarter(tile + 1, secondPalette);
                selTile.bottomLeft = createQuarter(tile + 32, secondPalette);
                selTile.bottomRight = createQuarter(tile + 33, secondPalette);

                selectTile(selTileNum);
                repaint();
            }
            else
            {
                if (selQuarter == null)
                    return;

                //if (secondPalette != ((selQuarter.ControlByte & 16) != 0))
                //    selQuarter.ControlByte ^= 16; //change byte#
                // that method doesn't always work
                // now that we found out that palettes need to be offset by the tileset number
                selQuarter.secondPalette = secondPalette;

                selQuarter.TileNum = tile;
                repaint();
            }
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
            if (DataUpdateFlag)
                return;
            if (selQuarter == null)
                return;

            selQuarter.TileNum = (int)tileNumber.Value;
            repaint();
        }

        private void repaint()
        {
            t.RenderMap16Tile(selTileNum);
            if (mustRepaintObjects != null)
                mustRepaintObjects();
            updateQuarterInfo();
        }

        private void controlByte_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag)
                return;
            if (selQuarter == null)
                return;

            selQuarter.ControlByte = (byte)controlByte.Value;
            repaint();
        }

        private void tileByte_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag)
                return;
            if (selQuarter == null)
                return;

            selQuarter.TileByte = (byte)tileByte.Value;
            repaint();
        }

        public void redrawThings()
        {
            map16Picker1.SetTileset(t);
        }

        public void reloadTileset() {
            map16Picker1.SetTileset(t);
            tilePicker1.SetTileset(t);
        }

        private void secPal_CheckedChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag || selQuarter == null)
                return;

            selQuarter.secondPalette = secPal.Checked;
            repaint();
        }

        private void xFlip_CheckedChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag || selQuarter == null)
                return;

            selQuarter.xFlip = xFlip.Checked;
            repaint();
        }

        private void yFlip_CheckedChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag || selQuarter == null)
                return;

            selQuarter.yFlip = yFlip.Checked;
            repaint();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            t.removeUnusedMap16();
            selectTile(0);
            t.repaintAllMap16();
            repaint();
        }
    }
}
