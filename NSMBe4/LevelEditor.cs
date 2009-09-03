using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NSMBe4 {
    public partial class LevelEditor : Form {

        public ObjectsEditionMode oem;
        public EntrancesEditionMode eem;
        public PathsEditionMode pem;
        public ViewsEditionMode vem;
        public ObjectPickerControl opc;

        public LevelEditor(NitroClass ROM, string LevelFilename) {
            InitializeComponent();
            this.ROM = ROM;
            this.LevelFilename = LevelFilename;
            editObjectsButton.Checked = true;

            smallBlockOverlaysToolStripMenuItem.Checked = Properties.Settings.Default.SmallBlockOverlays;


            if (Properties.Settings.Default.Language == 1)
            {
                saveLevelButton.Text = "Guardar Nivel";
                viewMinimapButton.Text = "Ver Mapa";
                levelConfigButton.Text = "Configuracion de Nivel";
                toolStripLabel1.Text = "Edicion de:";
                editObjectsButton.Text = "Objetos/Sprites";
                editEntrancesButton.Text = "Entradas";
                editPathsButton.Text = "Rutas";
                optionsMenu.Text = "Opciones";
                smallBlockOverlaysToolStripMenuItem.Text = "Ver contenidos de bloque en pequeño";
                deleteAllObjectsToolStripMenuItem.Text = "Borrar todos los objetos";
                deleteAllSpritesToolStripMenuItem.Text = "Borrar todos los sprites";

            }
        }

        private void MainForm_Load(object sender, EventArgs e) {
            //ToolStripManager.RenderMode = ToolStripManagerRenderMode.System;

            // First off prepare the sprite list
            string[] spritelist = new string[324];
            string[] rawlist;
            if (Properties.Settings.Default.Language != 1) {
                rawlist = Properties.Resources.spritelist.Split('\n');
            } else {
                rawlist = Properties.Resources.spritelist_lang1.Split('\n');
            }
            foreach (string sprite in rawlist) {
                string trimmedsprite = sprite.Trim();
                if (trimmedsprite == "") continue;
                int equalPos = trimmedsprite.IndexOf('=');
                spritelist[int.Parse(trimmedsprite.Substring(0, equalPos))] = trimmedsprite.Substring(0, equalPos) + ": " + trimmedsprite.Substring(equalPos + 1);
            }

            ushort LevelFileID = ROM.FileIDs[LevelFilename + ".bin"];
            ushort LevelBGDatFileID = ROM.FileIDs[LevelFilename + "_bgdat.bin"];

            // There's a catch 22 here: Level loading requires graphics. Graphics loading requires level.
            // Therefore, I have a simple loader here which gets this info.
            byte[] LevelFile = ROM.ExtractFile(LevelFileID);
            int Block1Offset = LevelFile[0] | (LevelFile[1] << 8) | (LevelFile[2] << 16) | (LevelFile[3] << 24);
            int Block4Offset = LevelFile[12] | (LevelFile[13] << 8) | (LevelFile[14] << 16) | (LevelFile[15] << 24);
            byte TilesetID = LevelFile[Block1Offset + 0x0C];
            byte TilesetPalID = LevelFile[Block4Offset + 3];

            GFX = new NSMBGraphics(ROM);
            GFX.LoadTilesets(TilesetID, TilesetPalID);

            Level = new NSMBLevel(ROM, LevelFileID, LevelBGDatFileID, GFX);
            levelEditorControl1.Initialise(GFX, Level, this);

            opc = new ObjectPickerControl();
            opc.Initialise(GFX);
            oem = new ObjectsEditionMode(Level, levelEditorControl1, opc);
            eem = new EntrancesEditionMode(Level, levelEditorControl1);
            pem = new PathsEditionMode(Level, levelEditorControl1);
            vem = new ViewsEditionMode(Level, levelEditorControl1, true);

            levelEditorControl1.SetEditionMode(oem);

        }

        private void viewMap16ToolStripMenuItem_Click(object sender, EventArgs e) {
            if (Map16ViewerForm == null || Map16ViewerForm.IsDisposed) {
                Map16ViewerForm = new Map16Viewer(GFX);
            }
            Map16ViewerForm.Show();
        }

        private Map16Viewer Map16ViewerForm;
        private LevelMinimap MinimapForm;
        private LevelConfig LevelConfigForm;
        private NSMBLevel Level;
        private NSMBGraphics GFX;
        private NitroClass ROM;

        public string LevelFilename;

        private bool Dirty;
        private bool DataUpdateFlag;
        private bool FocusFlag;

        private UserControl SelectedPanel;

        public void SetPanel(UserControl np)
        {
            if (SelectedPanel == np) return;

            if(SelectedPanel != null)
                SelectedPanel.Parent = null;
            np.Size = PanelContainer.Size;
            np.Location = new Point(0, 0);
            np.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            SelectedPanel = np;
            if (SelectedPanel != null)
                SelectedPanel.Parent = PanelContainer;
        }

        public bool ForceClose() {
            if (Dirty) {
                DialogResult dr;
                if (Properties.Settings.Default.Language != 1) {
                    dr = MessageBox.Show("This level contains unsaved changes.\nIf you close the editor without saving, you will lose them.\nDo you want to save?", "NSMB Editor 4", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                } else {
                    dr = MessageBox.Show("Este nivel tiene cambios sin guardar.\nSi cierras el editor sin guardarlo, los pierderas.\nQuiere guardarlos?", "NSMB Editor 4", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                }
                if (dr == DialogResult.Yes) {
                    Level.Save();
                } else if (dr == DialogResult.Cancel) {
                    return true;
                }
            }

            return false;
        }

        private void saveLevelButton_Click(object sender, EventArgs e) {
            Dirty = false;
            Level.Save();
        }

        private void LevelEditor_FormClosing(object sender, FormClosingEventArgs e) {
            if (ForceClose()) {
                e.Cancel = true;
            }
        }

        private void viewMinimapButton_Click(object sender, EventArgs e) {
            if (MinimapForm == null || MinimapForm.IsDisposed) {
                MinimapForm = new LevelMinimap(Level);
                MinimapForm.ScrollEditor += new LevelMinimap.ScrollEditorDelegate(MinimapForm_ScrollEditor);
            }
            MinimapForm.ViewableArea = levelEditorControl1.ViewableArea;
            MinimapForm.Show();
        }

        private void MinimapForm_ScrollEditor(Point NewPosition) {
            levelEditorControl1.ScrollEditor(NewPosition);
        }

        private void levelConfigButton_Click(object sender, EventArgs e) {
            if (LevelConfigForm == null || LevelConfigForm.IsDisposed) {
                LevelConfigForm = new LevelConfig(Level, ROM);
            }
            LevelConfigForm.SetDirtyFlag += new LevelConfig.SetDirtyFlagDelegate(levelEditorControl1_SetDirtyFlag);
            LevelConfigForm.ReloadTileset += new LevelConfig.ReloadTilesetDelegate(LevelConfigForm_ReloadTileset);
            LevelConfigForm.RefreshMainWindow += new LevelConfig.RefreshMainWindowDelegate(LevelConfigForm_RefreshMainWindow);
            LevelConfigForm.LoadSettings();
            LevelConfigForm.Show();
        }

        private void LevelConfigForm_ReloadTileset() {
            GFX.LoadTileset1(Level.Blocks[0][0xC], Level.Blocks[3][3]);
            Level.ReRenderAll();
            opc.ReRenderAll(1);
            Invalidate(true);
        }

        private void LevelConfigForm_RefreshMainWindow() {
            Invalidate(true);
        }

        private void LevelEditor_FormClosed(object sender, FormClosedEventArgs e) {
            if (Map16ViewerForm != null) {
                Map16ViewerForm.Close();
            }
            if (MinimapForm != null) {
                MinimapForm.Close();
            }
            if (LevelConfigForm != null) {
                LevelConfigForm.Close();
            }
        }

        private void levelEditorControl1_UpdateViewableArea() {
            if (MinimapForm != null && MinimapForm.Visible) {
                MinimapForm.ViewableArea = levelEditorControl1.ViewableArea;
                MinimapForm.Invalidate(true);
            }
        }
        /*
        private void levelEditorControl1_UpdateSelectedObjInfo() {
            if (levelEditorControl1.SelectedObject == -1 && EditingMode != EditingModeType.Entrances) {
                tabControl1.Visible = false;
            } else {
                DataUpdateFlag = true;

                if (levelEditorControl1.SelectedObjectType == LevelEditorControl.ObjectType.Object) {
                    tabControl1.SelectTab(0);
                    objXPosUpDown.Value = Level.Objects[levelEditorControl1.SelectedObject].X;
                    objYPosUpDown.Value = Level.Objects[levelEditorControl1.SelectedObject].Y;
                    objWidthUpDown.Value = Level.Objects[levelEditorControl1.SelectedObject].Width;
                    objHeightUpDown.Value = Level.Objects[levelEditorControl1.SelectedObject].Height;

                    objTileset0Button.Checked = (Level.Objects[levelEditorControl1.SelectedObject].Tileset == 0);
                    objTileset1Button.Checked = (Level.Objects[levelEditorControl1.SelectedObject].Tileset == 1);
                    objTileset2Button.Checked = (Level.Objects[levelEditorControl1.SelectedObject].Tileset == 2);

                    objTypeUpDown.Value = Level.Objects[levelEditorControl1.SelectedObject].ObjNum;

                    objectPickerControl1.CurrentTileset = Level.Objects[levelEditorControl1.SelectedObject].Tileset;
                    objectPickerControl1.SelectedObject = Level.Objects[levelEditorControl1.SelectedObject].ObjNum;
                    objectPickerControl1.EnsureObjVisible((int)objTypeUpDown.Value);
                    objectPickerControl1.Invalidate(true);
                } else if (levelEditorControl1.SelectedObjectType == LevelEditorControl.ObjectType.Sprite) {
                    tabControl1.SelectTab(1);
                    spriteXPosUpDown.Value = Level.Sprites[levelEditorControl1.SelectedObject].X;
                    spriteYPosUpDown.Value = Level.Sprites[levelEditorControl1.SelectedObject].Y;
                    spriteTypeUpDown.Value = Level.Sprites[levelEditorControl1.SelectedObject].Type;

                    byte[] SpriteData = Level.Sprites[levelEditorControl1.SelectedObject].Data;
                    spriteDataTextBox.Text = String.Format(
                        "{0:X2} {1:X2} {2:X2} {3:X2} {4:X2} {5:X2}",
                        SpriteData[0], SpriteData[1], SpriteData[2],
                        SpriteData[3], SpriteData[4], SpriteData[5]);

                    spriteListBox.SelectedIndex = Level.Sprites[levelEditorControl1.SelectedObject].Type;
                } else if (levelEditorControl1.SelectedObjectType == LevelEditorControl.ObjectType.Entrance) {
                    tabControl1.SelectTab(2);
                    if (levelEditorControl1.SelectedObject == -1) {
                        entranceListBox.SelectedItem = null;
                        deleteEntranceButton.Enabled = false;
                        groupBox2.Visible = false;
                    } else {
                        entranceListBox.SelectedIndex = levelEditorControl1.SelectedObject;
                        deleteEntranceButton.Enabled = true;
                        groupBox2.Visible = true;

                        NSMBEntrance Entrance = Level.Entrances[levelEditorControl1.SelectedObject];

                        entranceListBox.Items[levelEditorControl1.SelectedObject] = String.Format("{0}: {1} ({2},{3})", Entrance.Number,
                            Properties.Settings.Default.Language != 1 ? NSMBEntrance.TypeList[Entrance.Type] : NSMBEntrance.TypeList_lang1[Entrance.Type],
                            Entrance.X, Entrance.Y);

                        entranceXPosUpDown.Value = Entrance.X;
                        entranceYPosUpDown.Value = Entrance.Y;
                        entranceCameraXPosUpDown.Value = Entrance.CameraX;
                        entranceCameraYPosUpDown.Value = Entrance.CameraY;
                        entranceNumberUpDown.Value = Entrance.Number;
                        entranceDestAreaUpDown.Value = Entrance.DestArea;
                        entrancePipeIDUpDown.Value = Entrance.ConnectedPipeID;
                        entranceDestEntranceUpDown.Value = Entrance.DestEntrance;
                        entranceTypeComboBox.SelectedIndex = Entrance.Type;
                        entranceSetting128.Checked = (bool)((Entrance.Settings & 128) != 0);
                        entranceSetting16.Checked = (bool)((Entrance.Settings & 16) != 0);
                        entranceSetting8.Checked = (bool)((Entrance.Settings & 8) != 0);
                        entranceSetting1.Checked = (bool)((Entrance.Settings & 1) != 0);
                        entranceViewUpDown.Value = Entrance.EntryView;
                                            }
                }

                DataUpdateFlag = false;
                tabControl1.Visible = true;
                if (!FocusFlag) {
                    levelEditorControl1.Focus();
                } else {
                    FocusFlag = false;
                }
            }
        }
*/
        private void levelEditorControl1_SetDirtyFlag() {
            Dirty = true;
        }

        private void smallBlockOverlaysToolStripMenuItem_Click(object sender, EventArgs e) {
            smallBlockOverlaysToolStripMenuItem.Checked = !smallBlockOverlaysToolStripMenuItem.Checked;
            GFX.RepatchBlocks(smallBlockOverlaysToolStripMenuItem.Checked);
            Properties.Settings.Default.SmallBlockOverlays = smallBlockOverlaysToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
            Level.ReRenderAll();
            Invalidate(true);
        }
        
        private void editObjectsButton_Click(object sender, EventArgs e) {
            levelEditorControl1.SetEditionMode(oem);
            editObjectsButton.Checked = true;
            editEntrancesButton.Checked = false;
            editPathsButton.Checked = false;
            editViewsButton.Checked = false;
        }

        private void editEntrancesButton_Click(object sender, EventArgs e) {
            levelEditorControl1.SetEditionMode(eem);
            editObjectsButton.Checked = false;
            editEntrancesButton.Checked = true;
            editPathsButton.Checked = false;
            editViewsButton.Checked = false;
        }

        private void editPathsButton_Click(object sender, EventArgs e) {
            levelEditorControl1.SetEditionMode(pem);
            editObjectsButton.Checked = false;
            editEntrancesButton.Checked = false;
            editPathsButton.Checked = true;
            editViewsButton.Checked = false;
        }

        private void editViewsButton_Click(object sender, EventArgs e)
        {
            levelEditorControl1.SetEditionMode(vem);
            editObjectsButton.Checked = false;
            editEntrancesButton.Checked = false;
            editPathsButton.Checked = false;
            editViewsButton.Checked = true;

        }


        private void deleteAllObjectsToolStripMenuItem_Click(object sender, EventArgs e) {
            if (Properties.Settings.Default.Language != 1) {
                if (MessageBox.Show("Are you sure you want to delete every object in the level?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) {
                    return;
                }
            } else {
                if (MessageBox.Show("Estas seguro que quieres borrar todos los objetos en el nivel?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) {
                    return;
                }
            }

            Level.Objects.Clear();
            if(levelEditorControl1.mode != null)
                levelEditorControl1.mode.Refresh();
            levelEditorControl1.Invalidate(true);
            if (MinimapForm != null && MinimapForm.Visible) {
                MinimapForm.Invalidate(true);
            }
            Dirty = true;
        }

        private void deleteAllSpritesToolStripMenuItem_Click(object sender, EventArgs e) {
            if (Properties.Settings.Default.Language != 1) {
                if (MessageBox.Show("Are you sure you want to delete every sprite in the level?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) {
                    return;
                }
            } else {
                if (MessageBox.Show("Estas seguro que quieres borrar todos los sprites en el nivel?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) {
                    return;
                }
            }

            Level.Sprites.Clear();
            if(levelEditorControl1.mode != null)
                levelEditorControl1.mode.Refresh();
            levelEditorControl1.Invalidate(true);
            if (MinimapForm != null && MinimapForm.Visible) {
                MinimapForm.Invalidate(true);
            }
            Dirty = true;
        }

    }
}
