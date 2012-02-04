using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace NSMBe4
{
    public partial class NotesCtrl : UserControl
    {

        public NotesCtrl()
        {
            InitializeComponent();
        }

        private void NotesCtrl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(Properties.Resources.note, 0, 0, 16, 16);
        }

        private void NotesCtrl_Click(object sender, EventArgs e)
        {
            toolTip1.Show(this.Text, this);
        }
    }
}
