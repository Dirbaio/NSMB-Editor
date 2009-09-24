using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NSMBe4 {
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

            Application.Run(new LevelChooser());
        }
    }
}
