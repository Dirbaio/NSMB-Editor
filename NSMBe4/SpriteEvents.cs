using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Be.Timvw.Framework.ComponentModel;

namespace NSMBe4
{
    public partial class SpriteEventsViewer : Form
    {
        LevelEditorControl ed;
        SortableBindingList<SpriteDataRow> spriteList = new SortableBindingList<SpriteDataRow>();
        bool refreshing = false;

        public SpriteEventsViewer(LevelEditorControl ed)
        {
            InitializeComponent();
            this.ed = ed;
            spriteTable.DataSource = spriteList;
        }

        public void ReloadSprites(object sender, EventArgs e)
        {
            refreshing = true;
            spriteList.Clear();
            foreach (NSMBSprite s in ed.Level.Sprites) {
                if (s.Data[0] != 0)
                    spriteList.Add(new SpriteDataRow(s, s.Data[0]));
                if (s.Data[1] != 0)
                    spriteList.Add(new SpriteDataRow(s, s.Data[1]));
            }
            spriteTable.ClearSelection();
            refreshing = false;
        }

        private void SpriteEvents_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void spriteTable_SelectionChanged(object sender, EventArgs e)
        {
            if (refreshing) return;
            List<LevelItem> sprites = new List<LevelItem>();
            NSMBSprite s;
            foreach (DataGridViewRow row in spriteTable.SelectedRows)
            {
                s = spriteList[row.Index].sprite;
                if (ed.Level.Sprites.Contains(s) && !sprites.Contains(s))
                    sprites.Add(s);
            }
            ed.SelectObject(sprites);
            ed.ScrollToObjects(sprites);
            ed.repaint();
            this.BringToFront();
        }

        private class SpriteDataRow
        {
            public NSMBSprite sprite;
            public int _eventID;

            public SpriteDataRow(NSMBSprite sprite, int eventID)
            {
                this.sprite = sprite;
                this._eventID = eventID;
            }

            public int eventID {
                get {
                    return _eventID;
                }
            }
            public int spriteType {
                get {
                    return sprite.Type;
                }
            }
            public string spriteName {
                get {
                    return SpriteData.spriteNames[sprite.Type];
                }
            }
        }
    }
}
