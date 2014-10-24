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

        public BackgroundDragEditionMode bgdragem;

        public ToolsForm tools;
        public SpriteEventsViewer sprEvents;

        public LevelEditor(NSMBLevel Level)
        {
            InitializeComponent();

            this.Level = Level;
            this.GFX = Level.GFX;
            coordinateViewer1.EdControl = levelEditorControl1;
            //This is supposed to reduce flickering on stuff like the side panel...
            //But it doesn't :(
            this.SetStyle(
              ControlStyles.AllPaintingInWmPaint|
              ControlStyles.UserPaint |
              ControlStyles.DoubleBuffer, true); 
            
            if (Properties.Settings.Default.mdi)
                this.MdiParent = MdiParentForm.instance;
            if (Properties.Settings.Default.LevelMaximized)
                this.WindowState = FormWindowState.Maximized;

            smallBlockOverlaysToolStripMenuItem.Checked = Properties.Settings.Default.SmallBlockOverlays;
            showResizeHandles.Checked = Properties.Settings.Default.ShowResizeHandles;

            LanguageManager.ApplyToContainer(this, "LevelEditor");
            this.Text = LanguageManager.Get("General", "EditingSomething") + " " + Level.name;
            // these need to be added manually
            reloadTilesets.Text = LanguageManager.Get("LevelEditor", "reloadTilesets");
            smallBlockOverlaysToolStripMenuItem.Text = LanguageManager.Get("LevelEditor", "smallBlockOverlaysToolStripMenuItem");
            showResizeHandles.Text = LanguageManager.Get("LevelEditor", "showResizeHandles");
            setBgImageButton.Text = LanguageManager.Get("LevelEditor", "setBgImageButton");
            removeBgButton.Text = LanguageManager.Get("LevelEditor", "removeBgButton");
            moveBGToolStripMenuItem.Text = LanguageManager.Get("LevelEditor", "moveBGToolStripMenuItem");
            openImage.Filter = LanguageManager.Get("Filters", "image");

            levelEditorControl1.LoadUndoManager(undoButton, redoButton);

            Level.enableWrite();
            levelEditorControl1.Initialise(GFX, Level, this);

            oem = new ObjectsEditionMode(Level, levelEditorControl1);
            bgdragem = new BackgroundDragEditionMode(Level, levelEditorControl1);

            levelEditorControl1.SetEditionMode(oem);
            levelEditorControl1.minimapctrl = minimapControl1;

            tools = new ToolsForm(levelEditorControl1);
            sprEvents = new SpriteEventsViewer(levelEditorControl1);
            MinimapForm = new LevelMinimap(Level, levelEditorControl1);
            levelEditorControl1.minimap = MinimapForm;
            MinimapForm.Text = string.Format(LanguageManager.Get("LevelEditor", "MinimapTitle"), Level.name);
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
                ROM.fileBackups.Remove(Level.source.getBackupText());
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
            if (sprEvents != null)
                sprEvents.Close();
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

        private void zoomMenu_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
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

        public void zoomOut()
        {
            int idx = findZoomItemIndex();
            if (idx < zoomMenu.DropDown.Items.Count - 1)
                zoomMenu.DropDown.Items[idx + 1].PerformClick();
        }

        public void zoomIn()
        {
            int idx = findZoomItemIndex();
            if (idx > 0)
                zoomMenu.DropDown.Items[idx - 1].PerformClick();
        }

        private int findZoomItemIndex()
        {
            for (int i = 0; i < zoomMenu.DropDown.Items.Count; i++)
                if ((zoomMenu.DropDown.Items[i] as ToolStripMenuItem).Checked)
                    return i;
            return -1;
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
            if (openImage.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                Image i = Image.FromFile(openImage.FileName, false);
                removeBgButton_Click(null, null);
                levelEditorControl1.bgImage = i;
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format(LanguageManager.Get("LevelEditor", "ImageError"), ex.Message));
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
            if (moveBGToolStripMenuItem.Checked)
                levelEditorControl1.SetEditionMode(bgdragem);
            else
                levelEditorControl1.SetEditionMode(oem);
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

        private FormWindowState prevState;

        private void fullScreenButton_CheckedChanged(object sender, EventArgs e)
        {
            if (fullScreenButton.Checked)
            {
                prevState = this.WindowState;
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
                this.WindowState = prevState;
            }
        }

        public void ExitFullScreen()
        {
            fullScreenButton.Checked = false;
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            levelEditorControl1.delete();
        }

        private void lowerButton_Click(object sender, EventArgs e)
        {
            levelEditorControl1.lower();
        }

        private void raiseButton_Click(object sender, EventArgs e)
        {
            levelEditorControl1.raise();
        }

        private void backupTimer_Tick(object sender, EventArgs e)
        {
            if (!ROM.fileBackups.Contains(Level.source.getBackupText()))
            {
                ROM.fileBackups.Add(Level.source.getBackupText());
                ROM.writeBackupSetting();
            }
            levelSaver.RunWorkerAsync();
        }

        private void levelSaver_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Console.Out.WriteLine("Backing up level " + Level.source.getBackupText());
                ExportedLevel exlvl = Level.getExport();
                string backupPath = System.IO.Path.Combine(Application.StartupPath, "Backup");
                if (!System.IO.Directory.Exists(backupPath))
                    System.IO.Directory.CreateDirectory(backupPath);
                string filename = Level.source.getBackupFileName();
                System.IO.FileStream fs = new System.IO.FileStream(System.IO.Path.Combine(backupPath, filename), System.IO.FileMode.Create);
                System.IO.BinaryWriter bw = new System.IO.BinaryWriter(fs);
                exlvl.Write(bw);
                bw.Close();
            }
            catch (Exception ex) { }
        }

        private void LevelEditor_SizeChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.LevelMaximized = this.WindowState == FormWindowState.Maximized;
            Properties.Settings.Default.Save();
        }

        private void spriteEvents_Click(object sender, EventArgs e)
        {
            sprEvents.Show();
            sprEvents.BringToFront();
            sprEvents.ReloadSprites(null, null);
        }
    }
}
