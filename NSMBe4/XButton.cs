using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace NSMBe4
{
    public partial class XButton : UserControl
    {
        public bool hovered = false;

        public XButton()
        {
            InitializeComponent();
        }

        private void XButton_Paint(object sender, PaintEventArgs e)
        {
            if (hovered)
                e.Graphics.DrawImage(Properties.Resources.cross_script_bright, 0, 0, 16, 16);
            else
                e.Graphics.DrawImage(Properties.Resources.cross_script, 0, 0, 16, 16);
        }

        private void XButton_MouseEnter(object sender, EventArgs e)
        {
            hovered = true;
            this.Invalidate();
        }

        private void XButton_MouseLeave(object sender, EventArgs e)
        {
            hovered = false;
            this.Invalidate();
        }
    }
}
