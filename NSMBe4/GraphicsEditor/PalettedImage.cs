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

﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace NSMBe4
{
    public abstract class PalettedImage
    {
        public string name;
        
        public abstract Bitmap render(Palette p);
        public abstract void replaceWithPal(Bitmap b, Palette p);
        public abstract void replaceImgAndPal(Bitmap b, Palette p);
        public bool supportsReplaceWithPal() { return true; }
        public abstract void save();
        public abstract int getWidth();
        public abstract int getHeight();
        public abstract void beginEdit();
        public abstract void endEdit();

        //These two must return raw data representing the whole image
        //Used for undo/redo in the GraphicsEditor
        public abstract byte[] getRawData();
        public abstract void setRawData(byte[] data);
        
    }
}
