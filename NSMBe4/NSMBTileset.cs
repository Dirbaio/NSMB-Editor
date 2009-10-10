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
 *  - Width and Height as 2 bytes. 
 *    These are inaccurate, and my implementation doesn't use them
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
 * The first slope control byte defines the direction of slope:
 * 
 *  & with 1 -> Go left
 *  & with 2 -> Go Down
 *   
 * (The last three are only used at the ghost house tileset)
 * 
 * The slope format is:
 * 
 * A slope control indicating it's a slope and its direction.
 * Then follows a rectangular block of tiles. These have to be placed
 * corner-by-corner, respecting their size, like this:
 * 
 *              _|_|
 *      _      | |             __
 *    _|_|    _|_|          __|__|
 *  _|_|     | |         __|__|
 * |_|       |_|        |__|
 * 
 * The first corner must be placed on a corner of the object, on the opposite
 * side of the direction.
 * 
 * Optional: Then follows a 0x85 slope control, then another block of tiles 
 * that has to be placed under the previous blocks, or OVER if the slope goes down.
 * 
 * If the slope goes right the blocks have to be left-aligned:
 *  _
 * |_|__
 * |____|
 * 
 * If the slope goes left the blocks have to be right-aligned:
 *     _
 *  __|_|
 * |____|
 * 
 **/


namespace NSMBe4
{
   
    public class NSMBTileset
    {
        public NitroClass ROM;

        public ushort GFXFileID;
        public ushort PalFileID;
        public ushort Map16FileID;
        public ushort ObjFileID;
        public ushort ObjIndexFileID;
        public ushort TileBehaviorFileID;

        public int TilesetNumber; // 0 for Jyotyu, 1 for Normal, 2 for SubUnit

        public Bitmap Map16Buffer;
        public bool UseOverrides;
        public Bitmap OverrideBitmap;

        public bool UseNotes;
        public string[] ObjNotes;

        public ObjectDef[] Objects;

        public Map16Tile[] Map16;

        public byte[][] TileBehaviors;

        public Bitmap TilesetBuffer;
        private Graphics TilesetGraphics;
#if !USE_GDIPLUS
        IntPtr TilesetBufferHDC;
        IntPtr TilesetBufferHandle;
#endif

        private Graphics Map16Graphics;
#if !USE_GDIPLUS
        public IntPtr Map16BufferHDC;
        private IntPtr Map16BufferHandle;
#endif

#if !USE_GDIPLUS
        private Graphics OverrideGraphics;
        public IntPtr OverrideHDC;
        private IntPtr OverrideHandle;
#endif

        public NSMBTileset(NitroClass ROM, ushort GFXFile, ushort PalFile, ushort Map16File, ushort ObjFile, ushort ObjIndexFile, ushort TileBehaviorFile, bool OverrideFlag, int TilesetNumber)
        {
            int FilePos;

            this.ROM = ROM;

            GFXFileID = GFXFile;
            PalFileID = PalFile;
            Map16FileID = Map16File;
            ObjFileID = ObjFile;
            ObjIndexFileID = ObjIndexFile;
            TileBehaviorFileID = TileBehaviorFile;
            Console.Out.WriteLine(ROM.FileNames[TileBehaviorFile]);

            this.TilesetNumber = TilesetNumber;

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
            TilesetBuffer = new Bitmap(TileCount * 8, 16);

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


#if !USE_GDIPLUS
            TilesetGraphics = Graphics.FromImage(TilesetBuffer);
            TilesetBufferHDC = TilesetGraphics.GetHdc();
            TilesetBufferHandle = TilesetBuffer.GetHbitmap();
            GDIImports.SelectObject(TilesetBufferHDC, TilesetBufferHandle);
#endif
            loadMap16();
            loadTileBehaviors();

            // Finally the object file.
            loadObjects();

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
        }

        ~NSMBTileset()
        {
#if !USE_GDIPLUS
            try {
                GDIImports.DeleteObject(Map16BufferHandle);
                Map16Graphics.ReleaseHdc(Map16BufferHDC);
                GDIImports.DeleteObject(TilesetBufferHandle);
                TilesetGraphics.ReleaseHdc(TilesetBufferHDC);

                if (UseOverrides) {
                    GDIImports.DeleteObject(OverrideHandle);
                    OverrideGraphics.ReleaseHdc(OverrideHDC);
                }
            } catch {
            }

#endif
        }

        public void save()
        {
            saveObjects();
            saveMap16();
            saveTileBehaviors();
        }


        #region Tile Behaviors
        private void loadTileBehaviors()
        {
            if (TilesetNumber == 1)
            {
                byte[] tileBehaviorsFile = ROM.ExtractFile(TileBehaviorFileID);
                TileBehaviors = new byte[Map16.Length][];
                for (int i = 0; i < Map16.Length; i++)
                {
                    TileBehaviors[i] = new byte[4];
                    Array.Copy(tileBehaviorsFile, i*4, TileBehaviors[i], 0, 4);
                }
            }
        }

        private void saveTileBehaviors()
        {
            ByteArrayOutputStream file = new ByteArrayOutputStream();
            for (int i = 0; i < Map16.Length; i++)
                file.write(TileBehaviors[i]);

            ROM.ReplaceFile(TileBehaviorFileID, file.getArray());
        }

        #endregion
        #region Map16

        private void saveMap16()
        {
            ByteArrayOutputStream file = new ByteArrayOutputStream();
            foreach (Map16Tile t in Map16)
                t.save(file);

            ROM.ReplaceFile(Map16FileID, file.getArray());
        }

        private void loadMap16()
        {
            // Load Map16
            ByteArrayInputStream eMap16File = new ByteArrayInputStream(ROM.ExtractFile(Map16FileID));
            int Map16Count = eMap16File.available() / 8;
            Map16 = new Map16Tile[Map16Count];

            Map16Buffer = new Bitmap(Map16Count * 16, 16, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

            Map16Graphics = Graphics.FromImage(Map16Buffer);
            Map16Graphics.Clear(Color.LightSlateGray);
#if !USE_GDIPLUS
            Map16BufferHDC = Map16Graphics.GetHdc();
            Map16BufferHandle = Map16Buffer.GetHbitmap();
            GDIImports.SelectObject(Map16BufferHDC, Map16BufferHandle);
#endif


            for (int Map16Idx = 0; Map16Idx < Map16Count; Map16Idx++)
            {
                Map16[Map16Idx] = new Map16Tile(eMap16File);
                RenderMap16Tile(Map16Idx, TilesetNumber);
            }

        }


        private void RenderMap16Quarter(Map16Quarter q, int x, int y, int TilesetNumber)
        {
            int TileNum = q.TileNum;
            if (TilesetNumber == 1) TileNum -= 192;
            if (TilesetNumber == 2) TileNum -= 640;

            Rectangle SrcRect = new Rectangle(TileNum*8, ((q.ControlByte & 16) != 0) ? 8 : 0, 8, 8);
            Rectangle DestRect = new Rectangle(x, y, 8, 8);

            int xi = 7;
#if USE_GDIPLUS
            xi = 8;
#endif
            if ((q.ControlByte & 4) != 0) { DestRect.Width = -8; DestRect.X += xi; }
            if ((q.ControlByte & 8) != 0) { DestRect.Height = -8; DestRect.Y += xi; }
            if (q.TileNum != 0 || q.ControlByte != 0)
            {
#if USE_GDIPLUS
                Map16Graphics.DrawImage(TilesetBuffer, DestRect, SrcRect, GraphicsUnit.Pixel);
#else
                GDIImports.StretchBlt(Map16BufferHDC, DestRect.X, DestRect.Y, DestRect.Width, DestRect.Height, TilesetBufferHDC, SrcRect.X, SrcRect.Y, 8, 8, GDIImports.TernaryRasterOperations.SRCCOPY);
#endif
            }
        }

        private void RenderMap16Tile(int Map16Idx, int TilesetNumber)
        {
            Map16Tile t = Map16[Map16Idx];
            int x = Map16Idx*16;
            RenderMap16Quarter(t.topLeft, x, 0, TilesetNumber);
            RenderMap16Quarter(t.topRight, x + 8, 0, TilesetNumber);
            RenderMap16Quarter(t.bottomLeft, x, 8, TilesetNumber);
            RenderMap16Quarter(t.bottomRight, x + 8, 8, TilesetNumber);
        }

        public class Map16Tile
        {
            public Map16Quarter topLeft, topRight, bottomLeft, bottomRight;

            public Map16Tile() { }
            public Map16Tile(ByteArrayInputStream inp)
            {
                topLeft = new Map16Quarter(inp);
                topRight = new Map16Quarter(inp);
                bottomLeft = new Map16Quarter(inp);
                bottomRight = new Map16Quarter(inp);
            }

            public void save(ByteArrayOutputStream outp)
            {
                topLeft.save(outp);
                topRight.save(outp);
                bottomLeft.save(outp);
                bottomRight.save(outp);
            }
        }

        public class Map16Quarter
        {
            private byte ControlByteF;

            public byte ControlByte
            {
                get { return ControlByteF; }
                set { ControlByteF = value;}
            }
            private byte TileByteF;
            public byte TileByte
            {
                get{return TileByteF;}
                set{TileByteF = value;}
            }

            public int TileNum
            {
                get
                {
                    return TileByteF | ((ControlByte & 3) << 8);
                }
            }

            public Map16Quarter() { }
            public Map16Quarter(ByteArrayInputStream inp)
            {
                TileByteF = inp.readByte();
                ControlByteF = inp.readByte();
            }

            public void save(ByteArrayOutputStream outp)
            {
                outp.writeByte(TileByteF);
                outp.writeByte(ControlByteF);
            }
        }

        #endregion
        #region Objects
        public class ObjectDef
        {
            public List<List<ObjectDefTile>> tiles;
            public int width, height; //these are useless, but I keep them
                                      //in case the game uses them.

            public ObjectDef() { }
            public ObjectDef(byte[] data)
            {
                load(new ByteArrayInputStream(data));
            }

            public void load(ByteArrayInputStream inp)
            {
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

            public void save(ByteArrayOutputStream outp)
            {
                foreach (List<ObjectDefTile> row in tiles)
                {
                    foreach (ObjectDefTile t in row)
                        t.write(outp);
                    outp.writeByte(0xFE); //new line
                }
                outp.writeByte(0xFF); //end object
            }
        }

        public class ObjectDefTile
        {
            public int tileID;
            public byte controlByte;

            public bool emptyTile {
                get { return tileID == -1; }
            }

            public bool lineBreak {
                get { return controlByte == 0xFE; }
            }

            public bool objectEnd {
                get { return controlByte == 0xFF; }
            }

            public bool slopeControl
            {
                get { return (controlByte & 0x80) != 0; }
            }

            public bool controlTile {
                get { return lineBreak || objectEnd || slopeControl; }
            }

            public ObjectDefTile() { }
            public ObjectDefTile(ByteArrayInputStream inp)
            {
                controlByte = inp.readByte();

                if (!controlTile)
                {
                    byte a, b;
                    a = inp.readByte();
                    b = inp.readByte();

                    tileID = a + (((b != 0) ? b - 1 : 0)%3 << 8);

                    if ((controlByte & 64) != 0) //OVERRIDES
                        tileID += 768;
                    if (a == 0 && b == 0)
                        tileID = -1;
                }
            }

            public void write(ByteArrayOutputStream outp)
            {
                if (controlTile)
                    outp.writeByte(controlByte);
                else if(emptyTile)
                {
                    outp.writeByte(0);
                    outp.writeByte(0);
                    outp.writeByte(0);
                }
                else
                {
                    outp.writeByte(controlByte);
                    outp.writeByte((byte)(tileID % 256));
                    outp.writeByte((byte)(tileID / 256 + 1));
                }
            }
        }

        public void loadObjects()
        {
            ByteArrayInputStream eObjIndexFile = new ByteArrayInputStream(ROM.ExtractFile(ObjIndexFileID));
            ByteArrayInputStream eObjFile = new ByteArrayInputStream(ROM.ExtractFile(ObjFileID));

            Objects = new ObjectDef[256];

            //read object index
            int obj = 0;
            while (eObjIndexFile.available(4) && obj < Objects.Length)
            {
                Objects[obj] = new ObjectDef();
                int offset = eObjIndexFile.readUShort();
                Objects[obj].width = eObjIndexFile.readByte();
                Objects[obj].height = eObjIndexFile.readByte();

                eObjFile.seek(offset);
                Objects[obj].load(eObjFile);
                obj++;
            }
        }

        public void saveObjects()
        {
            ByteArrayOutputStream eObjIndexFile = new ByteArrayOutputStream();
            ByteArrayOutputStream eObjFile = new ByteArrayOutputStream();

            for (int i = 0; i < Objects.Length; i++)
            {
                if (Objects[i] == null)
                    break;

                eObjIndexFile.writeUShort((ushort)eObjFile.getPos());
                eObjIndexFile.writeByte((byte)Objects[i].width);
                eObjIndexFile.writeByte((byte)Objects[i].height);
                Objects[i].save(eObjFile);
            }

            ROM.ReplaceFile(ObjFileID, eObjFile.getArray());
            ROM.ReplaceFile(ObjIndexFileID, eObjIndexFile.getArray());
        }

        public int[,] RenderObject(int ObjNum, int Width, int Height)
        {
            // First allocate an array
            int[,] Dest = new int[Width, Height];

            // Non-existent objects can just be made out of 0s
            if (ObjNum >= Objects.Length || ObjNum < 0 || Objects[ObjNum] == null)
                return Dest;
            
            ObjectDef obj = Objects[ObjNum];

            if (Objects[ObjNum].tiles.Count == 0)
                return Dest;

            // Diagonal objects are rendered differently
            if ((Objects[ObjNum].tiles[0][0].controlByte & 0x80) != 0)
            {
                RenderDiagonalObject(Dest, obj, Width, Height);
            }
            else
            {

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

            }
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

#if false
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
                    x = numBlocks * xi;
                else
                    x = width - numBlocks * xi - xi;
            }

            if (goLeft) xi = -xi;

            //render the slope
            while ((x > -1 || !goLeft) && (x < width + 2 || goLeft) && y >= -obj.tiles.Count)
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
#endif

        private void RenderDiagonalObject(int[,] Dest, ObjectDef obj, int width, int height)
        {
            //empty tiles fill
            for (int xp = 0; xp < width; xp++)
                for (int yp = 0; yp < height; yp++)
                    Dest[xp, yp] = -1;

            //get sections
            List<ObjectDefTile[,]> sections = getSlopeSections(obj);
            ObjectDefTile[,] mainBlock = sections[0];
            ObjectDefTile[,] subBlock = null;
            if (sections.Count > 1)
                subBlock = sections[1];

            byte controlByte = obj.tiles[0][0].controlByte;

            //get direction
            bool goLeft = (controlByte & 1) != 0;
            bool goDown = (controlByte & 2) != 0;

            //get starting point

            int x = 0;
            int y = 0;
            if (!goDown)
                y = height - mainBlock.GetLength(1);
            if (goLeft)
                x = width - mainBlock.GetLength(0);

            //get increments
            int xi = mainBlock.GetLength(0);
            if (goLeft)
                xi = -xi;

            int yi = mainBlock.GetLength(1);
            if (!goDown)
                yi = -yi;

            //this is a strange stop condition.
            //Put tells if we have put a tile in the destination
            //When we don't put any tile in destination we are completely
            //out of bounds, so stop.
            bool put = true;
            while (put)
            {
                put = false;
                putArray(Dest, x, y, mainBlock, width, height, ref put);
                if (subBlock != null)
                {
                    int xb = x;
                    if (goLeft) // right align
                        xb = x + mainBlock.GetLength(0) - subBlock.GetLength(0);
                    if(goDown)
                        putArray(Dest, xb, y - subBlock.GetLength(1), subBlock, width, height, ref put);
                    else
                        putArray(Dest, xb, y + mainBlock.GetLength(1), subBlock, width, height, ref put);
                }
                x += xi;
                y += yi;
            }
        }

        private void putArray(int[,] Dest, int xo, int yo, ObjectDefTile[,] block, int width, int height, ref bool put)
        {
            for (int x = 0; x < block.GetLength(0); x++)
                for (int y = 0; y < block.GetLength(1); y++)
                    putTile(Dest, x + xo, y + yo, width, height, block[x, y], ref put);
        }

        private List<ObjectDefTile[,]> getSlopeSections(ObjectDef d)
        {
            List<ObjectDefTile[,]> sections = new List<ObjectDefTile[,]>();
            List<List<ObjectDefTile>> currentSection = null;

            foreach (List<ObjectDefTile> row in d.tiles)
            {
                if (row.Count > 0 && row[0].slopeControl) // begin new section
                {
                    if (currentSection != null)
                        sections.Add(createSection(currentSection));
                    currentSection = new List<List<ObjectDefTile>>();
                }
                currentSection.Add(row);
            }
            if (currentSection != null) //end last section
                sections.Add(createSection(currentSection));

            return sections;
        }

        private ObjectDefTile[,] createSection(List<List<ObjectDefTile>> tiles)
        {
            //calculate width
            int width = 0;
            foreach (List<ObjectDefTile> row in tiles)
            {
                int thiswidth = countTiles(row);
                if (width < thiswidth)
                    width = thiswidth;
            }

            //allocate array
            ObjectDefTile[,] section = new ObjectDefTile[width, tiles.Count];
            for (int y = 0; y < tiles.Count; y++)
            {
                int x = 0;
                foreach(ObjectDefTile t in tiles[y])
                    if (!t.controlTile)
                    {
                        section[x, y] = t;
                        x++;
                    }
            }

            return section;
        }

        private int countTiles(List<ObjectDefTile> l)
        {
            int res = 0;
            foreach (ObjectDefTile t in l)
                if (!t.controlTile)
                    res++;
            return res;
        }

        private void putTile(int[,] Dest, int x, int y, int width, int height, ObjectDefTile t, ref bool put)
        {
            if (x >= 0 && x < width)
                if (y >= 0 && y < height)
                {
                    put = true;
                    if(t != null)
                        Dest[x, y] = t.tileID;
                }
        }


        public bool objectExists(int objNum)
        {
            if (objNum < 0) return false;
            if (objNum >= Objects.Length) return false;
            if (Objects[objNum] == null) return false;
            return true;
        }
        #endregion

    }
}
