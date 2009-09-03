using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace NSMBe4 {
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
        }

        public void render(Graphics g)
        {
            Pen p = Pens.LightSteelBlue;
            if (Width < 16 * 16)
                p = Pens.Red;
            if (Height < 12 * 16)
                p = Pens.Red;

            g.DrawRectangle(p, X, Y, Width - 1, Height - 1);
            g.DrawRectangle(p, X + 1, Y + 1, Width - 3, Height - 3);
        }

        public void renderSelected(Graphics g)
        {
            g.DrawRectangle(Pens.Yellow, X - 1, Y - 1, Width + 1, Height + 1);
            g.DrawRectangle(Pens.Yellow, X + 2, Y + 2, Width - 5, Height - 5);
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

        public override string ToString()
        {
            return Number + ": " + X + ", " + Y + " (" + Width + " x " + Height + ")";
        }
    }
}
