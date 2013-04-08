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
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.IO;

using NSMBe4.DSFileSystem;
using NSMBe4.NSBMD;
using NSMBe4.Patcher;


namespace NSMBe4
{
    public static class Program
    {

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string langDir = System.IO.Path.Combine(Application.StartupPath, "Languages");
            string langFileName = System.IO.Path.Combine(langDir, Properties.Settings.Default.LanguageFile + ".ini");
            if (System.IO.File.Exists(langFileName))
            {
                System.IO.StreamReader rdr = new StreamReader(langFileName);
                LanguageManager.Load(rdr.ReadToEnd().Split('\n'));
                rdr.Close();
            }
            else
            {
                MessageBox.Show("File " + langFileName + " could not be found, so the language has defaulted to English.");
                LanguageManager.Load(Properties.Resources.English.Split('\n'));
            }

            new StartForm().Show();
            Application.Run();

            //string[] args = Environment.GetCommandLineArgs();


            /*
              
            if(args.Length > 2 && args[2] == "asmpatch")
            {
				PatchMaker pm = new PatchMaker(ROM.romfile.Directory);
				pm.restore();
				pm.generatePatch();
            }
            else if(args.Length > 2 && args[2] == "getcodeaddr")
            {
				PatchMaker pm = new PatchMaker(ROM.romfile.Directory);
				pm.restore();
                Console.Out.WriteLine(String.Format("{0:X8}", pm.getCodeAddr()));
            }*/
        }
    }
}
