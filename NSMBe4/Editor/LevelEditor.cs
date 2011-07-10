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
        public EntrancesEditionMode eem;
        public PathsEditionMode pem, ppem;
        public ViewsEditionMode vem, zem;
        public BackgroundDragEditionMode bgdragem;

        public ObjectPickerControl opc;

        public ToolsForm tools;
        private List<ToolStripButton> EditionModeButtons;

        public LevelEditor(string LevelFilename) {
            InitializeComponent();
            if (Properties.Settings.Default.mdi)
                this.MdiParent = MdiParentForm.instance;
            this.LevelFilename = LevelFilename;
            editObjectsButton.Checked = true;

            smallBlockOverlaysToolStripMenuItem.Checked = Properties.Settings.Default.SmallBlockOverlays;

            EditionModeButtons = new List<ToolStripButton>();
            EditionModeButtons.Add(editObjectsButton);
            EditionModeButtons.Add(editEntrancesButton);
            EditionModeButtons.Add(editPathsButton);
            EditionModeButtons.Add(editProgressButton);
            EditionModeButtons.Add(editViewsButton);
            EditionModeButtons.Add(editZonesButton);

            LanguageManager.ApplyToContainer(this, "LevelEditor");
            // these need to be added manually
            reloadTilesets.Text = LanguageManager.Get("LevelEditor", "reloadTilesets");
            smallBlockOverlaysToolStripMenuItem.Text = LanguageManager.Get("LevelEditor", "smallBlockOverlaysToolStripMenuItem");
            deleteAllObjectsToolStripMenuItem.Text = LanguageManager.Get("LevelEditor", "deleteAllObjectsToolStripMenuItem");
            deleteAllSpritesToolStripMenuItem.Text = LanguageManager.Get("LevelEditor", "deleteAllSpritesToolStripMenuItem");

            levelEditorControl1.LoadUndoManager(undoButton, redoButton);

            // First off prepare the sprite list
            string[] spritelist = new string[324];

            foreach (string sprite in LanguageManager.GetList("Sprites"))
            {
                string trimmedsprite = sprite.Trim();
                if (trimmedsprite == "") continue;
                int equalPos = trimmedsprite.IndexOf('=');
                spritelist[int.Parse(trimmedsprite.Substring(0, equalPos))] = trimmedsprite.Substring(0, equalPos) + ": " + trimmedsprite.Substring(equalPos + 1);
            }

            File LevelFileID = ROM.FS.getFileByName(LevelFilename + ".bin");
            File LevelBGDatFileID = ROM.FS.getFileByName(LevelFilename + "_bgdat.bin");

            // There's a catch 22 here: Level loading requires graphics. Graphics loading requires level.
            // Therefore, I have a simple loader here which gets this info.
            byte[] LevelFile = LevelFileID.getContents();
            int Block1Offset = LevelFile[0] | (LevelFile[1] << 8) | (LevelFile[2] << 16) | (LevelFile[3] << 24);
            int Block3Offset = LevelFile[16] | (LevelFile[17] << 8) | (LevelFile[18] << 16) | (LevelFile[19] << 24);
            byte TilesetID = LevelFile[Block1Offset + 0x0C];
            byte BGNSCID = LevelFile[Block3Offset + 2];

            GFX = new NSMBGraphics();
            GFX.LoadTilesets(TilesetID, BGNSCID);

            Level = new NSMBLevel(LevelFileID, LevelBGDatFileID, GFX);
            Level.enableWrite();
            levelEditorControl1.Initialise(GFX, Level, this);

            opc = new ObjectPickerControl();
            opc.Initialise(GFX);
            oem = new ObjectsEditionMode(Level, levelEditorControl1, opc);
            eem = new EntrancesEditionMode(Level, levelEditorControl1);
            pem = new PathsEditionMode(Level, levelEditorControl1, Level.Paths);
            ppem = new PathsEditionMode(Level, levelEditorControl1, Level.ProgressPaths);
            vem = new ViewsEditionMode(Level, levelEditorControl1, true);
            zem = new ViewsEditionMode(Level, levelEditorControl1, false);
            bgdragem = new BackgroundDragEditionMode(Level, levelEditorControl1);

            levelEditorControl1.SetEditionMode(oem);
            levelEditorControl1.minimapctrl = minimapControl1;

            tools = new ToolsForm(levelEditorControl1);
            MinimapForm = new LevelMinimap(Level, levelEditorControl1);
            levelEditorControl1.minimap = MinimapForm;
            MinimapForm.Text = string.Format(LanguageManager.Get("LevelEditor", "MinimapTitle"), this.Text);
            minimapControl1.loadMinimap(Level, levelEditorControl1);
        }

        private void reloadTilesets_Click(object sender, EventArgs e) {
            byte TilesetID = Level.Blocks[0][0x0C];
            byte BGNSCID = Level.Blocks[2][2];
            GFX.LoadTilesets(TilesetID, BGNSCID);
            oem.ReloadObjectPicker();
            Level.ReRenderAll();
            Refresh();
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
            np.Size = PanelContainer.Size;
            np.Location = new Point(0, 0);
            np.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
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
        }

        private void viewMinimapButton_Click(object sender, EventArgs e)
        {
            MinimapForm.Show();
        }

        private void levelConfigButton_Click(object sender, EventArgs e) {
            if (LevelConfigForm == null || LevelConfigForm.IsDisposed) {
                LevelConfigForm = new LevelConfig(levelEditorControl1);
            }
            LevelConfigForm.ReloadTileset += new LevelConfig.ReloadTilesetDelegate(LevelConfigForm_ReloadTileset);
            LevelConfigForm.RefreshMainWindow += new LevelConfig.RefreshMainWindowDelegate(LevelConfigForm_RefreshMainWindow);
            LevelConfigForm.LoadSettings();
            LevelConfigForm.Show();
        }

        private void LevelConfigForm_ReloadTileset() {
            GFX.LoadTilesets(Level.Blocks[0][0xC], Level.Blocks[2][2]);
            Level.ReRenderAll();
            opc.ReRenderAll(1);
            Invalidate(true);
        }

        private void LevelConfigForm_RefreshMainWindow() {
            Invalidate(true);
        }

        private void LevelEditor_FormClosed(object sender, FormClosedEventArgs e) {
            if (MinimapForm != null) {
                MinimapForm.Close();
            }
            if (LevelConfigForm != null) {
                LevelConfigForm.Close();
            }
            if (tools != null)
                tools.Close();
            GFX.close();
            Level.close();
        }

        private void smallBlockOverlaysToolStripMenuItem_Click(object sender, EventArgs e) {
            smallBlockOverlaysToolStripMenuItem.Checked = !smallBlockOverlaysToolStripMenuItem.Checked;
            GFX.RepatchBlocks(smallBlockOverlaysToolStripMenuItem.Checked);
            Properties.Settings.Default.SmallBlockOverlays = smallBlockOverlaysToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
            Level.ReRenderAll();
            Invalidate(true);
        }

        private void uncheckModeButtons()
        {
            foreach (ToolStripButton b in EditionModeButtons)
                b.Checked = false;
        }
        
        private void editObjectsButton_Click(object sender, EventArgs e) {
            levelEditorControl1.SetEditionMode(oem);
            uncheckModeButtons();
            editObjectsButton.Checked = true;
        }

        private void editEntrancesButton_Click(object sender, EventArgs e) {
            levelEditorControl1.SetEditionMode(eem);
            uncheckModeButtons();
            editEntrancesButton.Checked = true;
        }

        private void editPathsButton_Click(object sender, EventArgs e) {
            levelEditorControl1.SetEditionMode(pem);
            uncheckModeButtons();
            editPathsButton.Checked = true;
        }
        private void editProgressButton_Click(object sender, EventArgs e)
        {
            levelEditorControl1.SetEditionMode(ppem);
            uncheckModeButtons();
            editProgressButton.Checked = true;
        }

        private void editViewsButton_Click(object sender, EventArgs e)
        {
            levelEditorControl1.SetEditionMode(vem);
            uncheckModeButtons();
            editViewsButton.Checked = true;

        }

        private void editZonesButton_Click(object sender, EventArgs e)
        {
            levelEditorControl1.SetEditionMode(zem);
            uncheckModeButtons();
            editZonesButton.Checked = true;
        }

        private void deleteAllObjectsToolStripMenuItem_Click(object sender, EventArgs e) {
            if (Level.Objects.Count == 0)
                return;
            if (MessageBox.Show(LanguageManager.Get("LevelEditor", "ConfirmDelObjects"), LanguageManager.Get("General", "Question"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) {
                return;
            }
            levelEditorControl1.UndoManager.Do(new RemoveMultipleAction(Level.Objects.ToArray()));
        }

        private void deleteAllSpritesToolStripMenuItem_Click(object sender, EventArgs e) {
            if (Level.Sprites.Count == 0)
                return;
            if (MessageBox.Show(LanguageManager.Get("LevelEditor", "ConfirmDelSprites"), LanguageManager.Get("General", "Question"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) {
                return;
            }
            levelEditorControl1.UndoManager.Do(new RemoveMultipleAction(Level.Sprites.ToArray()));
        }

        private void spriteFinder_Click(object sender, EventArgs e)
        {
            tools.Show();
            tools.BringToFront();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            levelEditorControl1.cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            levelEditorControl1.copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
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

            removeBgButton_Click(null, null);
            Image i = Image.FromFile(openFileDialog1.FileName, false);
            levelEditorControl1.bgImage = i;
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
            levelEditorControl1.SetEditionMode(bgdragem);
            uncheckModeButtons();
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            PanelContainer.Invalidate(true);
        }
    }
}
