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
    public class NSMBView : LevelItem
    {
        public int X;
        public int Y;
        public int Width;
        public int Height;
        public int Number;
//        public int Camera;
        public int Music;
        public int Unknown1;
        public int Unknown2;
        public int Unknown3;
        public int Lighting;
        public int FlagpoleID;

        public int CameraTop, CameraBottom;
        public int CameraTopSpin, CameraBottomSpin;
        public int CameraBottomStick;

        public bool isZone = false;

        // save two dictionary lookups every repaint
        private string ViewDesc = LanguageManager.Get("NSMBView", "ViewDesc");
        private string ZoneDesc = LanguageManager.Get("NSMBView", "ZoneDesc");

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
        public int snap { get { return 1; } }

        public NSMBView()
        { }

        public NSMBView(NSMBView v)
        {
            X = v.X;
            Y = v.Y;
            Width = v.Width;
            Height = v.Height;
            Number = v.Number;
//            Camera = v.Camera;
            Music = v.Music;
            Unknown1 = v.Unknown1;
            Unknown2 = v.Unknown2;
            Unknown3 = v.Unknown3;
            Lighting = v.Lighting;
            FlagpoleID = v.FlagpoleID;
            isZone = v.isZone;
            CameraTop = v.CameraTop;
            CameraTopSpin = v.CameraTopSpin;
            CameraBottom = v.CameraBottom;
            CameraBottomSpin = v.CameraBottomSpin;
            CameraBottomStick = v.CameraBottomStick;
        }

        public void render(Graphics g, LevelEditorControl ed)
        {
            Pen p = Pens.LightSteelBlue;
            if (isZone)
                p = Pens.PaleGreen;

            g.DrawRectangle(p, X, Y, Width - 1, Height - 1);
            g.DrawRectangle(p, X + 1, Y + 1, Width - 3, Height - 3);

            Rectangle ViewableArea = ed.ViewableArea;

            int vx = ViewableArea.X;
            int vy = ViewableArea.Y;
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
                g.DrawString(GetDisplayString(), NSMBGraphics.InfoFont, Brushes.White, (float)numx, (float)numy);
            }
        }

        public string GetDisplayString()
        {
            return string.Format((isZone ? ZoneDesc : ViewDesc), Number);
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

        public void write(ByteArrayOutputStream outp, ByteArrayOutputStream cam, int camID)
        {
            outp.writeUShort((ushort)X);
            outp.writeUShort((ushort)Y);
            outp.writeUShort((ushort)Width);
            outp.writeUShort((ushort)Height);
            outp.writeByte((byte)Number);
            outp.writeByte((byte)camID); 
            outp.writeByte((byte)Music);
            outp.writeByte((byte)Unknown1);
            outp.writeByte((byte)Unknown2);
            outp.writeByte((byte)Unknown3);
            outp.writeByte((byte)Lighting);
            outp.writeByte((byte)FlagpoleID);

            cam.writeInt(CameraTop);
            cam.writeInt(CameraBottom);
            cam.writeInt(CameraTopSpin);
            cam.writeInt(CameraBottomSpin);
            cam.writeUShort((ushort)camID);
            cam.writeUShort((ushort)CameraBottomStick);
            cam.writeUInt(0); //This seems just padding.
        }
        public static NSMBView read(ByteArrayInputStream inp, ByteArrayInputStream cam)
        {
            NSMBView v = new NSMBView();

            v.X = inp.readUShort();
            v.Y = inp.readUShort();
            v.Width = inp.readUShort();
            v.Height = inp.readUShort();
            v.Number = inp.readByte();
            int camID = inp.readByte();
            v.Music = inp.readByte();
            v.Unknown1 = inp.readByte();
            v.Unknown2 = inp.readByte();
            v.Unknown3 = inp.readByte();
            v.Lighting = inp.readByte();
            v.FlagpoleID = inp.readByte();

            cam.seek(0);
            int camCount = (int)cam.available / 24;
//            Console.Out.WriteLine("CamCount: " + camCount);
            int goodCam = -1;
            for (int i = 0; i < camCount; i++)
            {
                cam.seek(i * 24 + 16);
                int thisCam = cam.readUShort();
//                Console.Out.WriteLine("Cam ID: " + thisCam);
                if (thisCam == camID)
                {
                    goodCam = i;
                    break;
                }
            }

            if (goodCam == -1)
                Console.Out.WriteLine("Warning: Could not find camera ID " + camID);
            else
            {
                cam.seek(goodCam * 24);
                v.CameraTop = cam.readInt();
                v.CameraBottom = cam.readInt();
                v.CameraTopSpin = cam.readInt();
                v.CameraBottomSpin = cam.readInt();
                cam.skip(2);
                v.CameraBottomStick = cam.readUShort();
            }
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

        public string ToStringClip()
        {
            return String.Format("VWZ:{0}:{1}:{2}:{3}:{4}:{5}:{6}:{7}:{8}:{9}:{10}:{11}:{12}:{13}:{14}:{15}", X, Y, Width, Height, Number,
                Music, Unknown1, Unknown2, Unknown3, Lighting, FlagpoleID, CameraTop, CameraTopSpin, CameraBottom, CameraBottomSpin, CameraBottomStick);
        }

        public override string ToString()
        {
            return string.Format(LanguageManager.Get("NSMBView", "ToString"), Number, X, Y, Width, Height);
        }

        public static NSMBView FromString(string[] strs, ref int idx)
        {
            NSMBView v = new NSMBView();
            v.X = int.Parse(strs[1 + idx]);
            v.Y = int.Parse(strs[2 + idx]);
            v.Width = int.Parse(strs[3 + idx]);
            v.Height = int.Parse(strs[4 + idx]);
            v.Number = int.Parse(strs[5 + idx]);
            v.Music = int.Parse(strs[6 + idx]);
            v.Unknown1 = int.Parse(strs[7 + idx]);
            v.Unknown2 = int.Parse(strs[8 + idx]);
            v.Unknown3 = int.Parse(strs[9 + idx]);
            v.Lighting = int.Parse(strs[10 + idx]);
            v.FlagpoleID = int.Parse(strs[11 + idx]);
            v.CameraTop = int.Parse(strs[12 + idx]);
            v.CameraTopSpin = int.Parse(strs[13 + idx]);
            v.CameraBottom = int.Parse(strs[14 + idx]);
            v.CameraBottomSpin = int.Parse(strs[15 + idx]);
            v.CameraBottomStick = int.Parse(strs[16 + idx]);
            idx += 17;
            return v;
        }

    }
}
