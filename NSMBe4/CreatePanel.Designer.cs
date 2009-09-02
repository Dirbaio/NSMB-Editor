namespace NSMBe4
{
    partial class CreatePanel
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
            this.CreateObject = new System.Windows.Forms.Button();
            this.CreateSprite = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CreateObject
            // 
            this.CreateObject.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.CreateObject.Location = new System.Drawing.Point(68, 13);
            this.CreateObject.Name = "CreateObject";
            this.CreateObject.Size = new System.Drawing.Size(110, 23);
            this.CreateObject.TabIndex = 0;
            this.CreateObject.Text = "Create Object";
            this.CreateObject.UseVisualStyleBackColor = true;
            this.CreateObject.Click += new System.EventHandler(this.CreateObject_Click);
            // 
            // CreateSprite
            // 
            this.CreateSprite.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.CreateSprite.Location = new System.Drawing.Point(68, 42);
            this.CreateSprite.Name = "CreateSprite";
            this.CreateSprite.Size = new System.Drawing.Size(110, 23);
            this.CreateSprite.TabIndex = 0;
            this.CreateSprite.Text = "Create Sprite";
            this.CreateSprite.UseVisualStyleBackColor = true;
            this.CreateSprite.Click += new System.EventHandler(this.CreateSprite_Click);
            // 
            // CreatePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CreateSprite);
            this.Controls.Add(this.CreateObject);
            this.Name = "CreatePanel";
            this.Size = new System.Drawing.Size(247, 316);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button CreateObject;
        private System.Windows.Forms.Button CreateSprite;
    }
}
