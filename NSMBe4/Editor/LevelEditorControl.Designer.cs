namespace NSMBe4 {
    partial class LevelEditorControl {
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.vScrollBar = new System.Windows.Forms.VScrollBar();
            this.hScrollBar = new System.Windows.Forms.HScrollBar();
            this.DrawingArea = new System.Windows.Forms.PictureBox();
            this.dragTimer = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.DrawingArea)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // vScrollBar
            // 
            this.vScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.vScrollBar.LargeChange = 4;
            this.vScrollBar.Location = new System.Drawing.Point(508, 0);
            this.vScrollBar.Maximum = 1024;
            this.vScrollBar.Name = "vScrollBar";
            this.vScrollBar.Size = new System.Drawing.Size(15, 454);
            this.vScrollBar.TabIndex = 0;
            this.vScrollBar.ValueChanged += new System.EventHandler(this.vScrollBar_ValueChanged);
            // 
            // hScrollBar
            // 
            this.hScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.hScrollBar.LargeChange = 4;
            this.hScrollBar.Location = new System.Drawing.Point(0, 454);
            this.hScrollBar.Maximum = 512;
            this.hScrollBar.Name = "hScrollBar";
            this.hScrollBar.Size = new System.Drawing.Size(508, 15);
            this.hScrollBar.TabIndex = 1;
            this.hScrollBar.ValueChanged += new System.EventHandler(this.hScrollBar_ValueChanged);
            // 
            // DrawingArea
            // 
            this.DrawingArea.BackColor = System.Drawing.Color.LightSlateGray;
            this.DrawingArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DrawingArea.Location = new System.Drawing.Point(0, 0);
            this.DrawingArea.Margin = new System.Windows.Forms.Padding(0);
            this.DrawingArea.Name = "DrawingArea";
            this.DrawingArea.Size = new System.Drawing.Size(508, 454);
            this.DrawingArea.TabIndex = 2;
            this.DrawingArea.TabStop = false;
            this.DrawingArea.SizeChanged += new System.EventHandler(this.DrawingArea_SizeChanged);
            this.DrawingArea.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawingArea_Paint);
            this.DrawingArea.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DrawingArea_MouseDown);
            this.DrawingArea.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DrawingArea_MouseMove);
            this.DrawingArea.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DrawingArea_MouseUp);
            // 
            // dragTimer
            // 
            this.dragTimer.Enabled = true;
            this.dragTimer.Interval = 15;
            this.dragTimer.Tick += new System.EventHandler(this.dragTimer_Tick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.DrawingArea, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.hScrollBar, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.vScrollBar, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(523, 469);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // LevelEditorControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "LevelEditorControl";
            this.Size = new System.Drawing.Size(523, 469);
            this.Resize += new System.EventHandler(this.LevelEditorControl_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.DrawingArea)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.VScrollBar vScrollBar;
        private System.Windows.Forms.HScrollBar hScrollBar;
        private System.Windows.Forms.PictureBox DrawingArea;
        private System.Windows.Forms.Timer dragTimer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
