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
        
        public NSMBObject(int ObjNum, int Tileset, int X, int Y, int Width, int Height, NSMBGraphics GFX)
        {
            this.ObjNum = ObjNum;
            this.Tileset = Tileset;
            this.GFX = GFX;
            this.X = X;
            this.Y = Y;
            this.Width = Width;
            this.Height = Height;
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
            UpdateObjCache();
        }

        public void UpdateObjCache() {
            CachedObj = GFX.Tilesets[Tileset].RenderObject(ObjNum, Width, Height);
        }

#if USE_GDIPLUS
        public void Render(Graphics g, int X, int Y) {
            GFX.Tilesets[Tileset].RenderCachedObject(g, CachedObj, X, Y);
        }

        public void Render(Graphics g, int XOffset, int YOffset, Rectangle Clip) {
            // This is basically just RenderCachedObject from the tileset class, with some edits
            Rectangle Limits = new Rectangle(XOffset - X, YOffset - Y, Clip.Width, Clip.Height);
            Rectangle srcRect = new Rectangle();
            Rectangle destRect = new Rectangle((X) * 16, (Y) * 16, 16, 16);
//            Rectangle destRect = new Rectangle((X - XOffset) * 16, (Y - YOffset) * 16, 16, 16);
            for (int CurrentX = 0; CurrentX < CachedObj.GetLength(0); CurrentX++)
            {
                for (int CurrentY = 0; CurrentY < CachedObj.GetLength(1); CurrentY++) {
                    if (CurrentX >= Limits.X && CurrentX < Limits.Right && CurrentY >= Limits.Y && CurrentY < Limits.Bottom) {
                        if (CachedObj[CurrentX, CurrentY] >= 768 && GFX.Tilesets[Tileset].UseOverrides) {
                            srcRect = new Rectangle((CachedObj[CurrentX, CurrentY] - 768) * 16, 0, 16, 16);
                            g.DrawImage(GFX.Tilesets[Tileset].OverrideBitmap, destRect, srcRect, GraphicsUnit.Pixel);
                        } else {
                            srcRect = new Rectangle(CachedObj[CurrentX, CurrentY] * 16, 0, 16, 16);
                            g.DrawImage(GFX.Tilesets[Tileset].Map16Buffer, destRect, srcRect, GraphicsUnit.Pixel);
                        }
                    }
                    destRect.Y += 16;
                }
                destRect.X += 16;
                destRect.Y = (Y) * 16;
            }
        }
#else
        public void Render(IntPtr target, int XOffset, int YOffset, Rectangle Clip, float zoom) {
            // This is basically just RenderCachedObject from the tileset class, with some edits
            Rectangle Limits = new Rectangle(XOffset - X, YOffset - Y, Clip.Width, Clip.Height);
            int destX = (X - XOffset) * 16;
            int destY = (Y - YOffset) * 16;
            for (int CurrentX = 0; CurrentX < CachedObj.GetLength(0); CurrentX++) {
                for (int CurrentY = 0; CurrentY < CachedObj.GetLength(1); CurrentY++) {
                    if (CurrentX >= Limits.X && CurrentX < Limits.Right && CurrentY >= Limits.Y && CurrentY < Limits.Bottom) {
                        if (CachedObj[CurrentX, CurrentY] >= 768 && GFX.Tilesets[Tileset].UseOverrides) {
                            if (zoom == 1)
                                GDIImports.BitBlt(target, destX, destY, 16, 16, GFX.Tilesets[Tileset].OverrideHDC, (CachedObj[CurrentX, CurrentY] - 768) * 16, 0, GDIImports.TernaryRasterOperations.SRCCOPY);
                            else
                                GDIImports.StretchBlt(target, (int)(destX * zoom), (int)(destY * zoom), (int)Math.Ceiling(16 * zoom), (int)Math.Ceiling(16 * zoom), GFX.Tilesets[Tileset].OverrideHDC, (CachedObj[CurrentX, CurrentY] - 768) * 16, 0, 16, 16, GDIImports.TernaryRasterOperations.SRCCOPY);
                        } else {
                            if (zoom == 1)
                                GDIImports.BitBlt(target, destX, destY, 16, 16, GFX.Tilesets[Tileset].Map16BufferHDC, CachedObj[CurrentX, CurrentY] * 16, 0, GDIImports.TernaryRasterOperations.SRCCOPY);
                            else
                                GDIImports.StretchBlt(target, (int)(destX * zoom), (int)(destY * zoom), (int)Math.Ceiling(16 * zoom), (int)Math.Ceiling(16 * zoom), GFX.Tilesets[Tileset].Map16BufferHDC, CachedObj[CurrentX, CurrentY] * 16, 0, 16, 16, GDIImports.TernaryRasterOperations.SRCCOPY);
                        }
                    }
                    destY += 16;
                }
                destX += 16;
                destY = (Y - YOffset) * 16;
            }
        }
#endif

#if USE_GDIPLUS
        public void RenderPlain(Graphics g, int X, int Y) {
            // This is basically just RenderCachedObject from the tileset class, with some edits
            Rectangle srcRect = new Rectangle();
            Rectangle destRect = new Rectangle(X, Y, 16, 16);
            for (int CurrentX = 0; CurrentX < CachedObj.GetLength(0); CurrentX++) {
                for (int CurrentY = 0; CurrentY < CachedObj.GetLength(1); CurrentY++) {
                    if (CachedObj[CurrentX, CurrentY] >= 768 && GFX.Tilesets[Tileset].UseOverrides) {
                        srcRect = new Rectangle((CachedObj[CurrentX, CurrentY] - 768) * 16, 0, 16, 16);
                        g.DrawImage(GFX.Tilesets[Tileset].OverrideBitmap, destRect, srcRect, GraphicsUnit.Pixel);
                    } else {
                        srcRect = new Rectangle(CachedObj[CurrentX, CurrentY] * 16, 0, 16, 16);
                        g.DrawImage(GFX.Tilesets[Tileset].Map16Buffer, destRect, srcRect, GraphicsUnit.Pixel);
                    }
                    destRect.Y += 16;
                }
                destRect.X += 16;
                destRect.Y = Y;
            }
        }
#else
        public void RenderPlain(IntPtr target, int X, int Y) {
            // This is basically just RenderCachedObject from the tileset class, with some edits
            int destX = X;
            int destY = Y;
            for (int CurrentX = 0; CurrentX < CachedObj.GetLength(0); CurrentX++) {
                for (int CurrentY = 0; CurrentY < CachedObj.GetLength(1); CurrentY++) {
                    if (CachedObj[CurrentX, CurrentY] >= 768 && GFX.Tilesets[Tileset].UseOverrides) {
                        GDIImports.BitBlt(target, destX, destY, 16, 16, GFX.Tilesets[Tileset].OverrideHDC, (CachedObj[CurrentX, CurrentY] - 768) * 16, 0, GDIImports.TernaryRasterOperations.SRCCOPY);
                    } else {
                        GDIImports.BitBlt(target, destX, destY, 16, 16, GFX.Tilesets[Tileset].Map16BufferHDC, CachedObj[CurrentX, CurrentY] * 16, 0, GDIImports.TernaryRasterOperations.SRCCOPY);
                    }
                    destY += 16;
                }
                destX += 16;
                destY = Y;
            }
        }
#endif

    }
}
