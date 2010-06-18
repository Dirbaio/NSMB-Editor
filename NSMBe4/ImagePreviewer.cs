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
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NSMBe4 {
    public partial class ImagePreviewer : Form {
        public ImagePreviewer(Image image) {
            InitializeComponent();
            this.MdiParent = MdiParentForm.instance;
            this.Size = image.Size;
            Console.Out.WriteLine("Width: " + image.Width + ", Height: " + image.Height);
            this.Width += 20;
            this.Height += 50;
            this.Image = image;
            LanguageManager.ApplyToContainer(this, "ImagePreviewer");
            pictureBox1.Image = image;
        }


        private Image Image;

    }
}
