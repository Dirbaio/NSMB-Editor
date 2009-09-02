using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace NSMBe4 {
    public partial class LevelChooser : Form {
        public LevelChooser() {
            InitializeComponent();
        }

        private void LevelChooser_Load(object sender, EventArgs e) {
            if (Properties.Settings.Default.Language == 1) {
                tabPage1.Text = "Sistema de archivos";
                label1.Text = "Info seleccion:";
                extractFileButton.Text = "Extraer";
                replaceFileButton.Text = "Sustituir";
                compressFileButton.Text = "Comprimir LZ";
                decompressFileButton.Text = "Descomprimir LZ";

                tabPage2.Text = "Editor de niveles";
                importLevelButton.Text = "Importar nivel";
                exportLevelButton.Text = "Exportar nivel";
                editLevelButton.Text = "Editar nivel";

                tabPage3.Text = "Opciones";
                label2.Text = "Idioma:";
                changeLanguageButton.Text = "Cambiar";
                dataFinderButton.Text = "Buscar Data";
            }

            if (openROMDialog.ShowDialog() == DialogResult.Cancel) {
                Application.Exit();
            } else {
                extractFileButton.Enabled = false;
                replaceFileButton.Enabled = false;
                compressFileButton.Enabled = false;
                decompressFileButton.Enabled = false;

                importLevelButton.Enabled = false;
                exportLevelButton.Enabled = false;
                editLevelButton.Enabled = false;
                hexEditLevelButton.Enabled = false;

                DirHolder = new Dictionary<int, TreeNode>();
                ROM = new NitroClass();
                ROM.DirReady += new NitroClass.DirReadyD(ROM_DirReady);
                ROM.FileReady += new NitroClass.FileReadyD(ROM_FileReady);
                ROM.LoadROM(openROMDialog.FileName);

                LoadLevelNames();

                OpenEditors = new List<LevelEditor>();
                OpenLevelHexEditors = new List<LevelHexEditor>();
            }
        }

        private void ROM_DirReady(int DirID, int ParentID, string DirName, bool IsRoot) {
            if (IsRoot) {
                DirHolder[61440] = fileTreeView.Nodes.Add("61440", "Root [" + openROMDialog.FileName.Substring(openROMDialog.FileName.LastIndexOf('\\') + 1) + "]", 0, 0);
                DirHolder[61440].Tag = "61440";
            } else {
                DirHolder[DirID] = DirHolder[ParentID].Nodes.Add(DirID.ToString(), DirName, 0, 0);
                DirHolder[DirID].Tag = DirID.ToString();
            }
        }

        private void ROM_FileReady(int FileID, int ParentID, string FileName) {
            DirHolder[ParentID].Nodes.Add(FileID.ToString(), FileName, 2, 2).Tag = FileID.ToString();
        }

        private Dictionary<int, TreeNode> DirHolder;
        public static NitroClass ROM;

        private void fileTreeView_AfterSelect(object sender, TreeViewEventArgs e) {
            ushort FSObjId = Convert.ToUInt16(e.Node.Tag);
            string StatusMsg;
            if (FSObjId >= 61440) {
                if (Properties.Settings.Default.Language != 1) {
                    StatusMsg = "Directory: " + e.Node.Text + " - ID " + e.Node.Tag;
                } else {
                    StatusMsg = "Carpeta: " + e.Node.Text + " - ID " + e.Node.Tag;
                }
                extractFileButton.Enabled = false;
                replaceFileButton.Enabled = false;
                compressFileButton.Enabled = false;
                decompressFileButton.Enabled = false;
            } else {
                if (Properties.Settings.Default.Language != 1) {
                    StatusMsg = "Offset: 0x" + ROM.FileOffsets[FSObjId].ToString("X") + " - Size: " + ROM.FileSizes[FSObjId].ToString() + " bytes - ID " + e.Node.Tag;
                } else {
                    StatusMsg = "Posicion: 0x" + ROM.FileOffsets[FSObjId].ToString("X") + " - Tamaño: " + ROM.FileSizes[FSObjId].ToString() + " bytes - ID " + e.Node.Tag;
                }
                extractFileButton.Enabled = true;
                replaceFileButton.Enabled = true;
                compressFileButton.Enabled = true;
                decompressFileButton.Enabled = true;
            }
            selectedFileInfo.Text = StatusMsg;
        }

        private void LoadLevelNames() {
            string[] LevelNames;
            if (Properties.Settings.Default.Language != 1) {
                LevelNames = NSMBe4.Properties.Resources.levelnames.Split('\n');
            } else {
                LevelNames = NSMBe4.Properties.Resources.levelnames_lang1.Split('\n');
            }

            TreeNode WorldNode = null;
            string WorldID = null;
            TreeNode LevelNode;
            TreeNode AreaNode;
            for (int NameIdx = 0; NameIdx < LevelNames.Length; NameIdx++) {
                LevelNames[NameIdx] = LevelNames[NameIdx].Trim();
                if (LevelNames[NameIdx] == "") continue;
                if (LevelNames[NameIdx][0] == '-') {
                    // Create a world
                    string[] ParseWorld = LevelNames[NameIdx].Substring(1).Split('|');
                    WorldNode = levelTreeView.Nodes.Add("ln" + NameIdx.ToString(), ParseWorld[0]);
                    WorldID = ParseWorld[1];
                } else {
                    // Create a level
                    string[] ParseLevel = LevelNames[NameIdx].Split('|');
                    if (ParseLevel[2] == "1") {
                        // One area only; no need for a subfolder here
                        LevelNode = WorldNode.Nodes.Add("ln" + NameIdx.ToString(), ParseLevel[0]);
                        LevelNode.Tag = WorldID + ParseLevel[1] + "_1";
                    } else {
                        // Create a subfolder
                        LevelNode = WorldNode.Nodes.Add("ln" + NameIdx.ToString(), ParseLevel[0]);
                        int AreaCount = int.Parse(ParseLevel[2]);
                        for (int AreaIdx = 1; AreaIdx <= AreaCount; AreaIdx++) {
                            AreaNode = LevelNode.Nodes.Add("ln" + NameIdx.ToString() + "a" + AreaIdx.ToString(), "Area " + AreaIdx.ToString());
                            AreaNode.Tag = WorldID + ParseLevel[1] + "_" + AreaIdx.ToString();
                        }
                    }
                }
            }
        }

        private void levelTreeView_AfterSelect(object sender, TreeViewEventArgs e) {
            if (e.Node.Tag != null) {
                importLevelButton.Enabled = true;
                exportLevelButton.Enabled = true;
                editLevelButton.Enabled = true;
                hexEditLevelButton.Enabled = true;
            } else {
                importLevelButton.Enabled = false;
                exportLevelButton.Enabled = false;
                editLevelButton.Enabled = false;
                hexEditLevelButton.Enabled = false;
            }
        }

        private void editLevelButton_Click(object sender, EventArgs e) {
            if (levelTreeView.SelectedNode == null) return;

            // Make sure this level isn't already open
            if (OpenEditors.Count > 0) {
                foreach (LevelEditor le in OpenEditors) {
                    if (!le.IsDisposed && le.LevelFilename == (string)levelTreeView.SelectedNode.Tag) {
                        le.Show();
                        return;
                    }
                }
            }

            if (OpenLevelHexEditors.Count > 0) {
                foreach (LevelHexEditor le in OpenLevelHexEditors) {
                    if (!le.IsDisposed && le.LevelFilename == (string)levelTreeView.SelectedNode.Tag) {
                        if (Properties.Settings.Default.Language != 1) {
                            MessageBox.Show("You are currently editing this level in a hex editor. To prevent problems, you can't edit it normally at the same time.", "NSMB Editor 4", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        } else {
                            MessageBox.Show("Tienes un editor hex abierto para este nivel. Para que no pasen problemas, no puedes editar el nivel en el editor normal a la misma vez.", "NSMB Editor 4", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        return;
                    }
                }
            }

            // Make a caption
            String EditorCaption;
            if (Properties.Settings.Default.Language != 1) {
                EditorCaption = "Editing ";
            } else {
                EditorCaption = "Edicion de ";
            }

            if (levelTreeView.SelectedNode.Parent.Parent == null) {
                EditorCaption += levelTreeView.SelectedNode.Text;
            } else {
                EditorCaption += levelTreeView.SelectedNode.Parent.Text + ", " + levelTreeView.SelectedNode.Text;
            }

            // Open it
            LevelEditor NewEditor = new LevelEditor(ROM, (string)levelTreeView.SelectedNode.Tag);
            OpenEditors.Add(NewEditor);
            NewEditor.Text = EditorCaption;
            NewEditor.Show();
        }

        private void hexEditLevelButton_Click(object sender, EventArgs e) {
            if (levelTreeView.SelectedNode == null) return;

            // Make sure this level isn't already open
            if (OpenEditors.Count > 0) {
                foreach (LevelEditor le in OpenEditors) {
                    if (!le.IsDisposed && le.LevelFilename == (string)levelTreeView.SelectedNode.Tag) {
                        if (Properties.Settings.Default.Language != 1) {
                            MessageBox.Show("You are currently editing this level in a normal editor. To prevent problems, you can't hex edit it at the same time.", "NSMB Editor 4", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        } else {
                            MessageBox.Show("Tienes un editor normal abierto para este nivel. Para que no pasen problemas, no puedes editar el nivel en el editor hex a la misma vez.", "NSMB Editor 4", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        return;
                    }
                }
            }

            if (OpenLevelHexEditors.Count > 0) {
                foreach (LevelHexEditor le in OpenLevelHexEditors) {
                    if (!le.IsDisposed && le.LevelFilename == (string)levelTreeView.SelectedNode.Tag) {
                        le.Show();
                        return;
                    }
                }
            }

            // Make a caption
            String EditorCaption;
            if (Properties.Settings.Default.Language != 1) {
                EditorCaption = "Editing ";
            } else {
                EditorCaption = "Edicion de ";
            }

            if (levelTreeView.SelectedNode.Parent.Parent == null) {
                EditorCaption += levelTreeView.SelectedNode.Text;
            } else {
                EditorCaption += levelTreeView.SelectedNode.Parent.Text + ", " + levelTreeView.SelectedNode.Text;
            }

            // Open it
            LevelHexEditor NewEditor = new LevelHexEditor(ROM, (string)levelTreeView.SelectedNode.Tag);
            OpenLevelHexEditors.Add(NewEditor);
            NewEditor.Text = EditorCaption;
            NewEditor.Show();
        }

        private List<LevelEditor> OpenEditors;
        private List<LevelHexEditor> OpenLevelHexEditors;

        private void LevelChooser_FormClosing(object sender, FormClosingEventArgs e) {
            if (OpenEditors != null && OpenEditors.Count > 0) {
                foreach (LevelEditor le in OpenEditors) {
                    if (!le.IsDisposed && le.ForceClose()) {
                        e.Cancel = true;
                        return;
                    }
                }
            }
        }

        private void levelTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e) {
            if (e.Node.Tag != null) {
                editLevelButton_Click(null, null);
            }
        }

        private void extractFileButton_Click(object sender, EventArgs e) {
            ushort FSObjID = Convert.ToUInt16(fileTreeView.SelectedNode.Tag);
            string FileName = ROM.FileNames[FSObjID];
            extractFileDialog.FileName = FileName;
            if (extractFileDialog.ShowDialog() == DialogResult.OK) {
                string DestFileName = extractFileDialog.FileName;
                byte[] TempFile = ROM.ExtractFile(FSObjID);
                FileStream wfs = new FileStream(DestFileName, FileMode.Create, FileAccess.Write, FileShare.None);
                wfs.Write(TempFile, 0, TempFile.GetLength(0));
                wfs.Dispose();
            }
        }

        private void replaceFileButton_Click(object sender, EventArgs e) {
            ushort FSObjID = Convert.ToUInt16(fileTreeView.SelectedNode.Tag);
            string FileName = ROM.FileNames[FSObjID];
            replaceFileDialog.FileName = FileName;
            if (replaceFileDialog.ShowDialog() == DialogResult.OK) {
                string SrcFileName = replaceFileDialog.FileName;
                FileStream rfs = new FileStream(SrcFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                byte[] TempFile = new byte[rfs.Length];
                rfs.Read(TempFile, 0, (int)rfs.Length);
                rfs.Dispose();
                ROM.ReplaceFile(FSObjID, TempFile);
            }
        }

        private void compressFileButton_Click(object sender, EventArgs e) {
            ushort FSObjID = Convert.ToUInt16(fileTreeView.SelectedNode.Tag);
            byte[] RawFile = ROM.ExtractFile(FSObjID);
            byte[] CompFile = ROM.LZ77_Compress(RawFile);
            ROM.ReplaceFile(FSObjID, CompFile);
        }

        private void decompressFileButton_Click(object sender, EventArgs e) {
            ushort FSObjID = Convert.ToUInt16(fileTreeView.SelectedNode.Tag);
            byte[] CompFile = ROM.ExtractFile(FSObjID);
            byte[] RawFile = ROM.LZ77_Decompress(CompFile);
            ROM.ReplaceFile(FSObjID, RawFile);
        }

        private void importLevelButton_Click(object sender, EventArgs e) {
            if (levelTreeView.SelectedNode == null) return;

            // Make sure this level isn't already open
            if (OpenEditors.Count > 0) {
                foreach (LevelEditor le in OpenEditors) {
                    if (!le.IsDisposed && le.LevelFilename == (string)levelTreeView.SelectedNode.Tag) {
                        if (Properties.Settings.Default.Language != 1) {
                            MessageBox.Show(
                                "You currently have an editor open with this level. Please close it before you import a new level into this slot.",
                                "Can't Replace", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        } else {
                            MessageBox.Show(
                                "Tienes un editor abierto con este nivel. Cerrar el editor antes que importa un nuevo nivel en esta posicion.",
                                "No puedes sustituir", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        return;
                    }
                }
            }

            // Figure out what file to import
            if (importLevelDialog.ShowDialog() == DialogResult.Cancel) {
                return;
            }

            // Get the file IDs
            string LevelFilename = (string)levelTreeView.SelectedNode.Tag;
            ushort LevelFileID = ROM.FileIDs[LevelFilename + ".bin"];
            ushort BGFileID = ROM.FileIDs[LevelFilename + "_bgdat.bin"];

            // Load it
            FileStream fs = new FileStream(importLevelDialog.FileName, FileMode.Open);
            BinaryReader br = new BinaryReader(fs);
            NSMBLevel.ImportLevel(ROM, LevelFileID, BGFileID, br);
            br.Close();
        }

        private void exportLevelButton_Click(object sender, EventArgs e) {
            if (levelTreeView.SelectedNode == null) return;

            // Figure out what file to export to
            if (exportLevelDialog.ShowDialog() == DialogResult.Cancel) {
                return;
            }

            // Get the file IDs
            string LevelFilename = (string)levelTreeView.SelectedNode.Tag;
            ushort LevelFileID = ROM.FileIDs[LevelFilename + ".bin"];
            ushort BGFileID = ROM.FileIDs[LevelFilename + "_bgdat.bin"];

            // Load it
            FileStream fs = new FileStream(exportLevelDialog.FileName, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            NSMBLevel.ExportLevel(ROM, LevelFileID, BGFileID, bw);
            bw.Close();
        }

        private void changeLanguageButton_Click(object sender, EventArgs e) {
            if (languageListBox.SelectedItem != null) {
                Properties.Settings.Default.Language = languageListBox.SelectedIndex;
                Properties.Settings.Default.Save();

                if (Properties.Settings.Default.Language != 1) {
                    MessageBox.Show("Language changed. You will have to close and re-open the editor to see the effect.",
                        "Changed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                } else {
                    MessageBox.Show("Idioma cambiado. Tendras que cerrar y abrir el editor para ver el efecto.",
                        "Cambiado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private DataFinder DataFinderForm;

        private void dataFinderButton_Click(object sender, EventArgs e) {
            if (DataFinderForm == null || DataFinderForm.IsDisposed) {
                DataFinderForm = new DataFinder(ROM);
            }

            DataFinderForm.Show();
            DataFinderForm.Activate();
        }
    }
}
