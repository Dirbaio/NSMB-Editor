using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using NSMBe4.DSFileSystem;
using System.IO;
using NSMBe4.NSBMD;


namespace NSMBe4 {
    /*public class TestLister : FileLister {
        public void DirReady(int DirID, int ParentID, string DirName, bool IsRoot) { }
        public void FileReady(int FileID, int ParentID, string FileName) { }
    }*/

    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            SpriteData.Load();
            //just for tesing

            /*
            NitroClass ROM = new NitroClass();
            ROM.LoadROM("C:\\Documents and Settings\\admin\\Escritorio\\gba\\SLOT\\New Super Mario Bros.nds");
            NSMBTileset t = new NSMBTileset(ROM, 398, 576, 776, 833, 836, false);
            ImagePreviewer.ShowCutImage(t.Map16Buffer, 256, 2);

            new Map16Viewer(t).Show();

//            Application.Run();*/

            // MORE TESTING!
            /*System.IO.File.Delete(@"C:\C\Emulation\ROMs\NDS\NSMBJTestOverlay0Edit.nds");
            System.IO.File.Copy(@"C:\C\Emulation\ROMs\NDS\442- New Super Mario Bros. (J)(WRG).nds", @"C:\C\Emulation\ROMs\NDS\NSMBJTestOverlay0Edit.nds");
            NitroClass ROM = new NitroClass(@"C:\C\Emulation\ROMs\NDS\NSMBJTestOverlay0Edit.nds");
            ROM.Load(new TestLister());
            ROM.load(ROM);
            ROM.SaveOverlay0();

            return;*/

            //AGAIN MORE TESTING! xD
            /*if (System.IO.File.Exists(@"E:\DCIM\100PENTX\IMGP1000.JPG")) {
                ImageIndexer.index(new Bitmap(@"E:\DCIM\100PENTX\IMGP1000.JPG"), 256);
            } else if (Application.StartupPath == @"H:\Users\Jan\Documents\Visual Studio 2008\Projects\NSMBe4\NSMBe4\bin\Debug") {
                //yeah, I copied your idea...
                //new ImagePreviewer(ImageIndexer.index(new Bitmap(@"C:\C\dcp3\224_PANA\P1320701.JPG"), 256)).Show();
                // well, converting a 3648x2736 10mp file is a bad idea, this is why: http://treeki.shacknet.nu/screenshots/misc/freeze.png
                new ImagePreviewer(ImageIndexer.index(new Bitmap(@"C:\htdocs\desktop.jpg"), 256)).Show();
            }*/
            /*
            string rom = @"C:\Documents and Settings\admin\Escritorio\no$gba_debug\SLOT\New Super Mario Bros U orig.nds";
            
            NitroFilesystem fs = new NitroFilesystem(rom);
            fs.mainDir.dumpFiles();
             
            
            NitroClass c = new NitroClass(rom);
            c.Load(null);
            */
            /*
            string rom = @"C:\Documents and Settings\admin\Escritorio\no$gba_debug\SLOT\New Super Mario Bros U orig.nds";
            string romc = @"C:\Documents and Settings\admin\Escritorio\no$gba_debug\SLOT\New Super Mario Bros U fstest.nds";

            Console.Out.WriteLine("Copying rom...");
            System.IO.File.Copy(rom, romc, true);

            Console.Out.WriteLine("Loading FS...");
            NitroFilesystem fs = new NitroFilesystem(romc);
            Random r = new Random();
            while (!fs.findErrors())
            {
                int size = r.Next(10000)+1;
                byte[] data = new byte[size];
                File f = null;
                while (f == null || f.isSystemFile)
                {
                    int i = r.Next(fs.allFiles.Count);
                    f = fs.allFiles[i];
                }
                Console.Out.WriteLine("Replacing " + f.name + " with length " + size);
                f.replace(data);
            }
            fs.dumpFilesOrdered();

            return;*//*
            
            string rom = @"C:\Documents and Settings\admin\Escritorio\no$gba_debug\SLOT\nsmb tstest.nds";
            ROM.load(rom);

            NSMBGraphics g = new NSMBGraphics();
            g.LoadTilesets(0);
            g.Tilesets[1].ImportGFX(@"C:\Documents and Settings\admin\Escritorio\no$gba_debug\SLOT\caca.png");
            g.Tilesets[1].enableWrite();
            g.Tilesets[1].save();
            ROM.close();
           */
            /*
            string path = @"C:\Documents and Settings\admin\Escritorio\no$gba_debug\SLOT\";
            string file = path + "Copia de Hard Super Dario Bros.nds";

            NitroFilesystem fs = new NitroFilesystem(file);
            List<DSFileSystem.File> filesToMove = new List<NSMBe4.DSFileSystem.File>();
            foreach (DSFileSystem.File f in fs.allFiles)
            {
                if (f.fileBegin > 0x1Fe800 && f.fileBegin < 0x1Fe800 + 0x0386A0 && f.fileBegin != 0)
                {
                    filesToMove.Add(f);
                }
            }

            foreach (DSFileSystem.File f in filesToMove)
            {
                Console.Out.WriteLine("Moving " + f.name);
                f.moveTo(fs.getFilesystemEnd());
            }

            fs.close();

            FileStream s = new FileStream(file, FileMode.Open, FileAccess.ReadWrite);

            Bitmap b = new Bitmap(path + "hsdbintroa.png");
            byte[] data = compressImage(b);
            uint imgoffs = 0x1Fe800 + 0x0286A0 + 0x1000;
            Console.Out.WriteLine("Inserting image at 0x"+imgoffs.ToString("X")+
                ", Size: 0x" + data.Length.ToString("X") + " bytes");
            s.Seek(imgoffs, SeekOrigin.Begin);
            s.Write(data, 0, data.Length);

            byte[] header = new byte[0x15E];
            s.Seek(0, SeekOrigin.Begin);
            s.Read(header, 0, 0x15E);

            ushort crc16 = ROM.CalcCRC16(header);
            s.Seek(0x15e, SeekOrigin.Begin);
            s.WriteByte((byte)(crc16 & 0xff));
            s.WriteByte((byte)(crc16 >> 8));
            s.Close();
            return;


             */

            /*
            Bitmap b = new Bitmap("C:\\image.png");
            new ImageTiler(b);
           */
            /*
            string path = @"C:\Documents and Settings\admin\Escritorio\no$gba_debug\SLOT\";
            string file = path + "Copia de Hard Super Dario Bros.nds";

            NitroFilesystem fs = new NitroFilesystem(file);
            new TextureEditor(fs.getFileByName("I_kinoko_ashib2u.nsbtx")).Show();

             */

            if (Properties.Settings.Default.Language == 0) {
                LanguageManager.Load(Properties.Resources.english.Split('\n'));
            } else if (Properties.Settings.Default.Language == 1) {
                LanguageManager.Load(Properties.Resources.spanish.Split('\n'));
            }

            Application.Run(new LevelChooser());
        }

        public static byte[] compressImage(Bitmap b)
        {
            ByteArrayOutputStream img = new ByteArrayOutputStream();
            bool first = true;
            ushort currVal = 0, currCount = 0;
            for (int y = 0; y < b.Height; y++)
                for (int x = 0; x < b.Width; x++)
                {
                    ushort val = NSMBTileset.toRGB15(b.GetPixel(x, y));
                    if ((val == currVal && !first) || currCount == 0xFFFF)
                    {
                        currCount++;
                    }
                    else
                    {
                        if (!first)
                        {
                            if (currCount == 1)
                                img.writeUShort(currVal);
                            else
                            {
                                img.writeUShort((ushort)(currVal | (ushort)0x8000));
                                img.writeUShort(currCount);
                            }
                        }
                        currVal = val;
                        currCount = 1;
                    }
                    first = false;
                }
            if (currCount == 1)
                img.writeUShort(currVal);
            else
            {
                img.writeUShort((ushort)(currVal | (ushort)0x8000));
                img.writeUShort(currCount);
            }
            return img.getArray();
        }
    }
}