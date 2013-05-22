namespace NSMBe4.Editor
{
    partial class CoordinateViewer
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.heightUpDown = new System.Windows.Forms.NumericUpDown();
            this.yUpDown = new System.Windows.Forms.NumericUpDown();
            this.xUpDown = new System.Windows.Forms.NumericUpDown();
            this.lblX = new System.Windows.Forms.Label();
            this.lblWidth = new System.Windows.Forms.Label();
            this.lblY = new System.Windows.Forms.Label();
            this.lblHeight = new System.Windows.Forms.Label();
            this.widthUpDown = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.heightUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.widthUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.heightUpDown, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.yUpDown, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.xUpDown, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblX, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblWidth, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblY, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblHeight, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.widthUpDown, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(328, 52);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // heightUpDown
            // 
            this.heightUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.heightUpDown.Location = new System.Drawing.Point(205, 29);
            this.heightUpDown.Name = "heightUpDown";
            this.heightUpDown.Size = new System.Drawing.Size(120, 20);
            this.heightUpDown.TabIndex = 7;
            this.heightUpDown.ValueChanged += new System.EventHandler(this.anyUpDownValueChanged);
            // 
            // yUpDown
            // 
            this.yUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.yUpDown.Location = new System.Drawing.Point(23, 29);
            this.yUpDown.Name = "yUpDown";
            this.yUpDown.Size = new System.Drawing.Size(120, 20);
            this.yUpDown.TabIndex = 6;
            this.yUpDown.ValueChanged += new System.EventHandler(this.anyUpDownValueChanged);
            // 
            // xUpDown
            // 
            this.xUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.xUpDown.Location = new System.Drawing.Point(23, 3);
            this.xUpDown.Name = "xUpDown";
            this.xUpDown.Size = new System.Drawing.Size(120, 20);
            this.xUpDown.TabIndex = 5;
            this.xUpDown.ValueChanged += new System.EventHandler(this.anyUpDownValueChanged);
            // 
            // lblX
            // 
            this.lblX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblX.AutoSize = true;
            this.lblX.Location = new System.Drawing.Point(3, 6);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(14, 13);
            this.lblX.TabIndex = 1;
            this.lblX.Text = "X";
            // 
            // lblWidth
            // 
            this.lblWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWidth.AutoSize = true;
            this.lblWidth.Location = new System.Drawing.Point(149, 6);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(50, 13);
            this.lblWidth.TabIndex = 0;
            this.lblWidth.Text = "<Width>";
            // 
            // lblY
            // 
            this.lblY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblY.AutoSize = true;
            this.lblY.Location = new System.Drawing.Point(3, 32);
            this.lblY.Name = "lblY";
            this.lblY.Size = new System.Drawing.Size(14, 13);
            this.lblY.TabIndex = 2;
            this.lblY.Text = "Y";
            // 
            // lblHeight
            // 
            this.lblHeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHeight.AutoSize = true;
            this.lblHeight.Location = new System.Drawing.Point(149, 32);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(50, 13);
            this.lblHeight.TabIndex = 3;
            this.lblHeight.Text = "<Height>";
            // 
            // widthUpDown
            // 
            this.widthUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.widthUpDown.Location = new System.Drawing.Point(205, 3);
            this.widthUpDown.Name = "widthUpDown";
            this.widthUpDown.Size = new System.Drawing.Size(120, 20);
            this.widthUpDown.TabIndex = 4;
            this.widthUpDown.ValueChanged += new System.EventHandler(this.anyUpDownValueChanged);
            // 
            // CoordinateViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "CoordinateViewer";
            this.Size = new System.Drawing.Size(328, 52);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.heightUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.widthUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblX;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.Label lblY;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.NumericUpDown heightUpDown;
        private System.Windows.Forms.NumericUpDown yUpDown;
        private System.Windows.Forms.NumericUpDown xUpDown;
        private System.Windows.Forms.NumericUpDown widthUpDown;
    }
}
