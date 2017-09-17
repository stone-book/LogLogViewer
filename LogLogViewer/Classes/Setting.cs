using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogLogViewer.Classes.Patterns;
using System.Xml.Serialization;
using System.IO;
using System.ComponentModel;

namespace LogLogViewer.Classes
{
    /// <summary>
    /// 設定クラス
    /// </summary>
    public class Setting
    {
        /// <summary>
        /// 読み込みサイズ（デフォルト96K）
        /// </summary>
        public int MaxReadSize = 96000;

        /// <summary>
        /// 表示文字列（デフォルト10000文字）
        /// </summary>
        public int MaxCharacterLength = 10000;

        /// <summary>
        /// 表示行数（デフォルト100行）
        /// </summary>
        public int MaxLineLength = 100;

        /// <summary>
        /// 背景色
        /// </summary>
        [XmlIgnore]
        public Color BackColor = Color.Black;

        /// <summary>
        /// シリアライズ用のダミー
        /// </summary>
        [Browsable(false)]
        public string BackColorString
        {
            set { this.BackColor = ConvertFromString<Color>(value); }
            get { return ConvertToString(this.BackColor); }
        }

        /// <summary>
        /// 文字色
        /// </summary>
        [XmlIgnore]
        public Color FontColor = Color.GhostWhite;

        /// <summary>
        /// シリアライズ用のダミー
        /// </summary>
        [Browsable(false)]
        public string FontColorString
        {
            set { this.FontColor = ConvertFromString<Color>(value); }
            get { return ConvertToString(this.FontColor); }
        }

        /// <summary>
        /// フォント
        /// </summary>
        [XmlIgnore]
        public Font TextBoxFont = new Font("MS UI Gothic", 10, System.Drawing.FontStyle.Regular);

        /// <summary>
        /// シリアライズ用のダミー
        /// </summary>
        [Browsable(false)]
        public string TextBoxFontString
        {
            set { this.TextBoxFont = ConvertFromString<Font>(value); }
            get { return ConvertToString(this.TextBoxFont); }
        }

        /// <summary>
        /// 自動設定文字コード
        /// </summary>
        public Encoding AutoCharacterEncoding = null;

        /// <summary>
        /// 手動設定文字コード
        /// </summary>
        public Encoding ManualCharacterEncoding = null;

        /// <summary>
        /// 追記のみ表示モード
        /// </summary>
        public bool OnlyPostscriptMode = false;

        /// <summary>
        /// 履歴を残す
        /// </summary>
        public bool IsSaveHistory = true;

        /// <summary>
        /// ステータスバーを表示
        /// </summary>
        public bool IsShowStatusBar = true;

        /// <summary>
        /// URLリンクを表示
        /// </summary>
        public bool IsUrlLink = true;

        /// <summary>
        /// 色分け使用フラグ
        /// </summary>
        public bool IsUseColor = false;

        /// <summary>
        /// 色分け設定
        /// </summary>
        public List<ColorPattern> ColorPatterns = new List<ColorPattern>();

        /// <summary>
        /// フィルター使用フラグ
        /// </summary>
        public bool IsUseFilter = false;

        /// <summary>
        /// 色分け設定
        /// </summary>
        public List<FilterPattern> FilterPatterns = new List<FilterPattern>();

        /// <summary>
        /// アラート使用フラグ
        /// </summary>
        public bool IsUseAlertMail = false;

        /// <summary>
        /// アラート設定
        /// </summary>
        public List<MailPattern> MailPatterns = new List<MailPattern>();

        /// <summary>
        /// アラートメール用設定
        /// </summary>
        public SmtpMailProvider smtpSetting = null;

        /// <summary>
        ///　Font、Colorなどを文字型に変換 シリアライズ用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ConvertToString<T>(T value)
        {
            return TypeDescriptor.GetConverter(typeof(T)).ConvertToString(value);
        }

        /// <summary>
        /// 文字からFont、Colorなどに変換 シリアライズ用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ConvertFromString<T>(string value)
        {
            return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(value);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Setting()
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Setting(Setting setting)
        {
            // コピーを作成
            this.MaxReadSize = setting.MaxReadSize;
            this.MaxCharacterLength = setting.MaxCharacterLength;
            this.MaxLineLength = setting.MaxLineLength;
            this.FontColor = setting.FontColor;
            this.BackColor = setting.BackColor;
            this.TextBoxFont = setting.TextBoxFont;

            this.AutoCharacterEncoding = setting.AutoCharacterEncoding;
            this.ManualCharacterEncoding = setting.ManualCharacterEncoding;
            this.OnlyPostscriptMode = setting.OnlyPostscriptMode;

            this.IsSaveHistory = setting.IsSaveHistory;
            this.IsShowStatusBar = setting.IsShowStatusBar;

            // カラーパターン
            this.IsUseColor = setting.IsUseColor;
            this.ColorPatterns = new List<ColorPattern>(setting.ColorPatterns);

            // フィルタ
            this.IsUseFilter = setting.IsUseFilter;
            this.FilterPatterns = new List<FilterPattern>(setting.FilterPatterns);

            // アラートメール
            this.IsUseAlertMail = setting.IsUseAlertMail;
            this.MailPatterns = new List<MailPattern>(setting.MailPatterns);
            // SMTPの設定のコピー (NULLならNULLになる）
            this.smtpSetting = SmtpMailProvider.GetSmtpMailProvider(setting.smtpSetting);

        }

        /// <summary>
        /// 設定保存
        /// </summary>
        public void SaveSetting()
        {


            Properties.Settings.Default.MaxReadSize = this.MaxReadSize;
            Properties.Settings.Default.MaxCharacterLength = this.MaxCharacterLength;
            Properties.Settings.Default.MaxLineLength = this.MaxLineLength;
            Properties.Settings.Default.FontColor = this.FontColor;
            Properties.Settings.Default.BackColor = this.BackColor;
            Properties.Settings.Default.TextBoxFont = this.TextBoxFont;

            int codepage = this.ManualCharacterEncoding != null ? this.ManualCharacterEncoding.CodePage : 0;
            Properties.Settings.Default.ManualCharacterEncoding = codepage;
            Properties.Settings.Default.OnlyPostscriptMode = this.OnlyPostscriptMode;

            Properties.Settings.Default.SaveHistory = this.IsSaveHistory;
            Properties.Settings.Default.ShowStatusBar = this.IsShowStatusBar;

            // 正規表現色塗り
            Properties.Settings.Default.IsUseColorPattern = this.IsUseColor;

            // シリアライズして保存
            string colorsxml = SerializeList<List<ColorPattern>>(this.ColorPatterns);
            Properties.Settings.Default.ColorPatterns = colorsxml;
            
            // 正規表現フィルター
            Properties.Settings.Default.IsUseFilterPattern = this.IsUseFilter;
            
            // シリアライズして保存
            string filterxml = SerializeList<List<FilterPattern>>(this.FilterPatterns);
            Properties.Settings.Default.FilterPatterns = filterxml;

            // 正規表現アラートメール
            Properties.Settings.Default.IsUseAlertMail = this.IsUseAlertMail;

            // シリアライズして保存
            string alertmailxml = SerializeList< List<MailPattern>>(this.MailPatterns);
            Properties.Settings.Default.MailPatterns = alertmailxml;

            // メールサーバ設定の保存
            if (this.smtpSetting != null)
            {
                Properties.Settings.Default.MailServer = this.smtpSetting.Server;
                Properties.Settings.Default.MailPort = this.smtpSetting.Port;
                Properties.Settings.Default.MailServerSSL = this.smtpSetting.SSL;
                Properties.Settings.Default.MailUserID = this.smtpSetting.Userid;
                Properties.Settings.Default.MailPassword = this.smtpSetting.Password;
                Properties.Settings.Default.EmailAddress = this.smtpSetting.EmailAddress;
            }

            Properties.Settings.Default.Save();
        }


        /// <summary>
        /// 設定を読み込む
        /// </summary>
        /// <returns></returns>
        public static Setting LoadSetting()
        {
          
            Setting setting = new Setting();

            try
            {
                setting.MaxReadSize = Properties.Settings.Default.MaxReadSize;
                setting.MaxCharacterLength = Properties.Settings.Default.MaxCharacterLength;
                setting.MaxLineLength = Properties.Settings.Default.MaxLineLength;

                setting.FontColor = Properties.Settings.Default.FontColor;
                setting.BackColor = Properties.Settings.Default.BackColor;
                setting.TextBoxFont = Properties.Settings.Default.TextBoxFont;

                int codepage = Properties.Settings.Default.ManualCharacterEncoding;
                setting.ManualCharacterEncoding = codepage != 0 ? Encoding.GetEncoding(codepage) : null;
                setting.OnlyPostscriptMode = Properties.Settings.Default.OnlyPostscriptMode;

                setting.IsSaveHistory = Properties.Settings.Default.SaveHistory;
                setting.IsShowStatusBar = Properties.Settings.Default.ShowStatusBar;

                // 正規表現色塗り
                setting.IsUseColor = Properties.Settings.Default.IsUseColorPattern;
                string colorxml = Properties.Settings.Default.ColorPatterns;
                setting.ColorPatterns = DeserializeList<List<ColorPattern>>(colorxml);

                // 正規表現フィルター
                setting.IsUseFilter = Properties.Settings.Default.IsUseFilterPattern;
                string filterxml = Properties.Settings.Default.FilterPatterns;
                setting.FilterPatterns = DeserializeList<List<FilterPattern>>(filterxml);

                // 正規表現アラートメール
                setting.IsUseAlertMail = Properties.Settings.Default.IsUseAlertMail;
                string mailxml = Properties.Settings.Default.MailPatterns;
                setting.MailPatterns = DeserializeList<List<MailPattern>>(mailxml);

                // メールサーバ設定の読込
                string server = Properties.Settings.Default.MailServer;
                int port = Properties.Settings.Default.MailPort;
                bool ssl = Properties.Settings.Default.MailServerSSL;
                string userid = Properties.Settings.Default.MailUserID;
                string password = Properties.Settings.Default.MailPassword;
                string email = Properties.Settings.Default.EmailAddress;
                setting.smtpSetting = SmtpMailProvider.GetSmtpMailProvider(server, port, userid, password, ssl, email);

                return setting;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return setting;
            }

        }

        /// <summary>
        /// シリアライズでオブジェクトからXMLにする
        /// フィルタなどの可変長さの設定保存用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        private static string SerializeList<T>(T list)
        {
            using (StringWriter sw = new StringWriter())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(sw, list);
                return sw.ToString();
            }
        }

        /// <summary>
        /// デシリアライズでオブジェクトに戻す
        /// フィルタなどの可変長さの設定保存用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        private static T DeserializeList<T>(string xml)
        {
            using (StringReader sr = new StringReader(xml))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                T list = (T)serializer.Deserialize(sr);
                return list;
            }
        }

        /// <summary>
        /// 設定ファイルを読込インスタンスを返却
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static Setting LoadXmlFile(string file) 
        {
            Setting setting = null;
            try
            {
                var serializer = new XmlSerializer(typeof(Setting));

                using (var sr = new StreamReader(file, new UTF8Encoding(false)))
                {
                    //XMLファイルから読み込み、逆シリアル化
                    setting = (Setting)serializer.Deserialize(sr);
                    sr.Close();
                }
            }
            catch
            {
                throw;
            }
            return setting;
        }

        /// <summary>
        /// 設定ファイルを書き込む
        /// </summary>
        /// <param name="file"></param>
        public static void SaveXmlFile(Setting setting, string file)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(Setting));
                using (var sw = new StreamWriter(file, false, new UTF8Encoding(false)))
                {
                    //シリアル化し、XMLファイルに保存
                    serializer.Serialize(sw, setting);
                    sw.Close();
                }
            }
            catch
            {
                throw;
            }
        }


    }
}
