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
using System.Text;
using System.Drawing;

namespace NSMBe4
{
    public class NSMBObject
    {
        public int ObjNum;
        public int Tileset;
        public int X;
        public int Y;
        public int Width;
        public int Height;
        private int[,] CachedObj;
        private NSMBGraphics GFX;

        public bool badObject;
        private string error;
        
        public NSMBObject(int ObjNum, int Tileset, int X, int Y, int Width, int Height, NSMBGraphics GFX)
        {
            this.ObjNum = ObjNum;
            this.Tileset = Tileset;
            this.GFX = GFX;
            this.X = X;
            this.Y = Y;
            this.Width = Width;
            this.Height = Height;
            CachedObj = new int[Width, Height];
            UpdateObjCache();
        }

        public NSMBObject(NSMBObject o) // clone constructor: returns an identical object
        {
            this.ObjNum = o.ObjNum;
            this.Tileset = o.Tileset;
            this.GFX = o.GFX;
            this.X = o.X;
            this.Y = o.Y;
            this.Width = o.Width;
            this.Height = o.Height;
            CachedObj = new int[Width, Height];
            UpdateObjCache();
        }

        public void UpdateObjCache() {
            if (GFX == null)
                return;

            badObject = false;

            try
            {
                CachedObj = GFX.Tilesets[Tileset].RenderObject(ObjNum, Width, Height);
            }
            catch (NSMBTileset.ObjectRenderingException e)
            {
                badObject = true;
                error = e.Message;
            }
        }

        public void Render(Graphics g, int XOffset, int YOffset, Rectangle Clip, float zoom)
        {
            if (badObject)
            {
                g.DrawRectangle(new Pen(Color.Red, 4), new Rectangle(X * 16, Y * 16, Width * 16, Height * 16));
                g.DrawLine(new Pen(Color.Red, 4), new Point(X * 16, (Y + Height) * 16), new Point((X + Width) * 16, (Y) * 16));
                g.DrawLine(new Pen(Color.Red, 4), new Point((X+Width)* 16, (Y + Height) * 16), new Point((X) * 16, (Y) * 16));
                return;
            }

            if(zoom > 1)
            {
                RectangleF Limits = new RectangleF(XOffset - X, YOffset - Y, Clip.Width, Clip.Height);
                RectangleF srcRect = new RectangleF();
                RectangleF destRect = new RectangleF(X << 4, Y << 4, 16, 16);
                for (int CurrentX = 0; CurrentX < CachedObj.GetLength(0); CurrentX++)
                {
                    for (int CurrentY = 0; CurrentY < CachedObj.GetLength(1); CurrentY++)
                    {
                        if (CurrentX >= Limits.X && CurrentX < Limits.Right && CurrentY >= Limits.Y && CurrentY < Limits.Bottom)
                        {
                            if (CachedObj[CurrentX, CurrentY] != -1)
                            {
                                srcRect = new RectangleF((CachedObj[CurrentX, CurrentY] << 4)-0.5f, -0.5f, 16, 16);
                                g.DrawImage(GFX.Tilesets[Tileset].Map16Buffer, destRect, srcRect, GraphicsUnit.Pixel);
                                if (GFX.Tilesets[Tileset].UseOverrides && GFX.Tilesets[Tileset].EditorOverrides[CachedObj[CurrentX, CurrentY]] != -1)
                                {
                                    srcRect = new RectangleF((GFX.Tilesets[Tileset].EditorOverrides[CachedObj[CurrentX, CurrentY]] << 4)-0.5f, -0.5f, 16, 16);
                                    g.DrawImage(GFX.Tilesets[Tileset].OverrideBitmap, destRect, srcRect, GraphicsUnit.Pixel);
                                }
                            }
                        }
                        destRect.Y += 16;
                    }
                    destRect.X += 16;
                    destRect.Y = Y << 4;
                }
            }
            else
            {
                Rectangle Limits = new Rectangle(XOffset - X, YOffset - Y, Clip.Width, Clip.Height);
                Rectangle srcRect = new Rectangle();
                Rectangle destRect;
                if(zoom == 1)
                    destRect = new Rectangle(X << 4, Y << 4, 16, 16);
                else
                    destRect = new Rectangle(X << 4, Y << 4, 17, 17);
                for (int CurrentX = 0; CurrentX < CachedObj.GetLength(0); CurrentX++)
                {
                    for (int CurrentY = 0; CurrentY < CachedObj.GetLength(1); CurrentY++)
                    {
                        if (CurrentX >= Limits.X && CurrentX < Limits.Right && CurrentY >= Limits.Y && CurrentY < Limits.Bottom)
                        {
                            if (CachedObj[CurrentX, CurrentY] != -1)
                            {
                                srcRect = new Rectangle(CachedObj[CurrentX, CurrentY] << 4, 0, 16, 16);
                                g.DrawImage(GFX.Tilesets[Tileset].Map16Buffer, destRect, srcRect, GraphicsUnit.Pixel);
                                if (GFX.Tilesets[Tileset].UseOverrides && GFX.Tilesets[Tileset].EditorOverrides[CachedObj[CurrentX, CurrentY]] != -1)
                                {
                                    srcRect = new Rectangle(GFX.Tilesets[Tileset].EditorOverrides[CachedObj[CurrentX, CurrentY]] << 4, 0, 16, 16);
                                    g.DrawImage(GFX.Tilesets[Tileset].OverrideBitmap, destRect, srcRect, GraphicsUnit.Pixel);
                                }
                            }
                        }
                        destRect.Y += 16;
                    }
                    destRect.X += 16;
                    destRect.Y = Y << 4;
                }
            }
        }

        public void RenderPlain(Graphics g, int X, int Y)
        {
            if (badObject)
            {
                g.DrawRectangle(new Pen(Color.Red, 4), new Rectangle(X, Y , Width * 16, Height * 16));
                g.DrawLine(new Pen(Color.Red, 4), new Point(X , Y + Height * 16), new Point(X + Width * 16, Y));
                g.DrawLine(new Pen(Color.Red, 4), new Point(X + Width * 16, Y + Height * 16), new Point(X, Y));
                return;
            }

            // This is basically just RenderCachedObject from the tileset class, with some edits
            Rectangle srcRect = new Rectangle();
            Rectangle destRect = new Rectangle(X, Y, 16, 16);
            for (int CurrentX = 0; CurrentX < CachedObj.GetLength(0); CurrentX++) {
                for (int CurrentY = 0; CurrentY < CachedObj.GetLength(1); CurrentY++) {
                    if (CachedObj[CurrentX, CurrentY] != -1) {
                        srcRect = new Rectangle(CachedObj[CurrentX, CurrentY] * 16, 0, 16, 16);
                        g.DrawImage(GFX.Tilesets[Tileset].Map16Buffer, destRect, srcRect, GraphicsUnit.Pixel);
                        if (GFX.Tilesets[Tileset].UseOverrides && GFX.Tilesets[Tileset].EditorOverrides[CachedObj[CurrentX, CurrentY]] != -1)
                        {
                            srcRect = new Rectangle(GFX.Tilesets[Tileset].EditorOverrides[CachedObj[CurrentX, CurrentY]] * 16, 0, 16, 16);
                            g.DrawImage(GFX.Tilesets[Tileset].OverrideBitmap, destRect, srcRect, GraphicsUnit.Pixel);
                        }
                    }
                    destRect.Y += 16;
                }
                destRect.X += 16;
                destRect.Y = Y;
            }
        }

        public override string ToString()
        {
            return String.Format("OBJ:{0}:{1}:{2}:{3}:{4}:{5}", X, Y, Width, Height, Tileset, ObjNum);
        }

        public static NSMBObject FromString(String[] strs, ref int idx, NSMBGraphics gfx) {
            NSMBObject o = new NSMBObject(
                int.Parse(strs[6 + idx]),
                int.Parse(strs[5 + idx]),
                int.Parse(strs[1 + idx]),
                int.Parse(strs[2 + idx]),
                int.Parse(strs[3 + idx]),
                int.Parse(strs[4 + idx]),
                gfx);
            idx += 7;
            return o;
        }
    }
}
