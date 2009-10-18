using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NSMBe4 {
    public partial class Map16Viewer : Form {
        public Map16Viewer(NSMBGraphics GFX) {
            InitializeComponent();
            this.GFX = GFX;
        }
        public Map16Viewer(NSMBTileset t)
        {
            InitializeComponent();
            LoadMap16(t);
        }

        private void button1_Click(object sender, EventArgs e) {
            LoadMap16(GFX.Tilesets[0]);
        }

        private void button2_Click(object sender, EventArgs e) {
            LoadMap16(GFX.Tilesets[1]);
        }

        private void button3_Click(object sender, EventArgs e) {
            LoadMap16(GFX.Tilesets[2]);
        }

        private void LoadMap16(NSMBTileset tileset) {
            SelectedTileset = tileset;
            if(GFX != null)
                SelectedTilesetData = GFX.ROM.ExtractFile(tileset.Map16FileID);
            int TileCount = tileset.Map16Buffer.Width / 16;
            int RowCount = TileCount / 16;

            Bitmap Output = new Bitmap(16 * 16, RowCount * 16);
            Graphics g = Graphics.FromImage(Output);

            for (int row = 0; row < RowCount; row++) {
                Rectangle destRect = new Rectangle(0, row * 16, 16 * 16, 16);
                Rectangle srcRect = new Rectangle(row * 16 * 16, 0, 16 * 16, 16);
                g.DrawImage(tileset.Map16Buffer, destRect, srcRect, GraphicsUnit.Pixel);
            }

            pictureBox1.Image = Output;
        }

        private NSMBGraphics GFX;
        private NSMBTileset SelectedTileset;
        private byte[] SelectedTilesetData;

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e) {
            pictureBox1_MouseMove(this, e);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e) {
            if (SelectedTilesetData == null)
                return;

            if (e.Button == MouseButtons.Left) {
                int ClickedTileX = (int)Math.Floor((double)e.X / 16);
                int ClickedTileY = (int)Math.Floor((double)e.Y / 16);
                int ClickedTileNum = (ClickedTileY * 16) + ClickedTileX;

                if ((ClickedTileNum * 8) >= SelectedTilesetData.Length) {
                    label1.Text = "-";
                } else {
                    label1.Text = String.Format(
                        "{0}: {1}/{2}, {3}/{4}, {5}/{6}, {7}/{8}",
                        ClickedTileNum,
                        SelectedTilesetData[(ClickedTileNum * 8)],
                        SelectedTilesetData[(ClickedTileNum * 8) + 1],
                        SelectedTilesetData[(ClickedTileNum * 8) + 2],
                        SelectedTilesetData[(ClickedTileNum * 8) + 3],
                        SelectedTilesetData[(ClickedTileNum * 8) + 4],
                        SelectedTilesetData[(ClickedTileNum * 8) + 5],
                        SelectedTilesetData[(ClickedTileNum * 8) + 6],
                        SelectedTilesetData[(ClickedTileNum * 8) + 7]
                        );
                }
            }
        }
    }
}
