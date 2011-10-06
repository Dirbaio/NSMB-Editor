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
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.sprSelectAll = new System.Windows.Forms.Button();
            this.spriteFindNext = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.SpriteNumber = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.objSelectAll = new System.Windows.Forms.Button();
            this.tableFindObj = new System.Windows.Forms.TableLayoutPanel();
            this.nudFindObjNum = new System.Windows.Forms.NumericUpDown();
            this.lblFindObjNum = new System.Windows.Forms.Label();
            this.nudFindTileset = new System.Windows.Forms.NumericUpDown();
            this.lblFindTileset = new System.Windows.Forms.Label();
            this.objFindNext = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SpriteNumber)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.tableFindObj.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFindObjNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFindTileset)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(293, 236);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(285, 210);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Find";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.sprSelectAll);
            this.groupBox4.Controls.Add(this.spriteFindNext);
            this.groupBox4.Controls.Add(this.tableLayoutPanel1);
            this.groupBox4.Location = new System.Drawing.Point(6, 120);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(273, 84);
            this.groupBox4.TabIndex = 12;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Sprites";
            // 
            // sprSelectAll
            // 
            this.sprSelectAll.Location = new System.Drawing.Point(139, 55);
            this.sprSelectAll.Name = "sprSelectAll";
            this.sprSelectAll.Size = new System.Drawing.Size(75, 23);
            this.sprSelectAll.TabIndex = 12;
            this.sprSelectAll.Text = "Select All";
            this.sprSelectAll.UseVisualStyleBackColor = true;
            this.sprSelectAll.Click += new System.EventHandler(this.sprSelectAll_Click);
            // 
            // spriteFindNext
            // 
            this.spriteFindNext.Location = new System.Drawing.Point(59, 55);
            this.spriteFindNext.Name = "spriteFindNext";
            this.spriteFindNext.Size = new System.Drawing.Size(74, 22);
            this.spriteFindNext.TabIndex = 12;
            this.spriteFindNext.Text = "Find Next";
            this.spriteFindNext.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.SpriteNumber, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
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
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sprite Number";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.objSelectAll);
            this.groupBox3.Controls.Add(this.tableFindObj);
            this.groupBox3.Controls.Add(this.objFindNext);
            this.groupBox3.Location = new System.Drawing.Point(6, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(273, 108);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Objects";
            // 
            // objSelectAll
            // 
            this.objSelectAll.Location = new System.Drawing.Point(139, 75);
            this.objSelectAll.Name = "objSelectAll";
            this.objSelectAll.Size = new System.Drawing.Size(75, 23);
            this.objSelectAll.TabIndex = 11;
            this.objSelectAll.Text = "Select All";
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
            this.tableFindObj.Controls.Add(this.lblFindObjNum, 0, 1);
            this.tableFindObj.Controls.Add(this.nudFindTileset, 1, 0);
            this.tableFindObj.Controls.Add(this.lblFindTileset, 0, 0);
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
            // lblFindObjNum
            // 
            this.lblFindObjNum.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblFindObjNum.AutoSize = true;
            this.lblFindObjNum.Location = new System.Drawing.Point(3, 31);
            this.lblFindObjNum.Name = "lblFindObjNum";
            this.lblFindObjNum.Size = new System.Drawing.Size(66, 13);
            this.lblFindObjNum.TabIndex = 2;
            this.lblFindObjNum.Text = "Object Num.";
            // 
            // nudFindTileset
            // 
            this.nudFindTileset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.nudFindTileset.Location = new System.Drawing.Point(133, 3);
            this.nudFindTileset.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nudFindTileset.Minimum = new decimal(new int[] {
            1,
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
            // lblFindTileset
            // 
            this.lblFindTileset.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblFindTileset.AutoSize = true;
            this.lblFindTileset.Location = new System.Drawing.Point(3, 6);
            this.lblFindTileset.Name = "lblFindTileset";
            this.lblFindTileset.Size = new System.Drawing.Size(38, 13);
            this.lblFindTileset.TabIndex = 0;
            this.lblFindTileset.Text = "Tileset";
            // 
            // objFindNext
            // 
            this.objFindNext.Location = new System.Drawing.Point(59, 75);
            this.objFindNext.Name = "objFindNext";
            this.objFindNext.Size = new System.Drawing.Size(74, 22);
            this.objFindNext.TabIndex = 9;
            this.objFindNext.Text = "Find Next";
            this.objFindNext.UseVisualStyleBackColor = true;
            this.objFindNext.Click += new System.EventHandler(this.objFindNext_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 72);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(74, 22);
            this.button1.TabIndex = 0;
            this.button1.Text = "Find Next";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 100);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
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
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "New Sprite Number";
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
            this.tabPage2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SpriteNumber)).EndInit();
            this.groupBox3.ResumeLayout(false);
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
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button objFindNext;
        private System.Windows.Forms.TableLayoutPanel tableFindObj;
        private System.Windows.Forms.NumericUpDown nudFindObjNum;
        private System.Windows.Forms.Label lblFindObjNum;
        private System.Windows.Forms.NumericUpDown nudFindTileset;
        private System.Windows.Forms.Label lblFindTileset;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button objSelectAll;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button sprSelectAll;
        private System.Windows.Forms.Button spriteFindNext;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.NumericUpDown SpriteNumber;
        private System.Windows.Forms.Label label1;
    }
}