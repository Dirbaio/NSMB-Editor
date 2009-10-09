using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace NSMBe4
{
    public partial class ByteArrayEditor : UserControl
    {

        bool DataUpdateFlag = false;
        byte[] array;
        string pat;

        public ByteArrayEditor()
        {
            InitializeComponent();
        }

        public void setArray(byte[] array)
        {
            if (array == null)
            {
                box.Enabled = false;
                return;
            }

            DataUpdateFlag = true;
            this.array = array;
            box.Text = "";
            pat = "";
            for (int i = 0; i < array.Length; i++)
            {
                box.Text += array[i].ToString("X2") + " ";
                pat += "[0-9a-f] *[0-9a-f] *";
            }
            pat = "^ *" + pat + "$";
            DataUpdateFlag = false;
            box.Enabled = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag || array == null)
                return;

            // validate
            if (System.Text.RegularExpressions.Regex.IsMatch(box.Text,
                pat, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
            {
                string parseit = box.Text.Replace(" ", "");
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = byte.Parse(parseit.Substring(i * 2, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
                }
                box.BackColor = SystemColors.Window;
            }
            else
            {
                box.BackColor = Color.Coral;
            }   
        }
    }
}
