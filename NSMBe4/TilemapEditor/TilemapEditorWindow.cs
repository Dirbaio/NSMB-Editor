using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NSMBe4.TilemapEditor
{
    public partial class TilemapEditorWindow : Form
    {
        Tilemap t;

        public TilemapEditorWindow(Tilemap t)
        {
            InitializeComponent();
            this.t = t;
            t.beginEdit();
            tilemapEditor1.showSaveButton();
            tilemapEditor1.load(t);
        }

        private void TilemapEditorWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            t.endEdit();
        }
    }
}
