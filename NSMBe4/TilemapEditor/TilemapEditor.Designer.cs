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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TilemapEditor));
            this.panel1 = new System.Windows.Forms.Panel();
            this.tilemapEditorControl1 = new NSMBe4.TilemapEditorControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tilePicker1 = new NSMBe4.TilePicker();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.drawToolButton = new System.Windows.Forms.ToolStripButton();
            this.xFlipToolButton = new System.Windows.Forms.ToolStripButton();
            this.yFlipToolButton = new System.Windows.Forms.ToolStripButton();
            this.copyToolButton = new System.Windows.Forms.ToolStripButton();
            this.pasteToolButton = new System.Windows.Forms.ToolStripButton();
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
            this.drawToolButton,
            this.xFlipToolButton,
            this.yFlipToolButton,
            this.copyToolButton,
            this.pasteToolButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(564, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // drawToolButton
            // 
            this.drawToolButton.CheckOnClick = true;
            this.drawToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.drawToolButton.Image = ((System.Drawing.Image)(resources.GetObject("drawToolButton.Image")));
            this.drawToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.drawToolButton.Name = "drawToolButton";
            this.drawToolButton.Size = new System.Drawing.Size(23, 22);
            this.drawToolButton.Text = "Draw";
            this.drawToolButton.Click += new System.EventHandler(this.drawToolButton_Click);
            // 
            // xFlipToolButton
            // 
            this.xFlipToolButton.CheckOnClick = true;
            this.xFlipToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xFlipToolButton.Image = ((System.Drawing.Image)(resources.GetObject("xFlipToolButton.Image")));
            this.xFlipToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xFlipToolButton.Name = "xFlipToolButton";
            this.xFlipToolButton.Size = new System.Drawing.Size(23, 22);
            this.xFlipToolButton.Text = "Horizontal flip";
            this.xFlipToolButton.Click += new System.EventHandler(this.xFlipToolButton_Click);
            // 
            // yFlipToolButton
            // 
            this.yFlipToolButton.CheckOnClick = true;
            this.yFlipToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.yFlipToolButton.Image = ((System.Drawing.Image)(resources.GetObject("yFlipToolButton.Image")));
            this.yFlipToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.yFlipToolButton.Name = "yFlipToolButton";
            this.yFlipToolButton.Size = new System.Drawing.Size(23, 22);
            this.yFlipToolButton.Text = "Vertical flip";
            this.yFlipToolButton.Click += new System.EventHandler(this.yFlipToolButton_Click);
            // 
            // copyToolButton
            // 
            this.copyToolButton.CheckOnClick = true;
            this.copyToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.copyToolButton.Image = ((System.Drawing.Image)(resources.GetObject("copyToolButton.Image")));
            this.copyToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copyToolButton.Name = "copyToolButton";
            this.copyToolButton.Size = new System.Drawing.Size(23, 22);
            this.copyToolButton.Text = "Copy";
            this.copyToolButton.Click += new System.EventHandler(this.copyToolButton_Click);
            // 
            // pasteToolButton
            // 
            this.pasteToolButton.CheckOnClick = true;
            this.pasteToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pasteToolButton.Image = ((System.Drawing.Image)(resources.GetObject("pasteToolButton.Image")));
            this.pasteToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pasteToolButton.Name = "pasteToolButton";
            this.pasteToolButton.Size = new System.Drawing.Size(23, 22);
            this.pasteToolButton.Text = "Paste";
            this.pasteToolButton.Click += new System.EventHandler(this.pasteToolButton_Click);
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
    }
}
