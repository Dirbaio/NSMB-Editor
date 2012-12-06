namespace NSMBe4 {
	partial class BehaviorEditor {
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
			this.flagsListBox = new System.Windows.Forms.CheckedListBox();
			this.subTypeLabel = new System.Windows.Forms.Label();
			this.flagsLabel = new System.Windows.Forms.Label();
			this.subTypeComboBox = new System.Windows.Forms.ComboBox();
			this.paramsLabel = new System.Windows.Forms.Label();
			this.paramsListBox = new System.Windows.Forms.ListBox();
			this.partialBlockParamPanel = new System.Windows.Forms.Panel();
			this.bottomRightPBCheckBox = new System.Windows.Forms.CheckBox();
			this.bottomLeftPBCheckBox = new System.Windows.Forms.CheckBox();
			this.topRightPBCheckBox = new System.Windows.Forms.CheckBox();
			this.topLeftPBCheckBox = new System.Windows.Forms.CheckBox();
			this.partialBlockExplainLabel = new System.Windows.Forms.Label();
			this.pipeDoorParamPanel = new System.Windows.Forms.Panel();
			this.pipeDoorTypeListBox = new System.Windows.Forms.ListBox();
			this.pipeDoorMainTypeComboBox = new System.Windows.Forms.ComboBox();
			this.partialBlockParamPanel.SuspendLayout();
			this.pipeDoorParamPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// flagsListBox
			// 
			this.flagsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.flagsListBox.CheckOnClick = true;
			this.flagsListBox.FormattingEnabled = true;
			this.flagsListBox.IntegralHeight = false;
			this.flagsListBox.Location = new System.Drawing.Point(3, 18);
			this.flagsListBox.Name = "flagsListBox";
			this.flagsListBox.Size = new System.Drawing.Size(155, 289);
			this.flagsListBox.TabIndex = 0;
			this.flagsListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.flagsListBox_ItemCheck);
			// 
			// subTypeLabel
			// 
			this.subTypeLabel.AutoSize = true;
			this.subTypeLabel.Location = new System.Drawing.Point(164, 4);
			this.subTypeLabel.Name = "subTypeLabel";
			this.subTypeLabel.Size = new System.Drawing.Size(76, 13);
			this.subTypeLabel.TabIndex = 1;
			this.subTypeLabel.Text = "Tile Sub-Type:";
			// 
			// flagsLabel
			// 
			this.flagsLabel.AutoSize = true;
			this.flagsLabel.Location = new System.Drawing.Point(3, 4);
			this.flagsLabel.Name = "flagsLabel";
			this.flagsLabel.Size = new System.Drawing.Size(35, 13);
			this.flagsLabel.TabIndex = 2;
			this.flagsLabel.Text = "Flags:";
			// 
			// subTypeComboBox
			// 
			this.subTypeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.subTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.subTypeComboBox.FormattingEnabled = true;
			this.subTypeComboBox.Location = new System.Drawing.Point(164, 18);
			this.subTypeComboBox.Name = "subTypeComboBox";
			this.subTypeComboBox.Size = new System.Drawing.Size(242, 21);
			this.subTypeComboBox.TabIndex = 3;
			this.subTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.subTypeComboBox_SelectedIndexChanged);
			// 
			// paramsLabel
			// 
			this.paramsLabel.AutoSize = true;
			this.paramsLabel.Location = new System.Drawing.Point(164, 53);
			this.paramsLabel.Name = "paramsLabel";
			this.paramsLabel.Size = new System.Drawing.Size(119, 13);
			this.paramsLabel.TabIndex = 4;
			this.paramsLabel.Text = "Parameters (NO TYPE):";
			// 
			// paramsListBox
			// 
			this.paramsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.paramsListBox.FormattingEnabled = true;
			this.paramsListBox.Location = new System.Drawing.Point(164, 69);
			this.paramsListBox.Name = "paramsListBox";
			this.paramsListBox.Size = new System.Drawing.Size(242, 238);
			this.paramsListBox.TabIndex = 5;
			this.paramsListBox.Visible = false;
			this.paramsListBox.SelectedIndexChanged += new System.EventHandler(this.paramsListBox_SelectedIndexChanged);
			// 
			// partialBlockParamPanel
			// 
			this.partialBlockParamPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.partialBlockParamPanel.Controls.Add(this.bottomRightPBCheckBox);
			this.partialBlockParamPanel.Controls.Add(this.bottomLeftPBCheckBox);
			this.partialBlockParamPanel.Controls.Add(this.topRightPBCheckBox);
			this.partialBlockParamPanel.Controls.Add(this.topLeftPBCheckBox);
			this.partialBlockParamPanel.Controls.Add(this.partialBlockExplainLabel);
			this.partialBlockParamPanel.Location = new System.Drawing.Point(164, 69);
			this.partialBlockParamPanel.Name = "partialBlockParamPanel";
			this.partialBlockParamPanel.Size = new System.Drawing.Size(242, 238);
			this.partialBlockParamPanel.TabIndex = 6;
			// 
			// bottomRightPBCheckBox
			// 
			this.bottomRightPBCheckBox.AutoSize = true;
			this.bottomRightPBCheckBox.Location = new System.Drawing.Point(24, 23);
			this.bottomRightPBCheckBox.Name = "bottomRightPBCheckBox";
			this.bottomRightPBCheckBox.Size = new System.Drawing.Size(15, 14);
			this.bottomRightPBCheckBox.TabIndex = 4;
			this.bottomRightPBCheckBox.UseVisualStyleBackColor = true;
			this.bottomRightPBCheckBox.CheckedChanged += new System.EventHandler(this.PBCheckBox_CheckedChanged);
			// 
			// bottomLeftPBCheckBox
			// 
			this.bottomLeftPBCheckBox.AutoSize = true;
			this.bottomLeftPBCheckBox.Location = new System.Drawing.Point(3, 23);
			this.bottomLeftPBCheckBox.Name = "bottomLeftPBCheckBox";
			this.bottomLeftPBCheckBox.Size = new System.Drawing.Size(15, 14);
			this.bottomLeftPBCheckBox.TabIndex = 3;
			this.bottomLeftPBCheckBox.UseVisualStyleBackColor = true;
			this.bottomLeftPBCheckBox.CheckedChanged += new System.EventHandler(this.PBCheckBox_CheckedChanged);
			// 
			// topRightPBCheckBox
			// 
			this.topRightPBCheckBox.AutoSize = true;
			this.topRightPBCheckBox.Location = new System.Drawing.Point(24, 3);
			this.topRightPBCheckBox.Name = "topRightPBCheckBox";
			this.topRightPBCheckBox.Size = new System.Drawing.Size(15, 14);
			this.topRightPBCheckBox.TabIndex = 2;
			this.topRightPBCheckBox.UseVisualStyleBackColor = true;
			this.topRightPBCheckBox.CheckedChanged += new System.EventHandler(this.PBCheckBox_CheckedChanged);
			// 
			// topLeftPBCheckBox
			// 
			this.topLeftPBCheckBox.AutoSize = true;
			this.topLeftPBCheckBox.Location = new System.Drawing.Point(3, 3);
			this.topLeftPBCheckBox.Name = "topLeftPBCheckBox";
			this.topLeftPBCheckBox.Size = new System.Drawing.Size(15, 14);
			this.topLeftPBCheckBox.TabIndex = 1;
			this.topLeftPBCheckBox.UseVisualStyleBackColor = true;
			this.topLeftPBCheckBox.CheckedChanged += new System.EventHandler(this.PBCheckBox_CheckedChanged);
			// 
			// partialBlockExplainLabel
			// 
			this.partialBlockExplainLabel.AutoSize = true;
			this.partialBlockExplainLabel.Location = new System.Drawing.Point(3, 40);
			this.partialBlockExplainLabel.Name = "partialBlockExplainLabel";
			this.partialBlockExplainLabel.Size = new System.Drawing.Size(180, 13);
			this.partialBlockExplainLabel.TabIndex = 0;
			this.partialBlockExplainLabel.Text = "The checked quadrants will be solid.";
			// 
			// pipeDoorParamPanel
			// 
			this.pipeDoorParamPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pipeDoorParamPanel.Controls.Add(this.pipeDoorTypeListBox);
			this.pipeDoorParamPanel.Controls.Add(this.pipeDoorMainTypeComboBox);
			this.pipeDoorParamPanel.Location = new System.Drawing.Point(164, 69);
			this.pipeDoorParamPanel.Name = "pipeDoorParamPanel";
			this.pipeDoorParamPanel.Size = new System.Drawing.Size(242, 238);
			this.pipeDoorParamPanel.TabIndex = 5;
			// 
			// pipeDoorTypeListBox
			// 
			this.pipeDoorTypeListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pipeDoorTypeListBox.FormattingEnabled = true;
			this.pipeDoorTypeListBox.IntegralHeight = false;
			this.pipeDoorTypeListBox.Location = new System.Drawing.Point(0, 27);
			this.pipeDoorTypeListBox.Name = "pipeDoorTypeListBox";
			this.pipeDoorTypeListBox.Size = new System.Drawing.Size(242, 211);
			this.pipeDoorTypeListBox.TabIndex = 1;
			this.pipeDoorTypeListBox.SelectedIndexChanged += new System.EventHandler(this.pipeDoorTypeListBox_SelectedIndexChanged);
			// 
			// pipeDoorMainTypeComboBox
			// 
			this.pipeDoorMainTypeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pipeDoorMainTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.pipeDoorMainTypeComboBox.FormattingEnabled = true;
			this.pipeDoorMainTypeComboBox.Location = new System.Drawing.Point(0, 0);
			this.pipeDoorMainTypeComboBox.Name = "pipeDoorMainTypeComboBox";
			this.pipeDoorMainTypeComboBox.Size = new System.Drawing.Size(242, 21);
			this.pipeDoorMainTypeComboBox.TabIndex = 0;
			this.pipeDoorMainTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.pipeDoorMainTypeComboBox_SelectedIndexChanged);
			// 
			// BehaviorEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pipeDoorParamPanel);
			this.Controls.Add(this.partialBlockParamPanel);
			this.Controls.Add(this.paramsListBox);
			this.Controls.Add(this.paramsLabel);
			this.Controls.Add(this.subTypeComboBox);
			this.Controls.Add(this.flagsLabel);
			this.Controls.Add(this.subTypeLabel);
			this.Controls.Add(this.flagsListBox);
			this.Name = "BehaviorEditor";
			this.Size = new System.Drawing.Size(413, 310);
			this.partialBlockParamPanel.ResumeLayout(false);
			this.partialBlockParamPanel.PerformLayout();
			this.pipeDoorParamPanel.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckedListBox flagsListBox;
		private System.Windows.Forms.Label subTypeLabel;
		private System.Windows.Forms.Label flagsLabel;
		private System.Windows.Forms.ComboBox subTypeComboBox;
		private System.Windows.Forms.Label paramsLabel;
		private System.Windows.Forms.ListBox paramsListBox;
		private System.Windows.Forms.Panel partialBlockParamPanel;
		private System.Windows.Forms.CheckBox bottomRightPBCheckBox;
		private System.Windows.Forms.CheckBox bottomLeftPBCheckBox;
		private System.Windows.Forms.CheckBox topRightPBCheckBox;
		private System.Windows.Forms.CheckBox topLeftPBCheckBox;
		private System.Windows.Forms.Label partialBlockExplainLabel;
		private System.Windows.Forms.Panel pipeDoorParamPanel;
		private System.Windows.Forms.ListBox pipeDoorTypeListBox;
		private System.Windows.Forms.ComboBox pipeDoorMainTypeComboBox;
	}
}
