namespace NSMBe4.TilemapEditor
{
    partial class TilemapEditor
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.tilemapEditorControl1 = new NSMBe4.TilemapEditorControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tilePicker1 = new NSMBe4.TilePicker();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.saveButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.drawToolButton = new System.Windows.Forms.ToolStripButton();
            this.xFlipToolButton = new System.Windows.Forms.ToolStripButton();
            this.yFlipToolButton = new System.Windows.Forms.ToolStripButton();
            this.copyToolButton = new System.Windows.Forms.ToolStripButton();
            this.pasteToolButton = new System.Windows.Forms.ToolStripButton();
            this.changePalToolButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.undoButton = new System.Windows.Forms.ToolStripButton();
            this.redoButton = new System.Windows.Forms.ToolStripButton();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.tilemapEditorControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(280, 373);
            this.panel1.TabIndex = 2;
            // 
            // tilemapEditorControl1
            // 
            this.tilemapEditorControl1.AutoSize = true;
            this.tilemapEditorControl1.Location = new System.Drawing.Point(4, 3);
            this.tilemapEditorControl1.MinimumSize = new System.Drawing.Size(256, 224);
            this.tilemapEditorControl1.Name = "tilemapEditorControl1";
            this.tilemapEditorControl1.Size = new System.Drawing.Size(256, 398);
            this.tilemapEditorControl1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.tilePicker1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(280, 25);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(284, 373);
            this.panel2.TabIndex = 3;
            // 
            // tilePicker1
            // 
            this.tilePicker1.AutoSize = true;
            this.tilePicker1.Location = new System.Drawing.Point(3, 3);
            this.tilePicker1.MinimumSize = new System.Drawing.Size(256, 224);
            this.tilePicker1.Name = "tilePicker1";
            this.tilePicker1.Size = new System.Drawing.Size(278, 265);
            this.tilePicker1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveButton,
            this.toolStripSeparator1,
            this.drawToolButton,
            this.xFlipToolButton,
            this.yFlipToolButton,
            this.copyToolButton,
            this.pasteToolButton,
            this.changePalToolButton,
            this.toolStripSeparator2,
            this.undoButton,
            this.redoButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(564, 25);
            this.toolStrip1.TabIndex = 1;
            // 
            // saveButton
            // 
            this.saveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveButton.Image = global::NSMBe4.Properties.Resources.save;
            this.saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(23, 22);
            this.saveButton.Text = "<Save>";
            this.saveButton.Visible = false;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator1.Visible = false;
            // 
            // drawToolButton
            // 
            this.drawToolButton.Checked = true;
            this.drawToolButton.CheckOnClick = true;
            this.drawToolButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.drawToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.drawToolButton.Image = global::NSMBe4.Properties.Resources.brush;
            this.drawToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.drawToolButton.Name = "drawToolButton";
            this.drawToolButton.Size = new System.Drawing.Size(23, 22);
            this.drawToolButton.Text = "<Draw (d)>";
            this.drawToolButton.Click += new System.EventHandler(this.drawToolButton_Click);
            // 
            // xFlipToolButton
            // 
            this.xFlipToolButton.CheckOnClick = true;
            this.xFlipToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xFlipToolButton.Image = global::NSMBe4.Properties.Resources.layer_flip;
            this.xFlipToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xFlipToolButton.Name = "xFlipToolButton";
            this.xFlipToolButton.Size = new System.Drawing.Size(23, 22);
            this.xFlipToolButton.Text = "<Horizontal flip (x)>";
            this.xFlipToolButton.Click += new System.EventHandler(this.xFlipToolButton_Click);
            // 
            // yFlipToolButton
            // 
            this.yFlipToolButton.CheckOnClick = true;
            this.yFlipToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.yFlipToolButton.Image = global::NSMBe4.Properties.Resources.layer_flip_vertical;
            this.yFlipToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.yFlipToolButton.Name = "yFlipToolButton";
            this.yFlipToolButton.Size = new System.Drawing.Size(23, 22);
            this.yFlipToolButton.Text = "<Vertical flip (y)>";
            this.yFlipToolButton.Click += new System.EventHandler(this.yFlipToolButton_Click);
            // 
            // copyToolButton
            // 
            this.copyToolButton.CheckOnClick = true;
            this.copyToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.copyToolButton.Image = global::NSMBe4.Properties.Resources.copy;
            this.copyToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copyToolButton.Name = "copyToolButton";
            this.copyToolButton.Size = new System.Drawing.Size(23, 22);
            this.copyToolButton.Text = "<Copy (c)>";
            this.copyToolButton.Click += new System.EventHandler(this.copyToolButton_Click);
            // 
            // pasteToolButton
            // 
            this.pasteToolButton.CheckOnClick = true;
            this.pasteToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pasteToolButton.Image = global::NSMBe4.Properties.Resources.paste;
            this.pasteToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pasteToolButton.Name = "pasteToolButton";
            this.pasteToolButton.Size = new System.Drawing.Size(23, 22);
            this.pasteToolButton.Text = "<Paste (v)>";
            this.pasteToolButton.Click += new System.EventHandler(this.pasteToolButton_Click);
            // 
            // changePalToolButton
            // 
            this.changePalToolButton.CheckOnClick = true;
            this.changePalToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.changePalToolButton.Image = global::NSMBe4.Properties.Resources.file_ncl;
            this.changePalToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.changePalToolButton.Name = "changePalToolButton";
            this.changePalToolButton.Size = new System.Drawing.Size(23, 22);
            this.changePalToolButton.Text = "<Change Palette (p)>";
            this.changePalToolButton.Click += new System.EventHandler(this.changePalToolButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // undoButton
            // 
            this.undoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.undoButton.Enabled = false;
            this.undoButton.Image = global::NSMBe4.Properties.Resources.undo;
            this.undoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.undoButton.Name = "undoButton";
            this.undoButton.Size = new System.Drawing.Size(23, 22);
            this.undoButton.Text = "<Undo>";
            // 
            // redoButton
            // 
            this.redoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.redoButton.Enabled = false;
            this.redoButton.Image = global::NSMBe4.Properties.Resources.redo;
            this.redoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.redoButton.Name = "redoButton";
            this.redoButton.Size = new System.Drawing.Size(23, 22);
            this.redoButton.Text = "<Redo>";
            // 
            // TilemapEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "TilemapEditor";
            this.Size = new System.Drawing.Size(564, 398);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TilePicker tilePicker1;
        private TilemapEditorControl tilemapEditorControl1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton drawToolButton;
        private System.Windows.Forms.ToolStripButton xFlipToolButton;
        private System.Windows.Forms.ToolStripButton yFlipToolButton;
        private System.Windows.Forms.ToolStripButton copyToolButton;
        private System.Windows.Forms.ToolStripButton pasteToolButton;
        private System.Windows.Forms.ToolStripButton changePalToolButton;
        private System.Windows.Forms.ToolStripButton saveButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton undoButton;
        private System.Windows.Forms.ToolStripButton redoButton;
    }
}
