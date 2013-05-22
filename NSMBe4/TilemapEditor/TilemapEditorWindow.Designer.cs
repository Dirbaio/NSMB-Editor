namespace NSMBe4.TilemapEditor
{
    partial class TilemapEditorWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tilemapEditor1 = new NSMBe4.TilemapEditor.TilemapEditor();
            this.SuspendLayout();
            // 
            // tilemapEditor1
            // 
            this.tilemapEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tilemapEditor1.Location = new System.Drawing.Point(0, 0);
            this.tilemapEditor1.Name = "tilemapEditor1";
            this.tilemapEditor1.Size = new System.Drawing.Size(921, 589);
            this.tilemapEditor1.TabIndex = 0;
            // 
            // TilemapEditorWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(921, 589);
            this.Controls.Add(this.tilemapEditor1);
            this.Name = "TilemapEditorWindow";
            this.Text = "<Tilemap Editor>";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TilemapEditorWindow_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private TilemapEditor tilemapEditor1;
    }
}