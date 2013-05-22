namespace NSMBe4
{
    partial class SpriteEventsViewer
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.spriteTable = new System.Windows.Forms.DataGridView();
            this.refresh = new System.Windows.Forms.Button();
            this.EventID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SpriteNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SpriteName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.spriteTable)).BeginInit();
            this.SuspendLayout();
            // 
            // spriteTable
            // 
            this.spriteTable.AllowUserToAddRows = false;
            this.spriteTable.AllowUserToDeleteRows = false;
            this.spriteTable.AllowUserToOrderColumns = true;
            this.spriteTable.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.spriteTable.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.spriteTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spriteTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.spriteTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.spriteTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EventID,
            this.SpriteNum,
            this.SpriteName});
            this.spriteTable.Location = new System.Drawing.Point(0, 0);
            this.spriteTable.Name = "spriteTable";
            this.spriteTable.ReadOnly = true;
            this.spriteTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.spriteTable.Size = new System.Drawing.Size(366, 221);
            this.spriteTable.TabIndex = 0;
            this.spriteTable.SelectionChanged += new System.EventHandler(this.spriteTable_SelectionChanged);
            // 
            // refresh
            // 
            this.refresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.refresh.BackgroundImage = global::NSMBe4.Properties.Resources.refresh;
            this.refresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.refresh.Location = new System.Drawing.Point(256, 227);
            this.refresh.Name = "refresh";
            this.refresh.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.refresh.Size = new System.Drawing.Size(98, 23);
            this.refresh.TabIndex = 1;
            this.refresh.Text = "<Refresh>";
            this.refresh.UseVisualStyleBackColor = true;
            this.refresh.Click += new System.EventHandler(this.ReloadSprites);
            // 
            // EventID
            // 
            this.EventID.DataPropertyName = "eventID";
            this.EventID.HeaderText = "<Event ID>";
            this.EventID.Name = "EventID";
            this.EventID.ReadOnly = true;
            this.EventID.Width = 84;
            // 
            // SpriteNum
            // 
            this.SpriteNum.DataPropertyName = "spriteType";
            this.SpriteNum.HeaderText = "<Sprite Number>";
            this.SpriteNum.Name = "SpriteNum";
            this.SpriteNum.ReadOnly = true;
            this.SpriteNum.Width = 109;
            // 
            // SpriteName
            // 
            this.SpriteName.DataPropertyName = "spriteName";
            this.SpriteName.HeaderText = "<Sprite Name>";
            this.SpriteName.Name = "SpriteName";
            this.SpriteName.ReadOnly = true;
            // 
            // SpriteEventsViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 262);
            this.Controls.Add(this.refresh);
            this.Controls.Add(this.spriteTable);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "SpriteEventsViewer";
            this.Text = "<Sprite Events>";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SpriteEvents_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.spriteTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView spriteTable;
        private System.Windows.Forms.Button refresh;
        private System.Windows.Forms.DataGridViewTextBoxColumn EventID;
        private System.Windows.Forms.DataGridViewTextBoxColumn SpriteNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn SpriteName;
    }
}