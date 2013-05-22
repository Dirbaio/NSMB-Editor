namespace NSMBe4 {
    partial class LevelHexEditor {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.saveBlockButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.blockComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.hexBox1 = new Be.Windows.Forms.HexBox();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveBlockButton,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.blockComboBox});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(667, 25);
            this.toolStrip1.TabIndex = 1;
            // 
            // saveBlockButton
            // 
            this.saveBlockButton.Image = global::NSMBe4.Properties.Resources.save;
            this.saveBlockButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveBlockButton.Name = "saveBlockButton";
            this.saveBlockButton.Size = new System.Drawing.Size(120, 22);
            this.saveBlockButton.Text = "<saveBlockButton>";
            this.saveBlockButton.Click += new System.EventHandler(this.saveBlockButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(90, 22);
            this.toolStripLabel1.Text = "<toolStripLabel1>";
            // 
            // blockComboBox
            // 
            this.blockComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.blockComboBox.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14"});
            this.blockComboBox.Name = "blockComboBox";
            this.blockComboBox.Size = new System.Drawing.Size(80, 25);
            this.blockComboBox.SelectedIndexChanged += new System.EventHandler(this.blockComboBox_SelectedIndexChanged);
            // 
            // hexBox1
            // 
            this.hexBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hexBox1.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hexBox1.LineInfoForeColor = System.Drawing.Color.Empty;
            this.hexBox1.Location = new System.Drawing.Point(0, 25);
            this.hexBox1.Name = "hexBox1";
            this.hexBox1.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
            this.hexBox1.Size = new System.Drawing.Size(667, 306);
            this.hexBox1.TabIndex = 2;
            this.hexBox1.VScrollBarVisible = true;
            // 
            // LevelHexEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 331);
            this.Controls.Add(this.hexBox1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "LevelHexEditor";
            this.Text = "<LevelHexEditor>";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LevelHexEditor_FormClosed);
            this.Load += new System.EventHandler(this.LevelHexEditor_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton saveBlockButton;
        private System.Windows.Forms.ToolStripComboBox blockComboBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private Be.Windows.Forms.HexBox hexBox1;
    }
}