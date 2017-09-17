namespace LogLogViewer.Forms
{
    partial class MailAccountDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MailAccountDialog));
            this.chkSmtpSSL = new System.Windows.Forms.CheckBox();
            this.txtSmtpPassword = new System.Windows.Forms.MaskedTextBox();
            this.txtSmtpAccountName = new System.Windows.Forms.TextBox();
            this.txtSmtpServer = new System.Windows.Forms.TextBox();
            this.txtSmtpPort = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.txtSmtpEmailAddress = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // chkSmtpSSL
            // 
            resources.ApplyResources(this.chkSmtpSSL, "chkSmtpSSL");
            this.chkSmtpSSL.Name = "chkSmtpSSL";
            this.chkSmtpSSL.UseVisualStyleBackColor = true;
            // 
            // txtSmtpPassword
            // 
            resources.ApplyResources(this.txtSmtpPassword, "txtSmtpPassword");
            this.txtSmtpPassword.Name = "txtSmtpPassword";
            this.txtSmtpPassword.PasswordChar = '*';
            // 
            // txtSmtpAccountName
            // 
            resources.ApplyResources(this.txtSmtpAccountName, "txtSmtpAccountName");
            this.txtSmtpAccountName.Name = "txtSmtpAccountName";
            // 
            // txtSmtpServer
            // 
            resources.ApplyResources(this.txtSmtpServer, "txtSmtpServer");
            this.txtSmtpServer.Name = "txtSmtpServer";
            // 
            // txtSmtpPort
            // 
            resources.ApplyResources(this.txtSmtpPort, "txtSmtpPort");
            this.txtSmtpPort.Name = "txtSmtpPort";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // txtSmtpEmailAddress
            // 
            resources.ApplyResources(this.txtSmtpEmailAddress, "txtSmtpEmailAddress");
            this.txtSmtpEmailAddress.Name = "txtSmtpEmailAddress";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // MailAccountDialog
            // 
            this.AcceptButton = this.buttonOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.txtSmtpEmailAddress);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.chkSmtpSSL);
            this.Controls.Add(this.txtSmtpPassword);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtSmtpAccountName);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtSmtpServer);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtSmtpPort);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MailAccountDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkSmtpSSL;
        private System.Windows.Forms.MaskedTextBox txtSmtpPassword;
        private System.Windows.Forms.TextBox txtSmtpAccountName;
        private System.Windows.Forms.TextBox txtSmtpServer;
        private System.Windows.Forms.TextBox txtSmtpPort;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.TextBox txtSmtpEmailAddress;
        private System.Windows.Forms.Label label7;
    }
}