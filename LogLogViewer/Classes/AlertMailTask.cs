using LogLogViewer.Classes.Patterns;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace LogLogViewer.Classes
{
    /// <summary>
    /// アラートメールを処理するタスク
    /// </summary>
    public class AlertMailTask
    {
        /// <summary>
        /// スレッド停止時間
        /// </summary>
        private const int sleepTime = 100;

        /// <summary>
        /// タスク
        /// </summary>
        private Task task;

        /// <summary>
        /// 読込フラグ
        /// </summary>
        public bool IsReading { get; private set; } = false;

        /// <summary>
        /// メール送信数
        /// </summary>
        private int sendMailCount = 0;

        /// <summary>
        /// メール送信条件
        /// </summary>
        private List<MailPattern> mailPatterns;

        /// <summary>
        /// メールの設定
        /// </summary>
        private SmtpMailProvider smtp;

        /// <summary>
        /// Threadの停止 メールの順次連続送信を実現
        /// </summary>
        private ManualResetEvent manualResetEvent = new ManualResetEvent(false);

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="mailPattern"></param>
        /// <param name="smtp"></param>
        public AlertMailTask(List<MailPattern> mailPattern, SmtpMailProvider smtp)
        {
            this.mailPatterns = mailPattern;
            this.smtp = smtp;
        }

        /// <summary>
        /// メール設定
        /// </summary>
        /// <param name="mailPatterns"></param>
        public void SetMailPatterns(List<MailPattern> mailPatterns)
        {
            this.mailPatterns = mailPatterns;
        }

        /// <summary>
        /// スレッド起動
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="startAddress"></param>
        /// <param name="encoding"></param>
        /// <param name="callbackAction"></param>
        public void Start(string filePath, long startAddress, Encoding encoding, Action<string> callbackAction)
        {

            if (IsReading)
                return;

            this.manualResetEvent.Set();
            this.IsReading = true;

            this.task = new TaskFactory().StartNew(() =>
            {
                try
                {
                    // 読み取り開始アドレス
                    long beforeFileSize = startAddress;

                    Debug.WriteLine("** AlertMailThread start");

                    while (IsReading)
                    {
                        using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        using (StreamReader sr = new StreamReader(fs, encoding))
                        {
                            // ファイルサイズ取得                    
                            long nowFileSize = fs.Length;

                            // ファイルに変化があったか
                            if (IsReading && beforeFileSize < nowFileSize)
                            {
                                // 以前読み込んだところまでシーク
                                fs.Seek(beforeFileSize, SeekOrigin.Begin);

                                Debug.WriteLine("** AlertMailThread Reading");

                                while (sr.Peek() > -1)
                                {
                                    // 1行読み取り
                                    string line = sr.ReadLine();
                                    //Debug.WriteLine("** AlertMailThread " + line);
                                    if (this.filetAlertMailRegularExp(line))
                                    {
                                        sendMailCount++;

                                        // 処理メッセージを作成し、コールバック
                                        string infomsg = string.Format(Properties.Resources.noticeAlertMailSent, DateTime.Now.ToString(), sendMailCount);
                                        callbackAction(infomsg);
                                    }
                                }
                                // ファイルサイズ保持
                                beforeFileSize = nowFileSize;
                            }
                        }
                        Thread.Sleep(sleepTime);
                    }
                    Debug.WriteLine("** AlertMailThread end");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return;
                }
            });

        }

        /// <summary>
        /// 正規表現で適応した場合、メールを飛ばす
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>
        private bool filetAlertMailRegularExp(string word)
        {
            // 有効フラグがついているもののカウント
            int c = this.mailPatterns.Count<MailPattern>(p => p.Enable == true);

            // フィルタがなし、もしくは有効のものがない場合は戻る
            if (this.mailPatterns.Count == 0 || c == 0 || this.smtp == null)
                return false;

            foreach (MailPattern ptn in this.mailPatterns)
            {
                if (ptn.Enable && Regex.IsMatch(word, ptn.RegularExp))
                {
                    // もしメールを送っていたら待つ
                    this.manualResetEvent.WaitOne();

                    this.sendAlertMail(word, ptn);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// メール送信非同期メソッド
        /// </summary>
        /// <param name="log"></param>
        /// <param name="ptn"></param>
        private async void sendAlertMail(string log, MailPattern ptn)
        {
            await Task.Run(() =>
            {
                try
                {
                    Debug.WriteLine("*** SendMailTask start");
                    // 非シグナル状態にする
                    this.manualResetEvent.Reset();

                    string message = ptn.Message.Replace("[LOG_MESSAGE]", log);
                    this.smtp.SendMail(ptn.ToAddress, ptn.Subject, message);

                    // メール送信したのでシグナル状態へ
                    this.manualResetEvent.Set();
                    Debug.WriteLine("*** SendMailTask end");
                }
                catch (Exception ex)
                {
                    // メール失敗してもシグナルを戻す
                    this.manualResetEvent.Set();
                    Console.WriteLine(ex.ToString());
                }
            });
        }

        /// <summary>
        /// スレッド停止
        /// </summary>
        public void Stop()
        {
            try
            {
                this.IsReading = false;
                this.task.Wait();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
            
    }
}
