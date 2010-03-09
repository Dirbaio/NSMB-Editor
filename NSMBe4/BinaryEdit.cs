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
                    for (int l = 1; l <= _CheckBoxCount; l++)
                    {
                        if (cb.Checked)
                            v += (int)Math.Pow(2, l - 1);
                        if (l < _CheckBoxCount)
                            cb = this.GetNextControl(cb, true) as CheckBox;
                    }
                }
                return v;
            }
            set {
                if (_CheckBoxCount > 0) {
                    CheckBox cb = this.Controls[0] as CheckBox;
                    for (int l = 1; l <= _CheckBoxCount; l++)
                    {
                        cb.Checked = value / (int)Math.Pow(2, l - 1) % 2 == 1;
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
                ValueChanged(this, new EventArgs());
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
