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
            this.deleteObjectButton = new System.Windows.Forms.Button();
            this.addObjectButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.objTypeUpDown = new System.Windows.Forms.NumericUpDown();
            this.objTileset0Button = new System.Windows.Forms.RadioButton();
            this.objTileset1Button = new System.Windows.Forms.RadioButton();
            this.objTileset2Button = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objTypeUpDown)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // deleteObjectButton
            // 
            this.deleteObjectButton.Location = new System.Drawing.Point(142, 3);
            this.deleteObjectButton.Name = "deleteObjectButton";
            this.deleteObjectButton.Size = new System.Drawing.Size(88, 23);
            this.deleteObjectButton.TabIndex = 12;
            this.deleteObjectButton.Text = "<deleteObjectButton>";
            this.deleteObjectButton.UseVisualStyleBackColor = true;
            this.deleteObjectButton.Click += new System.EventHandler(this.deleteObjectButton_Click);
            // 
            // addObjectButton
            // 
            this.addObjectButton.Location = new System.Drawing.Point(48, 3);
            this.addObjectButton.Name = "addObjectButton";
            this.addObjectButton.Size = new System.Drawing.Size(88, 23);
            this.addObjectButton.TabIndex = 11;
            this.addObjectButton.Text = "<addObjectButton>";
            this.addObjectButton.UseVisualStyleBackColor = true;
            this.addObjectButton.Click += new System.EventHandler(this.addObjectButton_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label6, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.objTypeUpDown, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.objTileset0Button, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.objTileset1Button, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.objTileset2Button, 3, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 32);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(278, 51);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(61, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "<label5>";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(61, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "<label6>";
            // 
            // objTypeUpDown
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.objTypeUpDown, 3);
            this.objTypeUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objTypeUpDown.Location = new System.Drawing.Point(114, 28);
            this.objTypeUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.objTypeUpDown.Name = "objTypeUpDown";
            this.objTypeUpDown.Size = new System.Drawing.Size(161, 20);
            this.objTypeUpDown.TabIndex = 2;
            this.objTypeUpDown.ValueChanged += new System.EventHandler(this.objTypeUpDown_ValueChanged);
            // 
            // objTileset0Button
            // 
            this.objTileset0Button.AutoSize = true;
            this.objTileset0Button.Location = new System.Drawing.Point(114, 3);
            this.objTileset0Button.Name = "objTileset0Button";
            this.objTileset0Button.Size = new System.Drawing.Size(31, 17);
            this.objTileset0Button.TabIndex = 3;
            this.objTileset0Button.Text = "0";
            this.objTileset0Button.UseVisualStyleBackColor = true;
            this.objTileset0Button.Click += new System.EventHandler(this.objTileset0Button_Click);
            // 
            // objTileset1Button
            // 
            this.objTileset1Button.AutoSize = true;
            this.objTileset1Button.Location = new System.Drawing.Point(169, 3);
            this.objTileset1Button.Name = "objTileset1Button";
            this.objTileset1Button.Size = new System.Drawing.Size(31, 17);
            this.objTileset1Button.TabIndex = 4;
            this.objTileset1Button.Text = "1";
            this.objTileset1Button.UseVisualStyleBackColor = true;
            this.objTileset1Button.Click += new System.EventHandler(this.objTileset1Button_Click);
            // 
            // objTileset2Button
            // 
            this.objTileset2Button.AutoSize = true;
            this.objTileset2Button.Location = new System.Drawing.Point(224, 3);
            this.objTileset2Button.Name = "objTileset2Button";
            this.objTileset2Button.Size = new System.Drawing.Size(31, 17);
            this.objTileset2Button.TabIndex = 5;
            this.objTileset2Button.Text = "2";
            this.objTileset2Button.UseVisualStyleBackColor = true;
            this.objTileset2Button.Click += new System.EventHandler(this.objTileset2Button_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.addObjectButton);
            this.panel1.Controls.Add(this.deleteObjectButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(278, 32);
            this.panel1.TabIndex = 13;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 83);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(278, 340);
            this.panel2.TabIndex = 14;
            // 
            // ObjectEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.panel1);
            this.Name = "ObjectEditor";
            this.Size = new System.Drawing.Size(278, 423);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objTypeUpDown)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button deleteObjectButton;
        private System.Windows.Forms.Button addObjectButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown objTypeUpDown;
        private System.Windows.Forms.RadioButton objTileset0Button;
        private System.Windows.Forms.RadioButton objTileset1Button;
        private System.Windows.Forms.RadioButton objTileset2Button;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;

    }
}
