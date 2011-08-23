using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NSMBe4
{
    public partial class MdiParentForm : Form
    {
        public static MdiParentForm instance = null;

        public MdiParentForm()
        {
            InitializeComponent();
            instance = this;
            this.Text = "NSMB Editor 5.2 " + Properties.Resources.version;
        }

        private void MdiParentForm_Load(object sender, EventArgs e)
        {
            LevelChooser lc = new LevelChooser();
            lc.MdiParent = this;
            lc.Show();
            
            //For some reason, without this, the MDI form is created behind other windows. WTF?
            this.Activate();
        }
    }
}
