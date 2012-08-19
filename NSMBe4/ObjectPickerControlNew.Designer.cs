namespace NSMBe4
{
    partial class ObjectPickerControlNew
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
            this.components = new System.ComponentModel.Container();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.SuspendLayout();
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipTitle = "LOL";
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScrollBar1.Location = new System.Drawing.Point(132, 0);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(18, 150);
            this.vScrollBar1.TabIndex = 0;
            this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
            // 
            // ObjectPickerControlNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.vScrollBar1);
            this.DoubleBuffered = true;
            this.Name = "ObjectPickerControlNew";
            this.Load += new System.EventHandler(this.ObjectPickerControlNew_Load);
            this.SizeChanged += new System.EventHandler(this.ObjectPickerControlNew_SizeChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ObjectPickerControlNew_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ObjectPickerControlNew_MouseDown);
            this.MouseLeave += new System.EventHandler(this.ObjectPickerControlNew_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ObjectPickerControlNew_MouseMove);
            this.Resize += new System.EventHandler(this.ObjectPickerControlNew_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.VScrollBar vScrollBar1;
    }
}
