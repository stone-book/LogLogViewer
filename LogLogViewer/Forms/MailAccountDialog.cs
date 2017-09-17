using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LogLogViewer.Classes;

namespace LogLogViewer.Forms
{
    public partial class MailAccountDialog : Form
    {
        /// <summary>
        /// SMTPの設定
        /// </summary>
        public SmtpMailProvider smtpSetting { private set; get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MailAccountDialog(SmtpMailProvider smtpSetting)
        {
            InitializeComponent();

            if (smtpSetting != null)
            {
                this.smtpSetting = smtpSetting;
                this.txtSmtpServer.Text = this.smtpSetting.Server;
                this.txtSmtpPort.Text = this.smtpSetting.Port.ToString();
                this.txtSmtpAccountName.Text = this.smtpSetting.Userid;
                this.txtSmtpPassword.Text = this.smtpSetting.Password;
                this.txtSmtpEmailAddress.Text = this.smtpSetting.EmailAddress;
                this.chkSmtpSSL.Checked = this.smtpSetting.SSL;
            }
        }

        /// <summary>
        /// OKボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            try
            {                
                string server = this.txtSmtpServer.Text.Trim();                
                string userid = this.txtSmtpAccountName.Text.Trim();
                string password = this.txtSmtpPassword.Text.Trim();
                string email = this.txtSmtpEmailAddress.Text.Trim();
                string portStr = this.txtSmtpPort.Text.Trim();
                int port = 0;

                // 入力チェック
                if (string.IsNullOrEmpty(server) || string.IsNullOrEmpty(userid) || string.IsNullOrEmpty(password) 
                    || string.IsNullOrEmpty(email) || !int.TryParse(portStr , out port))
                {
                    MessageBox.Show("error");
                    return;
                }
                
                bool ssl = this.chkSmtpSSL.Checked;
                this.smtpSetting = SmtpMailProvider.GetSmtpMailProvider(server, port, userid, password, ssl, email);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
