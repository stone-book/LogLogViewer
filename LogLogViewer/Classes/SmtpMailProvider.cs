using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogLogViewer.Classes
{
    public class SmtpMailProvider
    {
        /// <summary>
        /// サーバアドレス
        /// </summary>
        public string Server { set; get; }

        /// <summary>
        /// ポート番号
        /// </summary>
        public int Port { set; get; }

        /// <summary>
        /// ユーザID
        /// </summary>
        public string Userid { set; get; }

        /// <summary>
        /// パスワード
        /// </summary>
        public string Password { set; get; }

        /// <summary>
        /// メールアドレス
        /// </summary>
        public string EmailAddress { set; get; }

        /// <summary>
        /// SSL
        /// </summary>
        public bool SSL { set; get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        private SmtpMailProvider()
        {

        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="server"></param>
        /// <param name="port"></param>
        /// <param name="userid"></param>
        /// <param name="password"></param>
        /// <param name="ssl"></param>
        private SmtpMailProvider(string server, int port, string userid, string password, bool ssl, string email)
        {
            this.Server = server;
            this.Port = port;
            this.Userid = userid;
            this.Password = password;
            this.SSL = ssl;
            this.EmailAddress = email;
        }

        /// <summary>
        /// 値のチェックをしてインスタンスを返す
        /// </summary>
        /// <param name="server"></param>
        /// <param name="port"></param>
        /// <param name="userid"></param>
        /// <param name="password"></param>
        /// <param name="ssl"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public static SmtpMailProvider GetSmtpMailProvider(string server, int port, string userid, string password, bool ssl, string email)
        {
            if (string.IsNullOrEmpty(server) || port == 0 || string.IsNullOrEmpty(userid) || string.IsNullOrEmpty(email))
                return null;
            return new SmtpMailProvider(server, port, userid, password, ssl, email);
        }

        /// <summary>
        /// インスタンスのコピー
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static SmtpMailProvider GetSmtpMailProvider(SmtpMailProvider provider)
        {
            if (provider == null)
                return null;

            return new SmtpMailProvider(provider.Server, provider.Port, provider.Userid, provider.Password, provider.SSL, provider.EmailAddress);
        }

        /// <summary>
        /// メールを送信する
        /// </summary>
        /// <param name="address"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="attach"></param>
        /// <param name="attachDeleteFlag">添付ファイルを削除する</param>
        /// <returns></returns>
        public void SendMail(string address, string subject, string body, string attach = null, bool attachDeleteFlag = false)
        {
            try
            {
                //メールの設定
                using (System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient())
                {
                    smtp.Host = this.Server;
                    smtp.Port = this.Port;

                    //認証
                    smtp.Credentials = new System.Net.NetworkCredential(this.Userid, this.Password);

                    //SSL
                    smtp.EnableSsl = this.SSL;

                    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage(this.EmailAddress, address, subject, body);

                    smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

                    // 添付ファイル
                    if (attach != null && attach != "" && File.Exists(attach))
                    {
                        //ファイル添付
                        System.Net.Mail.Attachment attach1 = new System.Net.Mail.Attachment(attach);

                        mail.Attachments.Add(attach1);

                        if (attachDeleteFlag)
                        {
                            // 転送完了時に添付ファイルを削除する
                            smtp.SendCompleted += delegate
                            {
                                File.Delete(attach);
                            };
                        }
                    }

                    //メール送信
                    smtp.Send(mail);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}

