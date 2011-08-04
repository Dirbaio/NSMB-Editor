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
using NSMBe4.DSFileSystem;
using System.IO;
using NSMBe4.NSBMD;
using System.Diagnostics;


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
                LanguageManager.Load(Properties.Resources.english.Split('\n'));
            }

            SpriteData.Load();  

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

            if(path != "")
            {
                if (Properties.Settings.Default.mdi)
                {
                    Application.Run(new MdiParentForm(path));
                }
                else
                    Application.Run(new LevelChooser(path));
                if (Properties.Settings.Default.AutoUpdateSD)
                    SpriteData.update();
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