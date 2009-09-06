using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NSMBe4
{
    public partial class ToolsForm : Form
    {

        private LevelEditorControl EdControl;
        private NSMBSprite foundSprite;

        public ToolsForm(LevelEditorControl edc)
        {
            InitializeComponent();
            this.EdControl = edc;
        }

        private void spriteFindNext_Click(object sender, EventArgs e)
        {
            if (EdControl.Level.Sprites.Count == 0)
            {
                MessageBox.Show("No sprites in level.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int ind = -1;
            if (foundSprite != null && EdControl.Level.Sprites.Contains(foundSprite))
                ind = EdControl.Level.Sprites.IndexOf(foundSprite);


            int startInd = ind;
            ind++;
            ind %= EdControl.Level.Sprites.Count;
            bool found = false;

            while (ind != startInd && !found)
            {
                if (EdControl.Level.Sprites[ind].Type == SpriteNumber.Value)
                {
                    foundSprite = EdControl.Level.Sprites[ind];
                    found = true;
                }
                ind++;
                ind %= EdControl.Level.Sprites.Count;
            }

            if (found)
            {
                EdControl.SelectObject(foundSprite);
                EdControl.EnsurePosVisible(foundSprite.X, foundSprite.Y);
            }
            else
                MessageBox.Show("No sprites found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void spriteCount_Click(object sender, EventArgs e)
        {
            int count = 0;
            foreach (NSMBSprite s in EdControl.Level.Sprites)
                if (s.Type == SpriteNumber.Value)
                    count++;

            MessageBox.Show("Sprite "+SpriteNumber.Value+" appears "+count+" times in this level.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void spriteDelete_Click(object sender, EventArgs e)
        {
            List<NSMBSprite> toDelete = new List<NSMBSprite>();

            foreach (NSMBSprite s in EdControl.Level.Sprites)
                if (s.Type == SpriteNumber.Value)
                    toDelete.Add(s);

            foreach (NSMBSprite s in toDelete)
                EdControl.Level.Sprites.Remove(s);

            EdControl.FireSetDirtyFlag();
            EdControl.repaint();
            MessageBox.Show(toDelete.Count + " sprites deleted.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void spriteReplaceAll_Click(object sender, EventArgs e)
        {
            int count = 0;
            foreach (NSMBSprite s in EdControl.Level.Sprites)
                if (s.Type == SpriteNumber.Value)
                {
                    count++;
                    s.Type = (int) newSpriteNumber.Value;
                }

            EdControl.FireSetDirtyFlag();
            EdControl.repaint();
            MessageBox.Show(count + " sprites replaced.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ToolsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
