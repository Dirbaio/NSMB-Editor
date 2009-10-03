using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NSMBe4
{
    public partial class ProgressWindow : Form
    {
        public ProgressWindow(string title)
        {
            InitializeComponent();
            this.Text = title;
        }

        public void SetMax(int max)
        {
            progressBar1.Maximum = max;
        }

        public void setValue(int val)
        {
            progressBar1.Value = val;
            Application.DoEvents();
        }

        public void SetCurrentAction(string current)
        {
            currentAction.Text = current;
            Application.DoEvents();
        }

        public void WriteLine(string line)
        {
            textBox1.AppendText(line+"\r\n");
            Application.DoEvents();
        }
    }
}
