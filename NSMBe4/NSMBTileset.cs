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
using System.Drawing.Drawing2D;
using NSMBe4.DSFileSystem;
using System.Drawing.Imaging;
using System.Windows.Forms;
using NSMBe4.TilemapEditor;

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
 *  - Defines offsets in the Object file
 *  - 4 bytes per object
 *    - Offset as ushort
 *    - Width and Height as 2 bytes. 
 *      These are unused by the game, and are inaccurate for slopes,
 *      so my implementation doesn't use them
 *  
 * Object file:
 *  - Data for each object.
 *  
 * 0xFF - End of object
 * 0xFE - New Line
 * 0x8x - Slope Control Byte
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
 *  & with 2 -> Upside-down slope
 *   
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
 * side of the direction (if slope goes right, start is at left), and at the 
 * bottom, or at the top if its an upside-down slope.
 * 
 * Optional: Then follows a 0x85 slope control, then another block of tiles 
 * that has to be placed under the previous blocks, or OVER if its an upside-down slope.
 * 
 * If the slope goes right the blocks have to be left-aligned:
 *  _
 * |_|__  main block
 * |____| sub (0x85) block
 * 
 * If the slope goes left the blocks have to be right-aligned:
 *     _
 *  __|_|
 * |____|
 * 
 * EXAMPLE: Slope going up right with 1x1 main block and 2x1 sub block in a 6x5 obj
 *        _ _
 *      _|_|_|
 *    _|_|___|
 *  _|_|___|
 * |_|___| 
 * 
 * NOTE: This info is not complete. This works for all the slopes used in-game, but
 * there are some unused bits that change their behavior:
 * 
 * -0x04 control byte in 0x85 slope block: All slopes that have the 0x85 block have
 * all its tiles with an 0x04 control byte. 
 * 
 * IF ITS NOT SET, then the 0x85 block is used to fill all the area below 
 * (or over if its upside down???) the slope. Not
 * sure how does it behave if the 0x85 block has multiple tiles.
 * Probably the Nintendo Guys thought it was best to have it like that, and then
 * realized that it caused the triangle below the slope to be unusable (filled) 
 * and then created a new mode.
 * NOTE: The editor doesn't implement this.
 * 
 * Not sure if there's more like this...
 **/


namespace NSMBe4
{
   
    public class NSMBTileset
    {
        private bool editing = false;

        public File GFXFile;
        public File PalFile;
        public File Map16File;
        public File ObjFile;
        public File ObjIndexFile;
        public File TileBehaviorFile;

        public int TilesetNumber; // 0 for Jyotyu, 1 for Normal, 2 for SubUnit

        public int Map16TileOffset
        {
            get
            {
                if (TilesetNumber == 1)
                    return 192;
                else if (TilesetNumber == 2)
                    return 640;
                else
                    return 0;
            }
        }

        public int ObjectDefTileOffset {
            get {
                if (TilesetNumber == 1)
                    return 1;
                else if (TilesetNumber == 2)
                    return 4;
                else
                    return 0;
            }
        }

        public int Map16PaletteOffset
        {
            get
            {
                if (TilesetNumber == 0)
                    return 0;
                if (TilesetNumber == 1)
                    return 2;
                else
                    return 6;
            }
        }

        //Palettes
        public Palette[] palettes;

        //Graphics
        public Image2D graphics;

        //Map16
        public Map16Tilemap map16;

        public Bitmap Map16Buffer;
        private bool overrideFlag;
        public bool UseOverrides;
        public Bitmap OverrideBitmap = Properties.Resources.tileoverrides;

        public short[] Overrides;
        public static int[] BehaviorOverrides = { 0x00000500, 0x01000500, 0x02000500, 0x03000500, 0x04000500, 0x05000500, 0x06000500, 
                                           0x07000500, 0x08000500, 0x09000500, 0x0A000500, 0x0B000500, 0x0C000500, 0x0D000500 };

        //Tile behaviors 
        public uint[] TileBehaviors;

        //Objects
        public bool UseNotes;
        public string[] ObjNotes;

        public int objectCount = 256;
        public ObjectDef[] Objects;


        public NSMBTileset(File GFXFile, File PalFile, File Map16File, File ObjFile, File ObjIndexFile, File TileBehaviorFile, bool OverrideFlag, int TilesetNumber)
        {
            this.GFXFile = GFXFile;
            this.PalFile = PalFile;
            this.Map16File = Map16File;
            this.ObjFile = ObjFile;
            this.ObjIndexFile = ObjIndexFile;
            this.TileBehaviorFile = TileBehaviorFile;

            //Console.Out.WriteLine(ROM.FileNames[TileBehaviorFile]);

            this.TilesetNumber = TilesetNumber;
            this.overrideFlag = OverrideFlag;
            load();
        }

        public void load()
        {
            //Palettes
            int palCount = ROM.LZ77_GetDecompressedSize(PalFile.getContents()) / 512;
            
            palettes = new Palette[palCount];
            
            LZFile PalFileLz = new LZFile(PalFile, LZFile.CompressionType.LZ);
            for(int i = 0; i < palCount; i++)
	            palettes[i] = new FilePalette(new InlineFile(PalFileLz, i*512, 512, "Palette "+i));

            //Graphics
            graphics = new Image2D(GFXFile, 256, false);

            //Map16
            map16 = new Map16Tilemap(Map16File, 32, graphics, palettes, Map16TileOffset, Map16PaletteOffset);
            Overrides = new short[map16.getMap16TileCount()];
            Map16Buffer = map16.render();
            /*
            TilemapEditorTest t = new TilemapEditorTest();
            t.load(map16);
            t.Show();
            */
            //Tile Behaviors
            loadTileBehaviors();

            //Objects
            loadObjects();

/*            // Finally, load overrides
            if (overrideFlag)
            {
                UseOverrides = true;
                OverrideBitmap = Properties.Resources.tileoverrides;

                Overrides = new short[Map16.Length];
                EditorOverrides = new short[Map16.Length];

                for (int idx = 0; idx < Map16.Length; idx++)
                {
                    Overrides[idx] = -1;
                    EditorOverrides[idx] = -1;
                }
            }*/
        }

        public void beginEdit()
        {

            try
            {
            	for(int i = 0; i < palettes.Length; i++)
            		palettes[i].beginEdit();

                graphics.beginEdit();
                map16.beginEdit();

                ObjFile.beginEdit(this);
                ObjIndexFile.beginEdit(this);
                if (TileBehaviorFile != null)
                    TileBehaviorFile.beginEdit(this);
            }
            catch (AlreadyEditingException ex)
            {
                try
                {
		        	for(int i = 0; i < palettes.Length; i++)
		        		palettes[i].endEdit();
                    graphics.endEdit();
                    map16.endEdit();
                }
                catch (Exception) { }

                if (ObjFile.beingEditedBy(this))
                    ObjFile.endEdit(this);
                if (ObjIndexFile.beingEditedBy(this))
                    ObjIndexFile.endEdit(this);
                if(TileBehaviorFile != null)
                    if (TileBehaviorFile.beingEditedBy(this))
                        TileBehaviorFile.endEdit(this);
                throw ex;
            }
        }

        public void save()
        {
        	for(int i = 0; i < palettes.Length; i++)
        		palettes[i].save();

            graphics.save();
            map16.save();

            saveObjects();
            saveTileBehaviors();
        }

        public void endEdit()
        {
        	for(int i = 0; i < palettes.Length; i++)
        		palettes[i].endEdit();
        	
            graphics.endEdit();
            map16.endEdit();

            ObjFile.endEdit(this);
            ObjIndexFile.endEdit(this);
            if (TileBehaviorFile != null)
                TileBehaviorFile.endEdit(this);
        }

        public static ushort toRGB15(Color c)
        {
            byte r = (byte)(c.R >> 3);
            byte g = (byte)(c.G >> 3);
            byte b = (byte)(c.B >> 3);

            ushort val = 0;

            val |= r;
            val |= (ushort)(g << 5);
            val |= (ushort)(b << 10);
            return val;
        }

        public static Color fromRGB15(ushort c)
        {
            int cR = (c & 31) * 8;
            int cG = ((c >> 5) & 31) * 8;
            int cB = ((c >> 10) & 31) * 8;
            return Color.FromArgb(cR, cG, cB);
        }


        #region Tile Behaviors
        private void loadTileBehaviors()
        {
            byte[] x = null;

            if (TilesetNumber == 0)
                x = ROM.GetInlineFile(ROM.Data.File_Jyotyu_CHK);
            else if (TilesetNumber == 1 || TilesetNumber == 2)
                x = TileBehaviorFile.getContents();

            ByteArrayInputStream inp = new ByteArrayInputStream(x);

            int len = inp.available / 4;
            TileBehaviors = new uint[len];

            for (int i = 0; i < len; i++)
                TileBehaviors[i] = inp.readUInt();
        }

        private void saveTileBehaviors()
        {
            ByteArrayOutputStream file = new ByteArrayOutputStream();
            for (int i = 0; i < TileBehaviors.Length; i++)
                file.writeUInt(TileBehaviors[i]);

            if (TilesetNumber == 0) {
                ROM.ReplaceInlineFile(ROM.Data.File_Jyotyu_CHK, file.getArray());
            } else if (TilesetNumber == 1 || TilesetNumber == 2) {
                TileBehaviorFile.replace(file.getArray(), this);
            }
        }

        #endregion
        #region Objects
        public class ObjectDef
        {
            public List<List<ObjectDefTile>> tiles;
            public int width, height; //these are useless, but I keep them
                                      //in case the game uses them.
            private NSMBTileset t;

            public ObjectDef(NSMBTileset t)
            {
                this.t = t;
                tiles = new List<List<ObjectDefTile>>();
                List<ObjectDefTile> row = new List<ObjectDefTile>();
                tiles.Add(row);
            }

            public ObjectDef(byte[] data, NSMBTileset t)
            {
                this.t = t;
                load(new ByteArrayInputStream(data));
            }

            public void load(ByteArrayInputStream inp)
            {
                tiles = new List<List<ObjectDefTile>>();
                List<ObjectDefTile> row = new List<ObjectDefTile>();

                while (true)
                {
                    ObjectDefTile t = new ObjectDefTile(inp, this.t);
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

            public bool isSlopeObject()
            {
                if (tiles.Count == 0) return false;
                if (tiles[0].Count == 0) return false;
                return tiles[0][0].slopeControl;
            }

            public int getHeight()
            {
                int r = tiles.Count;
                if (tiles.Count < 1) r = 1;
                if(isSlopeObject()) r *= 2;
                return r;
            }

            public int getWidth()
            {
                int w = 1;
                foreach (List<ObjectDefTile> row in tiles)
                {
                    int tw = 0;
                    foreach (ObjectDefTile tile in row)
                        tw++;
                    if (tw > w) w = tw;
                }
                if (isSlopeObject()) w *= 2;
                return w;
            }
        }

        public class ObjectDefTile
        {
            public int tileID;
            public byte controlByte;
            private NSMBTileset t;

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

            public ObjectDefTile(NSMBTileset t) { this.t = t; }
            public ObjectDefTile(ByteArrayInputStream inp, NSMBTileset t)
            {
                this.t = t;
                if (inp.available < 1) //This should never happen. But sometimes, it does.
                {
                    controlByte = 0xFF; //Simulate object end.
                    return;
                }

                controlByte = inp.readByte();

                if (!controlTile)

                {
                    byte a, b;
                    a = inp.readByte();
                    b = inp.readByte();

                    tileID = a + ((b - t.ObjectDefTileOffset) << 8);

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
                    outp.writeByte((byte)(tileID / 256 + t.ObjectDefTileOffset));
                }
            }
        }

        public void loadObjects()
        {
            ByteArrayInputStream eObjIndexFile = new ByteArrayInputStream(ObjIndexFile.getContents());
            ByteArrayInputStream eObjFile = new ByteArrayInputStream(ObjFile.getContents());

            Objects = new ObjectDef[objectCount];

            //read object index
            int obj = 0;
            while (eObjIndexFile.lengthAvailable(4) && obj < Objects.Length)
            {
                Objects[obj] = new ObjectDef(this);
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

            ObjFile.replace(eObjFile.getArray(), this);
            ObjIndexFile.replace(eObjIndexFile.getArray(), this);
        }

        public class ObjectRenderingException : Exception
        {
            public ObjectRenderingException(string what) : base(what)
            {
            }
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
                throw new ObjectRenderingException("Objects can't be empty.");
            
            for(int i = 0; i < Objects[ObjNum].tiles.Count; i++)
                if (Objects[ObjNum].tiles[i].Count == 0)
                    throw new ObjectRenderingException("Objects can't have empty rows.");

            // Diagonal objects are rendered differently
            if ((Objects[ObjNum].tiles[0][0].controlByte & 0x80) != 0)
                RenderDiagonalObject(Dest, obj, Width, Height);
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

        private void RenderDiagonalObject(int[,] Dest, ObjectDef obj, int width, int height)
        {
            //empty tiles fill
            for (int xp = 0; xp < width; xp++)
                for (int yp = 0; yp < height; yp++)
                    Dest[xp, yp] = -2;

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

        public void exportTileset(string filename)
        {

            System.IO.BinaryWriter bw = new System.IO.BinaryWriter(
                new System.IO.FileStream(filename, System.IO.FileMode.Create, System.IO.FileAccess.Write));
            bw.Write("NSMBe Exported Tileset");
            writeFileContents(PalFile, bw);
            writeFileContents(GFXFile, bw);
            writeFileContents(Map16File, bw);
            writeFileContents(ObjFile, bw);
            writeFileContents(ObjIndexFile, bw);
            if (TileBehaviorFile != null)
                writeFileContents(TileBehaviorFile, bw);
            bw.Close();
        }

        private void writeFileContents(File f, System.IO.BinaryWriter bw)
        {
            bw.Write((int)f.fileSize);
            bw.Write(f.getContents());
        }

        public void importTileset(string filename)
        {
            try
            {
                PalFile.beginEdit(this);
                GFXFile.beginEdit(this);
                Map16File.beginEdit(this);
                ObjFile.beginEdit(this);
                ObjIndexFile.beginEdit(this);
                if (TileBehaviorFile != null)
                    TileBehaviorFile.beginEdit(this);
            }
            catch (AlreadyEditingException ex)
            {
                if (PalFile.beingEditedBy(this))
                    PalFile.endEdit(this);
                if (GFXFile.beingEditedBy(this))
                    GFXFile.endEdit(this);
                if (Map16File.beingEditedBy(this))
                    Map16File.endEdit(this);
                if (ObjFile.beingEditedBy(this))
                    ObjFile.endEdit(this);
                if (ObjIndexFile.beingEditedBy(this))
                    ObjIndexFile.endEdit(this);
                if (TileBehaviorFile != null)
                    if (TileBehaviorFile.beingEditedBy(this))
                        TileBehaviorFile.endEdit(this);
                throw ex;
            }

            System.IO.BinaryReader br = new System.IO.BinaryReader(
                new System.IO.FileStream(filename, System.IO.FileMode.Open, System.IO.FileAccess.Read));
            string header = br.ReadString();
            if (header != "NSMBe Exported Tileset")
            {
                MessageBox.Show(
                    LanguageManager.Get("NSMBLevel", "InvalidFile"),
                    LanguageManager.Get("NSMBLevel", "Unreadable"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            readFileContents(PalFile, br);
            readFileContents(GFXFile, br);
            readFileContents(Map16File, br);
            readFileContents(ObjFile, br);
            readFileContents(ObjIndexFile, br);
            if (TileBehaviorFile != null)
                readFileContents(TileBehaviorFile, br);
            br.Close();

            PalFile.endEdit(this);
            GFXFile.endEdit(this);
            Map16File.endEdit(this);
            ObjFile.endEdit(this);
            ObjIndexFile.endEdit(this);
            if (TileBehaviorFile != null)
                TileBehaviorFile.endEdit(this);
        }

        void readFileContents(File f, System.IO.BinaryReader br)
        {
            int len = br.ReadInt32();
            byte[] data = new byte[len];
            br.Read(data, 0, len);
            f.replace(data, this);
        }
    }
}
