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
            this.Width += 8;
            this.Height += 24;
            this.Image = image;
        }

        private void ImagePreviewer_Paint(object sender, PaintEventArgs e) {
            e.Graphics.DrawImage(this.Image, 0, 0);
        }

        private Image Image;
    }
}
