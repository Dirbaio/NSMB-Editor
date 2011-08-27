using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace NSMBe4
{
    interface LevelItem
    {
        public int x { get; set; }
        public int y { get; set; }
        public int width { get; set; }
        public int height { get; set; }

        //If it's not resizable, width.set and height.set just do nothing.
        public bool isResizable { get; }

        //Objects and sprites have snap 16, the others have snap 1.
        //x, y, width, height should always be multiples of snap.
        //Setting them to something that's not multiple of snap is OK, though.
        public int snap { get; }

        //Renders the object itself.
        public void render(Graphics g);

        //Renders the white selection box around the object.
        public void renderSelection(Graphics g);
    }
}
