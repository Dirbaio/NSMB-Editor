namespace NSMBe4
{
    partial class TilesetObjectEditor
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
            this.editZone = new System.Windows.Forms.PictureBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lblObjectEditor = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.description = new System.Windows.Forms.Label();
            this.desc = new System.Windows.Forms.TextBox();
            this.grpObjectSettings = new System.Windows.Forms.GroupBox();
            this.lblHeight = new System.Windows.Forms.Label();
            this.lblWidth = new System.Windows.Forms.Label();
            this.objWidth = new System.Windows.Forms.NumericUpDown();
            this.objHeight = new System.Windows.Forms.NumericUpDown();
            this.grpTileSettings = new System.Windows.Forms.GroupBox();
            this.lblMap16Num = new System.Windows.Forms.Label();
            this.lblControlByte = new System.Windows.Forms.Label();
            this.map16Tile = new System.Windows.Forms.NumericUpDown();
            this.controlByte = new System.Windows.Forms.NumericUpDown();
            this.previewBox = new System.Windows.Forms.PictureBox();
            this.lblObjectPreview = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tilePicker1 = new NSMBe4.TilePicker();
            this.lblMap16Tiles = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.deleteButton = new System.Windows.Forms.Button();
            this.newLineButton = new System.Windows.Forms.Button();
            this.emptyTileButton = new System.Windows.Forms.Button();
            this.slopeControlButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.editZone)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.grpObjectSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.objHeight)).BeginInit();
            this.grpTileSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.map16Tile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlByte)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // editZone
            // 
            this.editZone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editZone.Location = new System.Drawing.Point(0, 13);
            this.editZone.Name = "editZone";
            this.editZone.Size = new System.Drawing.Size(243, 249);
            this.editZone.TabIndex = 0;
            this.editZone.TabStop = false;
            this.editZone.Paint += new System.Windows.Forms.PaintEventHandler(this.editZone_Paint);
            this.editZone.MouseDown += new System.Windows.Forms.MouseEventHandler(this.editZone_MouseDown);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.editZone);
            this.splitContainer1.Panel1.Controls.Add(this.lblObjectEditor);
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.previewBox);
            this.splitContainer1.Panel2.Controls.Add(this.lblObjectPreview);
            this.splitContainer1.Size = new System.Drawing.Size(400, 389);
            this.splitContainer1.SplitterDistance = 262;
            this.splitContainer1.TabIndex = 1;
            // 
            // lblObjectEditor
            // 
            this.lblObjectEditor.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblObjectEditor.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblObjectEditor.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblObjectEditor.Location = new System.Drawing.Point(0, 0);
            this.lblObjectEditor.Name = "lblObjectEditor";
            this.lblObjectEditor.Size = new System.Drawing.Size(243, 13);
            this.lblObjectEditor.TabIndex = 5;
            this.lblObjectEditor.Text = "<ObjectEditor>";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.description);
            this.panel2.Controls.Add(this.desc);
            this.panel2.Controls.Add(this.grpObjectSettings);
            this.panel2.Controls.Add(this.grpTileSettings);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(243, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(157, 262);
            this.panel2.TabIndex = 1;
            // 
            // description
            // 
            this.description.AutoSize = true;
            this.description.Location = new System.Drawing.Point(9, 223);
            this.description.Name = "description";
            this.description.Size = new System.Drawing.Size(70, 13);
            this.description.TabIndex = 6;
            this.description.Text = "<description>";
            // 
            // desc
            // 
            this.desc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.desc.Location = new System.Drawing.Point(6, 239);
            this.desc.Name = "desc";
            this.desc.Size = new System.Drawing.Size(145, 20);
            this.desc.TabIndex = 5;
            this.desc.TextChanged += new System.EventHandler(this.desc_TextChanged);
            // 
            // grpObjectSettings
            // 
            this.grpObjectSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpObjectSettings.Controls.Add(this.lblHeight);
            this.grpObjectSettings.Controls.Add(this.lblWidth);
            this.grpObjectSettings.Controls.Add(this.objWidth);
            this.grpObjectSettings.Controls.Add(this.objHeight);
            this.grpObjectSettings.Location = new System.Drawing.Point(3, 113);
            this.grpObjectSettings.Name = "grpObjectSettings";
            this.grpObjectSettings.Size = new System.Drawing.Size(148, 99);
            this.grpObjectSettings.TabIndex = 4;
            this.grpObjectSettings.TabStop = false;
            this.grpObjectSettings.Text = "<ObjectSettings>";
            // 
            // lblHeight
            // 
            this.lblHeight.AutoSize = true;
            this.lblHeight.Location = new System.Drawing.Point(6, 55);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(50, 13);
            this.lblHeight.TabIndex = 0;
            this.lblHeight.Text = "<Height>";
            // 
            // lblWidth
            // 
            this.lblWidth.AutoSize = true;
            this.lblWidth.Location = new System.Drawing.Point(6, 16);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(47, 13);
            this.lblWidth.TabIndex = 0;
            this.lblWidth.Text = "<Width>";
            // 
            // objWidth
            // 
            this.objWidth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.objWidth.Location = new System.Drawing.Point(9, 32);
            this.objWidth.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.objWidth.Name = "objWidth";
            this.objWidth.Size = new System.Drawing.Size(129, 20);
            this.objWidth.TabIndex = 2;
            this.objWidth.ValueChanged += new System.EventHandler(this.objWidth_ValueChanged);
            // 
            // objHeight
            // 
            this.objHeight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.objHeight.Location = new System.Drawing.Point(9, 73);
            this.objHeight.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.objHeight.Name = "objHeight";
            this.objHeight.Size = new System.Drawing.Size(129, 20);
            this.objHeight.TabIndex = 2;
            this.objHeight.ValueChanged += new System.EventHandler(this.objHeight_ValueChanged);
            // 
            // grpTileSettings
            // 
            this.grpTileSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpTileSettings.Controls.Add(this.lblMap16Num);
            this.grpTileSettings.Controls.Add(this.lblControlByte);
            this.grpTileSettings.Controls.Add(this.map16Tile);
            this.grpTileSettings.Controls.Add(this.controlByte);
            this.grpTileSettings.Location = new System.Drawing.Point(3, 3);
            this.grpTileSettings.Name = "grpTileSettings";
            this.grpTileSettings.Size = new System.Drawing.Size(148, 101);
            this.grpTileSettings.TabIndex = 3;
            this.grpTileSettings.TabStop = false;
            this.grpTileSettings.Text = "<TileSettings>";
            // 
            // lblMap16Num
            // 
            this.lblMap16Num.AutoSize = true;
            this.lblMap16Num.Location = new System.Drawing.Point(6, 16);
            this.lblMap16Num.Name = "lblMap16Num";
            this.lblMap16Num.Size = new System.Drawing.Size(89, 13);
            this.lblMap16Num.TabIndex = 0;
            this.lblMap16Num.Text = "<Map16Number>";
            // 
            // lblControlByte
            // 
            this.lblControlByte.AutoSize = true;
            this.lblControlByte.Location = new System.Drawing.Point(6, 55);
            this.lblControlByte.Name = "lblControlByte";
            this.lblControlByte.Size = new System.Drawing.Size(73, 13);
            this.lblControlByte.TabIndex = 0;
            this.lblControlByte.Text = "<ControlByte>";
            // 
            // map16Tile
            // 
            this.map16Tile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.map16Tile.Location = new System.Drawing.Point(9, 32);
            this.map16Tile.Maximum = new decimal(new int[] {
            768,
            0,
            0,
            0});
            this.map16Tile.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.map16Tile.Name = "map16Tile";
            this.map16Tile.Size = new System.Drawing.Size(129, 20);
            this.map16Tile.TabIndex = 1;
            this.map16Tile.ValueChanged += new System.EventHandler(this.map16Tile_ValueChanged);
            // 
            // controlByte
            // 
            this.controlByte.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.controlByte.Hexadecimal = true;
            this.controlByte.Location = new System.Drawing.Point(9, 71);
            this.controlByte.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.controlByte.Name = "controlByte";
            this.controlByte.Size = new System.Drawing.Size(129, 20);
            this.controlByte.TabIndex = 1;
            this.controlByte.ValueChanged += new System.EventHandler(this.controlByte_ValueChanged);
            // 
            // previewBox
            // 
            this.previewBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.previewBox.Location = new System.Drawing.Point(0, 13);
            this.previewBox.Name = "previewBox";
            this.previewBox.Size = new System.Drawing.Size(400, 110);
            this.previewBox.TabIndex = 0;
            this.previewBox.TabStop = false;
            this.previewBox.Paint += new System.Windows.Forms.PaintEventHandler(this.previewBox_Paint);
            this.previewBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.previewBox_MouseDown);
            this.previewBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.previewBox_MouseMove);
            // 
            // lblObjectPreview
            // 
            this.lblObjectPreview.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblObjectPreview.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblObjectPreview.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblObjectPreview.Location = new System.Drawing.Point(0, 0);
            this.lblObjectPreview.Name = "lblObjectPreview";
            this.lblObjectPreview.Size = new System.Drawing.Size(400, 13);
            this.lblObjectPreview.TabIndex = 1;
            this.lblObjectPreview.Text = "<ObjectPreview>";
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.tilePicker1);
            this.panel1.Controls.Add(this.lblMap16Tiles);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(400, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(274, 389);
            this.panel1.TabIndex = 2;
            // 
            // tilePicker1
            // 
            this.tilePicker1.AutoSize = true;
            this.tilePicker1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tilePicker1.Location = new System.Drawing.Point(0, 68);
            this.tilePicker1.MinimumSize = new System.Drawing.Size(274, 224);
            this.tilePicker1.Name = "tilePicker1";
            this.tilePicker1.Size = new System.Drawing.Size(274, 321);
            this.tilePicker1.TabIndex = 6;
            this.tilePicker1.TileSelected += new NSMBe4.TilePicker.TileSelectedd(this.tilePicker1_TileSelected);
            // 
            // lblMap16Tiles
            // 
            this.lblMap16Tiles.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblMap16Tiles.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblMap16Tiles.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblMap16Tiles.Location = new System.Drawing.Point(0, 55);
            this.lblMap16Tiles.Name = "lblMap16Tiles";
            this.lblMap16Tiles.Size = new System.Drawing.Size(274, 13);
            this.lblMap16Tiles.TabIndex = 4;
            this.lblMap16Tiles.Text = "<Map16Tiles>";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.deleteButton);
            this.panel3.Controls.Add(this.newLineButton);
            this.panel3.Controls.Add(this.emptyTileButton);
            this.panel3.Controls.Add(this.slopeControlButton);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(274, 55);
            this.panel3.TabIndex = 5;
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(6, 29);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(87, 23);
            this.deleteButton.TabIndex = 0;
            this.deleteButton.Text = "<deleteButton>";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // newLineButton
            // 
            this.newLineButton.Location = new System.Drawing.Point(177, 3);
            this.newLineButton.Name = "newLineButton";
            this.newLineButton.Size = new System.Drawing.Size(72, 23);
            this.newLineButton.TabIndex = 0;
            this.newLineButton.Text = "<newLineButton>";
            this.newLineButton.UseVisualStyleBackColor = true;
            this.newLineButton.Click += new System.EventHandler(this.newLineButton_Click);
            // 
            // emptyTileButton
            // 
            this.emptyTileButton.Location = new System.Drawing.Point(99, 3);
            this.emptyTileButton.Name = "emptyTileButton";
            this.emptyTileButton.Size = new System.Drawing.Size(72, 23);
            this.emptyTileButton.TabIndex = 0;
            this.emptyTileButton.Text = "<emptyTileButton>";
            this.emptyTileButton.UseVisualStyleBackColor = true;
            this.emptyTileButton.Click += new System.EventHandler(this.emptyTileButton_Click);
            // 
            // slopeControlButton
            // 
            this.slopeControlButton.Location = new System.Drawing.Point(6, 3);
            this.slopeControlButton.Name = "slopeControlButton";
            this.slopeControlButton.Size = new System.Drawing.Size(87, 23);
            this.slopeControlButton.TabIndex = 0;
            this.slopeControlButton.Text = "<slopeControlButton>";
            this.slopeControlButton.UseVisualStyleBackColor = true;
            this.slopeControlButton.Click += new System.EventHandler(this.slopeControlButton_Click);
            // 
            // TilesetObjectEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Name = "TilesetObjectEditor";
            this.Size = new System.Drawing.Size(674, 389);
            ((System.ComponentModel.ISupportInitialize)(this.editZone)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.grpObjectSettings.ResumeLayout(false);
            this.grpObjectSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.objHeight)).EndInit();
            this.grpTileSettings.ResumeLayout(false);
            this.grpTileSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.map16Tile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlByte)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox editZone;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox previewBox;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.NumericUpDown map16Tile;
        private System.Windows.Forms.Label lblMap16Num;
        private System.Windows.Forms.NumericUpDown controlByte;
        private System.Windows.Forms.Label lblControlByte;
        private System.Windows.Forms.Label lblObjectEditor;
        private System.Windows.Forms.Label lblObjectPreview;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblMap16Tiles;
        private System.Windows.Forms.GroupBox grpObjectSettings;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.GroupBox grpTileSettings;
        private System.Windows.Forms.NumericUpDown objHeight;
        private System.Windows.Forms.NumericUpDown objWidth;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button emptyTileButton;
        private System.Windows.Forms.Button slopeControlButton;
        private System.Windows.Forms.Button newLineButton;
        private System.Windows.Forms.TextBox desc;
        private System.Windows.Forms.Label description;
        private System.Windows.Forms.Button deleteButton;
        private TilePicker tilePicker1;
    }
}
