using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NSMBe4 {
    public partial class ImagePreviewer : Form {
        public ImagePreviewer(Image image) {
            InitializeComponent();
            this.Size = image.Size;
            Console.Out.WriteLine("Width: " + image.Width + ", Height: " + image.Height);
            this.Width += 8;
            this.Height += 24;
            this.Image = image;
            pictureBox1.Image = image;
        }


        private Image Image;

    }
}
