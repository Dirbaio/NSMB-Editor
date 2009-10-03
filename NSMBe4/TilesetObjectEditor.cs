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
        bool couldDrawObject = true;

        public TilesetObjectEditor()
        {
            InitializeComponent();
        }

        public void setObject(int num)
        {
            previewObject.ObjNum = num;
            previewObject.UpdateObjCache();
            selTile = null;
            selRow = null;
            groupBox1.Visible = false;
            obj = tls.Objects[num];
            DataUpdateFlag = true;
            objWidth.Value = obj.width;
            objHeight.Value = obj.height;
            repaint();
        }

        public void repaint()
        {
            couldDrawObject = true;
            try
            {
                previewObject.UpdateObjCache();
            }
            catch (Exception)
            {
                couldDrawObject = false;
            }
            editZone.Invalidate(true);
            previewBox.Invalidate(true);
        }

        public void load(NSMBGraphics g)
        {
            this.tls = g.Tilesets[1];
            map16Picker1.SetTileset(tls);
            previewObject = new NSMBObject(0, 1, 0, 0, 6, 6, g);
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
#if USE_GDIPLUS
                        g.DrawImage(tls.Map16Buffer, x, y, new Rectangle(t.tileID * 16, 0, 16, 16), GraphicsUnit.Pixel);
#else
                        IntPtr hdc = g.GetHdc();
                        GDIImports.StretchBlt(hdc, x, y, 16, 16, tls.Map16BufferHDC, t.tileID * 16, 0, 16, 16, GDIImports.TernaryRasterOperations.SRCCOPY);
                        g.ReleaseHdc(hdc);
#endif
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
                NSMBTileset.ObjectDefTile nt = new NSMBTileset.ObjectDefTile();
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
            map16Picker1.selectTile(selTile.tileID);
            DataUpdateFlag = false;
        }

        private void map16Tile_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag)
                return;
            if (selTile == null)
                return;

            selTile.tileID = (int)map16Tile.Value;
            selTile.emptyTile = map16Tile.Value == -1;
            map16Picker1.selectTile(selTile.tileID);
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
            e.Graphics.FillRectangle(Brushes.LightSteelBlue, 0, 0, previewObject.Width * 16, previewObject.Height * 16);
#if USE_GDIPLUS
            previewObject.RenderPlain(e.Graphics, 0, 0);
#else
            IntPtr hdc = e.Graphics.GetHdc();
            previewObject.RenderPlain(hdc, 0, 0);
            e.Graphics.ReleaseHdc(hdc);
#endif
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
            if (selRow == null)
                return;

            obj.tiles.Insert(obj.tiles.IndexOf(selRow)+1, new List<NSMBTileset.ObjectDefTile>());
            repaint();
        }
    }
}

