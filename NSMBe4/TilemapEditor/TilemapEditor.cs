using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace NSMBe4.TilemapEditor
{
    public partial class TilemapEditor : UserControl
    {
        public TilemapEditor()
        {
            InitializeComponent();
        }

        public void load(Tilemap t)
        {
            tilePicker1.init(t.buffers, 8);
            tilemapEditorControl1.picker = tilePicker1;
            tilemapEditorControl1.load(t);
        }

        private void uncheckButtons()
        {
            drawToolButton.Checked = false;
            xFlipToolButton.Checked = false;
            yFlipToolButton.Checked = false;
            copyToolButton.Checked = false;
            pasteToolButton.Checked = false;
        }

        private void drawToolButton_Click(object sender, EventArgs e)
        {
            tilemapEditorControl1.mode = TilemapEditorControl.EditionMode.DRAW;
            uncheckButtons();
            drawToolButton.Checked = true;
        }

        private void xFlipToolButton_Click(object sender, EventArgs e)
        {
            tilemapEditorControl1.mode = TilemapEditorControl.EditionMode.XFLIP;
            uncheckButtons();
            xFlipToolButton.Checked = true;
        }

        private void yFlipToolButton_Click(object sender, EventArgs e)
        {
            tilemapEditorControl1.mode = TilemapEditorControl.EditionMode.YFLIP;
            uncheckButtons();
            yFlipToolButton.Checked = true;
        }

        private void copyToolButton_Click(object sender, EventArgs e)
        {
            tilemapEditorControl1.mode = TilemapEditorControl.EditionMode.COPY;
            uncheckButtons();
            copyToolButton.Checked = true;
        }

        private void pasteToolButton_Click(object sender, EventArgs e)
        {
            tilemapEditorControl1.mode = TilemapEditorControl.EditionMode.PASTE;
            uncheckButtons();
            pasteToolButton.Checked = true;
        }
    }
}
