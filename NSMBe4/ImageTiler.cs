using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace NSMBe4
{
    class ImageTiler
    {
        Bitmap originalImage;
        Bitmap tileBuffer;

        public ImageTiler(Bitmap originalImage)
        {
            this.originalImage = originalImage;
        }
    }
}
