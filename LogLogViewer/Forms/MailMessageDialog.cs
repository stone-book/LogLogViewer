using LogLogViewer.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogLogViewer.Forms
{
    public partial class MailMessageDialog : Form
    {
        private SmtpMailProvider smtp = null;

        public string ToAddress { private set; get; }

        public string Subject { private set; get; }

        public string Message { private set; get; }

        public MailMessageDialog(SmtpMailProvider smtp, string toAddress, string subject , string message)
        {
            InitializeComponent();

            this.smtp = smtp;

            this.textBoxToAddress.Text = toAddress;

            this.textBoxSubject.Text = subject;

            this.textBoxMessage.Text = message;
            
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (checkMessage() == false)
                return;

            this.ToAddress = this.textBoxToAddress.Text;
            this.Subject = this.textBoxSubject.Text;
            this.Message = this.textBoxMessage.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// メール送信テスト
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonTestMail_Click(object sender, EventArgs e)
        {
            try
            {
                if(checkMessage() == false)
                    return;

                if (this.smtp == null)
                {
                    MessageBox.Show(Properties.Resources.MailDlgSmtpError);
                    return;
                }

                string toaddress = this.textBoxToAddress.Text;
                string subject = this.textBoxSubject.Text;
                string message = this.textBoxMessage.Text;

                this.smtp.SendMail(toaddress, subject, message);

                MessageBox.Show(Properties.Resources.MailDlgTestMailSend);
            }
            catch(Exception ex)
            {
                MessageBox.Show(Properties.Resources.MailDlgTestMailError);
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 入力値チェック
        /// </summary>
        /// <returns></returns>
        private bool checkMessage()
        {
            this.textBoxToAddress.Text = this.textBoxToAddress.Text.Trim();
            this.textBoxSubject.Text = this.textBoxSubject.Text.Trim();
            this.textBoxMessage.Text = this.textBoxMessage.Text.Trim();

            string toaddress = this.textBoxToAddress.Text;
            string subject = this.textBoxSubject.Text;
            string message = this.textBoxMessage.Text;

            if(string.IsNullOrEmpty(toaddress) || string.IsNullOrEmpty(subject) || string.IsNullOrEmpty(message))
            {
                MessageBox.Show(Properties.Resources.MailDlgBlankError);
                return false;
            }

            return true;
        }
    }
}
