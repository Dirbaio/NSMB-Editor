using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NSMBe4
{
    public partial class TilesetChooser : Form
    {
        NitroClass ROM;
        public TilesetChooser(NitroClass ROM)
        {
            InitializeComponent();
            LanguageManager.ApplyToContainer(this, "TilesetChooser");

            this.ROM = ROM;

            string[] rawlist;
            string[] parsedlist;

            // Add tilesets to list
            if (Properties.Settings.Default.Language != 1) {
                rawlist = Properties.Resources.tilesetlist.Split('\n');
            } else {
                rawlist = Properties.Resources.tilesetlist_lang1.Split('\n');
            }

            int index = 0;
            parsedlist = new string[76];
            foreach (string name in rawlist) {
                string trimmedname = name.Trim();
                if (trimmedname == "") continue;
                parsedlist[index] = trimmedname;
                index += 1;
            }

            tilesetComboBox.Items.AddRange(parsedlist);
        }

        private void editJyotyuButton_Click(object sender, EventArgs e) {
            new TilesetEditor(ROM, 65535, "Jyotyu").Show();
            Close();
        }

        private void openTilesetButton_Click(object sender, EventArgs e) {
            new TilesetEditor(ROM, (ushort)tilesetComboBox.SelectedIndex, (string)tilesetComboBox.SelectedItem).Show();
            Close();
        }
    }
}
