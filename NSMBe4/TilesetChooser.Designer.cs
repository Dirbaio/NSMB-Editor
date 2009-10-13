namespace NSMBe4
{
    partial class TilesetChooser
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.editJyotyuButton = new System.Windows.Forms.Button();
            this.tilesetComboBox = new System.Windows.Forms.ComboBox();
            this.openTilesetButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.openTilesetButton);
            this.groupBox1.Controls.Add(this.tilesetComboBox);
            this.groupBox1.Controls.Add(this.editJyotyuButton);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(295, 77);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "<groupBox1>";
            // 
            // editJyotyuButton
            // 
            this.editJyotyuButton.Location = new System.Drawing.Point(6, 20);
            this.editJyotyuButton.Name = "editJyotyuButton";
            this.editJyotyuButton.Size = new System.Drawing.Size(283, 23);
            this.editJyotyuButton.TabIndex = 0;
            this.editJyotyuButton.Text = "<editJyotyuButton>";
            this.editJyotyuButton.UseVisualStyleBackColor = true;
            this.editJyotyuButton.Click += new System.EventHandler(this.editJyotyuButton_Click);
            // 
            // tilesetComboBox
            // 
            this.tilesetComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tilesetComboBox.FormattingEnabled = true;
            this.tilesetComboBox.Location = new System.Drawing.Point(6, 49);
            this.tilesetComboBox.Name = "tilesetComboBox";
            this.tilesetComboBox.Size = new System.Drawing.Size(229, 21);
            this.tilesetComboBox.TabIndex = 1;
            // 
            // openTilesetButton
            // 
            this.openTilesetButton.Location = new System.Drawing.Point(241, 48);
            this.openTilesetButton.Name = "openTilesetButton";
            this.openTilesetButton.Size = new System.Drawing.Size(48, 22);
            this.openTilesetButton.TabIndex = 2;
            this.openTilesetButton.Text = "<openTilesetButton>";
            this.openTilesetButton.UseVisualStyleBackColor = true;
            this.openTilesetButton.Click += new System.EventHandler(this.openTilesetButton_Click);
            // 
            // TilesetChooser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 102);
            this.Controls.Add(this.groupBox1);
            this.Name = "TilesetChooser";
            this.Text = "<_TITLE>";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button editJyotyuButton;
        private System.Windows.Forms.ComboBox tilesetComboBox;
        private System.Windows.Forms.Button openTilesetButton;
    }
}