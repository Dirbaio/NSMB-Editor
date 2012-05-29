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
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace NSMBe4
{
    public partial class TilesetObjectEditor : UserControl
    {
        List<NSMBTileset.ObjectDefTile> selRow;
        NSMBTileset.ObjectDefTile selTile;
        NSMBTileset.ObjectDef obj;
        NSMBTileset tls;
        NSMBObject previewObject;
        public int tnum;
        public TextBox descBox;
        public Label descLbl;

        public delegate void mustRepaintObjectsD();
        public event mustRepaintObjectsD mustRepaintObjects;
        public delegate void changeDescription();
        public event changeDescription DescriptionChanged;

        public TilesetObjectEditor()
        {
            InitializeComponent();
            LanguageManager.ApplyToContainer(this, "TilesetObjectEditor");
            descBox = desc;
            descLbl = description;
        }

        public void setObject(int num)
        {
            if (tls.Objects[num] == null)
                return;
            previewObject.ObjNum = num;
            groupBox1.Visible = false;
            obj = tls.Objects[num];
            if (obj.tiles.Count == 0)
                obj.tiles.Add(new List<NSMBTileset.ObjectDefTile>());
            selTile = null;
            selRow = obj.tiles[0];
            DataUpdateFlag = true;
            objWidth.Value = obj.width;
            objHeight.Value = obj.height;
            DataUpdateFlag = false;
            groupBox1.Visible = selTile != null;
            repaint();
        }

        public void repaint()
        {
            try
            {
                previewObject.UpdateObjCache();
            }
            catch (Exception)
            {
            }
            editZone.Invalidate(true);
            previewBox.Invalidate(true);
            if (mustRepaintObjects != null)
                mustRepaintObjects();
        }

        public void load(NSMBGraphics g, int TilesetNumber)
        {
            this.tnum = TilesetNumber;
            this.tls = g.Tilesets[tnum];
            tilePicker1.init(new Bitmap[] { tls.map16.buffer }, 16);
            previewObject = new NSMBObject(0, tnum, 0, 0, 6, 6, g);
        }

        private void editZone_Paint(object sender, PaintEventArgs e)
        {
            if (tls == null) return;
            if (obj == null) return;

            Graphics g = e.Graphics;
            g.FillRectangle(Brushes.LightSteelBlue, 0, 0, editZone.Width, editZone.Height);

            int x = 16;
            int y = 0;

            foreach (List<NSMBTileset.ObjectDefTile> row in obj.tiles)
            {
                foreach (NSMBTileset.ObjectDefTile t in row)
                {
                    if (t.controlTile)
                    {
                        g.FillRectangle(Brushes.White, x, y, 15, 15);
                        g.DrawRectangle(Pens.Black, x, y, 15, 15);
                        g.DrawString(String.Format("{0:X2}", t.controlByte), NSMBGraphics.InfoFont, Brushes.Black, x, y);
                    }
                    else if (!t.emptyTile)
                    {
                        g.DrawImage(tls.Map16Buffer, x, y, Image2D.getTileRectangle(tls.Map16Buffer, 16, t.tileID), GraphicsUnit.Pixel);
                        if ((t.controlByte & 1) != 0)
                            g.DrawRectangle(Pens.Red, x, y, 15, 15);
                        if ((t.controlByte & 2) != 0)
                            g.DrawRectangle(Pens.Blue, x+1, y+1, 13, 13);
                    }
                    if (t == selTile)
                        g.DrawRectangle(Pens.White, x, y, 15, 15);
                    x += 16;
                }
                g.DrawString((y / 16) + "", NSMBGraphics.InfoFont, Brushes.White, 0, y);
                if(selRow == row && selTile == null)
                    g.DrawRectangle(Pens.White, 0, y, 15, 15);

                x = 16;
                y += 16;
            }

        }

        bool DataUpdateFlag = false;

        private void insertTile(NSMBTileset.ObjectDefTile tile)
        {
            if (selRow == null)
                return;
            if (selTile == null)
                selRow.Insert(0, tile);
            else
                selRow.Insert(selRow.IndexOf(selTile) + 1, tile);

            selectTile(tile);
            repaint();
        }

        private void map16Picker1_TileSelected(int tile)
        {
            if (DataUpdateFlag)
                return;

            if (Control.ModifierKeys == Keys.Control)
            {
                NSMBTileset.ObjectDefTile nt = new NSMBTileset.ObjectDefTile(tls);
                nt.tileID = tile;
                insertTile(nt);
            }
            else if(selTile != null)
            {
                DataUpdateFlag = true;
                selTile.tileID = tile;
                map16Tile.Value = tile;
                DataUpdateFlag = false;
            }

            repaint();
        }

        private void editZone_MouseDown(object sender, MouseEventArgs e)
        {
            if (obj == null)
                return;

            int tx = e.X / 16 - 1;
            int ty = e.Y / 16;

            if(tx < -1) return;
            if(ty < 0) return;

            if (obj.tiles.Count > ty)
                if (obj.tiles[ty].Count > tx)
                {
                    selRow = obj.tiles[ty];
                    if (tx == -1)
                    {
                        selTile = null;
                        if (Control.ModifierKeys == Keys.Control)
                            obj.tiles.Remove(selRow);
                    }
                    else
                    {
                        selectTile(selRow[tx]);
                        if (Control.ModifierKeys == Keys.Control)
                        {
                            selRow.Remove(selTile);
                            selTile = null;
                        }
                        else if (Control.ModifierKeys == Keys.Shift)
                            selTile.controlByte ^= 2;
                        else if (Control.ModifierKeys == Keys.Alt)
                            selTile.controlByte ^= 1;
                    }
                    groupBox1.Visible = selTile != null;
                    repaint();
                }
        }

        private void selectTile(NSMBTileset.ObjectDefTile tile)
        {
            selTile = tile;
            DataUpdateFlag = true;
            map16Tile.Value = selTile.tileID;
            controlByte.Value = selTile.controlByte;
//            map16Picker1.selectTile(selTile.tileID);
            groupBox1.Visible = selTile != null;

            DataUpdateFlag = false;
        }

        private void map16Tile_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag)
                return;
            if (selTile == null)
                return;

            selTile.tileID = (int)map16Tile.Value;
//            map16Picker1.selectTile(selTile.tileID);
            repaint();
        }

        private void controlByte_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag)
                return;
            if (selTile == null)
                return;

            selTile.controlByte = (byte)controlByte.Value;
            repaint();
        }

        private void objWidth_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag)
                return;
            if (obj == null)
                return;
            obj.width = (int)objWidth.Value;
        }

        private void objHeight_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag)
                return;
            if (obj == null)
                return;
            obj.height = (int)objHeight.Value;
        }

        private void previewBox_Paint(object sender, PaintEventArgs e)
        {
            if (previewObject == null)
                return;

            e.Graphics.FillRectangle(Brushes.LightSteelBlue, 0, 0, previewObject.Width * 16, previewObject.Height * 16);
            previewObject.RenderPlain(e.Graphics, 0, 0);
        }

        private void previewBox_MouseDown(object sender, MouseEventArgs e)
        {
            previewBox_MouseMove(sender, e);
        }

        private void previewBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                previewObject.Width = e.X / 16 + 1;
                previewObject.Height = e.Y / 16 + 1;
                if (previewObject.Width < 1)
                    previewObject.Width = 1;
                if (previewObject.Height < 1)
                    previewObject.Height = 1;
                repaint();
            }
        }

        private void newLineButton_Click(object sender, EventArgs e)
        {
            newLine();
        }

        private void newLine()
        {
            if (selRow == null)
                return;

            obj.tiles.Insert(obj.tiles.IndexOf(selRow) + 1, new List<NSMBTileset.ObjectDefTile>());
            selRow = obj.tiles[obj.tiles.IndexOf(selRow) + 1];
            repaint();
        }

        public void redrawThings()
        {
            tilePicker1.init(new Bitmap[] { tls.map16.buffer }, 16);
        }

        private void emptyTileButton_Click(object sender, EventArgs e) {
            NSMBTileset.ObjectDefTile t = new NSMBTileset.ObjectDefTile(tls);
            t.tileID = -1;
            insertTile(t);
        }

        private void slopeControlButton_Click(object sender, EventArgs e) {
            NSMBTileset.ObjectDefTile tile = new NSMBTileset.ObjectDefTile(tls);
            tile.controlByte = 0x80;
            insertTile(tile);
        }

        private void desc_TextChanged(object sender, EventArgs e)
        {
            DescriptionChanged();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (selRow == null)
                return;
            if (selTile == null)
            {
                if (obj.tiles.Count - 1 == obj.tiles.IndexOf(selRow))
                    if (obj.tiles.IndexOf(selRow) == 0)
                        return;
                    else
                    {
                        selRow = obj.tiles[obj.tiles.IndexOf(selRow) - 1];
                        obj.tiles.RemoveAt(obj.tiles.IndexOf(selRow) + 1);
                    }
                else
                {
                    selRow = obj.tiles[obj.tiles.IndexOf(selRow) + 1];
                    obj.tiles.RemoveAt(obj.tiles.IndexOf(selRow) - 1);
                }
            }
            else
            {
                if (selRow.Count - 1 == selRow.IndexOf(selTile))
                    if (selRow.IndexOf(selTile) == 0)
                    {
                        selRow.Remove(selTile);
                            selTile=null;
                    }
                    else
                    {
                        selTile = selRow[selRow.IndexOf(selTile) - 1];
                        selRow.RemoveAt(selRow.IndexOf(selTile) + 1);
                    }
                else
                {
                    selTile = selRow[selRow.IndexOf(selTile) + 1];
                    selRow.RemoveAt(selRow.IndexOf(selTile) - 1);
                }
            }
            repaint();
        }

        private void tilePicker1_TileSelected(int selTileNum, int selTilePal, int selTileWidth, int selTileHeight)
        {
            for (int y = 0; y < selTileHeight; y++)
            {
                if (y != 0)
                    newLine();
                for(int x = 0; x < selTileWidth; x++)
                {
                    NSMBTileset.ObjectDefTile tile = new NSMBTileset.ObjectDefTile(tls);
                    tile.tileID = selTileNum + x + y * tilePicker1.bufferWidth;
                    insertTile(tile);
                }
            }
        }

    }
}

