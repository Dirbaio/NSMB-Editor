using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace NSMBe4 {
    public class NSMBEntrance {
        //public byte[] Data;

        public int X;
        public int Y;
        public int CameraX;
        public int CameraY;
        public int Number;
        public int DestArea;
        public int ConnectedPipeID;
        public int DestEntrance;
        public int Type;
        public int Settings;
        public int Unknown1;
        public int EntryView;
        public int Unknown2;

        public NSMBEntrance() { }
        public NSMBEntrance(NSMBEntrance e)
        {
            X = e.X;
            Y = e.Y;
            CameraX = e.CameraX;
            CameraY = e.CameraY;
            Number = e.Number;
            DestArea = e.DestArea;
            ConnectedPipeID = e.ConnectedPipeID;
            DestEntrance = e.DestEntrance;
            Type = e.Type;
            Settings = e.Settings;
            Unknown1 = e.Unknown1;
            EntryView = e.EntryView;
            Unknown2 = e.Unknown2;
        }

        public void Render(Graphics g) {
            int EntranceShowType = 13;
            if (Type == 0) EntranceShowType = 0;
            if (Type == 2) EntranceShowType = 1;
            if (Type >= 3 && Type <= 6) EntranceShowType = Type - 1;
            if (Type == 8) EntranceShowType = 6;
            if (Type >= 16 && Type <= 19) EntranceShowType = Type - 9;
            if (Type == 20) EntranceShowType = 11;
            if (Type == 21) EntranceShowType = 12;

            int EntranceArrowColour = 0;
            // connected pipes have the grey blob (or did, it's kind of pointless)
            /*if (((Type >= 3 && Type <= 6) || (Type >= 16 && Type <= 19) || (Type >= 22 && Type <= 25)) && (Settings & 8) != 0) {
                EntranceArrowColour = 2;
            }*/
            // doors and pipes can be exits, so mark them as one if they're not 128
            if (((Type >= 2 && Type <= 6) || (Type >= 16 && Type <= 19) || (Type >= 22 && Type <= 25)) && (Settings & 128) == 0) {
                EntranceArrowColour = 1;
            }

            g.DrawImage(Properties.Resources.entrances, new Rectangle(X, Y, 16, 16), new Rectangle(EntranceShowType * 16, EntranceArrowColour * 16, 16, 16), GraphicsUnit.Pixel);
        }

        public override string ToString()
        {
 	         return String.Format("{0}: {1} ({2},{3})", Number,
                    LanguageManager.GetList("EntranceTypes")[Type],
                    X, Y);
        }
    }
}
