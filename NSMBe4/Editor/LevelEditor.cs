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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NSMBe4.DSFileSystem;


namespace NSMBe4 {
    public partial class LevelEditor : Form {

        public ObjectsEditionMode oem;

        //TODO: Kill this shit!
        public BackgroundDragEditionMode bgdragem;

        public ToolsForm tools;

        public LevelEditor(string LevelFilename, string LevelName)
        {
            InitializeComponent();
            File LevelFileID = ROM.FS.getFileByName(LevelFilename + ".bin");
            File LevelBGDatFileID = ROM.FS.getFileByName(LevelFilename + "_bgdat.bin");
            LoadEditor(LevelFilename, LevelName, LevelFileID.getContents(), LevelBGDatFileID.getContents(), LevelFileID, LevelBGDatFileID);
        }

        public LevelEditor(string LevelFilename, string LevelName, byte[] levelFileData, byte[] bgdatFileData)
        {
            InitializeComponent();
            File LevelFileID = ROM.FS.getFileByName(LevelFilename + ".bin");
            File LevelBGDatFileID = ROM.FS.getFileByName(LevelFilename + "_bgdat.bin");
            LoadEditor(LevelFilename, LevelName, levelFileData, bgdatFileData, LevelFileID, LevelBGDatFileID);
        }

        public LevelEditor(string ExportedFileName, byte[] levelFileData, byte[] bgFileData, ushort SavedLevelFileID, ushort SavedBGFileID)
        {
            InitializeComponent();
            if (ExportedFileName == "")
                LoadEditor("", "Level on Clipboard", levelFileData, bgFileData, null, null);
            else
                LoadEditor(ExportedFileName, "Exported Level - " + ExportedFileName, levelFileData, bgFileData, null, null);
            Level.SavedLevelFileID = SavedLevelFileID;
            Level.SavedBGFileID = SavedBGFileID;
        }

        private void LoadEditor(string LevelFilename, string LevelName, byte[] LevelFile, byte[] BGDatFile, File LevelFileID, File LevelBGDatFileID) {
            coordinateViewer1.EdControl = levelEditorControl1;
            //This is supposed to reduce flickering on stuff like the side panel...
            //But it doesn't :(
            this.SetStyle(
              ControlStyles.AllPaintingInWmPaint|
              ControlStyles.UserPaint |
              ControlStyles.DoubleBuffer, true); 
            

            if (Properties.Settings.Default.mdi)
                this.MdiParent = MdiParentForm.instance;
            this.LevelFilename = LevelFilename;

            smallBlockOverlaysToolStripMenuItem.Checked = Properties.Settings.Default.SmallBlockOverlays;
            showResizeHandles.Checked = Properties.Settings.Default.ShowResizeHandles;

            LanguageManager.ApplyToContainer(this, "LevelEditor");
            this.Text = LanguageManager.Get("General", "EditingSomething") + " " + LevelName;
            // these need to be added manually
            reloadTilesets.Text = LanguageManager.Get("LevelEditor", "reloadTilesets");
            smallBlockOverlaysToolStripMenuItem.Text = LanguageManager.Get("LevelEditor", "smallBlockOverlaysToolStripMenuItem");
            setBgImageButton.Text = LanguageManager.Get("LevelEditor", "setBgImageButton");
            removeBgButton.Text = LanguageManager.Get("LevelEditor", "removeBgButton");
            moveBGToolStripMenuItem.Text = LanguageManager.Get("LevelEditor", "moveBGToolStripMenuItem");

            levelEditorControl1.LoadUndoManager(undoButton, redoButton);

            // There's a catch 22 here: Level loading requires graphics. Graphics loading requires level.
            // Therefore, I have a simple loader here which gets this info.
            int Block1Offset = LevelFile[0] | (LevelFile[1] << 8) | (LevelFile[2] << 16) | (LevelFile[3] << 24);
            int Block3Offset = LevelFile[16] | (LevelFile[17] << 8) | (LevelFile[18] << 16) | (LevelFile[19] << 24);
            byte TilesetID = LevelFile[Block1Offset + 0x0C];
            byte BGNSCID = LevelFile[Block3Offset + 2];

            GFX = new NSMBGraphics();
            GFX.LoadTilesets(TilesetID, BGNSCID);
            if (LevelFileID == null)
                Level = new NSMBLevel(LevelFilename, LevelFile, BGDatFile, GFX);
            else
                Level = new NSMBLevel(LevelFileID, LevelBGDatFileID, LevelFile, BGDatFile, GFX);
            Level.enableWrite();
            levelEditorControl1.Initialise(GFX, Level, this);

            oem = new ObjectsEditionMode(Level, levelEditorControl1);
            bgdragem = new BackgroundDragEditionMode(Level, levelEditorControl1);

            levelEditorControl1.SetEditionMode(oem);
            levelEditorControl1.minimapctrl = minimapControl1;

            tools = new ToolsForm(levelEditorControl1);
            MinimapForm = new LevelMinimap(Level, levelEditorControl1);
            levelEditorControl1.minimap = MinimapForm;
            MinimapForm.Text = string.Format(LanguageManager.Get("LevelEditor", "MinimapTitle"), LevelName);
            minimapControl1.loadMinimap(Level, levelEditorControl1);
            this.Icon = Properties.Resources.nsmbe;

            if (Properties.Settings.Default.AutoBackup > 0)
            {
                backupTimer.Interval = Properties.Settings.Default.AutoBackup * 60000;
                backupTimer.Start();
            }
        }

        private void reloadTilesets_Click(object sender, EventArgs e) {
            byte TilesetID = Level.Blocks[0][0x0C];
            byte BGNSCID = Level.Blocks[2][2];
            LevelConfigForm_ReloadTileset();
        }

        private LevelMinimap MinimapForm;
        private LevelConfig LevelConfigForm;
        private NSMBLevel Level;
        private NSMBGraphics GFX;

        public string LevelFilename;

        private UserControl SelectedPanel;

        public void SetPanel(UserControl np)
        {
            if (SelectedPanel == np) return;
            
            if (SelectedPanel != null)
                SelectedPanel.Parent = null;
            np.Dock = DockStyle.Fill;
            np.Size = PanelContainer.Size;
//            np.Size = PanelContainer.Size;
//            np.Location = new Point(0, 0);
//            np.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            SelectedPanel = np;
            if (SelectedPanel != null)
                SelectedPanel.Parent = PanelContainer;
        }

        private void saveLevelButton_Click(object sender, EventArgs e) {
            levelEditorControl1.UndoManager.Clean();
            Level.Save();
        }

        private void LevelEditor_FormClosing(object sender, FormClosingEventArgs e) {
            if (levelEditorControl1.UndoManager.dirty) {
                DialogResult dr;
                dr = MessageBox.Show(LanguageManager.Get("LevelEditor", "UnsavedLevel"), "NSMB Editor 4", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                if (dr == DialogResult.Yes) {
                    Level.Save();
                } else if (dr == DialogResult.Cancel) {
                    e.Cancel = true;
                }
            }
            if (!e.Cancel)
            {
                ROM.fileBackups.Remove(LevelFilename);
                ROM.writeBackupSetting();
            }
        }

        private void viewMinimapButton_Click(object sender, EventArgs e)
        {
            MinimapForm.Show();
        }

        public void LevelConfigForm_ReloadTileset() {
            GFX.LoadTilesets(Level.Blocks[0][0xC], Level.Blocks[2][2]);
            Level.ReRenderAll();

            Level.repaintAllTilemap();
            levelEditorControl1.updateTileCache(true);
            levelEditorControl1.repaint();

            oem.ReloadObjectPicker();
            Invalidate(true);
        }

        private void LevelEditor_FormClosed(object sender, FormClosedEventArgs e) {
            if (MinimapForm != null) {
                MinimapForm.Close();
            }

            if (tools != null)
                tools.Close();
            GFX.close();
            Level.close();
        }

        private void smallBlockOverlaysToolStripMenuItem_Click(object sender, EventArgs e) {
            GFX.RepatchBlocks(smallBlockOverlaysToolStripMenuItem.Checked);
            Properties.Settings.Default.SmallBlockOverlays = smallBlockOverlaysToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
            Level.ReRenderAll();
            levelEditorControl1.updateTileCache(true);
            Invalidate(true);
        }

        private void showResizeHandles_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.ShowResizeHandles = showResizeHandles.Checked;
            Properties.Settings.Default.Save();
            oem.resizeHandles = showResizeHandles.Checked;
            Invalidate(true);
        }

        private void spriteFinder_Click(object sender, EventArgs e)
        {
            tools.Show();
            tools.BringToFront();
        }

        private void cutButton_Click(object sender, EventArgs e)
        {
            levelEditorControl1.cut();
        }

        private void copyButton_Click(object sender, EventArgs e)
        {
            levelEditorControl1.copy();
        }

        private void pasteButton_Click(object sender, EventArgs e)
        {
            levelEditorControl1.paste();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            levelEditorControl1.delete();
        }

        private void toolStripDropDownButton1_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            foreach (ToolStripMenuItem it in zoomMenu.DropDown.Items)
                it.Checked = false;
            (e.ClickedItem as ToolStripMenuItem).Checked = true;

            String s = e.ClickedItem.Text;

            int ind = s.IndexOf(" %");
            s = s.Remove(ind);

            float z = Int32.Parse(s);
            levelEditorControl1.SetZoom(z / 100);
        }

        private void editTileset_Click(object sender, EventArgs e)
        {
            try
            {
                new TilesetEditor(Level.Blocks[0][0xC], "").Show();
            }
            catch (AlreadyEditingException)
            {
                MessageBox.Show(LanguageManager.Get("Errors", "Tileset"));
            }
        }

        private void setBgImageButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                Image i = Image.FromFile(openFileDialog1.FileName, false);
                removeBgButton_Click(null, null);
                levelEditorControl1.bgImage = i;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading image file: " + ex.Message);
            }
            levelEditorControl1.repaint();
        }

        private void removeBgButton_Click(object sender, EventArgs e)
        {
            if (levelEditorControl1.bgImage != null)
            {
                levelEditorControl1.bgImage.Dispose();
                levelEditorControl1.bgImage = null;
            }
            levelEditorControl1.repaint();
        }

        private void moveBGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO: Fix this shit
            levelEditorControl1.SetEditionMode(bgdragem);
//            uncheckModeButtons();
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            PanelContainer.Invalidate(true);
        }

        private void dsScreenShowButton_Click(object sender, EventArgs e)
        {
            levelEditorControl1.showDSScreen = dsScreenShowButton.Checked;
            levelEditorControl1.repaint();
        }

        private void snapToggleButton_Click(object sender, EventArgs e)
        {
            oem.snapTo8Pixels = snapToggleButton.Checked;
            oem.UpdateSelectionBounds();
        }

        private void showGridButton_Click(object sender, EventArgs e)
        {
            levelEditorControl1.showGrid = showGridButton.Checked;
            levelEditorControl1.repaint();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            levelEditorControl1.delete();
        }

        private void backupTimer_Tick(object sender, EventArgs e)
        {
            if (!ROM.fileBackups.Contains(LevelFilename))
            {
                ROM.fileBackups.Add(LevelFilename);
                ROM.writeBackupSetting();
            }
            levelSaver.RunWorkerAsync();
        }

        private void levelSaver_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Console.Out.WriteLine("Backing up level " + LevelFilename);
                byte[] LevelFileData;
                byte[] BGDatFileData;
                levelEditorControl1.Level.getSaveFiles(out LevelFileData, out BGDatFileData);
                string backupPath = System.IO.Path.Combine(Application.StartupPath, "Backup");
                if (!System.IO.Directory.Exists(backupPath))
                    System.IO.Directory.CreateDirectory(backupPath);
                System.IO.FileStream fs = new System.IO.FileStream(System.IO.Path.Combine(backupPath, LevelFilename + ".nml"), System.IO.FileMode.Create);
                System.IO.BinaryWriter bw = new System.IO.BinaryWriter(fs);
                NSMBLevel.ExportLevel(levelEditorControl1.Level.LevelFile.id, levelEditorControl1.Level.BGFile.id, LevelFileData, BGDatFileData, bw);
                bw.Close();
            }
            catch (Exception ex) { }
        }
    }
}
