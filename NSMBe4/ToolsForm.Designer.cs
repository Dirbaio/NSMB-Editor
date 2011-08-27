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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.SpriteNumber = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.newSpriteNumber = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.spriteReplaceAll = new System.Windows.Forms.Button();
            this.spriteFindNext = new System.Windows.Forms.Button();
            this.spriteDelete = new System.Windows.Forms.Button();
            this.spriteCount = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.objFindNext = new System.Windows.Forms.Button();
            this.objDelete = new System.Windows.Forms.Button();
            this.objCount = new System.Windows.Forms.Button();
            this.objReplaceBox = new System.Windows.Forms.GroupBox();
            this.tableReplaceObj = new System.Windows.Forms.TableLayoutPanel();
            this.nudReplaceObjNum = new System.Windows.Forms.NumericUpDown();
            this.lblReplaceObjNum = new System.Windows.Forms.Label();
            this.nudReplaceTS = new System.Windows.Forms.NumericUpDown();
            this.lblReplaceTileset = new System.Windows.Forms.Label();
            this.objReplaceAll = new System.Windows.Forms.Button();
            this.tableFindObj = new System.Windows.Forms.TableLayoutPanel();
            this.nudFindObjNum = new System.Windows.Forms.NumericUpDown();
            this.lblFindObjNum = new System.Windows.Forms.Label();
            this.nudFindTileset = new System.Windows.Forms.NumericUpDown();
            this.lblFindTileset = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SpriteNumber)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.newSpriteNumber)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.objReplaceBox.SuspendLayout();
            this.tableReplaceObj.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudReplaceObjNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudReplaceTS)).BeginInit();
            this.tableFindObj.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFindObjNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFindTileset)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.SuspendLayout();
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
            this.tableLayoutPanel1.Location = new System.Drawing.Point(8, 6);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(271, 30);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // SpriteNumber
            // 
            this.SpriteNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.SpriteNumber.Location = new System.Drawing.Point(138, 5);
            this.SpriteNumber.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.SpriteNumber.Name = "SpriteNumber";
            this.SpriteNumber.Size = new System.Drawing.Size(130, 20);
            this.SpriteNumber.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "<label1>";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(293, 216);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.spriteFindNext);
            this.tabPage1.Controls.Add(this.spriteDelete);
            this.tabPage1.Controls.Add(this.spriteCount);
            this.tabPage1.Controls.Add(this.tableLayoutPanel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(285, 190);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "<tabPage1>";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel2);
            this.groupBox1.Controls.Add(this.spriteReplaceAll);
            this.groupBox1.Location = new System.Drawing.Point(8, 70);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(270, 77);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "<groupBox1>";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.newSpriteNumber, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 19);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(261, 30);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // newSpriteNumber
            // 
            this.newSpriteNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.newSpriteNumber.Location = new System.Drawing.Point(133, 5);
            this.newSpriteNumber.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.newSpriteNumber.Name = "newSpriteNumber";
            this.newSpriteNumber.Size = new System.Drawing.Size(125, 20);
            this.newSpriteNumber.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "<label2>";
            // 
            // spriteReplaceAll
            // 
            this.spriteReplaceAll.Location = new System.Drawing.Point(3, 55);
            this.spriteReplaceAll.Name = "spriteReplaceAll";
            this.spriteReplaceAll.Size = new System.Drawing.Size(74, 22);
            this.spriteReplaceAll.TabIndex = 0;
            this.spriteReplaceAll.Text = "<spriteReplaceAll>";
            this.spriteReplaceAll.UseVisualStyleBackColor = true;
            this.spriteReplaceAll.Click += new System.EventHandler(this.spriteReplaceAll_Click);
            // 
            // spriteFindNext
            // 
            this.spriteFindNext.Location = new System.Drawing.Point(11, 42);
            this.spriteFindNext.Name = "spriteFindNext";
            this.spriteFindNext.Size = new System.Drawing.Size(74, 22);
            this.spriteFindNext.TabIndex = 0;
            this.spriteFindNext.Text = "<spriteFindNext>";
            this.spriteFindNext.UseVisualStyleBackColor = true;
            this.spriteFindNext.Click += new System.EventHandler(this.spriteFindNext_Click);
            // 
            // spriteDelete
            // 
            this.spriteDelete.Location = new System.Drawing.Point(168, 42);
            this.spriteDelete.Name = "spriteDelete";
            this.spriteDelete.Size = new System.Drawing.Size(74, 22);
            this.spriteDelete.TabIndex = 0;
            this.spriteDelete.Text = "<spriteDelete>";
            this.spriteDelete.UseVisualStyleBackColor = true;
            this.spriteDelete.Click += new System.EventHandler(this.spriteDelete_Click);
            // 
            // spriteCount
            // 
            this.spriteCount.Location = new System.Drawing.Point(88, 42);
            this.spriteCount.Name = "spriteCount";
            this.spriteCount.Size = new System.Drawing.Size(74, 22);
            this.spriteCount.TabIndex = 0;
            this.spriteCount.Text = "<spriteCount>";
            this.spriteCount.UseVisualStyleBackColor = true;
            this.spriteCount.Click += new System.EventHandler(this.spriteCount_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.objFindNext);
            this.tabPage2.Controls.Add(this.objDelete);
            this.tabPage2.Controls.Add(this.objCount);
            this.tabPage2.Controls.Add(this.objReplaceBox);
            this.tabPage2.Controls.Add(this.tableFindObj);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(285, 190);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Find Objects";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // objFindNext
            // 
            this.objFindNext.Location = new System.Drawing.Point(11, 59);
            this.objFindNext.Name = "objFindNext";
            this.objFindNext.Size = new System.Drawing.Size(74, 22);
            this.objFindNext.TabIndex = 9;
            this.objFindNext.Text = "Find Next";
            this.objFindNext.UseVisualStyleBackColor = true;
            this.objFindNext.Click += new System.EventHandler(this.objFindNext_Click);
            // 
            // objDelete
            // 
            this.objDelete.Location = new System.Drawing.Point(168, 59);
            this.objDelete.Name = "objDelete";
            this.objDelete.Size = new System.Drawing.Size(74, 22);
            this.objDelete.TabIndex = 8;
            this.objDelete.Text = "Delete All";
            this.objDelete.UseVisualStyleBackColor = true;
            this.objDelete.Click += new System.EventHandler(this.objDelete_Click);
            // 
            // objCount
            // 
            this.objCount.Location = new System.Drawing.Point(88, 59);
            this.objCount.Name = "objCount";
            this.objCount.Size = new System.Drawing.Size(74, 22);
            this.objCount.TabIndex = 7;
            this.objCount.Text = "Count";
            this.objCount.UseVisualStyleBackColor = true;
            this.objCount.Click += new System.EventHandler(this.objCount_Click);
            // 
            // objReplaceBox
            // 
            this.objReplaceBox.Controls.Add(this.tableReplaceObj);
            this.objReplaceBox.Controls.Add(this.objReplaceAll);
            this.objReplaceBox.Location = new System.Drawing.Point(6, 87);
            this.objReplaceBox.Name = "objReplaceBox";
            this.objReplaceBox.Size = new System.Drawing.Size(270, 100);
            this.objReplaceBox.TabIndex = 6;
            this.objReplaceBox.TabStop = false;
            this.objReplaceBox.Text = "Replace";
            // 
            // tableReplaceObj
            // 
            this.tableReplaceObj.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableReplaceObj.ColumnCount = 2;
            this.tableReplaceObj.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableReplaceObj.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableReplaceObj.Controls.Add(this.nudReplaceObjNum, 1, 1);
            this.tableReplaceObj.Controls.Add(this.lblReplaceObjNum, 0, 1);
            this.tableReplaceObj.Controls.Add(this.nudReplaceTS, 1, 0);
            this.tableReplaceObj.Controls.Add(this.lblReplaceTileset, 0, 0);
            this.tableReplaceObj.Location = new System.Drawing.Point(3, 19);
            this.tableReplaceObj.Name = "tableReplaceObj";
            this.tableReplaceObj.RowCount = 2;
            this.tableReplaceObj.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableReplaceObj.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableReplaceObj.Size = new System.Drawing.Size(261, 50);
            this.tableReplaceObj.TabIndex = 0;
            // 
            // nudReplaceObjNum
            // 
            this.nudReplaceObjNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.nudReplaceObjNum.Location = new System.Drawing.Point(133, 28);
            this.nudReplaceObjNum.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudReplaceObjNum.Name = "nudReplaceObjNum";
            this.nudReplaceObjNum.Size = new System.Drawing.Size(125, 20);
            this.nudReplaceObjNum.TabIndex = 2;
            // 
            // lblReplaceObjNum
            // 
            this.lblReplaceObjNum.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblReplaceObjNum.AutoSize = true;
            this.lblReplaceObjNum.Location = new System.Drawing.Point(3, 31);
            this.lblReplaceObjNum.Name = "lblReplaceObjNum";
            this.lblReplaceObjNum.Size = new System.Drawing.Size(66, 13);
            this.lblReplaceObjNum.TabIndex = 2;
            this.lblReplaceObjNum.Text = "Object Num.";
            // 
            // nudReplaceTS
            // 
            this.nudReplaceTS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.nudReplaceTS.Location = new System.Drawing.Point(133, 3);
            this.nudReplaceTS.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nudReplaceTS.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudReplaceTS.Name = "nudReplaceTS";
            this.nudReplaceTS.Size = new System.Drawing.Size(125, 20);
            this.nudReplaceTS.TabIndex = 1;
            this.nudReplaceTS.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblReplaceTileset
            // 
            this.lblReplaceTileset.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblReplaceTileset.AutoSize = true;
            this.lblReplaceTileset.Location = new System.Drawing.Point(3, 6);
            this.lblReplaceTileset.Name = "lblReplaceTileset";
            this.lblReplaceTileset.Size = new System.Drawing.Size(38, 13);
            this.lblReplaceTileset.TabIndex = 0;
            this.lblReplaceTileset.Text = "Tileset";
            // 
            // objReplaceAll
            // 
            this.objReplaceAll.Location = new System.Drawing.Point(3, 75);
            this.objReplaceAll.Name = "objReplaceAll";
            this.objReplaceAll.Size = new System.Drawing.Size(74, 22);
            this.objReplaceAll.TabIndex = 0;
            this.objReplaceAll.Text = "Replace All";
            this.objReplaceAll.UseVisualStyleBackColor = true;
            this.objReplaceAll.Click += new System.EventHandler(this.objReplaceAll_Click);
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
            this.tableFindObj.Location = new System.Drawing.Point(8, 6);
            this.tableFindObj.Name = "tableFindObj";
            this.tableFindObj.RowCount = 2;
            this.tableFindObj.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableFindObj.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableFindObj.Size = new System.Drawing.Size(271, 50);
            this.tableFindObj.TabIndex = 2;
            // 
            // nudFindObjNum
            // 
            this.nudFindObjNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.nudFindObjNum.Location = new System.Drawing.Point(138, 28);
            this.nudFindObjNum.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudFindObjNum.Name = "nudFindObjNum";
            this.nudFindObjNum.Size = new System.Drawing.Size(130, 20);
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
            this.nudFindTileset.Location = new System.Drawing.Point(138, 3);
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
            this.nudFindTileset.Size = new System.Drawing.Size(130, 20);
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
            this.ClientSize = new System.Drawing.Size(293, 216);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ToolsForm";
            this.ShowInTaskbar = false;
            this.Text = "<_TITLE>";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ToolsForm_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SpriteNumber)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.newSpriteNumber)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.objReplaceBox.ResumeLayout(false);
            this.tableReplaceObj.ResumeLayout(false);
            this.tableReplaceObj.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudReplaceObjNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudReplaceTS)).EndInit();
            this.tableFindObj.ResumeLayout(false);
            this.tableFindObj.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFindObjNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFindTileset)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.NumericUpDown SpriteNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button spriteCount;
        private System.Windows.Forms.Button spriteFindNext;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.NumericUpDown newSpriteNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button spriteReplaceAll;
        private System.Windows.Forms.Button spriteDelete;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button objFindNext;
        private System.Windows.Forms.Button objDelete;
        private System.Windows.Forms.Button objCount;
        private System.Windows.Forms.GroupBox objReplaceBox;
        private System.Windows.Forms.TableLayoutPanel tableReplaceObj;
        private System.Windows.Forms.NumericUpDown nudReplaceObjNum;
        private System.Windows.Forms.Label lblReplaceObjNum;
        private System.Windows.Forms.NumericUpDown nudReplaceTS;
        private System.Windows.Forms.Label lblReplaceTileset;
        private System.Windows.Forms.Button objReplaceAll;
        private System.Windows.Forms.TableLayoutPanel tableFindObj;
        private System.Windows.Forms.NumericUpDown nudFindObjNum;
        private System.Windows.Forms.Label lblFindObjNum;
        private System.Windows.Forms.NumericUpDown nudFindTileset;
        private System.Windows.Forms.Label lblFindTileset;
    }
}