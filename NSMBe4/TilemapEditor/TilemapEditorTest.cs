using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NSMBe4.TilemapEditor
{
    public partial class TilemapEditorTest : Form
    {
        public TilemapEditorTest()
        {
            InitializeComponent();
        }

        public void load(Tilemap t)
        {
            tilemapEditor1.load(t);
        }
    }
}
