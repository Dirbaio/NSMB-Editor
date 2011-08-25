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
            if (Properties.Settings.Default.MDIWindowInit)
            {
                this.Location = Properties.Settings.Default.MDIWindowPos;
                this.Size = Properties.Settings.Default.MDIWindowSize;
                if (Properties.Settings.Default.MDIWindowMax) this.WindowState = FormWindowState.Maximized;
            }
        }

        private void MdiParentForm_Load(object sender, EventArgs e)
        {
            LevelChooser lc = new LevelChooser();
            lc.MdiParent = this;
            lc.Show();
            
            //For some reason, without this, the MDI form is created behind other windows. WTF?
            this.Activate();
        }

        private void MdiParentForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.MDIWindowInit = true;
            if (this.WindowState == FormWindowState.Maximized) {
                Properties.Settings.Default.MDIWindowMax = true;
                Properties.Settings.Default.MDIWindowSize = this.RestoreBounds.Size;
                Properties.Settings.Default.MDIWindowPos = this.RestoreBounds.Location;
            } else {
                Properties.Settings.Default.MDIWindowMax = false;
                Properties.Settings.Default.MDIWindowSize = this.Size;
                Properties.Settings.Default.MDIWindowPos = this.Location;
            }
            Properties.Settings.Default.Save();
        }

        private void MdiParentForm_SizeChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Window size changed!");
        }
    }
}
