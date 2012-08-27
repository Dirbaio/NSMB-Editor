namespace NSMBe4
{
    partial class TilesetEditor
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
            this.tilesetObjectEditor1 = new NSMBe4.TilesetObjectEditor();
            this.objectPickerControl1 = new NSMBe4.ObjectPickerControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tilemapEditor1 = new NSMBe4.TilemapEditor.TilemapEditor();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.imageManager1 = new NSMBe4.ImageManager();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tileBehaviorPicker = new NSMBe4.TilePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.eightLab = new System.Windows.Forms.Label();
            this.sevenLab = new System.Windows.Forms.Label();
            this.eight = new System.Windows.Forms.ComboBox();
            this.seven = new System.Windows.Forms.ComboBox();
            this.sixLab = new System.Windows.Forms.Label();
            this.fiveLab = new System.Windows.Forms.Label();
            this.fourLab = new System.Windows.Forms.Label();
            this.threeLab = new System.Windows.Forms.Label();
            this.twoLab = new System.Windows.Forms.Label();
            this.oneLab = new System.Windows.Forms.Label();
            this.six = new System.Windows.Forms.ComboBox();
            this.five = new System.Windows.Forms.ComboBox();
            this.four = new System.Windows.Forms.ComboBox();
            this.three = new System.Windows.Forms.ComboBox();
            this.two = new System.Windows.Forms.ComboBox();
            this.one = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tileBehaviorEditor = new NSMBe4.ByteArrayEditor();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteAllButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.createDescriptions = new System.Windows.Forms.ToolStripButton();
            this.deleteDescriptions = new System.Windows.Forms.ToolStripButton();
            this.setend = new System.Windows.Forms.ToolStripButton();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog2 = new System.Windows.Forms.SaveFileDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(957, 536);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.tilesetObjectEditor1);
            this.tabPage1.Controls.Add(this.objectPickerControl1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(949, 510);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "<tabPage1>";
            // 
            // tilesetObjectEditor1
            // 
            this.tilesetObjectEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tilesetObjectEditor1.Location = new System.Drawing.Point(181, 3);
            this.tilesetObjectEditor1.Name = "tilesetObjectEditor1";
            this.tilesetObjectEditor1.Size = new System.Drawing.Size(765, 504);
            this.tilesetObjectEditor1.TabIndex = 2;
            this.tilesetObjectEditor1.mustRepaintObjects += new NSMBe4.TilesetObjectEditor.mustRepaintObjectsD(this.mustRepaintObjects);
            this.tilesetObjectEditor1.DescriptionChanged += new NSMBe4.TilesetObjectEditor.changeDescription(this.tilesetObjectEditor1_DescriptionChanged);
            // 
            // objectPickerControl1
            // 
            this.objectPickerControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.objectPickerControl1.Location = new System.Drawing.Point(3, 3);
            this.objectPickerControl1.Name = "objectPickerControl1";
            this.objectPickerControl1.Size = new System.Drawing.Size(178, 504);
            this.objectPickerControl1.TabIndex = 1;
            this.objectPickerControl1.ObjectSelected += new NSMBe4.ObjectPickerControl.ObjectSelectedDelegate(this.objectPickerControl1_ObjectSelected);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.tilemapEditor1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(949, 510);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "<tabPage2>";
            // 
            // tilemapEditor1
            // 
            this.tilemapEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tilemapEditor1.Location = new System.Drawing.Point(3, 3);
            this.tilemapEditor1.Name = "tilemapEditor1";
            this.tilemapEditor1.Size = new System.Drawing.Size(943, 504);
            this.tilemapEditor1.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.imageManager1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(949, 510);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // imageManager1
            // 
            this.imageManager1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageManager1.Location = new System.Drawing.Point(3, 3);
            this.imageManager1.Name = "imageManager1";
            this.imageManager1.Size = new System.Drawing.Size(943, 504);
            this.imageManager1.TabIndex = 0;
            this.imageManager1.SomethingSaved += new NSMBe4.ImageManager.SomethingSavedD(this.imageManager1_SomethingSaved);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.panel2);
            this.tabPage5.Controls.Add(this.panel1);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(949, 510);
            this.tabPage5.TabIndex = 3;
            this.tabPage5.Text = "Tile behaviors";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.tileBehaviorPicker);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(286, 504);
            this.panel2.TabIndex = 2;
            // 
            // tileBehaviorPicker
            // 
            this.tileBehaviorPicker.AutoSize = true;
            this.tileBehaviorPicker.Location = new System.Drawing.Point(3, 4);
            this.tileBehaviorPicker.MinimumSize = new System.Drawing.Size(256, 224);
            this.tileBehaviorPicker.Name = "tileBehaviorPicker";
            this.tileBehaviorPicker.Size = new System.Drawing.Size(256, 224);
            this.tileBehaviorPicker.TabIndex = 0;
            this.tileBehaviorPicker.TileSelected += new NSMBe4.TilePicker.TileSelectedd(this.tileBehaviorPicker_TileSelected);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.eightLab);
            this.panel1.Controls.Add(this.sevenLab);
            this.panel1.Controls.Add(this.eight);
            this.panel1.Controls.Add(this.seven);
            this.panel1.Controls.Add(this.sixLab);
            this.panel1.Controls.Add(this.fiveLab);
            this.panel1.Controls.Add(this.fourLab);
            this.panel1.Controls.Add(this.threeLab);
            this.panel1.Controls.Add(this.twoLab);
            this.panel1.Controls.Add(this.oneLab);
            this.panel1.Controls.Add(this.six);
            this.panel1.Controls.Add(this.five);
            this.panel1.Controls.Add(this.four);
            this.panel1.Controls.Add(this.three);
            this.panel1.Controls.Add(this.two);
            this.panel1.Controls.Add(this.one);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tileBehaviorEditor);
            this.panel1.Location = new System.Drawing.Point(295, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(651, 504);
            this.panel1.TabIndex = 1;
            // 
            // eightLab
            // 
            this.eightLab.AutoSize = true;
            this.eightLab.Location = new System.Drawing.Point(455, 303);
            this.eightLab.Name = "eightLab";
            this.eightLab.Size = new System.Drawing.Size(49, 13);
            this.eightLab.TabIndex = 17;
            this.eightLab.Text = "<sixLab>";
            // 
            // sevenLab
            // 
            this.sevenLab.AutoSize = true;
            this.sevenLab.Location = new System.Drawing.Point(140, 303);
            this.sevenLab.Name = "sevenLab";
            this.sevenLab.Size = new System.Drawing.Size(54, 13);
            this.sevenLab.TabIndex = 16;
            this.sevenLab.Text = "<fiveLab>";
            // 
            // eight
            // 
            this.eight.FormattingEnabled = true;
            this.eight.Location = new System.Drawing.Point(318, 319);
            this.eight.Name = "eight";
            this.eight.Size = new System.Drawing.Size(274, 21);
            this.eight.TabIndex = 15;
            this.eight.SelectedIndexChanged += new System.EventHandler(this.eight_SelectedIndexChanged);
            // 
            // seven
            // 
            this.seven.FormattingEnabled = true;
            this.seven.Location = new System.Drawing.Point(6, 319);
            this.seven.Name = "seven";
            this.seven.Size = new System.Drawing.Size(306, 21);
            this.seven.TabIndex = 14;
            this.seven.SelectedIndexChanged += new System.EventHandler(this.seven_SelectedIndexChanged);
            // 
            // sixLab
            // 
            this.sixLab.AutoSize = true;
            this.sixLab.Location = new System.Drawing.Point(455, 235);
            this.sixLab.Name = "sixLab";
            this.sixLab.Size = new System.Drawing.Size(49, 13);
            this.sixLab.TabIndex = 13;
            this.sixLab.Text = "<sixLab>";
            // 
            // fiveLab
            // 
            this.fiveLab.AutoSize = true;
            this.fiveLab.Location = new System.Drawing.Point(140, 235);
            this.fiveLab.Name = "fiveLab";
            this.fiveLab.Size = new System.Drawing.Size(54, 13);
            this.fiveLab.TabIndex = 12;
            this.fiveLab.Text = "<fiveLab>";
            // 
            // fourLab
            // 
            this.fourLab.AutoSize = true;
            this.fourLab.Location = new System.Drawing.Point(452, 163);
            this.fourLab.Name = "fourLab";
            this.fourLab.Size = new System.Drawing.Size(55, 13);
            this.fourLab.TabIndex = 11;
            this.fourLab.Text = "<fourLab>";
            // 
            // threeLab
            // 
            this.threeLab.AutoSize = true;
            this.threeLab.Location = new System.Drawing.Point(140, 163);
            this.threeLab.Name = "threeLab";
            this.threeLab.Size = new System.Drawing.Size(61, 13);
            this.threeLab.TabIndex = 10;
            this.threeLab.Text = "<threeLab>";
            // 
            // twoLab
            // 
            this.twoLab.AutoSize = true;
            this.twoLab.Location = new System.Drawing.Point(449, 86);
            this.twoLab.Name = "twoLab";
            this.twoLab.Size = new System.Drawing.Size(54, 13);
            this.twoLab.TabIndex = 9;
            this.twoLab.Text = "<twoLab>";
            // 
            // oneLab
            // 
            this.oneLab.AutoSize = true;
            this.oneLab.Location = new System.Drawing.Point(140, 86);
            this.oneLab.Name = "oneLab";
            this.oneLab.Size = new System.Drawing.Size(55, 13);
            this.oneLab.TabIndex = 8;
            this.oneLab.Text = "<oneLab>";
            // 
            // six
            // 
            this.six.FormattingEnabled = true;
            this.six.Location = new System.Drawing.Point(318, 251);
            this.six.Name = "six";
            this.six.Size = new System.Drawing.Size(274, 21);
            this.six.TabIndex = 7;
            this.six.SelectedIndexChanged += new System.EventHandler(this.six_SelectedIndexChanged);
            // 
            // five
            // 
            this.five.FormattingEnabled = true;
            this.five.Location = new System.Drawing.Point(6, 251);
            this.five.Name = "five";
            this.five.Size = new System.Drawing.Size(306, 21);
            this.five.TabIndex = 6;
            this.five.SelectedIndexChanged += new System.EventHandler(this.five_SelectedIndexChanged);
            // 
            // four
            // 
            this.four.FormattingEnabled = true;
            this.four.Location = new System.Drawing.Point(318, 179);
            this.four.Name = "four";
            this.four.Size = new System.Drawing.Size(274, 21);
            this.four.TabIndex = 5;
            this.four.SelectedIndexChanged += new System.EventHandler(this.four_SelectedIndexChanged);
            // 
            // three
            // 
            this.three.FormattingEnabled = true;
            this.three.Location = new System.Drawing.Point(6, 179);
            this.three.Name = "three";
            this.three.Size = new System.Drawing.Size(306, 21);
            this.three.TabIndex = 4;
            this.three.SelectedIndexChanged += new System.EventHandler(this.three_SelectedIndexChanged);
            // 
            // two
            // 
            this.two.FormattingEnabled = true;
            this.two.Location = new System.Drawing.Point(318, 102);
            this.two.Name = "two";
            this.two.Size = new System.Drawing.Size(274, 21);
            this.two.TabIndex = 3;
            this.two.SelectedIndexChanged += new System.EventHandler(this.two_SelectedIndexChanged);
            // 
            // one
            // 
            this.one.FormattingEnabled = true;
            this.one.Location = new System.Drawing.Point(6, 102);
            this.one.Name = "one";
            this.one.Size = new System.Drawing.Size(306, 21);
            this.one.TabIndex = 2;
            this.one.SelectedIndexChanged += new System.EventHandler(this.one_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Behavior: ";
            // 
            // tileBehaviorEditor
            // 
            this.tileBehaviorEditor.Location = new System.Drawing.Point(64, 3);
            this.tileBehaviorEditor.Name = "tileBehaviorEditor";
            this.tileBehaviorEditor.Size = new System.Drawing.Size(167, 23);
            this.tileBehaviorEditor.TabIndex = 0;
            this.tileBehaviorEditor.ValueChanged += new NSMBe4.ByteArrayEditor.ValueChangedD(this.tileBehaviorEditor_ValueChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripSeparator1,
            this.deleteAllButton,
            this.toolStripSeparator3,
            this.createDescriptions,
            this.deleteDescriptions,
            this.setend});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(957, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = global::NSMBe4.Properties.Resources.save;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(66, 22);
            this.toolStripButton1.Text = "<save>";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // deleteAllButton
            // 
            this.deleteAllButton.Image = global::NSMBe4.Properties.Resources.cross_script;
            this.deleteAllButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteAllButton.Name = "deleteAllButton";
            this.deleteAllButton.Size = new System.Drawing.Size(77, 22);
            this.deleteAllButton.Text = "<Del All>";
            this.deleteAllButton.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // createDescriptions
            // 
            this.createDescriptions.Image = global::NSMBe4.Properties.Resources.textfield_add;
            this.createDescriptions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.createDescriptions.Name = "createDescriptions";
            this.createDescriptions.Size = new System.Drawing.Size(107, 22);
            this.createDescriptions.Text = "<descriptions>";
            this.createDescriptions.Click += new System.EventHandler(this.createDescriptions_Click);
            // 
            // deleteDescriptions
            // 
            this.deleteDescriptions.Image = global::NSMBe4.Properties.Resources.cross_script;
            this.deleteDescriptions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteDescriptions.Name = "deleteDescriptions";
            this.deleteDescriptions.Size = new System.Drawing.Size(142, 22);
            this.deleteDescriptions.Text = "<delete descriptions>";
            this.deleteDescriptions.Click += new System.EventHandler(this.deleteDescriptions_Click);
            // 
            // setend
            // 
            this.setend.Image = global::NSMBe4.Properties.Resources.cross_script;
            this.setend.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.setend.Name = "setend";
            this.setend.Size = new System.Drawing.Size(81, 22);
            this.setend.Text = "<set end>";
            this.setend.ToolTipText = "makes the selected block the last in the tileset";
            this.setend.Click += new System.EventHandler(this.setend_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "PNG|*.png";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "PNG|*.png";
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.Filter = "NSMB Tilesets|*.nmt";
            // 
            // saveFileDialog2
            // 
            this.saveFileDialog2.Filter = "NSMB Tilesets|*.nmt";
            // 
            // TilesetEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(957, 561);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "TilesetEditor";
            this.Text = "TilesetEditor";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TilesetEditor_FormClosed);
            this.Load += new System.EventHandler(this.TilesetEditor_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private ObjectPickerControl objectPickerControl1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private TilesetObjectEditor tilesetObjectEditor1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripButton deleteAllButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.SaveFileDialog saveFileDialog2;
        private System.Windows.Forms.ToolStripButton createDescriptions;
        private System.Windows.Forms.ToolStripButton deleteDescriptions;
        private System.Windows.Forms.ToolStripButton setend;
        private System.Windows.Forms.TabPage tabPage3;
        private ImageManager imageManager1;
        private TilemapEditor.TilemapEditor tilemapEditor1;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Panel panel2;
        private TilePicker tileBehaviorPicker;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private ByteArrayEditor tileBehaviorEditor;
        public System.Windows.Forms.ComboBox one;
        private System.Windows.Forms.ComboBox five;
        private System.Windows.Forms.ComboBox four;
        private System.Windows.Forms.ComboBox three;
        private System.Windows.Forms.ComboBox two;
        private System.Windows.Forms.ComboBox six;
        private System.Windows.Forms.Label sixLab;
        private System.Windows.Forms.Label fiveLab;
        private System.Windows.Forms.Label fourLab;
        private System.Windows.Forms.Label threeLab;
        private System.Windows.Forms.Label twoLab;
        private System.Windows.Forms.Label oneLab;
        private System.Windows.Forms.Label eightLab;
        private System.Windows.Forms.Label sevenLab;
        private System.Windows.Forms.ComboBox eight;
        private System.Windows.Forms.ComboBox seven;
    }
}