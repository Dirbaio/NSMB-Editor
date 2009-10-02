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
 * Object index file:
 *  - 4 bytes per object
 *  - Defines offsets in the Object file
 *  - Offset as ushort
 *  - Width and Height as byte
 *  
 * Object file:
 *  - Data for each object.
 *  
 * 0xFF - End of object
 * 0xFE - New Line
 * & 0x80 - Slope Control Byte
 * Else, its the beginning of a tile:
 *   Control Byte
 *   Map16 tile number as ushort
 *   
 * STANDARD OBJECTS:
 * 
 * Tile control byte:
 *  1 = horizontal repeat
 *  2 = vertical repeat
 *  
 * If no repeats, repeat all.
 * If there are repeats, divide everyting in 3:
 *    Before repeat (no repeat set)
 *    In repeat     (repeat set)
 *    After repeat  (no repeat set)
 *    
 * Then put the before repeat at the beginning.
 * The after repeat at the end
 * And then fill the space between them (if any) repeating the In repeat tiles.
 * 
 * SLOPED OBJECTS:
 * 
 * These objects are a pain to work with.
 * 
 * Slope anchors depending on first slope control byte:
 * 
 *   byte   anchor           direction
 *   ==============================
 *   0x80:  Bottom left      Top right
 *   0x81:  Bottom right     Top left
 *   0x82:  Top left         Bottom right
 *   0x83:  Top right        Bottom left
 *   0x84:  Top left         Bottom right
 *   
 * (The last three are only used at the ghost house tileset)
 * 
 * The slope format is:
 * 
 * A slope control indicating it's a slope and its anchor.
 * Then follows a rectangular block of tiles. These have to be placed
 * corner-by-corner, respecting their size, like this:
 * 
 *              _|_|
 *      _      | |             __
 *    _|_|    _|_|          __|__|
 *  _|_|     | |         __|__|
 * |_|       |_|        |__|
 * 
 * Optional: Then follows a 0x85 slope control, then another block of tiles 
 * that has to be placed UNDER the previous blocks, and they are also UNDER the anchor.
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
            byte[] ePalFile = FileSystem.LZ77_Decompress(ROM.ExtractFile(PalFile));
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
            byte[] eGFXFile = FileSystem.LZ77_Decompress(ROM.ExtractFile(GFXFile));
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
                byte[] data = new byte[ObjDataLength];
                Array.Copy(eObjFile, Objects[ObjIdx].Offset, data, 0, ObjDataLength);
                Objects[ObjIdx].setData(data);
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

        public class ObjectDef {
            public int Offset;
            public int Width;
            public int Height;
            public byte[] Data;

            public List<List<ObjectDefTile>> tiles;

            public ObjectDef() { }

            public ObjectDef(int Offset, int Width, int Height, byte[] Data) {
                this.Offset = Offset;
                this.Width = Width;
                this.Height = Height;
                setData(Data);
            }
            public void setData(byte[] Data)
            {
                this.Data = Data;

                ByteArrayInputStream inp = new ByteArrayInputStream(Data);
                tiles = new List<List<ObjectDefTile>>();
                List<ObjectDefTile> row = new List<ObjectDefTile>();

                while (true)
                {
                    ObjectDefTile t = new ObjectDefTile(inp);
                    if (t.lineBreak)
                    {
                        tiles.Add(row);
                        row = new List<ObjectDefTile>();
                    }
                    else if (t.objectEnd)
                        break;
                    else
                        row.Add(t);
                }
            }
        }

        public class ObjectDefTile
        {
            public int tileID;
            public byte controlByte;

            public bool emptyTile = false;

            public bool lineBreak = false;
            public bool objectEnd = false;
            public bool slopeControl = false;

            public bool controlTile = false; // any type of nondisplaying tile

            public ObjectDefTile(ByteArrayInputStream inp)
            {
                controlByte = inp.readByte();

                if (controlByte == 0xFE) //line break
                    lineBreak = true;
                else if (controlByte == 0xFF)
                    objectEnd = true;
                else if ((controlByte & 0x80) > 0) //slope control byte
                    slopeControl = true;
                else
                {
                    byte a, b;
                    a = inp.readByte();
                    b = inp.readByte();

                    tileID = a + (((b != 0) ? b - 1 : 0) % 3 << 8);

                    if ((controlByte & 64) != 0)
                        tileID += 768;
                    if (tileID == 0 && controlByte == 0)
                    {
                        tileID = -1;
                        emptyTile = true;
                    }
                }
                controlTile = lineBreak || objectEnd || slopeControl;
            }
        }

        public int[,] RenderObject(int ObjNum, int Width, int Height) {
            // First allocate an array
            int[,] Dest = new int[Width, Height];
            // Non-existent objects can just be made out of 0s
            if (ObjNum >= Objects.Length)
            {
                return Dest;
            }

            ObjectDef obj = Objects[ObjNum];


            // Diagonal objects are rendered differently
            if ((Objects[ObjNum].Data[0] & 0x80) != 0) {
                RenderDiagonalObject(Dest, obj, Width, Height);
            } else {

                bool repeatFound = false;
                List<List<ObjectDefTile>> beforeRepeat = new List<List<ObjectDefTile>>();
                List<List<ObjectDefTile>> inRepeat = new List<List<ObjectDefTile>>();
                List<List<ObjectDefTile>> afterRepeat = new List<List<ObjectDefTile>>();

                foreach (List<ObjectDefTile> row in obj.tiles)
                {
                    if (row.Count == 0)
                        continue;

                    if ((row[0].controlByte & 2) != 0)
                    {
                        repeatFound = true;
                        inRepeat.Add(row);
                    }
                    else
                    {
                        if (repeatFound)
                            afterRepeat.Add(row);
                        else
                            beforeRepeat.Add(row);
                    }
                }

                for (int y = 0; y < Height; y++)
                {
                    if (inRepeat.Count == 0) //if no repeat data, just repeat all
                        renderStandardRow(Dest, beforeRepeat[y % beforeRepeat.Count], y, Width);
                    else if (y < beforeRepeat.Count) //if repeat data
                        renderStandardRow(Dest, beforeRepeat[y], y, Width);
                    else if (y > Height - afterRepeat.Count - 1)
                        renderStandardRow(Dest, afterRepeat[y - Height + afterRepeat.Count], y, Width);
                    else
                        renderStandardRow(Dest, inRepeat[(y - beforeRepeat.Count) % inRepeat.Count], y, Width);
                }

                /*Size Rendered = RenderStandardObject(Dest, ObjNum, 0, 0, Width, Height, Width, Height);
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
                }*/
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

        private void renderStandardRow(int[,] Dest, List<ObjectDefTile> row, int y, int width)
        {
            bool repeatFound = false;
            List<ObjectDefTile> beforeRepeat = new List<ObjectDefTile>();
            List<ObjectDefTile> inRepeat =     new List<ObjectDefTile>();
            List<ObjectDefTile> afterRepeat =  new List<ObjectDefTile>();

            foreach (ObjectDefTile tile in row)
            {
                if ((tile.controlByte & 1) != 0)
                {
                    repeatFound = true;
                    inRepeat.Add(tile);
                }
                else
                {
                    if (repeatFound)
                        afterRepeat.Add(tile);
                    else
                        beforeRepeat.Add(tile);
                }
            }

            for (int x = 0; x < width; x++)
            {
                if (inRepeat.Count == 0) //if no repeat data, just repeat all
                    Dest[x, y] = beforeRepeat[x % beforeRepeat.Count].tileID;
                else if (x < beforeRepeat.Count) //if repeat data
                    Dest[x, y] = beforeRepeat[x].tileID;
                else if (x > width - afterRepeat.Count - 1)
                    Dest[x, y] = afterRepeat[x - width + afterRepeat.Count].tileID;
                else
                    Dest[x, y] = inRepeat[(x - beforeRepeat.Count) % inRepeat.Count].tileID;
            }
        }

        private void RenderDiagonalObject(int[,] Dest, ObjectDef obj, int width, int height)
        {
            //empty tiles fill
            for (int xp = 0; xp < width; xp++)
                for (int yp = 0; yp < height; yp++)
                    Dest[xp, yp] = -1;

            //find out direction:
            byte controlByte = obj.tiles[0][0].controlByte;
            bool goLeft = controlByte == 0x81 || controlByte == 0x82 || controlByte == 0x84; //note: this means go top left or bottom right

            //find out anchor:
            bool topAnchor = controlByte >= 0x82 && controlByte <= 0x84;

            //find vertical increment
            int yi = 0;
            bool bottomControlFound = false;
            foreach (List<ObjectDefTile> row in obj.tiles)
            {
                if (row[0].controlByte == 0x85)
                    bottomControlFound = true;

                if (!bottomControlFound)
                    yi++;
            }
            if (yi == 0) yi = 1;

            //find horizontal increment
            int xi = countTiles(obj.tiles[0]);
            if (xi == 0) xi = 1;
            if (goLeft) xi = -xi;

            //starting position
            int x = goLeft ? width - 1 : 0;
            int y = height - yi;
            if (topAnchor)
            {
                int numBlocks = height / yi;
                if (numBlocks * yi != height) //round up
                    numBlocks++;

                y = numBlocks * yi;
                if (goLeft)
                    x = numBlocks * xi - xi;
                else
                    x = width - numBlocks * xi - xi;
            }

            //render the slope
            while (x > -1 && x < width + 2 && y >= -obj.tiles.Count)
            {
                int yy = y;

                foreach (List<ObjectDefTile> row in obj.tiles)
                {
                    int xx = x;
                    if (goLeft)
                        xx -= countTiles(row) - 1;
                    /*
                    if (bottomControlFound && upMove > 1)
                        xx--;*/

                    foreach (ObjectDefTile tile in row)
                    {
                        if (tile.controlTile)
                            continue;
                        putTile(Dest, xx, yy, width, height, tile);
                        xx++;
                    }
                    yy++;
                }
                y -= yi;
                x += xi;
            }
        }
        
        private int countTiles(List<ObjectDefTile> l)
        {
            int res = 0;
            foreach (ObjectDefTile t in l)
                if (!t.controlTile)
                    res++;
            return res;
        }

        private void putTile(int[,] Dest, int x, int y, int width, int height, ObjectDefTile t)
        {
            if (x >= 0 && x < width)
                if (y >= 0 && y < height)
                    Dest[x, y] = t.tileID;
        }


#if false
        private Size RenderStandardObject(int[,] Dest, int ObjNum, int XOffset, int YOffset, int Width, int Height, int MaxXBound, int MaxYBound) {
            Size ReturnVal = new Size();

            // First of all clear this all out to blank tiles
            for (int clearX = XOffset; clearX < Math.Min(XOffset + Width, MaxXBound); clearX++) {
                for (int clearY = YOffset; clearY < Math.Min(YOffset + Height, MaxYBound); clearY++) {
                    Dest[clearX, clearY] = -1;
                }
            }

            ObjTile t = new ObjTile();

            int x = XOffset, y = YOffset;
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
                if (ObjData[FilePos] == 0xFE)
                {
                    // Linebreak
                    // Here if the last tile was an X repeat, this means we never processed X looping earlier
                    // So let's do it here.
                    if ((lr & 1) != 0 && y < YOffset + Height && x < XOffset + Width && y < MaxYBound && x < MaxXBound && hre > hrs) {
                        for (int i = x; i < Width; i++) {
                            if (i < Width) {
                                Dest[i, y] = Dest[x + (i % (hre - hrs)) - (hre - hrs), y];
                                if (i > ReturnVal.Width) ReturnVal.Width = i;
                                if (y > ReturnVal.Height) ReturnVal.Height = y;
                            }
                        }
                    }
                    y++; // increment Y
                    x = XOffset; // reset X
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
                t.TileID = ObjData[FilePos + 1] + (((ObjData[FilePos + 2] != 0) ? ObjData[FilePos + 2]-1 : 0) %3 << 8);
                if ((t.ControlByte & 64) != 0) t.TileID += 768;
                if (t.TileID == 0 && t.ControlByte == 0) t.TileID = -1;
                FilePos += 3;

                if (vrs < 0 && ((t.ControlByte & 2) != 0)) vrs = y;
                if (vrs >= 0 && vre < 0 && ((t.ControlByte & 2) == 0)) vre = y;

                if ((t.ControlByte & 1) == 0)
                {
                    if ((lr & 1) != 0)
                    {
                        // H-repeating region ended
                        if (y < YOffset + Height) {
                            for (int i = x; i < XOffset + Width; i++) {
                                if (i < MaxXBound && y < MaxYBound) {
                                    Dest[i, y] = Dest[x + (i % (hre - hrs)) - (hre - hrs), y];
                                    if (i > ReturnVal.Width) ReturnVal.Width = i;
                                    if (y > ReturnVal.Height) ReturnVal.Height = y;
                                }
                            }
                        }
                        x = Width - 1;
                    }
                } else {
                    if ((lr & 1) == 0) hrs = hre = x; //this might be the start of the repeated region...
                    hre++; //...either way, it shifts its end to the right by one
                }
                if (x != 0 && x == (XOffset + Width - 1) && (lr & 1) == 0 && hre >= 0 && y < Height && x < MaxXBound && y < MaxYBound) {
                    //Dest[a - 1, b] = Dest[a, b];
                }
                if (x < XOffset + Width && y < YOffset + Height && x < MaxXBound && y < MaxYBound) {
                    Dest[x, y] = t.TileID;
                    if (x > ReturnVal.Width) ReturnVal.Width = x;
                    if (y > ReturnVal.Height) ReturnVal.Height = y;
                }

                //if (a != (Width - 1)) a++; // move 1 to the right
                //if ((t.ControlByte & 1) != 0 || a != (Width - 1)) a++;
                //if ((t.ControlByte & 1) != 0) a++;
                x++;

                lr = t.ControlByte;
            }

            if (vrs < 0) vrs = y;
            if (vre < 0) vre = y;

            // now stretch vertically; direction of loops IS important
            // relocate bottom lines...
            for (int i = y - 1; i >= vre; i--) {
                if (i < YOffset + Height) {
                    for (int CopyOffset = XOffset; CopyOffset < XOffset + Width; CopyOffset++) {
                        if (CopyOffset < MaxXBound) {
                            Dest[CopyOffset, YOffset + Height + i - y] = Dest[CopyOffset, i];
                            if ((Height + i - y) > ReturnVal.Height) ReturnVal.Height = (Height + i - y);
                        }
                    }
                }
            }
            // and copypaste the replicated zone into the gap
            if (vre != vrs) {
                for (int i = YOffset + Height + vre - y - 1; i >= vre; i--) {
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
    
        /*
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
        //}
#endif

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
