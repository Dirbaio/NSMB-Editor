using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NSMBe4
{
    public partial class TextInputForm : Form
    {
        public TextInputForm()
        {
            InitializeComponent();
        }

        public DialogResult ShowDialog(string prompt, string defaultText, out string result)
        {
            lblPrompt.Text = prompt;
            textBox1.Text = defaultText;
            textBox1.Focus();
            textBox1.SelectAll();
            DialogResult dresult = ShowDialog();
            result = textBox1.Text;
            return dresult;
        }

        private void OKbtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
