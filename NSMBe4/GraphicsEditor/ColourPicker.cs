using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NSMBe4 {
    public partial class ColourPicker : Form {
        public int R { get { return colourPickerControl1.R; } set { colourPickerControl1.R = value; } }
        public int G { get { return colourPickerControl1.G; } set { colourPickerControl1.G = value; } }
        public int B { get { return colourPickerControl1.B; } set { colourPickerControl1.B = value; } }
        public int Value { get { return colourPickerControl1.Value; } set { colourPickerControl1.Value = value; } }

        public ColourPicker() {
            InitializeComponent();
            LanguageManager.ApplyToContainer(this, "ColourPicker");
        }
    }
}
