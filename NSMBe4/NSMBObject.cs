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
    public class NSMBObject : LevelItem
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

        //LevelItem implementation.
        public int x { get { return X * snap; } set { X = value / snap; } }
        public int y { get { return Y * snap; } set { Y = value / snap; } }
        public int width { get { return Width * snap; } set { Width = value / snap; } }
        public int height { get { return Height * snap; } set { Height = value / snap; } }

        public int rx { get { return X * snap; } }
        public int ry { get { return Y * snap; } }
        public int rwidth { get { return Width * snap; } }
        public int rheight { get { return Height * snap; } }

        public bool isResizable { get { return true; } }
        public int snap { get { return 16; } }

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

        public Rectangle getRectangle()
        {
            return new Rectangle(X, Y, Width, Height);
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

//        public void Render(Graphics g, int XOffset, int YOffset, Rectangle Clip, float zoom)

        public void render(Graphics g, LevelEditorControl ed)
        {
            //This method is really messy due to the quirky rendering of .NET
            //I need to do a lot of hacks to get objects rendered correctly with
            //high and low zoom.

            if (badObject)
            {
                g.DrawRectangle(new Pen(Color.Red, 4), new Rectangle(X * 16, Y * 16, Width * 16, Height * 16));
                g.DrawLine(new Pen(Color.Red, 4), new Point(X * 16, (Y + Height) * 16), new Point((X + Width) * 16, (Y) * 16));
                g.DrawLine(new Pen(Color.Red, 4), new Point((X + Width) * 16, (Y + Height) * 16), new Point((X) * 16, (Y) * 16));
                return;
            }

            if (ed.zoom > 1)
            {
                RectangleF srcRect = new RectangleF(0, 0, 16, 16);
                RectangleF destRect = new RectangleF(X << 4, Y << 4, 16, 16);

                for (int xx = 0; xx < CachedObj.GetLength(0); xx++)
                    for (int yy = 0; yy < CachedObj.GetLength(1); yy++)
                    {
                        int t = CachedObj[xx, yy];
                        if (t == -1) continue;

                        destRect.X = (X + xx) << 4;
                        destRect.Y = (Y + yy) << 4;

                        srcRect.X = (t % 16) * 16 - 0.5f;
                        srcRect.Y = (t / 16) * 16 - 0.5f;

                        g.DrawImage(GFX.Tilesets[Tileset].Map16Buffer, destRect.X, destRect.Y, srcRect, GraphicsUnit.Pixel);

                        if (!GFX.Tilesets[Tileset].UseOverrides) continue;
                        int t2 = GFX.Tilesets[Tileset].Overrides[t];
                        if (t2 == -1) continue;
                        if (t2 == 0) continue;

                        srcRect.X = t2 * 16 - 0.5f;
                        srcRect.Y = 0 - 0.5f;

                        g.DrawImage(GFX.Tilesets[Tileset].OverrideBitmap, destRect.X, destRect.Y, srcRect, GraphicsUnit.Pixel);
                    }
            }
            else
            {
                Rectangle srcRect = new Rectangle(0, 0, 16, 16);
                Rectangle destRect = new Rectangle(X << 4, Y << 4, 16, 16);

                for (int xx = 0; xx < CachedObj.GetLength(0); xx++)
                    for (int yy = 0; yy < CachedObj.GetLength(1); yy++)
                    {
                        int t = CachedObj[xx, yy];
                        if (t == -1) continue;

                        destRect.X = (X + xx) << 4;
                        destRect.Y = (Y + yy) << 4;

                        srcRect.X = (t % 16) * 16;
                        srcRect.Y = (t / 16) * 16;

                        g.DrawImage(GFX.Tilesets[Tileset].Map16Buffer, destRect.X, destRect.Y, srcRect, GraphicsUnit.Pixel);

                        if (!GFX.Tilesets[Tileset].UseOverrides) continue;
                        int t2 = GFX.Tilesets[Tileset].Overrides[t];
                        if (t2 == -1) continue;
                        if (t2 == 0) continue;

                        srcRect.X = t2 * 16;
                        srcRect.Y = 0;

                        g.DrawImage(GFX.Tilesets[Tileset].OverrideBitmap, destRect.X, destRect.Y, srcRect, GraphicsUnit.Pixel);
                    }
            }
        }


        public void renderTilemap(int[,] tilemap)
        {
            for (int xx = 0; xx < CachedObj.GetLength(0); xx++)
                for (int yy = 0; yy < CachedObj.GetLength(1); yy++)
                {
                    int t = CachedObj[xx, yy];
                    if (t == -1) continue;

                    if (Tileset == 1)
                        t += 256;
                    else if (Tileset == 2)
                        t += 256 * 4;

                    tilemap[X + xx, Y + yy] = t;
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
            Rectangle srcRect = new Rectangle(0, 0, 16, 16);
            Rectangle destRect = new Rectangle(X, Y, 16, 16);

            for (int xx = 0; xx < CachedObj.GetLength(0); xx++)
                for (int yy = 0; yy < CachedObj.GetLength(1); yy++)
                {
                    int t = CachedObj[xx, yy];
                    if (t == -1) continue;

                    destRect.X = X + xx << 4;
                    destRect.Y = Y + yy << 4;

                    srcRect.X = (t % 16) * 16;
                    srcRect.Y = (t / 16) * 16;

                    g.DrawImage(GFX.Tilesets[Tileset].Map16Buffer, destRect.X, destRect.Y, srcRect, GraphicsUnit.Pixel);

                    if (!GFX.Tilesets[Tileset].UseOverrides) continue;
                    int t2 = GFX.Tilesets[Tileset].Overrides[t];
                    if (t2 == -1) continue;
                    if (t2 == 0) continue;

                    srcRect.X = t2 * 16;
                    srcRect.Y = 0;

                    g.DrawImage(GFX.Tilesets[Tileset].OverrideBitmap, destRect, srcRect, GraphicsUnit.Pixel);
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
