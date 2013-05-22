namespace NSMBe4
{
    partial class StartForm
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
            this.grpOpenROM = new System.Windows.Forms.GroupBox();
            this.openRomButton = new System.Windows.Forms.Button();
            this.grpServer = new System.Windows.Forms.GroupBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.lblHost = new System.Windows.Forms.Label();
            this.portTextBox = new System.Windows.Forms.TextBox();
            this.hostTextBox = new System.Windows.Forms.TextBox();
            this.connectButton = new System.Windows.Forms.Button();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.grpOpenROM.SuspendLayout();
            this.grpServer.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpOpenROM
            // 
            this.grpOpenROM.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpOpenROM.Controls.Add(this.openRomButton);
            this.grpOpenROM.Location = new System.Drawing.Point(12, 37);
            this.grpOpenROM.Name = "grpOpenROM";
            this.grpOpenROM.Size = new System.Drawing.Size(262, 60);
            this.grpOpenROM.TabIndex = 0;
            this.grpOpenROM.TabStop = false;
            this.grpOpenROM.Text = "<Open ROM file>";
            // 
            // openRomButton
            // 
            this.openRomButton.Location = new System.Drawing.Point(16, 19);
            this.openRomButton.Name = "openRomButton";
            this.openRomButton.Size = new System.Drawing.Size(110, 23);
            this.openRomButton.TabIndex = 0;
            this.openRomButton.Text = "<Open ROM...>";
            this.openRomButton.UseVisualStyleBackColor = true;
            this.openRomButton.Click += new System.EventHandler(this.openRomButton_Click);
            // 
            // grpServer
            // 
            this.grpServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpServer.Controls.Add(this.lblPort);
            this.grpServer.Controls.Add(this.lblHost);
            this.grpServer.Controls.Add(this.portTextBox);
            this.grpServer.Controls.Add(this.hostTextBox);
            this.grpServer.Controls.Add(this.connectButton);
            this.grpServer.Location = new System.Drawing.Point(12, 103);
            this.grpServer.Name = "grpServer";
            this.grpServer.Size = new System.Drawing.Size(262, 103);
            this.grpServer.TabIndex = 1;
            this.grpServer.TabStop = false;
            this.grpServer.Text = "<Connect to server>";
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(6, 48);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(41, 13);
            this.lblPort.TabIndex = 4;
            this.lblPort.Text = "<Port:>";
            // 
            // lblHost
            // 
            this.lblHost.AutoSize = true;
            this.lblHost.Location = new System.Drawing.Point(6, 22);
            this.lblHost.Name = "lblHost";
            this.lblHost.Size = new System.Drawing.Size(41, 13);
            this.lblHost.TabIndex = 3;
            this.lblHost.Text = "<Host>";
            // 
            // portTextBox
            // 
            this.portTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.portTextBox.Location = new System.Drawing.Point(47, 45);
            this.portTextBox.Name = "portTextBox";
            this.portTextBox.Size = new System.Drawing.Size(209, 20);
            this.portTextBox.TabIndex = 2;
            this.portTextBox.Text = "7373";
            // 
            // hostTextBox
            // 
            this.hostTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hostTextBox.Location = new System.Drawing.Point(47, 19);
            this.hostTextBox.Name = "hostTextBox";
            this.hostTextBox.Size = new System.Drawing.Size(209, 20);
            this.hostTextBox.TabIndex = 1;
            // 
            // connectButton
            // 
            this.connectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.connectButton.Location = new System.Drawing.Point(146, 71);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(110, 23);
            this.connectButton.TabIndex = 0;
            this.connectButton.Text = "<Connect>";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Location = new System.Drawing.Point(12, 9);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(152, 13);
            this.lblWelcome.TabIndex = 4;
            this.lblWelcome.Text = "<Welcome to NSMB Editor 5!>";
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 218);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.grpServer);
            this.Controls.Add(this.grpOpenROM);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "StartForm";
            this.Text = "<NSMB Editor 5>";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.StartForm_FormClosed);
            this.grpOpenROM.ResumeLayout(false);
            this.grpServer.ResumeLayout(false);
            this.grpServer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpOpenROM;
        private System.Windows.Forms.Button openRomButton;
        private System.Windows.Forms.GroupBox grpServer;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.Label lblHost;
        private System.Windows.Forms.TextBox portTextBox;
        private System.Windows.Forms.TextBox hostTextBox;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Label lblWelcome;
    }
}