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
using NSMBe4.DSFileSystem;
using System.Drawing;

/*

struct JNCD_Header {
	char magic[4]; // JNCD
	u16 unk_4; // 0x101? checked in sub_2055978 maybe others too?
	u16 entryCount;

	u32 offsetToEntries; // 0x08
	u32 offsetToSubEntries; // 0x0C
	u32 offsetToTextureData; // 0x10
	u32 sizeOfTextureData; // 0x14
	u32 unk_18; // maybe not used at all?
};


// This struct overall is found using 20559A4
struct JNCD_Entry {
	u8 width; // Accessed by stuff in sub_20042D8
	u8 height; // ^^^^^

	u16 subEntryStartIndex;
	// pointer to this entry is returned by 20559CC

	u16 subEntryCount; // returned by 20559F4
};

struct JNCD_SubEntry {
	u16 oamAttr1; //See gbatek for info on these. Not sure if all bits are usable.
	u16 oamAttr2;
	u32 unk_4;
	u16 tileNumber;
	u16 tileCount;
};

*/

namespace NSMBe4
{
    public class Bncd
    {
		//Size, Shape
		public static int[,] widths = {
			{1, 2, 1},
			{2, 4, 1},
			{4, 4, 2},
			{8, 8, 4}
		};
		//Size, Shape
		public static int[,] heights = {
			{1, 1, 2},
			{2, 1, 4},
			{4, 2, 4},
			{8, 4, 8}
		};

   		public List<BncdEntry> entries = new List<BncdEntry>();
   		public List<BncdImage> images = new List<BncdImage>();
   		ushort unk; //Let's save it just in case.
   		
    	public Bncd(File f)
    	{
    		ByteArrayInputStream inp = new ByteArrayInputStream(f.getContents());
    		inp.readInt(); //Magic;
    		unk = inp.readUShort();
    		ushort entryCount = inp.readUShort();
    		uint entriesOffset = inp.readUInt();
    		uint subEntriesOffset = inp.readUInt();
    		uint dataOffset = inp.readUInt();
    		uint dataSize = inp.readUInt();
    		
    		inp.seek(entriesOffset);
    		
    		//Stores tilenum, tilecount to imageid
    		Dictionary<uint, int> imagesDict = new Dictionary<uint, int> ();
    		
    		for(uint entryId = 0; entryId < entryCount; entryId++)
    		{
    			BncdEntry e = new BncdEntry();
    			e.width = inp.readByte();
    			e.height = inp.readByte();
    			
    			uint subEntryIdx = inp.readUShort();
    			uint subEntryCt = inp.readUShort();
    			
    			inp.savePos();
    			inp.seek(subEntriesOffset + subEntryIdx*12);
    			for(int i = 0; i < subEntryCt; i++)
    			{
    				BncdSubEntry se = new BncdSubEntry();
    				e.subEntries.Add(se);
    				
    				se.oamAttr0 = inp.readUShort();
    				se.oamAttr1 = inp.readUShort();
    				se.unk = inp.readUInt();
    				se.tileNumber = inp.readUShort();
    				se.tileCount = inp.readUShort();
    				
    				uint imageCode = (uint) ((se.tileNumber << 16) | se.tileCount);
    				int imageId = imagesDict.Count;
    				if(imagesDict.ContainsKey(imageCode))
    					imageId = imagesDict[imageCode];
    				else
    				{
    					imagesDict[imageCode] = imageId;
				    	BncdImage img = new BncdImage();
				    	images.Add(img);
				    	img.tileNumber = se.tileNumber;
				    	img.tileCount = se.tileCount;

				    	int oamShape = se.oamAttr0>>14;
				    	int oamSize = se.oamAttr1>>14;

				    	img.tileWidth = widths[oamSize, oamShape];
					}
					    				
    				se.imageId = imageId;
    			}
    			inp.loadPos();
    		}
    		
            LevelChooser.showImgMgr();
			int tileLen = 8*8/2;
			foreach(BncdImage img in images)
			{
		        File imgFile = new InlineFile(f, (int)dataOffset+img.tileNumber*tileLen, img.tileCount*tileLen, f.name);
		        LevelChooser.imgMgr.m.addImage(new Image2D(imgFile, 8*img.tileWidth, true, false));
			}
    	}
    	
    	public class BncdEntry
    	{
    		public List<BncdSubEntry> subEntries = new List<BncdSubEntry>();
    		public int width, height;
    	}
    	
    	public class BncdSubEntry
    	{
    		public ushort oamAttr0, oamAttr1;
    		public uint unk;
    		public ushort tileNumber, tileCount;
    		
    		public int imageId;
    	}
    	
    	public class BncdImage
    	{
    		public int tileNumber;
    		public int tileCount;
    		public int tileWidth;
    	}
    }
}
