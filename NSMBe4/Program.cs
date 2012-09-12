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


            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            TraceListener log = new TextWriterTraceListener("NSMBe4-log.txt");
            Trace.Listeners.Add(log);
            Trace.Listeners.Add(new ConsoleTraceListener());

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

            //This makes the editor behave BAD when no internet. 
            //This actually catches the error now, but I'm leaving it disabled because the Sprite DB outputs a corrupt file

            //if (Properties.Settings.Default.AutoUpdateSD)
            //    SpriteData.update();


            string path = "";
            string[] args = Environment.GetCommandLineArgs();

            if (args.Length > 1)
                path = args[1];
            else
            {
                OpenFileDialog openROMDialog = new OpenFileDialog();
                openROMDialog.Filter = LanguageManager.Get("LevelChooser", "ROMFilter");
                if (openROMDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    path = openROMDialog.FileName;
            }


            if (path != "")
            {
                try
                {
                    ROM.load(path);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not open ROM file for writing. Is it open with other program?\n"+ex.Message);
                    return;
                }
                
                if(args.Length > 2 && args[2] == "asmpatch")
                {
				    PatchMaker pm = new PatchMaker(ROM.romfile.Directory);
				    pm.generatePatch();
                }
                else if(args.Length > 2 && args[2] == "getcodeaddr")
                {
				    PatchMaker pm = new PatchMaker(ROM.romfile.Directory);
                    Console.Out.WriteLine(String.Format("{0:X8}", pm.getCodeAddr()));
                }
				else
				{
		            SpriteData.Load();
		            if (Properties.Settings.Default.mdi)
		                Application.Run(new MdiParentForm());
		            else
		                Application.Run(new LevelChooser());
		        }
            }
        }


        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                Exception ex = (Exception)e.ExceptionObject;

                Trace.Write("Whoops! Please contact the developers with the following information:\n\n" + ex.Message + ex.StackTrace + ex.InnerException.Message + ex.InnerException.StackTrace);
                Trace.Flush();
            }
            finally
            {
                Application.Exit();
            }
        }
    }
}
