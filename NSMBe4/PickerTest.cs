using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NSMBe4
{
    public partial class PickerTest : Form
    {
        public PickerTest(NSMBGraphics g)
        {
            InitializeComponent();
            objectPickerControlNew1.Initialise(g, 1);
            this.Icon = Properties.Resources.nsmbe;
        }
    }
}
