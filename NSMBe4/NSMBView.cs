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
    public class NSMBView {
        public int X;
        public int Y;
        public int Width;
        public int Height;
        public int Number;
        public int Camera;
        public int Music;
        public int Unknown1;
        public int Unknown2;
        public int Unknown3;
        public int Lighting;
        public int FlagpoleID;

        public bool isZone = false;

        // save two dictionary lookups every repaint
        private string ViewDesc = LanguageManager.Get("NSMBView", "ViewDesc");
        private string ZoneDesc = LanguageManager.Get("NSMBView", "ZoneDesc");

        public NSMBView()
        { }

        public NSMBView(NSMBView v)
        {
            X = v.X;
            Y = v.Y;
            Width = v.Width;
            Height = v.Height;
            Number = v.Number;
            Camera = v.Camera;
            Music = v.Music;
            Unknown1 = v.Unknown1;
            Unknown2 = v.Unknown2;
            Unknown3 = v.Unknown3;
            Lighting = v.Lighting;
            FlagpoleID = v.FlagpoleID;
            isZone = v.isZone;
        }

        public void render(Graphics g, int vx, int vy)
        {
            Pen p = Pens.LightSteelBlue;
            if (isZone)
                p = Pens.PaleGreen;

            g.DrawRectangle(p, X, Y, Width - 1, Height - 1);
            g.DrawRectangle(p, X + 1, Y + 1, Width - 3, Height - 3);

            if (X + Width > vx * 16 && Y + Height > vy * 16)
            {
                int numx = X;
                int numy = Y;

                if (numx < vx * 16)
                    numx = vx * 16;
                if (numy < vy * 16)
                    numy = vy * 16;
                if (isZone)
                    numy += 16;
                g.DrawString(string.Format((isZone ? ZoneDesc : ViewDesc), Number), NSMBGraphics.InfoFont, Brushes.White, (float)numx, (float)numy);
            }
        }

        public void renderSelected(Graphics g)
        {
            Pen col = Pens.White;
            if (isZone)
                col = Pens.LightGreen;

            g.DrawRectangle(col, X - 1, Y - 1, Width + 1, Height + 1);
            g.DrawRectangle(col, X + 2, Y + 2, Width - 5, Height - 5);

            for (int x = X+16-X%16; x < X+Width; x += 16)
                g.DrawLine(col, x, Y, x, Y + Height);
            for (int y = Y + 16 - Y % 16; y < Y + Height; y += 16)
                g.DrawLine(col, X, y, X + Width, y);
        }

        public void write(ByteArrayOutputStream outp)
        {
            outp.writeUShort((ushort)X);
            outp.writeUShort((ushort)Y);
            outp.writeUShort((ushort)Width);
            outp.writeUShort((ushort)Height);
            outp.writeByte((byte)Number);
            outp.writeByte((byte)Camera);
            outp.writeByte((byte)Music);
            outp.writeByte((byte)Unknown1);
            outp.writeByte((byte)Unknown2);
            outp.writeByte((byte)Unknown3);
            outp.writeByte((byte)Lighting);
            outp.writeByte((byte)FlagpoleID);
        }
        public static NSMBView read(ByteArrayInputStream inp)
        {
            NSMBView v = new NSMBView();

            v.X = inp.readUShort();
            v.Y = inp.readUShort();
            v.Width = inp.readUShort();
            v.Height = inp.readUShort();
            v.Number = inp.readByte();
            v.Camera = inp.readByte();
            v.Music = inp.readByte();
            v.Unknown1 = inp.readByte();
            v.Unknown2 = inp.readByte();
            v.Unknown3 = inp.readByte();
            v.Lighting = inp.readByte();
            v.FlagpoleID = inp.readByte();

            return v;
        }

        public void writeZone(ByteArrayOutputStream outp)
        {
            outp.writeUShort((ushort)X);
            outp.writeUShort((ushort)Y);
            outp.writeUShort((ushort)Width);
            outp.writeUShort((ushort)Height);
            outp.writeByte((byte)Number);
            outp.writeByte(0);
            outp.writeByte(0);
            outp.writeByte(0);
        }

        public static NSMBView readZone(ByteArrayInputStream inp)
        {
            NSMBView v = new NSMBView();

            v.X = inp.readUShort();
            v.Y = inp.readUShort();
            v.Width = inp.readUShort();
            v.Height = inp.readUShort();
            v.Number = inp.readByte();
            v.isZone = true;
            inp.skip(3);

            return v;
        }

        public override string ToString()
        {
            return string.Format(LanguageManager.Get("NSMBView", "ToString"), Number, X, Y, Width, Height);
        }
    }
}
