using System;
using System.Collections.Generic;
using System.Windows.Forms;

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
            NSMBDataHandler.load(ROM);
            NSMBDataHandler.SaveOverlay0();

            return;*/


            if (Properties.Settings.Default.Language == 0) {
                LanguageManager.Load(Properties.Resources.english.Split('\n'));
            } else if (Properties.Settings.Default.Language == 1) {
                LanguageManager.Load(Properties.Resources.spanish.Split('\n'));
            }

            Application.Run(new LevelChooser());
        }
    }
}
