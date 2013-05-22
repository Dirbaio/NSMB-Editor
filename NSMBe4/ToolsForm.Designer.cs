namespace NSMBe4
{
    partial class ToolsForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.sprSelectAll = new System.Windows.Forms.Button();
            this.sprFindNext = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.SpriteNumber = new System.Windows.Forms.NumericUpDown();
            this.lblSpriteNum = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.objSelectAll = new System.Windows.Forms.Button();
            this.tableFindObj = new System.Windows.Forms.TableLayoutPanel();
            this.nudFindObjNum = new System.Windows.Forms.NumericUpDown();
            this.lblObjNum = new System.Windows.Forms.Label();
            this.nudFindTileset = new System.Windows.Forms.NumericUpDown();
            this.lblTileset = new System.Windows.Forms.Label();
            this.objFindNext = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SpriteNumber)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tableFindObj.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFindObjNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFindTileset)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(293, 236);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(285, 210);
            this.tabPage1.TabIndex = 1;
            this.tabPage1.Text = "<Find>";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.sprSelectAll);
            this.groupBox2.Controls.Add(this.sprFindNext);
            this.groupBox2.Controls.Add(this.tableLayoutPanel1);
            this.groupBox2.Location = new System.Drawing.Point(6, 120);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(273, 84);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "<Sprites>";
            // 
            // sprSelectAll
            // 
            this.sprSelectAll.Location = new System.Drawing.Point(139, 55);
            this.sprSelectAll.Name = "sprSelectAll";
            this.sprSelectAll.Size = new System.Drawing.Size(75, 23);
            this.sprSelectAll.TabIndex = 12;
            this.sprSelectAll.Text = "<Select All>";
            this.sprSelectAll.UseVisualStyleBackColor = true;
            this.sprSelectAll.Click += new System.EventHandler(this.sprSelectAll_Click);
            // 
            // sprFindNext
            // 
            this.sprFindNext.Location = new System.Drawing.Point(59, 55);
            this.sprFindNext.Name = "sprFindNext";
            this.sprFindNext.Size = new System.Drawing.Size(74, 22);
            this.sprFindNext.TabIndex = 12;
            this.sprFindNext.Text = "<Find Next>";
            this.sprFindNext.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.SpriteNumber, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblSpriteNum, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 19);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(261, 30);
            this.tableLayoutPanel1.TabIndex = 11;
            // 
            // SpriteNumber
            // 
            this.SpriteNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.SpriteNumber.Location = new System.Drawing.Point(133, 5);
            this.SpriteNumber.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.SpriteNumber.Name = "SpriteNumber";
            this.SpriteNumber.Size = new System.Drawing.Size(125, 20);
            this.SpriteNumber.TabIndex = 1;
            // 
            // lblSpriteNum
            // 
            this.lblSpriteNum.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSpriteNum.AutoSize = true;
            this.lblSpriteNum.Location = new System.Drawing.Point(3, 8);
            this.lblSpriteNum.Name = "lblSpriteNum";
            this.lblSpriteNum.Size = new System.Drawing.Size(86, 13);
            this.lblSpriteNum.TabIndex = 0;
            this.lblSpriteNum.Text = "<Sprite Number>";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.objSelectAll);
            this.groupBox1.Controls.Add(this.tableFindObj);
            this.groupBox1.Controls.Add(this.objFindNext);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(273, 108);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "<Objects>";
            // 
            // objSelectAll
            // 
            this.objSelectAll.Location = new System.Drawing.Point(139, 75);
            this.objSelectAll.Name = "objSelectAll";
            this.objSelectAll.Size = new System.Drawing.Size(75, 23);
            this.objSelectAll.TabIndex = 11;
            this.objSelectAll.Text = "<Select All>";
            this.objSelectAll.UseVisualStyleBackColor = true;
            this.objSelectAll.Click += new System.EventHandler(this.objSelectAll_Click);
            // 
            // tableFindObj
            // 
            this.tableFindObj.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableFindObj.ColumnCount = 2;
            this.tableFindObj.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableFindObj.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableFindObj.Controls.Add(this.nudFindObjNum, 1, 1);
            this.tableFindObj.Controls.Add(this.lblObjNum, 0, 1);
            this.tableFindObj.Controls.Add(this.nudFindTileset, 1, 0);
            this.tableFindObj.Controls.Add(this.lblTileset, 0, 0);
            this.tableFindObj.Location = new System.Drawing.Point(6, 19);
            this.tableFindObj.Name = "tableFindObj";
            this.tableFindObj.RowCount = 2;
            this.tableFindObj.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableFindObj.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableFindObj.Size = new System.Drawing.Size(261, 50);
            this.tableFindObj.TabIndex = 2;
            // 
            // nudFindObjNum
            // 
            this.nudFindObjNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.nudFindObjNum.Location = new System.Drawing.Point(133, 28);
            this.nudFindObjNum.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudFindObjNum.Name = "nudFindObjNum";
            this.nudFindObjNum.Size = new System.Drawing.Size(125, 20);
            this.nudFindObjNum.TabIndex = 7;
            // 
            // lblObjNum
            // 
            this.lblObjNum.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblObjNum.AutoSize = true;
            this.lblObjNum.Location = new System.Drawing.Point(3, 31);
            this.lblObjNum.Name = "lblObjNum";
            this.lblObjNum.Size = new System.Drawing.Size(78, 13);
            this.lblObjNum.TabIndex = 2;
            this.lblObjNum.Text = "<Object Num.>";
            // 
            // nudFindTileset
            // 
            this.nudFindTileset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.nudFindTileset.Location = new System.Drawing.Point(133, 3);
            this.nudFindTileset.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudFindTileset.Name = "nudFindTileset";
            this.nudFindTileset.Size = new System.Drawing.Size(125, 20);
            this.nudFindTileset.TabIndex = 1;
            this.nudFindTileset.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblTileset
            // 
            this.lblTileset.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTileset.AutoSize = true;
            this.lblTileset.Location = new System.Drawing.Point(3, 6);
            this.lblTileset.Name = "lblTileset";
            this.lblTileset.Size = new System.Drawing.Size(50, 13);
            this.lblTileset.TabIndex = 0;
            this.lblTileset.Text = "<Tileset>";
            // 
            // objFindNext
            // 
            this.objFindNext.Location = new System.Drawing.Point(59, 75);
            this.objFindNext.Name = "objFindNext";
            this.objFindNext.Size = new System.Drawing.Size(74, 22);
            this.objFindNext.TabIndex = 9;
            this.objFindNext.Text = "<Find Next>";
            this.objFindNext.UseVisualStyleBackColor = true;
            this.objFindNext.Click += new System.EventHandler(this.objFindNext_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.numericUpDown2, 1, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(200, 100);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDown2.Location = new System.Drawing.Point(103, 40);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(94, 20);
            this.numericUpDown2.TabIndex = 1;
            // 
            // ToolsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 236);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ToolsForm";
            this.ShowInTaskbar = false;
            this.Text = "<_TITLE>";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ToolsForm_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SpriteNumber)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.tableFindObj.ResumeLayout(false);
            this.tableFindObj.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFindObjNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFindTileset)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button objFindNext;
        private System.Windows.Forms.TableLayoutPanel tableFindObj;
        private System.Windows.Forms.NumericUpDown nudFindObjNum;
        private System.Windows.Forms.Label lblObjNum;
        private System.Windows.Forms.NumericUpDown nudFindTileset;
        private System.Windows.Forms.Label lblTileset;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button objSelectAll;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button sprSelectAll;
        private System.Windows.Forms.Button sprFindNext;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.NumericUpDown SpriteNumber;
        private System.Windows.Forms.Label lblSpriteNum;
    }
}