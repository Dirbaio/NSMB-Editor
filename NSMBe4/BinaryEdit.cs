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
    public partial class BinaryEdit : UserControl
    {
        public BinaryEdit()
        {
            InitializeComponent();
            AddCheckBoxes();
        }

        public int value {
            get {
                int v = 0;
                if (_CheckBoxCount > 0) {
                    CheckBox cb = this.Controls[0] as CheckBox;
                    for (int l = 0; l < _CheckBoxCount; l++) {
                        if (cb.Checked)
                            v |= 1 << l;
                        cb = this.GetNextControl(cb, true) as CheckBox;
                    }
                }
                return v;
            }
            set {
                if (_CheckBoxCount > 0) {
                    CheckBox cb = this.Controls[0] as CheckBox;
                    for (int l = 0; l < _CheckBoxCount; l++) {
                        cb.Checked = (value & (1 << l)) > 0;
                        cb = this.GetNextControl(cb, true) as CheckBox;
                    }
                }
            }
        }

        private int _CheckBoxCount = 8;

        public int CheckBoxCount {
            get {
                return _CheckBoxCount;
            }
            set {
                _CheckBoxCount = value;
                AddCheckBoxes();
            }
        }

        public event EventHandler ValueChanged;

        public void RaiseValueChanged()
        {
            if (ValueChanged != null)
                ValueChanged(this, EventArgs.Empty);
        }

        private void CheckBoxCheckedChanged(object sender, EventArgs e)
        {
            RaiseValueChanged();
        }

        private void AddCheckBoxes() {
            this.Controls.Clear();
            int pos = 2;
            CheckBox cb;
            for (int l = 1; l <= _CheckBoxCount; l++) {
                cb = new CheckBox();
                cb.AutoSize = true;
                cb.Text = "";
                cb.Location = new Point(pos, 3);
                cb.CheckedChanged += new EventHandler(CheckBoxCheckedChanged);
                this.Controls.Add(cb);
                pos += 17;
            }
        }

    }
}
