namespace NSMBe4
{
    partial class TilesetList
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.exportTilesetBtn = new System.Windows.Forms.Button();
            this.importTilesetBtn = new System.Windows.Forms.Button();
            this.editTilesetBtn = new System.Windows.Forms.Button();
            this.RenameBtn = new System.Windows.Forms.Button();
            this.tilesetListBox = new System.Windows.Forms.ListBox();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.exportTilesetBtn);
            this.flowLayoutPanel1.Controls.Add(this.importTilesetBtn);
            this.flowLayoutPanel1.Controls.Add(this.editTilesetBtn);
            this.flowLayoutPanel1.Controls.Add(this.RenameBtn);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 272);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(451, 29);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // exportTilesetBtn
            // 
            this.exportTilesetBtn.Location = new System.Drawing.Point(373, 3);
            this.exportTilesetBtn.Name = "exportTilesetBtn";
            this.exportTilesetBtn.Size = new System.Drawing.Size(75, 23);
            this.exportTilesetBtn.TabIndex = 0;
            this.exportTilesetBtn.Text = "<Export>";
            this.exportTilesetBtn.UseVisualStyleBackColor = true;
            this.exportTilesetBtn.Click += new System.EventHandler(this.exportTilesetBtn_Click);
            // 
            // importTilesetBtn
            // 
            this.importTilesetBtn.Location = new System.Drawing.Point(292, 3);
            this.importTilesetBtn.Name = "importTilesetBtn";
            this.importTilesetBtn.Size = new System.Drawing.Size(75, 23);
            this.importTilesetBtn.TabIndex = 1;
            this.importTilesetBtn.Text = "<Import>";
            this.importTilesetBtn.UseVisualStyleBackColor = true;
            this.importTilesetBtn.Click += new System.EventHandler(this.importTilesetBtn_Click);
            // 
            // editTilesetBtn
            // 
            this.editTilesetBtn.Location = new System.Drawing.Point(211, 3);
            this.editTilesetBtn.Name = "editTilesetBtn";
            this.editTilesetBtn.Size = new System.Drawing.Size(75, 23);
            this.editTilesetBtn.TabIndex = 2;
            this.editTilesetBtn.Text = "<Edit>";
            this.editTilesetBtn.UseVisualStyleBackColor = true;
            this.editTilesetBtn.Click += new System.EventHandler(this.editTilesetBtn_Click);
            // 
            // RenameBtn
            // 
            this.RenameBtn.Location = new System.Drawing.Point(130, 3);
            this.RenameBtn.Name = "RenameBtn";
            this.RenameBtn.Size = new System.Drawing.Size(75, 23);
            this.RenameBtn.TabIndex = 2;
            this.RenameBtn.Text = "<Rename>";
            this.RenameBtn.UseVisualStyleBackColor = true;
            this.RenameBtn.Click += new System.EventHandler(this.RenameBtn_Click);
            // 
            // tilesetListBox
            // 
            this.tilesetListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tilesetListBox.FormattingEnabled = true;
            this.tilesetListBox.Location = new System.Drawing.Point(0, 0);
            this.tilesetListBox.Name = "tilesetListBox";
            this.tilesetListBox.Size = new System.Drawing.Size(451, 272);
            this.tilesetListBox.TabIndex = 1;
            this.tilesetListBox.DoubleClick += new System.EventHandler(this.tilesetListBox_DoubleClick);
            // 
            // TilesetList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tilesetListBox);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "TilesetList";
            this.Size = new System.Drawing.Size(451, 301);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button exportTilesetBtn;
        private System.Windows.Forms.Button importTilesetBtn;
        private System.Windows.Forms.Button editTilesetBtn;
        private System.Windows.Forms.ListBox tilesetListBox;
        private System.Windows.Forms.Button RenameBtn;
    }
}
