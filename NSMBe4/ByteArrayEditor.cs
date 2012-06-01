/*
*   This file is part of NSMB Editor 5.
*
*   NSMB Editor 5 is free software: you can redistribute it and/or modify
*   it under the terms of the GNU General Public License as published by
*   the Free Software Foundation, either version 3 of the License, or
*   (at your option) any later version.
*
*   NSMB Editor 5 is distributed in the hope that it will be useful,
*   but WITHOUT ANY WARRANTY; without even the implied warranty of
*   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*   GNU General Public License for more details.
*
*   You should have received a copy of the GNU General Public License
*   along with NSMB Editor 5.  If not, see <http://www.gnu.org/licenses/>.
*/

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

        public delegate void ValueChangedD(byte[] val);
        public event ValueChangedD ValueChanged;

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
            box.BackColor = SystemColors.Window;
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
                if (ValueChanged != null)
                    ValueChanged(array);
            }
            else
            {
                box.BackColor = Color.Coral;
            }   
        }
    }
}
