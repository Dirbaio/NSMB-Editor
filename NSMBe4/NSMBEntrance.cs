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
                    Properties.Settings.Default.Language != 1 ? NSMBEntrance.TypeList[Type] : NSMBEntrance.TypeList_lang1[Type],
                    X, Y);
        }

        public static string[] TypeList = ("Normal|Unused|Door|" +
            "Pipe facing up|Pipe facing down|Pipe facing left|Pipe facing right|" +
            "Unused|Ground pound (entrance only)|Sliding (entrance only)|Swimming (entrance only)|" +
            "Unused|Unused|Unused|Unused|Unused|" +
            "Mini pipe facing up|Mini pipe facing down|Mini pipe facing left|Mini pipe facing right|" +
            "Jumping (entrance only)|Climbing vine (entrance only)|Unknown 1|Unknown 2|Unknown 3|Unknown 4").Split('|');

        public static string[] TypeList_lang1 = ("Normal|Sin usar|Puerta|" +
            "Tuberia (arriba)|Tuberia (abajo)|Tuberia (izquierda)|Tuberia (derecha)|" +
            "Sin usar|Pegar al suelo (solo entrada)|Deslizandose (solo entrada)|Nadando (solo entrada)|" +
            "Sin usar|Sin usar|Sin usar|Sin usar|Sin usar|" +
            "Tuberia pequeña (arriba)|Tuberia pequeña (abajo)|Tuberia pequeña (izquierda)|Tuberia pequeña (derecha)|" +
            "Saltando|Enredadera|Desconocido 1|Desconocido 2|Desconocido 3|Desconocido 4").Split('|');
    }
}
