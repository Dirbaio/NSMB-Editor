using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NSMBe4.DSFileSystem;

namespace NSMBe4
{
    public partial class StartForm : Form
    {
        bool close = true;

        public StartForm()
        {
            InitializeComponent();
        }

        private void openRomButton_Click(object sender, EventArgs e)
        {

            string path = "";
            string[] backups = null;

            if (Properties.Settings.Default.BackupFiles != "" &&
                MessageBox.Show("NSMBe did not shut down correctly and has recovered some of your levels.\nWould you like to open those now? If not, they can be opened later from the /Backup folder", "Open backups?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                backups = Properties.Settings.Default.BackupFiles.Split(';');
                path = backups[0];
            }
            else
            {
                OpenFileDialog openROMDialog = new OpenFileDialog();
                openROMDialog.Filter = LanguageManager.Get("LevelChooser", "ROMFilter");
                if (Properties.Settings.Default.ROMFolder != "")
                    openROMDialog.InitialDirectory = Properties.Settings.Default.ROMFolder;
                if (openROMDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    path = openROMDialog.FileName;
            }

            if (path == "")
                return;

            try
            {
                NitroROMFilesystem fs = new NitroROMFilesystem(path);
                Properties.Settings.Default.ROMFolder = System.IO.Path.GetDirectoryName(path);
                Properties.Settings.Default.Save();

                if (backups != null)
                    for (int l = 1; l < backups.Length; l++)
                        ROM.fileBackups.Add(backups[l]);

                run(fs);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            NetFilesystem fs = new NetFilesystem(hostTextBox.Text, Int32.Parse(portTextBox.Text));
            run(fs);
        }

        private void run(Filesystem fs)
        {
            ROM.load(fs);

            SpriteData.Load();
            if (Properties.Settings.Default.mdi)
                new MdiParentForm().Show();
            else
                new LevelChooser().Show();

            close = false;
            Close();
        }

        private void StartForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(close)
                Application.Exit();
        }

    }
}
