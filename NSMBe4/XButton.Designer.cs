namespace NSMBe4
{
    partial class XButton
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
            this.SuspendLayout();
            // 
            // XButton
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.DoubleBuffered = true;
            this.Name = "XButton";
            this.Size = new System.Drawing.Size(16, 16);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.XButton_Paint);
            this.MouseEnter += new System.EventHandler(this.XButton_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.XButton_MouseLeave);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
