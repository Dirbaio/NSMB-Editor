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
    public class NSMBSprite {
        public int X;
        public int Y;
        public int Type;
        public byte[] Data;
        private NSMBLevel Level;

        public NSMBSprite(NSMBLevel Level)
        {
            this.Level = Level;
        }

        public NSMBSprite(NSMBSprite s)
        {
            this.X = s.X;
            this.Y = s.Y;
            this.Type = s.Type;
            this.Level = s.Level;

            this.Data = new byte[6];
            Array.Copy(s.Data, Data, 6);
        }

        public Rectangle getRect() {
            int x = this.X * 16;
            int y = this.Y * 16;
            int width = 16;
            int height = 16;
            switch (this.Type) {
                case 23:
                    y -= 28;
                    width = 32; height = 28;
                    break;
                case 24:
                    y += 32;
                    width = 32; height = 28;
                    break;
                case 25:
                    x += 32;
                    width = 28; height = 32;
                    break;
                case 26:
                    x -= 28;
                    width = 28; height = 32;
                    break;
                case 31:
                    width = 21;
                    break;
                case 32:
                    x -= 29; y -= 119;
                    width = 27; height = 24;
                    break;
                case 33:
                    if (Data[2] % 0x10 > 0) {
                        x -= 8; y -= 16;
                        width = 32; height = 32;
                    }
                    if (Data[2] / 0x10 > 0)
                        x += 8;
                    break;
                case 34:
                    x -= 7; y -= 26;
                    width = 30; height = 42;
                    break;
                case 37:
                    width = 20;
                    break;
                case 38:
                    width = 24; height = 20;
                    break;
                case 48:
                    y -= 90;
                    width = 32; height = 74;
                    break;
                case 49:
                    y += 32;
                    width = 32; height = 74;
                    break;
                case 50:
                    x += 32;
                    width = 74; height = 32;
                    break;
                case 51:
                    x -= 90;
                    width = 74; height = 32;
                    break;
                case 53:
                    y -= 10;
                    width = 17; height = 26;
                    break;
                case 54:
                    height = 29;
                    break;
                case 55:
                    width = 21;
                    break;
                case 59:
                    y -= 18;
                    width = 18; height = 34;
                    break;
                case 66:
                    if (Data[2] % 0x10 != 1)
                        y -= 2;
                    height = 18;
                    break;
                case 68:
                case 69:
                    width = Math.Max(16 * (Data[2] + 1), 32);
                    break;
                case 75:
                    width = 256;
                    break;
                case 76:
                    x -= 24; y -= 10;
                    width = 16 * (Data[2] % 0x10) + 48;
                    height = 16 * Math.Max(Data[2] / 0x10, Data[3] % 0x10) + 18;
                    break;
                case 79:
                    x -= 28; y -= 34;
                    width = 92; height = 85;
                    break;
                case 82:
                    x -= 24; y -= 8;
                    width = 64; height = 32;
                    break;
                case 86:
                    x -= 14; y -= 6;
                    width = 44; height = 39;
                    break;
                case 94:
                    if (Data[2] > 0) {
                        x -= 6; y -= 16;
                        width = 26; height = 37;
                    } else {
                        height = 18;
                    }
                    break;
                case 95:
                    x -= 6;
                    width = 26; height = 26;
                    break;
                case 102:
                    width = 30; height = 32;
                    break;
                case 103:
                    x -= 51; y -= 59;
                    width = 115; height = 76;
                    break;
                //case 106:
                //    Red Coins don't need to be modified
                case 107:
                    if (Data[2] != 1) {
                        y -= 2;
                    }
                    height = 18;
                    break;
                case 108:
                    if (Data[2] != 1) {
                        y -= 2;
                    }
                    height = 18;
                    break;
                case 109:
                    x -= 6; y -= 6;
                    int shift = Data[2] / 0x10;
                    if (shift == 1 || shift == 3)
                        x += 8;
                    if (shift == 2 || shift == 3)
                        y += 8;
                    height = 28; width = 27;
                    break;
                case 113:
                    width = 60; height = 47;
                    break;
                case 115:
                    y -= 42;
                    width = 63; height = 58;
                    break;
                case 116:
                    x -= 16; y -= 5;
                    width = 41; height = 21;
                    break;
                case 117:
                    x -= 12; y -= 8;
                    width = 44; height = 23;
                    break;
                case 120:
                    y += 3;
                    width = 33; height = 29;
                    break;
                case 127:
                    x -= 144; y -= 104;
                    width = 288; height = 208;
                    break;
                case 130:
                    width = 21;
                    break;
                case 131:
                case 132:
                    width = 22; height = 33;
                    break;
                case 136:
                    width = 18; height = 16 * (Data[2] + 2) + 7;
                    y -= height - 16;
                    break;
                case 146:
                    width = 32; height = 8;
                    break;
                case 147:
                    x -= Data[2] * 8 + 6;
                    width = 16 * (Data[2] - 1) + 28;
                    height = 10;
                    break;
                case 148:
                    y -= 2;
                    height = 18;
                    break;
                case 149:
                    y -= 13;
                    height = 29;
                    break;
                case 150:
                    y -= 16;
                    width = 19; height = 32;
                    break;
                //case 152:
                //    Switch blocks don't need to be modified
                case 155:
                    width = 16 * (Data[3] % 0x10 + 1); height = 16 * (Data[2] / 0x10 + 1);
                    break;
                case 162:
                    x -= 16 * (Data[3] / 0x10 + 1);
                    width = 32 * (Data[3] / 0x10 + 2);
                    height = 16 * (Data[4] % 0x10 + 4);
                    break;
                case 180:
                    width = 18; height = 26;
                    break;
                case 183:
                    y -= 23;
                    width = 26; height = 39;
                    break;
                case 186:
                    y -= 12;
                    width = 24; height = 28;
                    break;
                case 189:
                    y -= 48;
                    width = 32; height = 64;
                    break;
                case 193:
                    y -= 28;
                    width = 25; height = 44;
                    break;
                case 204:
                    y -= 7;
                    width = 27; height = 23;
                    break;
                case 207:
                    x -= 20; y -= 32;
                    width = 63; height = 52;
                    break;
                case 211:
                    y -= 16;
                    width = 26; height = 32;
                    break;
                case 220:
                    y -= 25;
                    width = 31; height = 41;
                    break;
                case 222:
                    if (Data[2] / 0x10 > 0)
                        x += 8;
                    if (Data[2] % 0x10 > 0)
                        y += 8;
                    width = 8; height = 8;
                    break;
                case 223:
                    width = 32; height = 32;
                    break;
                case 224:
                    width = 192; height = 64;
                    break;
                case 226:
                    x -= 14; y += 80;
                    width = 44; height = 37;
                    break;
                case 228:
                    width = 20; height = 20;
                    break;
                case 235:
                    switch (Data[2] / 0x10) {
                        case 1:
                            x += 8;
                            break;
                        case 2:
                            x -= 8;
                            break;
                    }
                    switch (Data[2] % 0x10) {
                        case 1:
                            y += 8;
                            break;
                        case 2:
                            y -= 8;
                            break;
                    }
                    width = 32; height = 32;
                    break;
                case 236:
                    x -= 16; y -= 16;
                    if (Data[5] / 0x10 > 0)
                        y += 8;
                    width = 48; height = 48;
                    break;
                case 238:
                    x -= 16 * (Data[2] / 0x10 + 1);
                    width = 32 * (Data[2] / 0x10 + 2);
                    height = 112;
                    break;
                case 244:
                    switch (Data[2] / 0x10)
                    {
                        case 1:
                            x -= 8;
                            break;
                        case 2:
                            x += 8;
                            break;
                    }
                    x -= 40;
                    width = 96; height = 16 * (Data[2] % 0x10 + 2);
                    break;
                case 246:
                    x -= 16; y -= 8;
                    width = 48; height = 48;
                    break;
                case 249:
                    x -= Data[2] % 0x10 * 8 + 24; y += 5;
                    width = Data[2] % 0x10 * 16 + 64;
                    height = Data[3] / 0x10 * 16 + 83;
                    break;
                case 277:
                    width = 32; height = 32;
                    break;
                case 279:
                    if (Data[2] == 0 || Data[2] == 1) {
                        y -= 39;
                        width = 16; height = 39;
                    } else if (Data[2] == 2 || Data[2] == 3) {
                        y += 16;
                        width = 16; height = 39;
                    } else if (Data[2] == 4 || Data[2] == 5) {
                        x += 16;
                        width = 39; height = 16;
                    } else if (Data[2] == 6 || Data[2] == 7) {
                        x -= 39;
                        width = 39; height = 16;
                    }
                    break;
                case 283:
                    y -= 32;
                    width = 54; height = 55;
                    break;
                case 284:
                    y -= 2;
                    width = 18; height = 18;
                    break;
                //case 289:
                //  Expandable Blocks don't need to be modified
                case 298:
                    width = Math.Max(16 * (Data[2] % 0x10 + 1), 32);
                    height = 16 * (Data[2] / 0x10 + 1);
                    int spikes = Data[5] % 0x10;
                    if (width == 32 && (spikes == 1 || spikes == 2))
                        width = 48;
                    if (width == 32 && spikes == 3)
                        width = 64;
                    if (height == 16 && (spikes == 4 || spikes == 5))
                        height = 32;
                    if (height == 16 && spikes == 6)
                        height = 48;
                    break;
                case 323:
                    height = 32;
                    width = 32 * (Data[2] + 2);
                    break;
            }

            return new Rectangle(x, y, width, height);
        }

        public Rectangle getRectB() {
            Rectangle rect = getRect();
            int t = rect.X;
            rect.X = (int)Math.Floor((float)rect.X / 16);
            rect.Width += t - 16 * rect.X;
            t = rect.Y;
            rect.Y = (int)Math.Floor((float)rect.Y / 16);
            rect.Height += t - 16 * rect.Y;
            rect.Width = (int)Math.Ceiling((float)rect.Width / 16);
            rect.Height = (int)Math.Ceiling((float)rect.Height / 16);
            return rect;
        }

        public void Render(Graphics g) {
            int RenderX = X * 16;
            int RenderY = Y * 16;
            Bitmap img;

            bool customRendered = true;

            switch (this.Type)
            {
                case 23:
                    g.DrawImage(Properties.Resources.PiranhaplantTube, RenderX, RenderY - 28, 32, 28);
                    break;
                case 24:
                    img = Properties.Resources.PiranhaplantTube;
                    img.RotateFlip(RotateFlipType.RotateNoneFlipY);
                    g.DrawImage(img, RenderX, RenderY + 32, 32, 28);
                    break;
                case 25:
                    img = Properties.Resources.PiranhaplantTube;
                    img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    g.DrawImage(img, RenderX + 32, RenderY, 28, 32);
                    break;
                case 26:
                    img = Properties.Resources.PiranhaplantTube;
                    img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    g.DrawImage(img, RenderX - 28, RenderY, 28, 32);
                    break;
                case 31:
                    g.DrawImage(Properties.Resources.CheepCheep, RenderX, RenderY, 21, 16);
                    break;
                case 32:
                    if (Data[5] == 0x10)
                        img = Properties.Resources.EndingFlagRed;
                    else
                        img = Properties.Resources.EndingFlag;
                    g.DrawImage(img, RenderX - 29, RenderY - 119, 27, 24);
                    break;
                case 33:
                    if (Data[2] % 0x10 > 0) {
                        img = Properties.Resources.SpringGiant;
                        RenderY -= 16;
                        RenderX -= 8;
                    } else
                        img = Properties.Resources.Spring;
                    if (Data[2] / 0x10 > 0)
                        RenderX += 8;
                    g.DrawImage(img, RenderX, RenderY, img.Width, img.Height);
                    break;
                case 34:
                    g.DrawImage(Properties.Resources.RedCoinRing, RenderX - 7, RenderY - 26, 30, 42);
                    break;
                case 37:
                    g.DrawImage(Properties.Resources.Spiny, RenderX, RenderY, 20, 16);
                    break;
                case 38:
                    g.DrawImage(Properties.Resources.Boo, RenderX, RenderY, 24, 20);
                    break;
                case 48:
                    g.DrawImage(Properties.Resources.TubeBubbles, RenderX, RenderY - 90, 32, 74);
                    break;
                case 49:
                    img = Properties.Resources.TubeBubbles;
                    img.RotateFlip(RotateFlipType.RotateNoneFlipY);
                    g.DrawImage(img, RenderX, RenderY + 32, 32, 74);
                    break;
                case 50:
                    img = Properties.Resources.TubeBubbles;
                    img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    g.DrawImage(img, RenderX + 32, RenderY, 74, 32);
                    break;
                case 51:
                    img = Properties.Resources.TubeBubbles;
                    img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    g.DrawImage(img, RenderX - 90, RenderY, 74, 32);
                    break;
                case 53:
                    g.DrawImage(Properties.Resources.DryBones, RenderX, RenderY - 10, 17, 26);
                    break;
                case 54:
                    g.DrawImage(Properties.Resources.FireBall, RenderX, RenderY, 16, 29);
                    break;
                case 55:
                    g.DrawImage(Properties.Resources.BulletBill, RenderX, RenderY, 21, 16);
                    break;
                case 59:
                    g.DrawImage(Properties.Resources.HammerBro, RenderX, RenderY - 18, 18, 34);
                    break;
                case 66:
                    img = Properties.Resources.PSwitch;
                    if (Data[2] % 0x10 == 1)
                        img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    else
                        RenderY -= 2;
                    g.DrawImage(img, RenderX, RenderY, 16, 18);
                    break;
                case 68:
                case 69:
                    g.DrawImage(Properties.Resources.MovingPlatformLeft, RenderX, RenderY, 16, 16);
                    for (int l = 0; l < Data[2] - 1; l++) {
                        RenderX += 16;
                        g.DrawImage(Properties.Resources.MovingPlatformSection, RenderX, RenderY, 16, 16);
                    }
                    g.DrawImage(Properties.Resources.MovingPlatformRight, RenderX + 16, RenderY, 16, 16);
                    break;
                case 75:
                    g.DrawImage(Properties.Resources.SeeSaw, RenderX, RenderY, 256, 16);
                    break;
                case 76:
                    int RenderX2 = RenderX + 16 * (Data[2] % 0x10) - 10;
                    g.DrawImage(Properties.Resources.ScalePlatformBolt, RenderX - 2, RenderY - 10, 13, 13);
                    g.DrawImage(Properties.Resources.ScalePlatformBolt, RenderX2, RenderY - 10, 13, 13);
                    Pen rope = new Pen(Color.FromArgb(49, 24, 74));
                    g.DrawLine(Pens.White, RenderX + 8, RenderY - 9, RenderX2 + 2, RenderY - 9);
                    g.DrawLine(rope, RenderX + 9, RenderY - 8, RenderX2 + 1, RenderY - 8);
                    int RenderY2 = RenderY + 16 * (Data[2] / 0x10) - 8;
                    int RenderY3 = RenderY + 16 * (Data[3] % 0x10) - 8;
                    g.DrawLine(Pens.White, RenderX - 1, RenderY, RenderX - 1, RenderY2 - 1);
                    g.DrawLine(rope, RenderX, RenderY + 1, RenderX, RenderY2 - 1);
                    g.DrawLine(Pens.White, RenderX2 + 9, RenderY + 3, RenderX2 + 9, RenderY3 - 1);
                    g.DrawLine(rope, RenderX2 + 10, RenderY + 1, RenderX2 + 10, RenderY3 - 1);
                    g.DrawImage(Properties.Resources.ScalePlatformEnd, RenderX2 - 14, RenderY3, 48, 16);
                    g.DrawImage(Properties.Resources.ScalePlatformEnd, RenderX - 24, RenderY2, 48, 16);
                    break;
                case 79:
                    g.DrawImage(Properties.Resources.Spinning3PointedPlatform, RenderX - 28, RenderY - 34, 92, 85);
                    break;
                case 82:
                    g.DrawImage(Properties.Resources.SpinningRectanglePlatform, RenderX - 24, RenderY - 8, 64, 32);
                    break;
                case 86:
                    g.DrawImage(Properties.Resources.SpinningTrianglePlatform, RenderX - 14, RenderY - 6, 44, 39);
                    break;
                case 94:
                    if (Data[2] > 0)
                        g.DrawImage(Properties.Resources.SwooperLarge, RenderX - 6, RenderY - 16, 26, 37);
                    else
                        g.DrawImage(Properties.Resources.Swooper, RenderX, RenderY, 16, 18);
                    break;
                case 95:
                    g.DrawImage(Properties.Resources.SpinBoard, RenderX - 6, RenderY, 26, 26);
                    break;
                case 102:
                    g.DrawImage(Properties.Resources.SpikeBallSmall, RenderX, RenderY, 30, 32);
                    break;
                case 103:
                    g.DrawImage(Properties.Resources.Dorrie, RenderX - 51, RenderY - 59, 115, 76);
                    break;
                case 106:
                    g.DrawImage(Properties.Resources.RedCoin, RenderX, RenderY, 16, 16);
                    break;
                case 107:
                    img = Properties.Resources.QSwitch;
                    if (Data[2] == 1)
                        img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    else
                        RenderY -= 2;
                    g.DrawImage(img, RenderX, RenderY, 16, 18);
                    break;
                case 108:
                    img = Properties.Resources.RedSwitch;
                    if (Data[2] == 1)
                        img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    else
                        RenderY -= 2;
                    g.DrawImage(img, RenderX, RenderY, 16, 18);
                    break;
                case 109:
                    int shift = Data[2] / 0x10;
                    if (shift == 1 || shift == 3)
                        RenderX += 8;
                    if (shift == 2 || shift == 3)
                        RenderY += 8;
                    g.DrawImage(Properties.Resources.ElectricBall, RenderX - 6, RenderY - 6, 28, 27);
                    break;
                case 113:
                    g.DrawImage(Properties.Resources.CheepChomp, RenderX, RenderY, 60, 47);
                    break;
                case 115:
                    g.DrawImage(Properties.Resources.SpikeBallLarge, RenderX, RenderY - 42, 63, 58);
                    break;
                case 116:
                    g.DrawImage(Properties.Resources.WaterBug, RenderX - 16, RenderY - 5, 41, 21);
                    break;
                case 117:
                    g.DrawImage(Properties.Resources.FlyingBlock, RenderX - 12, RenderY - 8, 44, 23);
                    break;
                case 120:
                    g.DrawImage(Properties.Resources.PiranhaplantGround, RenderX, RenderY + 3, 33, 29);
                    break;
                case 127:
                    g.DrawImage(Properties.Resources.GiantSpinningPlatform, RenderX - 144, RenderY - 104, 288, 208);
                    break;
                case 130:
                    g.DrawImage(Properties.Resources.CheepCheep, RenderX, RenderY, 21, 16);
                    break;
                case 131:
                case 132:
                    g.DrawImage(Properties.Resources.MidpointFlag, RenderX, RenderY, 22, 33);
                    break;
                case 136:
                    for (int l = 0; l <= Data[2]; l++) {
                        g.DrawImage(Properties.Resources.PokeySection, RenderX, RenderY, 17, 16);
                        RenderY -= 16;
                    }
                    g.DrawImage(Properties.Resources.PokeyHead, RenderX, RenderY - 9, 18, 25);
                    break;
                case 146:
                    g.DrawImage(Properties.Resources.StarGate, RenderX, RenderY, 32, 8);
                    break;
                case 147:
                    RenderX -= Data[2] * 8 + 6;
                    g.DrawImage(Properties.Resources.BumpPlatformLeft, RenderX, RenderY, 14, 10);
                    RenderX += 14;
                    for (int l = 0; l < Data[2] - 1; l++) {
                        g.DrawImage(Properties.Resources.BumpPlatformSection, RenderX, RenderY, 16, 10);
                        RenderX += 16;
                    }
                    if (Data[2] > 0)
                        g.DrawImage(Properties.Resources.BumpPlatformRight, RenderX, RenderY, 14, 10);
                    break;
                case 148:
                    g.DrawImage(Properties.Resources.Goomba, RenderX, RenderY - 2, 16, 18);
                    break;
                case 149:
                    switch (Data[2]) {
                        case 1:
                            img = Properties.Resources.KoopaRed;
                            break;
                        case 2:
                        case 3:
                            img = Properties.Resources.KoopaBlue;
                            break;
                        default:
                            img = Properties.Resources.KoopaGreen;
                            break;
                    }
                    g.DrawImage(img, RenderX, RenderY - 13, 16, 29);
                    break;
                case 150:
                    switch (Data[2] % 0x10) {
                        case 1:
                            img = Properties.Resources.ParakoopaRed;
                            break;
                        case 2:
                            img = Properties.Resources.ParakoopaBlue;
                            break;
                        default:
                            img = Properties.Resources.ParakoopaGreen;
                            break;
                    }
                    g.DrawImage(img, RenderX, RenderY - 16, 19, 32);
                    break;
                case 152:
                    g.DrawImage(Properties.Resources.SwitchBlock, RenderX, RenderY, 16, 16);
                    break;
                case 155:
                    Rectangle rect = this.getRect();
                    rect.Offset(1, 1);
                    g.DrawRectangle(Pens.Black, rect);
                    rect.Offset(-1, -1);
                    g.DrawRectangle(Pens.White, rect);
                    g.DrawString("Warp", NSMBGraphics.SmallInfoFont, Brushes.Black, RenderX + 1, RenderY + 1);
                    g.DrawString("Warp", NSMBGraphics.SmallInfoFont, Brushes.White, RenderX, RenderY);
                    break;
                case 162:
                    RenderY += 24;
                    RenderX += 6;
                    g.DrawImage(Properties.Resources.MushroomStalkTop, RenderX, RenderY, 20, 8);
                    RenderY += 8;
                    g.DrawImage(Properties.Resources.MushroomStalk, RenderX, RenderY, 20, 16);
                    for (int l = 0; l <= Data[4] % 0x10; l++) {
                        RenderY += 16;
                        g.DrawImage(Properties.Resources.MushroomStalk, RenderX, RenderY, 20, 16);
                    }
                    RenderX = X * 16 - 16 * (Data[3] / 0x10 + 1);
                    RenderY = Y * 16;
                    img = Properties.Resources.MushroomEdge;
                    g.DrawImage(img, RenderX, RenderY, 32, 24);
                    RenderX += 32;
                    for (int l = 0; l < Data[3] / 0x10; l++) {
                        g.DrawImage(Properties.Resources.MushroomSection, RenderX, RenderY, 32, 24);
                        RenderX += 32;
                    }
                    img.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    g.DrawImage(img, RenderX, RenderY, 32, 24);
                    break;
                case 180:
                    if (Data[2] % 0x10 == 1)
                        img = Properties.Resources.FenceKoopaRed;
                    else
                        img = Properties.Resources.FenceKoopaGreen;
                    g.DrawImage(img, RenderX, RenderY, 18, 26);
                    break;
                case 183:
                    g.DrawImage(Properties.Resources.Lakitu, RenderX, RenderY - 23, 26, 39);
                    break;
                case 186:
                    g.DrawImage(Properties.Resources.Paragoomba, RenderX, RenderY - 12, 24, 28);
                    break;
                case 189:
                    g.DrawImage(Properties.Resources.Pipe, RenderX, RenderY - 48, 32, 64);
                    break;
                case 193:
                    g.DrawImage(Properties.Resources.DryBonesLarge, RenderX, RenderY - 28, 25, 44);
                    break;
                case 204:
                    g.DrawImage(Properties.Resources.JumpingFlame, RenderX, RenderY - 7, 27, 23);
                    break;
                case 207:
                    if (Data[2] == 0)
                        img = Properties.Resources.CheepCheepGiant;
                    else
                        img = Properties.Resources.CheepCheepGiantGreen;
                    g.DrawImage(img, RenderX - 20, RenderY - 32, 63, 52);
                    break;
                case 211:
                    g.DrawImage(Properties.Resources.Blooper, RenderX, RenderY - 16, 26, 32);
                    break;
                case 220:
                    g.DrawImage(Properties.Resources.BowserJr, RenderX, RenderY - 25, 31, 41);
                    break;
                case 222:
                    if (Data[2] / 0x10 > 0)
                        RenderX += 8;
                    if (Data[2] % 0x10 > 0)
                        RenderY += 8;
                    g.DrawImage(Properties.Resources.MiniGoomba, RenderX, RenderY, 9, 9);
                    break;
                case 223:
                    g.DrawImage(Properties.Resources.FlipGateSmall, RenderX, RenderY, 32, 32);
                    break;
                case 224:
                    g.DrawImage(Properties.Resources.FlipGateLarge, RenderX, RenderY, 192, 64);
                    break;
                case 226:
                    g.DrawImage(Properties.Resources.HangingScuttleBug, RenderX - 14, RenderY + 80, 44, 37);
                    g.DrawLine(new Pen(Color.White, 2), RenderX + 8, RenderY, RenderX + 8, RenderY + 82);
                    break;
                case 228:
                    g.DrawImage(Properties.Resources.RouletteBlock, RenderX, RenderY, 20, 20);
                    break;
                case 235:
                    switch (Data[2] / 0x10) {
                        case 1:
                            RenderX += 8;
                            break;
                        case 2:
                            RenderX -= 8;
                            break;
                    }
                    switch (Data[2] % 0x10) {
                        case 1:
                            RenderY += 8;
                            break;
                        case 2:
                            RenderY -= 8;
                            break;
                    }
                    g.DrawImage(Properties.Resources.StarCoin, RenderX, RenderY, 32, 32);
                    break;
                case 236:
                    if (Data[5] / 0x10 > 0)
                        RenderY += 8;
                    g.DrawImage(Properties.Resources.SpinningSquarePlatform, RenderX - 16, RenderY - 16, 48, 48);
                    break;
                case 238:
                    g.DrawImage(Properties.Resources.MushroomStalkTop, RenderX + 6, RenderY + 24, 20, 8);
                    RenderY += 32;
                    for (int l = 0; l < 5; l++){
                        g.DrawImage(Properties.Resources.MushroomStalk, RenderX + 6, RenderY, 20, 16);
                        RenderY += 16;
                    }
                    RenderX = X * 16 - 16 * (Data[2] / 0x10 + 1);
                    RenderY = Y * 16;
                    img = Properties.Resources.PurpleMushroomEdge;
                    g.DrawImage(img, RenderX, RenderY, 32, 24);
                    RenderX += 32;
                    for (int l = 0; l < Data[2] / 0x10; l++) {
                        g.DrawImage(Properties.Resources.PurpleMushroomSection, RenderX, RenderY, 32, 24);
                        RenderX += 32;
                    }
                    img.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    g.DrawImage(img, RenderX, RenderY, 32, 24);
                    break;
                case 244:
                    switch (Data[2] / 0x10) {
                        case 1:
                            RenderX -= 8;
                            break;
                        case 2:
                            RenderX += 8;
                            break;
                    }
                    g.DrawImage(Properties.Resources.BouncingMushroom, RenderX - 40, RenderY, 96, 32);
                    RenderY += 32;
                    RenderX += 3;
                    for (int l = 0; l < Data[2] % 0x10; l++) {
                        g.DrawImage(Properties.Resources.BouncingMushroomStalk, RenderX, RenderY, 10, 16);
                        RenderY += 16;
                    }
                    break;
                case 246:
                    g.DrawImage(Properties.Resources.Barrel, RenderX - 16, RenderY - 8, 48, 49);
                    break;
                case 249:
                    RenderX -= Data[2] % 0x10 * 8 + 24;
                    RenderY += Data[3] / 0x10 * 16 + 88;
                    int XOff = Data[2] % 0x10 * 16 + 48;
                    for (int l = 0; l < Data[3] / 0x10 + 4; l++) {
                        RenderY -= 16;
                        g.DrawImage(Properties.Resources.WallJumpPlatformBlock, RenderX, RenderY, 16, 16);
                        g.DrawImage(Properties.Resources.WallJumpPlatformBlock, RenderX + XOff, RenderY, 16, 16);
                    }
                    for (int l = 0; l < Data[2] % 0x10 + 2; l++){
                        RenderX += 16;
                        g.DrawImage(Properties.Resources.WallJumpPlatformBlock, RenderX, RenderY, 16, 16);
                    }
                    g.DrawImage(Properties.Resources.LineAttachment, X * 16 - 3, Y * 16 + 5, 22, 22);
                    break;
                case 277:
                    int direction = Data[5] % 8;
                    if (direction == 0 || direction == 2 || direction == 4 || direction == 6)
                        img = Properties.Resources.Arrow;
                    else
                        img = Properties.Resources.ArrowRotate45;
                    if (direction == 2 || direction == 3)
                        img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    if (direction == 4 || direction == 5)
                        img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    if (direction == 6 || direction == 7)
                        img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    g.DrawImage(img, RenderX, RenderY, 32, 32);
                    break;
                case 279:
                    img = Properties.Resources.OneWayDoor;
                    if (Data[2] == 0 || Data[2] == 1) {
                        img.RotateFlip(RotateFlipType.RotateNoneFlipY);
                        RenderY -= 39;
                    } else if (Data[2] == 2 || Data[2] == 3) {
                        RenderY += 16;
                    } else if (Data[2] == 4 || Data[2] == 5) {
                        img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        RenderX += 16;
                    } else if (Data[2] == 6 || Data[2] == 7) {
                        img.RotateFlip(RotateFlipType.Rotate270FlipX);
                        RenderX -= 39;
                    }
                    if (Data[2] == 0 || Data[2] == 2)
                        img.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    else if (Data[2] == 4 || Data[2] == 6)
                        img.RotateFlip(RotateFlipType.RotateNoneFlipY);
                    g.DrawImage(img, RenderX, RenderY, img.Width, img.Height);
                    break;
                case 283:
                    g.DrawImage(Properties.Resources.SpikeBass, RenderX, RenderY - 32, 54, 55);
                    break;
                case 284:
                    g.DrawImage(Properties.Resources.Pumpkin, RenderX, RenderY - 2, 18, 18);
                    break;
                case 289:
                    g.DrawImage(Properties.Resources.ExpandableBlock, RenderX, RenderY, 16, 16);
                    break;
                case 298:
                    int width = Data[2] % 0x10;
                    int height = Data[2] / 0x10;
                    int spikes = Data[5] % 0x10;
                    if (spikes == 1 || spikes == 3) {
                        height -= 1;
                        RenderY += 16;
                    }
                    if (spikes == 2 || spikes == 3)
                        height -= 1;
                    if (spikes == 4 || spikes == 6)
                        width -= 1;
                    if (spikes == 5 || spikes == 6) {
                        width -= 1;
                        RenderX += 16;
                    }
                    if (width < 1)
                        width = 1;
                    if (height < 0)
                        height = 0;
                    int xp = RenderX;
                    int yp = RenderY;
                    if (height == 0) {
                        g.DrawImage(Properties.Resources.StoneBlockFlatHorizLeft, RenderX, RenderY, 16, 16);
                        for (int l = 0; l < width - 1; l++) {
                            RenderX += 16;
                            g.DrawImage(Properties.Resources.StoneBlockFlatHorizMiddle, RenderX, RenderY, 16, 16);
                        }
                        RenderX += 16;
                        g.DrawImage(Properties.Resources.StoneBlockFlatHorizRight, RenderX, RenderY, 16, 16);
                    }
                    else {
                        g.DrawImage(Properties.Resources.StoneBlockTopLeft, RenderX, RenderY, 16, 16);
                        for (int l = 0; l < width - 1; l++) {
                            RenderX += 16;
                            g.DrawImage(Properties.Resources.StoneBlockTop, RenderX, RenderY, 16, 16);
                        }
                        RenderX += 16;
                        g.DrawImage(Properties.Resources.StoneBlockTopRight, RenderX, RenderY, 16, 16);
                        for (int l = 0; l < height - 1; l++) {
                            RenderY += 16;
                            g.DrawImage(Properties.Resources.StoneBlockRight, RenderX, RenderY, 16, 16);
                        }
                        RenderY += 16;
                        g.DrawImage(Properties.Resources.StoneBlockBottomRight, RenderX, RenderY, 16, 16);
                        for (int l = 0; l < width - 1; l++) {
                            RenderX -= 16;
                            g.DrawImage(Properties.Resources.StoneBlockBottom, RenderX, RenderY, 16, 16);
                        }
                        RenderX -= 16;
                        g.DrawImage(Properties.Resources.StoneBlockBottomLeft, RenderX, RenderY, 16, 16);
                        for (int l = 0; l < height - 1; l++) {
                            RenderY -= 16;
                            g.DrawImage(Properties.Resources.StoneBlockLeft, RenderX, RenderY, 16, 16);
                        }
                        RenderX = xp;
                        RenderY = yp;
                        int xStart = RenderX;
                        for (int l = 0; l < height - 1; l++) {
                            RenderY += 16;
                            for (int m = 0; m < width - 1; m++) {
                                RenderX += 16;
                                g.DrawImage(Properties.Resources.StoneBlockMiddle, RenderX, RenderY, 16, 16);
                            }
                            RenderX = xStart;
                        }
                    }
                    if (spikes == 1 || spikes == 3) {
                        RenderX = X * 16;
                        RenderY = Y * 16;
                        for (int l = 0; l <= width; l++)  {
                            g.DrawImage(Properties.Resources.StoneBlockSpikes, RenderX, RenderY, 16, 16);
                            RenderX += 16;
                        }
                    }
                    if (spikes == 2 || spikes == 3) {
                        RenderX = X * 16;
                        RenderY = (Y + height + 1) * 16;
                        if (spikes == 3)
                            RenderY += 16;
                        Bitmap spikes2 = Properties.Resources.StoneBlockSpikes;
                        spikes2.RotateFlip(RotateFlipType.RotateNoneFlipY);
                        for (int l = 0; l <= Data[2] % 0x10; l++) {
                            g.DrawImage(spikes2, RenderX, RenderY, 16, 16);
                            RenderX += 16;
                        }
                    }
                    if (spikes == 4 || spikes == 6) {
                        RenderX = (X + width + 1) * 16;
                        RenderY = Y * 16;
                        if (spikes == 6)
                            RenderX += 16;
                        Bitmap spikes2 = Properties.Resources.StoneBlockSpikes;
                        spikes2.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        for (int l = 0; l <= Data[2] / 0x10; l++) {
                            g.DrawImage(spikes2, RenderX, RenderY, 16, 16);
                            RenderY += 16;
                        }
                    }
                    if (spikes == 5 || spikes == 6) {
                        RenderX = X * 16;
                        RenderY = Y * 16;
                        Bitmap spikes2 = Properties.Resources.StoneBlockSpikes;
                        spikes2.RotateFlip(RotateFlipType.Rotate90FlipX);
                        for (int l = 0; l <= height; l++) {
                            g.DrawImage(spikes2, RenderX, RenderY, 16, 16);
                            RenderY += 16;
                        }
                    }
                    break;
                case 323:
                    g.DrawImage(Properties.Resources.CloudLeftEdge, RenderX, RenderY, 16, 26);
                    RenderX += 16;
                    for (int l = 0; l <= Data[2]; l++) {
                        g.DrawImage(Properties.Resources.CloudSection, RenderX, RenderY, 32, 32);
                        RenderX += 32;
                    }
                    g.DrawImage(Properties.Resources.CloudRightEdge, RenderX, RenderY, 16, 25);
                    break;
                default:
                    customRendered = false;
                    if (Level.ValidSprites[this.Type])
                        img = Properties.Resources.sprite;
                    else
                        img = Properties.Resources.sprite_invalid;
                    g.DrawImage(img, RenderX, RenderY, 16, 16);
                    g.DrawString(Type.ToString(), NSMBGraphics.SmallInfoFont, Brushes.White, (float)RenderX, (float)RenderY);
                    break;
            }

            if (!Level.ValidSprites[this.Type] && customRendered)
                g.FillRectangle(new SolidBrush(Color.FromArgb(100, 255, 0, 0)), this.getRect());

        }
    }
}
