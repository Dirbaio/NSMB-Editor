using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace NSMBe4
{
    public interface LevelItem
    {
        int x { get; set; }
        int y { get; set; }
        int width { get; set; }
        int height { get; set; }

        //These are the "real" object rectangle.
        //It is useful for knowing the real position and size,
        //since the real position shouldn't go out of the level space.
        //Though, the "drawn" position can. For example, see sprite End-of-level Flag.
        int rx { get; }
        int ry { get; }
        int rwidth { get; }
        int rheight { get; }

        //If it's not resizable, width.set and height.set just do nothing.
        bool isResizable { get; }

        //Objects and sprites have snap 16, the others have snap 1.
        //x, y, width, height should always be multiples of snap.
        //Setting them to something that's not multiple of snap is OK, though.
        int snap { get; }

        //Renders the object itself.
        void render(Graphics g, LevelEditorControl ed);

        //Renders the white selection box around the object.
//        void render(Graphics g, LevelEditorControl ed);

    }
}
