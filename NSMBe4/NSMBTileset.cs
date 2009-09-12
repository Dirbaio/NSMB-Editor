using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

/**
 * 
 * TILESET FORMAT DOCS:
 * 
 * Graphics file:
 *  - LZ Compressed
 *  - 8bpp 8x8 tiles
 *  
 * Palette file:
 *  - LZ Compressed
 *  - 512 entries
 *  - 2-byte entries in RGB15 format
 *  - 0 and 256 transparent
 *  - 2 different palettes: 0-255 and 256-511
 *  
 * Map16 file:
 *  - Groups 8x8 tiles into 16x16 tiles
 *  - 8-byte per 16x16 tile
 *  - order: top-left, top-right, bottom-left, bottom-right
 *  - 2-byte per tile.
 *  
 **/


namespace NSMBe4 {
    public class NSMBTileset {
        public NSMBTileset(NitroClass ROM, ushort GFXFile, ushort PalFile, ushort Map16File, ushort ObjFile, ushort ObjIndexFile, bool OverrideFlag) {
            int FilePos;
            GFXFileID = GFXFile;
            PalFileID = PalFile;
            Map16FileID = Map16File;
            ObjFileID = ObjFile;
            ObjIndexFileID = ObjIndexFile;

            Console.Out.WriteLine("Load Tileset: " + GFXFile + ", " + PalFile + ", " + Map16File + ", " + ObjFile + ", " + ObjIndexFile);
            // First get the palette out
            byte[] ePalFile = ROM.LZ77_Decompress(ROM.ExtractFile(PalFile));
            Color[] Palette = new Color[512];

            for (int PalIdx = 0; PalIdx < 512; PalIdx++) {
                int ColourVal = ePalFile[PalIdx * 2] + (ePalFile[(PalIdx * 2) + 1] << 8);
                int cR = (ColourVal & 31) * 8;
                int cG = ((ColourVal >> 5) & 31) * 8;
                int cB = ((ColourVal >> 10) & 31) * 8;
                Palette[PalIdx] = Color.FromArgb(cR, cG, cB);
            }

            //Palette[0] = Color.Fuchsia;
            //Palette[256] = Color.Fuchsia;
            Palette[0] = Color.LightSlateGray;
            Palette[256] = Color.LightSlateGray;

            // Load graphics
            byte[] eGFXFile = ROM.LZ77_Decompress(ROM.ExtractFile(GFXFile));
            int TileCount = eGFXFile.Length / 64;
            Bitmap TilesetBuffer = new Bitmap(TileCount * 8, 16);

            FilePos = 0;
            for (int TileIdx = 0; TileIdx < TileCount; TileIdx++) {
                int TileSrcX = TileIdx * 8;
                for (int TileY = 0; TileY < 8; TileY++) {
                    for (int TileX = 0; TileX < 8; TileX++) {
                        TilesetBuffer.SetPixel(TileSrcX + TileX, TileY, Palette[eGFXFile[FilePos]]);
                        TilesetBuffer.SetPixel(TileSrcX + TileX, TileY + 8, Palette[eGFXFile[FilePos] + 256]);
                        FilePos++;
                    }
                }
            }
            
//            ImagePreviewer.ShowCutImage(TilesetBuffer, 256, 2);
            
#if USE_GDIPLUS
            Bitmap TilesetBufferFlipX = (Bitmap)TilesetBuffer.Clone();
            TilesetBufferFlipX.RotateFlip(RotateFlipType.RotateNoneFlipX);
            Bitmap TilesetBufferFlipY = (Bitmap)TilesetBuffer.Clone();
            TilesetBufferFlipY.RotateFlip(RotateFlipType.RotateNoneFlipY);
            Bitmap TilesetBufferFlipXY = (Bitmap)TilesetBuffer.Clone();
            TilesetBufferFlipXY.RotateFlip(RotateFlipType.RotateNoneFlipXY);
#else
            Graphics TilesetGraphics = Graphics.FromImage(TilesetBuffer);
            IntPtr TilesetBufferHDC = TilesetGraphics.GetHdc();
            IntPtr TilesetBufferHandle = TilesetBuffer.GetHbitmap();
            GDIImports.SelectObject(TilesetBufferHDC, TilesetBufferHandle);
#endif

            // Load Map16
            byte[] eMap16File = ROM.ExtractFile(Map16File);
            int Map16Count = eMap16File.Length / 8;
            Map16Buffer = new Bitmap(Map16Count * 16, 16, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
#if USE_GDIPLUS
            Graphics Map16Graphics;
#endif
            Map16Graphics = Graphics.FromImage(Map16Buffer);
            Map16Graphics.Clear(Color.LightSlateGray);
#if !USE_GDIPLUS
            Map16BufferHDC = Map16Graphics.GetHdc();
            Map16BufferHandle = Map16Buffer.GetHbitmap();
            GDIImports.SelectObject(Map16BufferHDC, Map16BufferHandle);
#endif

            FilePos = 0;
            int TileNum;
            byte ControlByte;
            Rectangle SrcRect, DestRect;
#if USE_GDIPLUS
            Bitmap SourceBitmap = null;
#endif

            for (int Map16Idx = 0; Map16Idx < Map16Count; Map16Idx++) {
                int Map16SrcX = Map16Idx * 16;
                // This algorithm makes absolutely NO SENSE to me, but for some reason it works.
                // Maybe it made more sense back in 2007 when I wrote it..
                // I wonder what the game actually uses.
                
                // Top-left Tile
                TileNum = eMap16File[FilePos];
                ControlByte = eMap16File[FilePos + 1];
                if ((ControlByte & 64) != 0) {
                    TileNum -= 128;
                } else {
                    if ((ControlByte & 32) != 0) TileNum += 64;
                    if (TileNum >= 256 && ((ControlByte & 1) == 0)) TileNum -= 256;
                    if ((ControlByte & 2) != 0) TileNum += 256;
                }
                SrcRect = new Rectangle(TileNum * 8, ((eMap16File[FilePos + 1] & 16) != 0) ? 8 : 0, 8, 8);
                DestRect = new Rectangle(Map16SrcX, 0, 8, 8);
                //GDIImports.BitBlt(Map16BufferHDC, DestRect.X, DestRect.Y, DestRect.Width, DestRect.Height, TilesetBufferHDC, SrcRect.X, SrcRect.Y, GDIImports.TernaryRasterOperations.SRCCOPY);
#if USE_GDIPLUS
                if ((ControlByte & 4) != 0 && (ControlByte & 8) != 0) {
                    SourceBitmap = TilesetBufferFlipXY;
                    SrcRect.X = TilesetBufferFlipXY.Width - SrcRect.X - 8;
                    SrcRect.Y = TilesetBufferFlipXY.Height - SrcRect.Y - 8;
                } else if ((ControlByte & 8) != 0) {
                    SourceBitmap = TilesetBufferFlipY;
                    SrcRect.Y = TilesetBufferFlipY.Height - SrcRect.Y - 8;
                } else if ((ControlByte & 4) != 0) {
                    SourceBitmap = TilesetBufferFlipX;
                    SrcRect.X = TilesetBufferFlipX.Width - SrcRect.X - 8;
                } else {
                    SourceBitmap = TilesetBuffer;
                }
                if (TileNum != 0 || ControlByte != 0) {
                    Map16Graphics.DrawImage(SourceBitmap, DestRect, SrcRect, GraphicsUnit.Pixel);
                }
#else
                if ((ControlByte & 4) != 0) { DestRect.Width = -8; DestRect.X += 7; }
                if ((ControlByte & 8) != 0) { DestRect.Height = -8; DestRect.Y += 7; }
                if (TileNum != 0 || ControlByte != 0) {
                    GDIImports.StretchBlt(Map16BufferHDC, DestRect.X, DestRect.Y, DestRect.Width, DestRect.Height, TilesetBufferHDC, SrcRect.X, SrcRect.Y, 8, 8, GDIImports.TernaryRasterOperations.SRCCOPY);
                }
#endif
                FilePos += 2;
                // Top-right Tile
                TileNum = eMap16File[FilePos];
                ControlByte = eMap16File[FilePos + 1];
                if ((ControlByte & 64) != 0) {
                    TileNum -= 128;
                } else {
                    if ((ControlByte & 32) != 0) TileNum += 64;
                    if (TileNum >= 256 && ((ControlByte & 1) == 0)) TileNum -= 256;
                    if ((ControlByte & 2) != 0) TileNum += 256;
                }
                SrcRect = new Rectangle(TileNum * 8, ((eMap16File[FilePos + 1] & 16) != 0) ? 8 : 0, 8, 8);
                DestRect = new Rectangle(Map16SrcX + 8, 0, 8, 8);
#if USE_GDIPLUS
                if ((ControlByte & 4) != 0 && (ControlByte & 8) != 0) {
                    SourceBitmap = TilesetBufferFlipXY;
                    SrcRect.X = TilesetBufferFlipXY.Width - SrcRect.X - 8;
                    SrcRect.Y = TilesetBufferFlipXY.Height - SrcRect.Y - 8;
                } else if ((ControlByte & 8) != 0) {
                    SourceBitmap = TilesetBufferFlipY;
                    SrcRect.Y = TilesetBufferFlipY.Height - SrcRect.Y - 8;
                } else if ((ControlByte & 4) != 0) {
                    SourceBitmap = TilesetBufferFlipX;
                    SrcRect.X = TilesetBufferFlipX.Width - SrcRect.X - 8;
                } else {
                    SourceBitmap = TilesetBuffer;
                }
                if (TileNum != 0 || ControlByte != 0) {
                    Map16Graphics.DrawImage(SourceBitmap, DestRect, SrcRect, GraphicsUnit.Pixel);
                }
#else
                if ((ControlByte & 4) != 0) { DestRect.Width = -8; DestRect.X += 7; }
                if ((ControlByte & 8) != 0) { DestRect.Height = -8; DestRect.Y += 7; }
                if (TileNum != 0 || ControlByte != 0) {
                    GDIImports.StretchBlt(Map16BufferHDC, DestRect.X, DestRect.Y, DestRect.Width, DestRect.Height, TilesetBufferHDC, SrcRect.X, SrcRect.Y, 8, 8, GDIImports.TernaryRasterOperations.SRCCOPY);
                }
#endif
                FilePos += 2;
                // Bottom-left Tile
                TileNum = eMap16File[FilePos];
                ControlByte = eMap16File[FilePos + 1];
                if ((ControlByte & 64) != 0) {
                    TileNum -= 128;
                } else {
                    if ((ControlByte & 32) != 0) TileNum += 64;
                    if (TileNum >= 256 && ((ControlByte & 1) == 0)) TileNum -= 256;
                    if ((ControlByte & 2) != 0) TileNum += 256;
                }
                SrcRect = new Rectangle(TileNum * 8, ((eMap16File[FilePos + 1] & 16) != 0) ? 8 : 0, 8, 8);
                DestRect = new Rectangle(Map16SrcX, 8, 8, 8);
#if USE_GDIPLUS
                if ((ControlByte & 4) != 0 && (ControlByte & 8) != 0) {
                    SourceBitmap = TilesetBufferFlipXY;
                    SrcRect.X = TilesetBufferFlipXY.Width - SrcRect.X - 8;
                    SrcRect.Y = TilesetBufferFlipXY.Height - SrcRect.Y - 8;
                } else if ((ControlByte & 8) != 0) {
                    SourceBitmap = TilesetBufferFlipY;
                    SrcRect.Y = TilesetBufferFlipY.Height - SrcRect.Y - 8;
                } else if ((ControlByte & 4) != 0) {
                    SourceBitmap = TilesetBufferFlipX;
                    SrcRect.X = TilesetBufferFlipX.Width - SrcRect.X - 8;
                } else {
                    SourceBitmap = TilesetBuffer;
                }
                if (TileNum != 0 || ControlByte != 0) {
                    Map16Graphics.DrawImage(SourceBitmap, DestRect, SrcRect, GraphicsUnit.Pixel);
                }
#else
                if ((ControlByte & 4) != 0) { DestRect.Width = -8; DestRect.X += 7; }
                if ((ControlByte & 8) != 0) { DestRect.Height = -8; DestRect.Y += 7; }
                if (TileNum != 0 || ControlByte != 0) {
                    GDIImports.StretchBlt(Map16BufferHDC, DestRect.X, DestRect.Y, DestRect.Width, DestRect.Height, TilesetBufferHDC, SrcRect.X, SrcRect.Y, 8, 8, GDIImports.TernaryRasterOperations.SRCCOPY);
                }
#endif
                FilePos += 2;
                // Bottom-right Tile
                TileNum = eMap16File[FilePos];
                ControlByte = eMap16File[FilePos + 1];
                if ((ControlByte & 64) != 0) {
                    TileNum -= 128;
                } else {
                    if ((ControlByte & 32) != 0) TileNum += 64;
                    if (TileNum >= 256 && ((ControlByte & 1) == 0)) TileNum -= 256;
                    if ((ControlByte & 2) != 0) TileNum += 256;
                }
                SrcRect = new Rectangle(TileNum * 8, ((eMap16File[FilePos + 1] & 16) != 0) ? 8 : 0, 8, 8);
                DestRect = new Rectangle(Map16SrcX + 8, 8, 8, 8);
#if USE_GDIPLUS
                if ((ControlByte & 4) != 0 && (ControlByte & 8) != 0) {
                    SourceBitmap = TilesetBufferFlipXY;
                    SrcRect.X = TilesetBufferFlipXY.Width - SrcRect.X - 8;
                    SrcRect.Y = TilesetBufferFlipXY.Height - SrcRect.Y - 8;
                } else if ((ControlByte & 8) != 0) {
                    SourceBitmap = TilesetBufferFlipY;
                    SrcRect.Y = TilesetBufferFlipY.Height - SrcRect.Y - 8;
                } else if ((ControlByte & 4) != 0) {
                    SourceBitmap = TilesetBufferFlipX;
                    SrcRect.X = TilesetBufferFlipX.Width - SrcRect.X - 8;
                } else {
                    SourceBitmap = TilesetBuffer;
                }

                
                if (TileNum != 0 || ControlByte != 0) {
                    Map16Graphics.DrawImage(SourceBitmap, DestRect, SrcRect, GraphicsUnit.Pixel);
                }
#else
                if ((ControlByte & 4) != 0) { DestRect.Width = -8; DestRect.X += 7; }
                if ((ControlByte & 8) != 0) { DestRect.Height = -8; DestRect.Y += 7; }
                if (TileNum != 0 || ControlByte != 0) {
                    GDIImports.StretchBlt(Map16BufferHDC, DestRect.X, DestRect.Y, DestRect.Width, DestRect.Height, TilesetBufferHDC, SrcRect.X, SrcRect.Y, 8, 8, GDIImports.TernaryRasterOperations.SRCCOPY);
                }
#endif
                FilePos += 2;
            }

#if !USE_GDIPLUS
            GDIImports.DeleteObject(TilesetBufferHandle);
            TilesetGraphics.ReleaseHdc(TilesetBufferHDC);
#endif
            // Finally the object file.
            byte[] eObjIndexFile = ROM.ExtractFile(ObjIndexFile);
            byte[] eObjFile = ROM.ExtractFile(ObjFile);

            int ObjectCount = eObjIndexFile.Length / 4;
            Objects = new ObjectDef[ObjectCount];

            FilePos = 0;
            for (int ObjIdx = 0; ObjIdx < ObjectCount; ObjIdx++) {
                Objects[ObjIdx] = new ObjectDef();
                Objects[ObjIdx].Offset = eObjIndexFile[FilePos] + (eObjIndexFile[FilePos + 1] << 8);
                // The width here is inaccurate for slopes and diagonal objects. :(
                //Objects[ObjIdx].Width = eObjIndexFile[FilePos + 2];
                //Objects[ObjIdx].Height = eObjIndexFile[FilePos + 3];
                FilePos += 4;
            }

            // Now load each individual object's data
            // There's a sort of hack here, the format doesn't store the size of each object
            // So I must calculate it from the following object's size.
            for (int ObjIdx = 0; ObjIdx < ObjectCount; ObjIdx++) {
                int NextOffset = (ObjIdx == (ObjectCount - 1)) ? eObjFile.Length : Objects[ObjIdx + 1].Offset;
                int ObjDataLength = NextOffset - Objects[ObjIdx].Offset;
                Objects[ObjIdx].Data = new byte[ObjDataLength];
                Array.Copy(eObjFile, Objects[ObjIdx].Offset, Objects[ObjIdx].Data, 0, ObjDataLength);
            }

            // Finally for objects, I have to calculate the width and height. x_x
            for (int ObjIdx = 0; ObjIdx < ObjectCount; ObjIdx++) {
                int ObjWidth = 0;
                int ObjHeight = 0;
                int ObjDataPos = 0;
                int CurLineWidth = 0;
                while (Objects[ObjIdx].Data[ObjDataPos] != 0xFF) {
                    CurLineWidth = 0;
                    // Skip past slope control byte
                    if ((Objects[ObjIdx].Data[ObjDataPos] & 0x80) != 0) {
                        // This is just part of the slope screwery
                        if ((Objects[ObjIdx].Data[ObjDataPos] & 4) != 0) {
                            ObjHeight -= 1;
                        }
                        ObjDataPos += 1;
                    }
                    while (Objects[ObjIdx].Data[ObjDataPos] != 0xFE) {
                        ObjDataPos += 3;
                        CurLineWidth += 1;
                    }
                    if (CurLineWidth > ObjWidth) ObjWidth = CurLineWidth;
                    ObjHeight += 1;
                    ObjDataPos += 1;
                }
                Objects[ObjIdx].Width = ObjWidth;
                Objects[ObjIdx].Height = ObjHeight;
            }

            // Finally, load overrides
            if (OverrideFlag) {
                UseOverrides = true;
                OverrideBitmap = Properties.Resources.tileoverrides;
#if !USE_GDIPLUS
                OverrideGraphics = Graphics.FromImage(OverrideBitmap);
                OverrideHDC = OverrideGraphics.GetHdc();
                OverrideHandle = OverrideBitmap.GetHbitmap();
                GDIImports.SelectObject(OverrideHDC, OverrideHandle);
#endif
            }

            //GDIImports.DeleteObject(Map16BufferHandle);
            //Map16Graphics.ReleaseHdc(Map16BufferHDC);
        }

        ~NSMBTileset() {
#if !USE_GDIPLUS
            try {
                GDIImports.DeleteObject(Map16BufferHandle);
                Map16Graphics.ReleaseHdc(Map16BufferHDC);
            } catch {
            }

            if (UseOverrides) {
                GDIImports.DeleteObject(OverrideHandle);
                OverrideGraphics.ReleaseHdc(OverrideHDC);
            }
#endif
        }

        public ushort GFXFileID;
        public ushort PalFileID;
        public ushort Map16FileID;
        public ushort ObjFileID;
        public ushort ObjIndexFileID;

        public Bitmap Map16Buffer;
#if !USE_GDIPLUS
        private Graphics Map16Graphics;
        public IntPtr Map16BufferHDC;
        private IntPtr Map16BufferHandle;
#endif

        public bool UseOverrides;
        public Bitmap OverrideBitmap;
#if !USE_GDIPLUS
        private Graphics OverrideGraphics;
        public IntPtr OverrideHDC;
        private IntPtr OverrideHandle;
#endif

        public bool UseNotes;
        public string[] ObjNotes;

        public ObjectDef[] Objects;

        public struct ObjectDef {
            public int Offset;
            public int Width;
            public int Height;
            public byte[] Data;

            public ObjectDef(int Offset, int Width, int Height, byte[] Data) {
                this.Offset = Offset;
                this.Width = Width;
                this.Height = Height;
                this.Data = Data;
            }
        }

        public int[,] RenderObject(int ObjNum, int Width, int Height) {
            // First allocate an array
            int[,] Dest = new int[Width, Height];

            // Non-existent objects can just be made out of 0s
            if (ObjNum >= Objects.Length) {
                return Dest;
            }

            // Diagonal objects are rendered differently
            if ((Objects[ObjNum].Data[0] & 0x80) != 0) {
                RenderDiagonalObject(Dest, ObjNum, Width, Height);
            } else {
                Size Rendered = RenderStandardObject(Dest, ObjNum, 0, 0, Width, Height, Width, Height);
                int RemainingX = (int)Math.Ceiling((double)(Width - Rendered.Width) / Rendered.Width);
                int RemainingY = (int)Math.Ceiling((double)(Height - Rendered.Height) / Rendered.Height);
                for (int RepeatX = 0; RepeatX < RemainingX; RepeatX++) {
                    RenderStandardObject(Dest, ObjNum, Rendered.Width * (RepeatX + 1), 0, Rendered.Width, Rendered.Height, Width, Height);
                }
                for (int RepeatY = 0; RepeatY < RemainingY; RepeatY++) {
                    RenderStandardObject(Dest, ObjNum, 0, Rendered.Height * (RepeatY + 1), Rendered.Width, Rendered.Height, Width, Height);
                }
                if (RemainingX > 0 && RemainingY > 0) {
                    for (int RepeatX = 0; RepeatX < RemainingX; RepeatX++) {
                        for (int RepeatY = 0; RepeatY < RemainingY; RepeatY++) {
                            RenderStandardObject(Dest, ObjNum, Rendered.Width * (RepeatX + 1), Rendered.Height * (RepeatY + 1), Rendered.Width, Rendered.Height, Width, Height);
                        }
                    }
                }
            }

            /*int XLoops = (int)Math.Ceiling((float)Width / (float)Objects[ObjNum].Width);
            int YLoops = (int)Math.Ceiling((float)Height / (float)Objects[ObjNum].Height);

            int destX = 0;
            int destY = 0;
            for (int CurXLoop = 0; CurXLoop < XLoops; CurXLoop++) {
                for (int CurYLoop = 0; CurYLoop < YLoops; CurYLoop++) {
                    CopyLoopedObject(Dest, ObjNum, destX, destY, 1, 2);
                    destY += Objects[ObjNum].Height;
                }
                destX += Objects[ObjNum].Width;
                destY = 0;
            }*/
            

            return Dest;
        }

        private struct ObjTile {
            public byte ControlByte;
            public int TileID;
        }

        private Size RenderStandardObject(int[,] Dest, int ObjNum, int XOffset, int YOffset, int Width, int Height, int MaxXBound, int MaxYBound) {
            Size ReturnVal = new Size();

            // First of all clear this all out to blank tiles
            for (int clearX = XOffset; clearX < Math.Min(XOffset + Width, MaxXBound); clearX++) {
                for (int clearY = YOffset; clearY < Math.Min(YOffset + Height, MaxYBound); clearY++) {
                    Dest[clearX, clearY] = -1;
                }
            }

            ObjTile t = new ObjTile();

            int a = XOffset, b = YOffset;
            int vrs = -1, vre = -1;
            int hrs = -1, hre = -1;

            byte lr = 0;

            byte[] ObjData = Objects[ObjNum].Data; // Create a reference for quicker access
            int FilePos = 0;

            // At the start of the file, there will always be an object so this assumption is safe
            if ((ObjData[FilePos] & 0x80) != 0) {
                FilePos++;
            }

            while (true) {
                if (ObjData[FilePos] == 0xFE) {
                    // Linebreak
                    // Here if the last tile was an X repeat, this means we never processed X looping earlier
                    // So let's do it here.
                    if ((lr & 1) != 0 && b < YOffset + Height && a < XOffset + Width && b < MaxYBound && a < MaxXBound && hre > hrs) {
                        for (int i = a; i < Width; i++) {
                            if (i < Width) {
                                Dest[i, b] = Dest[a + (i % (hre - hrs)) - (hre - hrs), b];
                                if (i > ReturnVal.Width) ReturnVal.Width = i;
                                if (b > ReturnVal.Height) ReturnVal.Height = b;
                            }
                        }
                    }
                    b++; // increment Y
                    a = XOffset; // reset X
                    lr = 0; // reset last repeat flag
                    hrs = -1; hre = -1; // reset h-repeat start/end
                    FilePos++; // eat a byte
                    if ((ObjData[FilePos] & 0x80) != 0 && ObjData[FilePos] != 0xFF) {
                        // Same as earlier - Basically, if the first byte in a line has bit 7 set,
                        // it's a diagonal object. I skip over it since it's not part of the control byte.
                        FilePos++;
                    }
                }
                if (ObjData[FilePos] == 0xFF) {
                    break; // tile over
                }
                // Parse a tile
                // I know this is a totally WTFy method of grabbing the tile ID. But for some reason it works.
                t.ControlByte = ObjData[FilePos];
                t.TileID = ObjData[FilePos + 1] + ((((ObjData[FilePos + 2] > 0) ? ObjData[FilePos + 2] - 1 : ObjData[FilePos + 2]) % 3) * 256);
                if ((t.ControlByte & 64) != 0) t.TileID += 768;
                if (t.TileID == 0 && t.ControlByte == 0) t.TileID = -1;
                FilePos += 3;

                if (vrs < 0 && ((t.ControlByte & 2) != 0)) vrs = b;
                if (vrs >= 0 && vre < 0 && ((t.ControlByte & 2) == 0)) vre = b;

                if ((t.ControlByte & 1) == 0) {
                    if ((lr & 1) != 0) {
                        // H-repeating region ended
                        if (b < YOffset + Height) {
                            for (int i = a; i < XOffset + Width; i++) {
                                if (i < MaxXBound && b < MaxYBound) {
                                    Dest[i, b] = Dest[a + (i % (hre - hrs)) - (hre - hrs), b];
                                    if (i > ReturnVal.Width) ReturnVal.Width = i;
                                    if (b > ReturnVal.Height) ReturnVal.Height = b;
                                }
                            }
                        }
                        a = Width - 1;
                    }
                } else {
                    if ((lr & 1) == 0) hrs = hre = a; //this might be the start of the repeated region...
                    hre++; //...either way, it shifts its end to the right by one
                }
                if (a != 0 && a == (XOffset + Width - 1) && (lr & 1) == 0 && hre >= 0 && b < Height && a < MaxXBound && b < MaxYBound) {
                    //Dest[a - 1, b] = Dest[a, b];
                }
                if (a < XOffset + Width && b < YOffset + Height && a < MaxXBound && b < MaxYBound) {
                    Dest[a, b] = t.TileID;
                    if (a > ReturnVal.Width) ReturnVal.Width = a;
                    if (b > ReturnVal.Height) ReturnVal.Height = b;
                }

                //if (a != (Width - 1)) a++; // move 1 to the right
                //if ((t.ControlByte & 1) != 0 || a != (Width - 1)) a++;
                //if ((t.ControlByte & 1) != 0) a++;
                a++;

                lr = t.ControlByte;
            }

            if (vrs < 0) vrs = b;
            if (vre < 0) vre = b;

            // now stretch vertically; direction of loops IS important
            // relocate bottom lines...
            for (int i = b - 1; i >= vre; i--) {
                if (i < YOffset + Height) {
                    for (int CopyOffset = XOffset; CopyOffset < XOffset + Width; CopyOffset++) {
                        if (CopyOffset < MaxXBound) {
                            Dest[CopyOffset, YOffset + Height + i - b] = Dest[CopyOffset, i];
                            if ((Height + i - b) > ReturnVal.Height) ReturnVal.Height = (Height + i - b);
                        }
                    }
                }
            }
            // and copypaste the replicated zone into the gap
            if (vre != vrs) {
                for (int i = YOffset + Height + vre - b - 1; i >= vre; i--) {
                    for (int CopyOffset = XOffset; CopyOffset < XOffset + Width; CopyOffset++) {
                        if (CopyOffset < MaxXBound && i < MaxYBound) {
                            Dest[CopyOffset, i] = Dest[CopyOffset, vrs + (i % (vre - vrs))];
                            if (i > ReturnVal.Height) ReturnVal.Height = i;
                        }
                    }
                }
            }

            ReturnVal.Height += 1;
            ReturnVal.Width += 1;
            return ReturnVal;
        }

        private void CopyLoopedObject(int[,] Dest, int ObjNum, int X, int Y, int CanLoopX, int CanLoopY) {
            // Changed the algorithm to something that works better
            // Basically now it relies on the inbuilt linefeeds like NSMBe3 did
            // and ignores the width/height listed in the object indexes file.
            int destX = X;
            int destY = Y;
            int ObjDataPos = 0;
            //bool RowBlank = true;
            while (true) { // Y loop
                if (destY >= Dest.GetLength(1)) break;
                if (Objects[ObjNum].Data[ObjDataPos] == 0xFF) {
                    break;
                }
                // Ignore diagonal object control data
                if ((Objects[ObjNum].Data[ObjDataPos] & 0x80) != 0) {
                    ObjDataPos++;
                }
                while (true) { // X loop
                    if (destX >= Dest.GetLength(0)) {
                        while (Objects[ObjNum].Data[ObjDataPos] != 0xFE) {
                            ObjDataPos += 3;
                        }
                        break;
                    }
                    if ((CanLoopX != -1 && (Objects[ObjNum].Data[ObjDataPos] & CanLoopX) == 0) || (CanLoopY != -1 && (Objects[ObjNum].Data[ObjDataPos] & CanLoopY) == 0)) {
                        ObjDataPos += 3;
                    } else {
                        ObjDataPos++;
                        if (destX >= 0 && destY >= 0) {
                            //RowBlank = false;
                            // Ignore completely blank tiles
                            if (Objects[ObjNum].Data[ObjDataPos - 1] != 0 || Objects[ObjNum].Data[ObjDataPos] != 0 || Objects[ObjNum].Data[ObjDataPos + 1] != 0) {
                                Dest[destX, destY] = Objects[ObjNum].Data[ObjDataPos] + (((Objects[ObjNum].Data[ObjDataPos + 1] > 0) ? Objects[ObjNum].Data[ObjDataPos + 1] - 1 : Objects[ObjNum].Data[ObjDataPos + 1]) * 256);
                            } else {
                                Dest[destX, destY] = -1;
                            }
                        }
                        ObjDataPos += 2;
                        destX++;
                    }
                    if (Objects[ObjNum].Data[ObjDataPos] == 0xFE) {
                        break;
                    }
                }
                destX = X;
                //if (!RowBlank) {
                destY++;
                //}
                //RowBlank = true;
                ObjDataPos++;
            }
            /*int destX = X;
            int destY = Y;
            int ObjDataPos = 0;
            for (int srcY = 0; srcY < Objects[ObjNum].Height; srcY++) {
                if (destY >= Dest.GetLength(1)) break;
                for (int srcX = 0; srcX < Objects[ObjNum].Width; srcX++) {
                    if (destX >= Dest.GetLength(0)) {
                        while (Objects[ObjNum].Data[ObjDataPos] != 0xFE) {
                            ObjDataPos++;
                        }
                        break;
                    }
                    ObjDataPos++;
                    // Yet another one of the weird algorithms that doesn't make sense but works from NSMBe3
                    Dest[destX, destY] = Objects[ObjNum].Data[ObjDataPos] + (((Objects[ObjNum].Data[ObjDataPos + 1] > 0) ? Objects[ObjNum].Data[ObjDataPos + 1] - 1 : Objects[ObjNum].Data[ObjDataPos + 1]) * 256);
                    ObjDataPos += 2;
                    destX++;
                }
                destX = X;
                destY++;
                ObjDataPos++;
            }*/
        }

        private void RenderDiagonalObject(int[,] Dest, int ObjNum, int Width, int Height) {
            // First of all clear this all out to blank tiles
            for (int clearX = 0; clearX < Width; clearX++) {
                for (int clearY = 0; clearY < Height; clearY++) {
                    Dest[clearX, clearY] = -1;
                }
            }

            int XLoops = (int)Math.Ceiling((float)Width / (float)Objects[ObjNum].Width);

            int destX;
            int destY;
            int XStep;
            int YStep;

            // Okay, depending on the direction this changes
            if ((Objects[ObjNum].Data[0] & 1) != 0) {
                // Y goes down
                destY = 0;
                YStep = Objects[ObjNum].Height;
            } else {
                // Y goes up
                destY = (int)((Height - Objects[ObjNum].Height) / Objects[ObjNum].Height) * Objects[ObjNum].Height;
                YStep = Objects[ObjNum].Height * -1;
            }
            
            destX = 0;
            XStep = Objects[ObjNum].Width;

            for (int CurXLoop = 0; CurXLoop < XLoops; CurXLoop++) {
                CopyLoopedObject(Dest, ObjNum, destX, destY, -1, -1);
                destX += XStep;
                destY += YStep;
            }

            /*int destX = 0;
            int destY = 0;
            int ObjDataPos = 0;
            int LineStartPos;
            while (true) { // Y loop
                if (Objects[ObjNum].Data[ObjDataPos] == 0xFF) {
                    ObjDataPos = 0;
                }
                ObjDataPos++;
                LineStartPos = ObjDataPos;
                while (true) { // X loop
                    ObjDataPos++;
                    Dest[destX, destY] = Objects[ObjNum].Data[ObjDataPos] + (((Objects[ObjNum].Data[ObjDataPos + 1] > 0) ? Objects[ObjNum].Data[ObjDataPos + 1] - 1 : Objects[ObjNum].Data[ObjDataPos + 1]) * 256);
                    ObjDataPos += 2;
                    if (Objects[ObjNum].Data[ObjDataPos] == 0xFE) {
                        ObjDataPos = LineStartPos;
                    }
                    destX++;
                    if (destX == Width) {
                        // Skip past to next FE
                        while (Objects[ObjNum].Data[ObjDataPos] != 0xFE) {
                            ObjDataPos += 3;
                        }
                        break;
                    }
                }
                destX = 0;
                destY++;
                ObjDataPos++;
                if (destY == Height) break;
            }*/
        }

#if USE_GDIPLUS
        public void RenderCachedObject(Graphics g, int[,] Obj, int X, int Y) {
            Rectangle srcRect = new Rectangle();
            Rectangle destRect = new Rectangle(X, Y, 16, 16);
            for (int CurrentX = 0; CurrentX < Obj.GetLength(0); CurrentX++) {
                for (int CurrentY = 0; CurrentY < Obj.GetLength(1); CurrentY++) {
                    srcRect = new Rectangle(Obj[CurrentX, CurrentY] * 16, 0, 16, 16);
                    g.DrawImage(Map16Buffer, destRect, srcRect, GraphicsUnit.Pixel);
                    destRect.Y += 16;
                }
                destRect.X += 16;
                destRect.Y = Y;
            }
        }
#else
        public void RenderCachedObject(IntPtr target, int[,] Obj, int X, int Y) {
            int destX = X;
            int destY = Y;
            for (int CurrentX = 0; CurrentX < Obj.GetLength(0); CurrentX++) {
                for (int CurrentY = 0; CurrentY < Obj.GetLength(1); CurrentY++) {
                    if (Obj[CurrentX, CurrentY] >= 768 && UseOverrides) {
                        GDIImports.BitBlt(target, destX, destY, 16, 16, OverrideHDC, (Obj[CurrentX, CurrentY] - 768) * 16, 0, GDIImports.TernaryRasterOperations.SRCCOPY);
                    } else {
                        GDIImports.BitBlt(target, destX, destY, 16, 16, Map16BufferHDC, Obj[CurrentX, CurrentY] * 16, 0, GDIImports.TernaryRasterOperations.SRCCOPY);
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
