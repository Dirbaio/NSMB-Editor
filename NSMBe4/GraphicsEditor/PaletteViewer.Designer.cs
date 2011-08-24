namespace NSMBe4
{
    partial class PaletteViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaletteViewer));
            this.paletteList = new System.Windows.Forms.ListBox();
            this.is4bpp = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.addAllToManager = new System.Windows.Forms.Button();
            this.addToManager = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // paletteList
            // 
            this.paletteList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.paletteList.FormattingEnabled = true;
            this.paletteList.Location = new System.Drawing.Point(0, 0);
            this.paletteList.Name = "paletteList";
            this.paletteList.Size = new System.Drawing.Size(144, 266);
            this.paletteList.TabIndex = 0;
            this.paletteList.SelectedIndexChanged += new System.EventHandler(this.paletteList_SelectedIndexChanged);
            // 
            // is4bpp
            // 
            this.is4bpp.AutoSize = true;
            this.is4bpp.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.is4bpp.Location = new System.Drawing.Point(0, 266);
            this.is4bpp.Name = "is4bpp";
            this.is4bpp.Size = new System.Drawing.Size(144, 17);
            this.is4bpp.TabIndex = 1;
            this.is4bpp.Text = "16 Color";
            this.is4bpp.UseVisualStyleBackColor = true;
            this.is4bpp.CheckedChanged += new System.EventHandler(this.is4bpp_CheckedChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(144, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(379, 243);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.paletteList);
            this.panel1.Controls.Add(this.is4bpp);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(144, 283);
            this.panel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.addAllToManager);
            this.panel2.Controls.Add(this.addToManager);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(144, 243);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(379, 40);
            this.panel2.TabIndex = 4;
            // 
            // addAllToManager
            // 
            this.addAllToManager.Location = new System.Drawing.Point(292, 6);
            this.addAllToManager.Name = "addAllToManager";
            this.addAllToManager.Size = new System.Drawing.Size(75, 22);
            this.addAllToManager.TabIndex = 1;
            this.addAllToManager.Text = "Add all";
            this.addAllToManager.UseVisualStyleBackColor = true;
            this.addAllToManager.Click += new System.EventHandler(this.addAllToManager_Click);
            // 
            // addToManager
            // 
            this.addToManager.Location = new System.Drawing.Point(211, 6);
            this.addToManager.Name = "addToManager";
            this.addToManager.Size = new System.Drawing.Size(75, 22);
            this.addToManager.TabIndex = 0;
            this.addToManager.Text = "Add";
            this.addToManager.UseVisualStyleBackColor = true;
            this.addToManager.Click += new System.EventHandler(this.addToManager_Click);
            // 
            // PaletteViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 283);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PaletteViewer";
            this.Text = "PaletteViewer";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox paletteList;
        private System.Windows.Forms.CheckBox is4bpp;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button addAllToManager;
        private System.Windows.Forms.Button addToManager;
    }
}