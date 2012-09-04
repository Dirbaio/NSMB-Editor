namespace NSMBe4
{
    partial class ObjectEditor
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tileset0tab = new System.Windows.Forms.TabPage();
            this.tileset0picker = new NSMBe4.ObjectPickerControlNew();
            this.tileset1tab = new System.Windows.Forms.TabPage();
            this.tileset1picker = new NSMBe4.ObjectPickerControlNew();
            this.tileset2tab = new System.Windows.Forms.TabPage();
            this.tileset2picker = new NSMBe4.ObjectPickerControlNew();
            this.tabControl1.SuspendLayout();
            this.tileset0tab.SuspendLayout();
            this.tileset1tab.SuspendLayout();
            this.tileset2tab.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tileset0tab);
            this.tabControl1.Controls.Add(this.tileset1tab);
            this.tabControl1.Controls.Add(this.tileset2tab);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(278, 423);
            this.tabControl1.TabIndex = 15;
            // 
            // tileset0tab
            // 
            this.tileset0tab.Controls.Add(this.tileset0picker);
            this.tileset0tab.Location = new System.Drawing.Point(4, 22);
            this.tileset0tab.Name = "tileset0tab";
            this.tileset0tab.Padding = new System.Windows.Forms.Padding(3);
            this.tileset0tab.Size = new System.Drawing.Size(270, 397);
            this.tileset0tab.TabIndex = 0;
            this.tileset0tab.Text = "Tileset 0";
            this.tileset0tab.UseVisualStyleBackColor = true;
            // 
            // tileset0picker
            // 
            this.tileset0picker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tileset0picker.Location = new System.Drawing.Point(3, 3);
            this.tileset0picker.Name = "tileset0picker";
            this.tileset0picker.Size = new System.Drawing.Size(264, 391);
            this.tileset0picker.TabIndex = 0;
            this.tileset0picker.ObjectSelected += new NSMBe4.ObjectPickerControlNew.ObjectSelectedDelegate(this.tileset0picker_ObjectSelected);
            // 
            // tileset1tab
            // 
            this.tileset1tab.Controls.Add(this.tileset1picker);
            this.tileset1tab.Location = new System.Drawing.Point(4, 22);
            this.tileset1tab.Name = "tileset1tab";
            this.tileset1tab.Padding = new System.Windows.Forms.Padding(3);
            this.tileset1tab.Size = new System.Drawing.Size(270, 361);
            this.tileset1tab.TabIndex = 1;
            this.tileset1tab.Text = "Tileset 1";
            this.tileset1tab.UseVisualStyleBackColor = true;
            // 
            // tileset1picker
            // 
            this.tileset1picker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tileset1picker.Location = new System.Drawing.Point(3, 3);
            this.tileset1picker.Name = "tileset1picker";
            this.tileset1picker.Size = new System.Drawing.Size(264, 326);
            this.tileset1picker.TabIndex = 1;
            this.tileset1picker.ObjectSelected += new NSMBe4.ObjectPickerControlNew.ObjectSelectedDelegate(this.tileset1picker_ObjectSelected);
            // 
            // tileset2tab
            // 
            this.tileset2tab.Controls.Add(this.tileset2picker);
            this.tileset2tab.Location = new System.Drawing.Point(4, 22);
            this.tileset2tab.Name = "tileset2tab";
            this.tileset2tab.Size = new System.Drawing.Size(270, 361);
            this.tileset2tab.TabIndex = 2;
            this.tileset2tab.Text = "Tileset 2";
            this.tileset2tab.UseVisualStyleBackColor = true;
            // 
            // tileset2picker
            // 
            this.tileset2picker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tileset2picker.Location = new System.Drawing.Point(0, 0);
            this.tileset2picker.Name = "tileset2picker";
            this.tileset2picker.Size = new System.Drawing.Size(270, 332);
            this.tileset2picker.TabIndex = 1;
            this.tileset2picker.ObjectSelected += new NSMBe4.ObjectPickerControlNew.ObjectSelectedDelegate(this.tileset2picker_ObjectSelected);
            // 
            // ObjectEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "ObjectEditor";
            this.Size = new System.Drawing.Size(278, 423);
            this.tabControl1.ResumeLayout(false);
            this.tileset0tab.ResumeLayout(false);
            this.tileset1tab.ResumeLayout(false);
            this.tileset2tab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tileset0tab;
        private System.Windows.Forms.TabPage tileset1tab;
        private System.Windows.Forms.TabPage tileset2tab;
        public ObjectPickerControlNew tileset0picker;
        public ObjectPickerControlNew tileset1picker;
        public ObjectPickerControlNew tileset2picker;

    }
}
