﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NSMBe4
{
    public partial class ImageManagerWindow : Form
    {
        public ImageManagerWindow()
        {
            InitializeComponent();
            this.MdiParent = MdiParentForm.instance;
            this.Icon = Properties.Resources.nsmbe;
        }

        private void ImageManagerWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            m.close();
        }
    }
}
