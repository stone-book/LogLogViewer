using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using LogLogViewer.Classes;
using LogLogViewer.Classes.Patterns;
using System.Diagnostics;

namespace LogLogViewer.Forms
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// 読み込むログファイル
        /// </summary>
        private string file = "";

        /// <summary>
        /// 読み込みフラグ
        /// </summary>
        private bool isReading = false;

        /// <summary>
        /// 中断フラグ
        /// </summary>
        private bool isPause = false;

        /// <summary>
        /// 設定クラス
        /// </summary>
        private Setting setting = null;

        /// <summary>
        /// 検索ダイアログ
        /// </summary>
        private FindDialog findDialog = null;

        /// <summary>
        /// 検索文字列
        /// </summary>
        public string Findword = null;

        /// <summary>
        /// ヒストリー制御
        /// </summary>
        private HistoryControl historyCntrol = null;


        private Task task = null;


        private AlertMailTask alertMailTask = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            this.textBox1.AllowDrop = true;
            this.textBox1.DragDrop += this.MainForm_DragDrop;
            this.textBox1.DragEnter += this.MainForm_DragEnter;

            // フォントの自動変更を防止する
            this.textBox1.LanguageOption = RichTextBoxLanguageOptions.UIFonts;

            // 履歴管理クラス
            this.historyCntrol = new HistoryControl(this);

            //  設定ファイルの読み込み
            this.loadSetting(Setting.LoadSetting());

            // 初期化処理
            this.initControl();
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="file">読み込むファイルパス</param>
        public MainForm(string file) : this()
        {
            this.file = file;
        }

        /// <summary>
        /// フォームロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                // テキストボックスを初期選択させない
                this.textBox1.SelectionStart = 0;

                // 起動時にパスをもらっている場合
                if (this.file != null && this.file != "")
                {
                    // ログを読み込む
                    this.readLogFile(file);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// メニューの開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                openFileDialog.Filter = "Text File (*.txt;*.log)|*.txt; *.log|All File (*.*)|*.*";
                if (openFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    string file = openFileDialog.FileName;

                    // ログを読み込む
                    this.readLogFile(file);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// 中断／再開ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.isPause)
                {
                    // 中断ボタンにする
                    this.changeReloadMenuIcon(false);
                    // 中断フラグをオン
                    this.isPause = false;
                }
                else
                {
                    // 再開ボタンにする
                    this.changeReloadMenuIcon(true);
                    // 中断フラグをオフ
                    this.isPause = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 中断／再開ボタンのアイコンの切り替え
        /// </summary>
        /// <param name="flg"></param>
        private void changeReloadMenuIcon(bool flg)
        {
            if (flg)
            {
                // 再開ボタンにする
                this.reloadRToolStripMenuItem.Text = Properties.Resources.reloadToolStripMenuItemResume;
                this.reloadRToolStripMenuItem.Image = Properties.Resources.control;
            }
            else
            {
                // 中断ボタンにする
                this.reloadRToolStripMenuItem.Text = Properties.Resources.reloadToolStripMenuItemPause;
                this.reloadRToolStripMenuItem.Image = Properties.Resources.control_pause;
            }
        }

        /// <summary>
        /// ログファイルを開く
        /// </summary>
        /// <param name="file"></param>
        private void readLogFile(string file)
        {
            // コントロール初期化
            this.initControl();

            // ファイルチェック
            if (File.Exists(file))
            {
                // カウント数
                long size = new FileInfo(file).Length;

                if (size < 0)
                {
                    MessageBox.Show(Properties.Resources.ErrorMessageFileReadError, Properties.Resources.ErrorMessageBoxTitle,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ファイル履歴処理
                if (this.setting.IsSaveHistory)
                {
                    this.historyCntrol.IntoHistory(file);
                    this.initHistoryMenu();
                }

                // アラートメール有効の場合警告
                if (this.setting.IsUseAlertMail)
                {
                    if (MessageBox.Show(Properties.Resources.noticeAlertMailStart, Properties.Resources.QuestionMessageBoxTitle,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }
                }

                // エンコーディング判定
                this.setting.AutoCharacterEncoding = ReadTextBinary.GetEncoding(file);
                this.setEncodingStatus();

                this.file = file;
                this.textBox1.Text = "";

                // 読み込んだファイル名をタイトルに
                this.Text = Path.GetFileName(file) + " - LogLogViewer";

                // 中断ボタンにし、有効にする
                this.isPause = false;
                this.changeReloadMenuIcon(false);
                this.reloadRToolStripMenuItem.Enabled = true;

                // 監視タスクの起動
                this.threadTask();
            }
            else
            {
                MessageBox.Show(Properties.Resources.ErrorMessageFileNotFound, Properties.Resources.ErrorMessageBoxTitle,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        /// <summary>
        /// コントロールを初期化する
        /// </summary>
        private void initControl()
        {
            // プログラム名の変更
            this.Text = "LogLogViewer";

            // 監視タスクの終了フラグの設定
            this.isReading = false;
            this.isPause = false;
            
            // タスクの終了まち
            if (this.task != null)
            {
                this.task.Wait();
                this.task = null;
            }

            // 読み込みファイルの修正
            this.file = "";

            // 中断・再開ボタンの無効
            this.changeReloadMenuIcon(true);
            this.reloadRToolStripMenuItem.Enabled = false;

            // エンコーディングステータスを空に
            this.encodingStatusLabel.Text = "";
        }



        /// <summary>
        /// ファイルをドロップ中の場合のイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    // ドラッグ中のファイルやディレクトリの取得
                    string[] drags = (string[])e.Data.GetData(DataFormats.FileDrop);

                    foreach (string d in drags)
                    {
                        if (!System.IO.File.Exists(d))
                        {
                            // ファイル以外であればイベント・ハンドラを抜ける
                            return;
                        }
                    }

                    e.Effect = DragDropEffects.Copy;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ドロップ後
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                // ドラッグ＆ドロップされたファイル
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                // グリッドを読み込む
                this.readLogFile(files[0]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// あふれた行数分削除する
        /// </summary>
        /// <param name="len"></param>
        private void deleteLines(RichTextBox textBox , int len)
        {
            //削除すべき行数
            int delLines = textBox.Lines.Length + len - this.setting.MaxLineLength;

            if (textBox.Lines.Length > 0 && delLines > 0)
            {
                int delCharLength = 0;
                for (int i = 0; i < delLines && i < textBox.Lines.Length; i++)
                    delCharLength += textBox.Lines[i].Length + 1;

                textBox.Select(0, delCharLength);
                textBox.SelectedText = string.Empty;
            }
        }

        delegate void appendTextDelegate(List<TextBoxItems> list, bool delflag);

        /// <summary>
        /// テキストボックスに表示文字を追記
        /// </summary>
        /// <param name="list"></param>
        /// <param name=""></param>
        /// <param name="delflag"></param>
        private void appendText(List<TextBoxItems>list, bool delflag)
        {
            //this.manualResetEvent.Reset();
            RichTextBox workRtb = new RichTextBox();
            workRtb.BackColor = this.textBox1.BackColor;
            workRtb.ForeColor = this.textBox1.ForeColor;
            workRtb.Font = this.textBox1.Font;
            workRtb.Rtf = this.textBox1.Rtf;
               
            if (delflag)
                workRtb.Text = "";
            else
                deleteLines(workRtb, list.Count);

            // これがないと i=0 の色がつかない
            workRtb.SelectionStart = workRtb.Text.Length;

            for (int i = 0; i < list.Count; i++)
            {
                
                if (list[i].HasColor)
                {
                    workRtb.SelectionColor = list[i].TextColor;
                    //Debug.Print("Index {0} : {1} : {2}", i, list[i].Text.TrimEnd(), workRtb.SelectionColor.ToString());
                }
                workRtb.AppendText(list[i].Text);

            }

            this.textBox1.Rtf = workRtb.Rtf;

            ///Application.DoEvents();
            // スクロール移動
            this.textBox1.Focus();
            this.textBox1.SelectionStart = this.textBox1.Text.Length;
            
            //なんかずれる
            //this.textBox1.ScrollToCaret();

            //this.textBox1.HideSelection = false;
            //this.textBox1.AppendText("");
            //this.manualResetEvent.Set();
        }

        /// <summary>
        /// 読み込んだ文字列を解析しテキストボックスに表示するアイテムリストにする
        /// </summary>
        /// <param name="readStr"></param>
        /// <returns></returns>
        private List<TextBoxItems> createTextBoxItems(string readStr)
        {
            List<TextBoxItems> itemlist = new List<TextBoxItems>();

            // 番兵を足す
            readStr = "\n" + readStr;
            
            // 検索カーソル
            int searchCursor = readStr.Length - 1;

            while (searchCursor > 0 && itemlist.Count < this.setting.MaxLineLength)
            {
                int foundIndex = readStr.LastIndexOf('\n', searchCursor - 1);

                int subStringLen = searchCursor - foundIndex;

                string readline = readStr.Substring(foundIndex + 1, subStringLen);

                // 改行を吸収
                //readline = readline.Replace("\r\n", "\n");

                searchCursor = foundIndex;

                // 正規表現のフィルタを使うか
                if (this.setting.IsUseFilter)
                {
                    // フィルタの判定
                    if (filterRegularExp(readline) == false)
                        continue;
                }

                // 表示用アイテム
                TextBoxItems item = new TextBoxItems();
                item.Text = readline;

                // 正規表現による色設定
                if (this.setting.IsUseColor)
                    this.setTextColor(item);

                // 表示リストに追加
                itemlist.Add(item);
            }

            return itemlist;
        }



        /// <summary>
        /// 監視タスクの起動
        /// </summary>
        private void threadTask()
        {

            this.isReading = true;

            TaskFactory taskFactory = new TaskFactory();
            this.task = taskFactory.StartNew(() =>
            {
                // タスクの開始
                Debug.Print("* main task start.");

                // 初回フラグ
                bool firstRead = true;

                long beforeFileSize = 0;

                while (true)
                {
                    try
                    {
                        if (isPause == false)
                        {
                            // ファイルサイズ取得
                            long nowFileSize = new FileInfo(this.file).Length;

                            if (isReading && beforeFileSize < nowFileSize)
                            {
                                long readStartAdress = 0;
                                bool isContinue = false;

                                // 読み込める行が最大Byteを超えていないかチェック
                                // もしくはアラートメール有効時は全部もれなく読む（初回は除く）
                                if (nowFileSize - beforeFileSize < this.setting.MaxReadSize)
                                {
                                    // 連続性を保証
                                    isContinue = true;
                                    readStartAdress = beforeFileSize;
                                }
                                else
                                {
                                    readStartAdress = nowFileSize - this.setting.MaxReadSize;
                                }

                                // TODO デバッグ
                                Debug.Print("* readfile continue: {0}", isContinue.ToString());

                                // 所定のアドレスからファイルを読み込む（この瞬間もファイルサイズが変更している可能性もある）
                                string readStr = ReadTextBinary.Read(this.file, readStartAdress, this.getPriorityEncoding(), out beforeFileSize);

                                // ただし逆順で作られる
                                List<TextBoxItems> itemlist = this.createTextBoxItems(readStr);

                                // フィルタで空の場合
                                if (itemlist.Count == 0)
                                    continue;

                                // 連続で読み込まない場合の注意
                                if (isContinue == false)
                                {
                                    // 表示用アイテム
                                    TextBoxItems item = new TextBoxItems();
                                    item.Text = string.Format("***** LogLogViewer read from {0:#,0} Bytes. *****" + System.Environment.NewLine,
                                        readStartAdress);
                                    itemlist.Add(item);
                                }

                                // 反転
                                itemlist.Reverse();

                                // テキストボックスの内容の全削除フラグ
                                bool allDelFlag = false;

                                // 表示がこえているか、それとも差分表示モードか
                                if (itemlist.Count >= this.setting.MaxLineLength || this.setting.OnlyPostscriptMode)
                                {
                                    allDelFlag = true;
                                }

                                // 文字を表示
                                this.textBox1.Invoke(new appendTextDelegate(appendText), new object[] { itemlist, allDelFlag });

                                // アラートメールが有効なら開始（初回だけ読込後
                                if (firstRead && this.setting.IsUseAlertMail)
                                    this.StartAlertMailTask(beforeFileSize);

                            }
                            else if (!isReading || this.IsDisposed)
                            {
                                break;
                            }
                        }
                        else if (!isReading)
                        {
                            break;
                        }

                        // アラートメールの判定 有効ならスレッド開始、無効なら停止
                        if (firstRead == false && this.setting.IsUseAlertMail == true)
                            this.StartAlertMailTask(beforeFileSize);
                        else if (this.setting.IsUseAlertMail == false)
                            this.StopAlertMailTask();

                        // 監視処理のメインループを待機
                        Thread.Sleep(100);

                        // 初回読込フラグを下げる
                        firstRead = false;
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("* ERROR:" + ex.ToString());
                        MessageBox.Show(ex.Message);
                        isReading = false;
                        break;
                    }
                }
                // メール監視を停止
                this.StopAlertMailTask();

                // タスクの終了
                Debug.Print("* main task end.");
            });

        }

        /// <summary>
        /// 正規表現で適応するものは色を返す
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private TextBoxItems setTextColor(TextBoxItems item)
        {
            // 有効フラグがついているもののカウント
            int c = this.setting.ColorPatterns.Count<ColorPattern>(p => p.Enable == true);

            // フィルタがなし、もしくは有効のものがない場合は戻る
            if (this.setting.ColorPatterns.Count == 0 || c == 0)
                return item;
            
            foreach (ColorPattern ptn in this.setting.ColorPatterns)
            {
                if (ptn.Enable && Regex.IsMatch(item.Text, ptn.RegularExp))
                {
                    item.TextColor = ptn.TextColor;
                    item.HasColor = true;
                    return item;
                }
            }
            return item;
        }
        

        /// <summary>
        /// 正規表現で適応しないものは削る
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>
        private bool filterRegularExp(string word)
        {
            // 有効フラグがついているもののカウント
            int c = this.setting.FilterPatterns.Count<FilterPattern>(p => p.Enable == true);

            // フィルタがなし、もしくは有効のものがない場合は戻る
            if (this.setting.FilterPatterns.Count == 0 || c == 0)
                return true;

            // フィルタのチェック
            foreach (FilterPattern ptn in this.setting.FilterPatterns)
            {
                if (ptn.Enable && Regex.IsMatch(word, ptn.RegularExp))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// アラートメールのスレッドを実行する
        /// </summary>
        /// <param name="starAddress"></param>
        private void StartAlertMailTask(long starAddress)
        {
            if (this.alertMailTask == null || (this.alertMailTask != null && this.alertMailTask.IsReading == false))
            {
                // TODO:Threadごとにローカル設定が必要があるため、"en"が効かない
                // 表示メッセージ
                this.toolStripStatusLabel.Text = Properties.Resources.noticeAlertMailTaskStart;

                // ステータスバーへのデリゲート
                Action<string> act = new Action<string>((msg) => { this.toolStripStatusLabel.Text = msg; });

                this.alertMailTask = new AlertMailTask(this.setting.MailPatterns, this.setting.smtpSetting);
                this.alertMailTask.Start(this.file, starAddress, this.getPriorityEncoding(), act);
            }
        }

        /// <summary>
        /// アラートメールのスレッドを閉じる
        /// </summary>
        private void StopAlertMailTask()
        {
            // アラートメールを修了する
            if (this.alertMailTask != null)
            {
                this.alertMailTask.Stop();
                // 表示メッセージ
                this.toolStripStatusLabel.Text = Properties.Resources.noticeAlertMailTaskEnd;
            }

            this.alertMailTask = null;
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // 監視タスクの終了フラグの設定
                this.isReading = false;

                // タスクの終了まち
                if (this.task != null)
                {
                    this.task.Wait();
                    this.task = null;
                }

                // 設定の保存
                this.setting.SaveSetting();


                this.historyCntrol.SaveHistory(this.setting.IsSaveHistory);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// アバウトボックス表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void helpHToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox ab = new AboutBox();
            ab.ShowDialog();
        }

        /// <summary>
        /// 設定ダイアログ表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void settingSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SettingDialog sd = new SettingDialog(this.setting);
                if (sd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.loadSetting(sd.setting);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 設定読み込み
        /// </summary>
        /// <param name="setting"></param>
        private void loadSetting(Setting setting)
        {
            // 文字コード設定が変更されたか
            bool encodingChange = this.setting != null && this.setting.ManualCharacterEncoding != setting.ManualCharacterEncoding ? true : false;

            // 設定コピー
            this.setting = setting;

            // ステータス反映
            this.setEncodingStatus();

            // 画面反映
            if (this.textBox1.ForeColor != this.setting.FontColor)
                this.textBox1.ForeColor = this.setting.FontColor;
            if (this.textBox1.BackColor != this.setting.BackColor)
                this.textBox1.BackColor = this.setting.BackColor;
            if (this.textBox1.Font != this.setting.TextBoxFont)
                this.textBox1.Font = this.setting.TextBoxFont;

            this.textBox1.DetectUrls = this.setting.IsUrlLink;

            // フィルタなどの有効状態を表示
            this.filterStatusLabel.Visible = this.setting.IsUseFilter;
            this.alerttMailStatusLabel.Visible = this.setting.IsUseAlertMail;

            // メール通知条件を監視タスクに渡す
            if (this.setting.IsUseAlertMail && this.alertMailTask != null)
                this.alertMailTask.SetMailPatterns(this.setting.MailPatterns);

            // 再読み込みするか？
            if (!string.IsNullOrEmpty(this.file) && encodingChange)
            {
                DialogResult result = MessageBox.Show(
                    Properties.Resources.InfoMessageChangeEncoding,
                    Properties.Resources.QuestionMessageBoxTitle,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                    );

                if (result == DialogResult.Yes)
                {
                    // グリッドを読み込む
                    this.readLogFile(this.file);
                }
            }

            // ファイル履歴メニューを初期化
            this.initHistoryMenu();

            // ステータスバーのON/OFF
            this.statusStrip1.Visible = this.setting.IsShowStatusBar;
        }

        /// <summary>
        /// ファイル履歴メニュー初期化
        /// </summary>
        private void initHistoryMenu()
        {
            // ファイル履歴メニュー初期化
            this.toolStripMenuItem1.DropDownItems.Clear();
            this.toolStripMenuItem1.Enabled = false;

            if (this.setting.IsSaveHistory)
            {

                ToolStripMenuItem[] items = this.historyCntrol.GetHistoryMenu();
                if (items != null && items.Length > 0)
                {
                    this.toolStripMenuItem1.DropDownItems.AddRange(items);
                    this.toolStripMenuItem1.Enabled = true;
                }
            }
        }

        /// <summary>
        /// ファイル履歴メニューを開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void HistoryFile_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripMenuItem item = (ToolStripMenuItem)sender;
                // ファイルを読み込む
                this.readLogFile(item.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 終了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.isReading = false;
            this.Close();
        }

        /// <summary>
        /// ステータスラベルにエンコード情報を表示
        /// </summary>
        private void setEncodingStatus()
        {
            if (this.getPriorityEncoding() != null)
            {
                this.encodingStatusLabel.Text = this.getPriorityEncoding().WebName;

                if (this.setting.ManualCharacterEncoding == null)
                    this.encodingStatusLabel.Text += " " + Properties.Resources.StatusMessageAutoEncoding;
            }
            else
                this.encodingStatusLabel.Text = "";
        }

        /// <summary>
        /// 優先されるエンコードを取得
        /// </summary>
        /// <returns></returns>
        private Encoding getPriorityEncoding()
        {
            if (this.setting.ManualCharacterEncoding != null)
                return this.setting.ManualCharacterEncoding;
            return this.setting.AutoCharacterEncoding;
        }

        #region 文字検索
        /// <summary>
        /// 検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.findDialog != null && this.findDialog.IsDisposed == false)
                {
                    this.findDialog.Activate();
                }
                else
                {
                    this.findDialog = new FindDialog(this);
                    this.findDialog.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 次を検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void findNextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.Findword))
                {
                    // 空だったら通常の検索を表示
                    this.findToolStripMenuItem_Click(null, null);
                    return;
                }
                else
                {
                    // カレット位置を取得（次のため+1)
                    int caretIndex = this.textBox1.SelectionStart + 1;
                    this.FindTextBox(caretIndex, false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 前に検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void findPreviousToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.Findword))
                {
                    // 空だったら通常の検索を表示
                    this.findToolStripMenuItem_Click(null, null);
                    return;
                }
                else
                {
                    // カレット位置を取得（戻るため-1をしている）
                    int caretIndex = this.textBox1.SelectionStart - 1;
                    this.FindTextBox(caretIndex, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 検索メソッド
        /// </summary>
        public void FindTextBox(int startIndex = 0, bool previous = false)
        {
            // 検索
            int findIndex = -1;

            if (previous)
                findIndex = this.textBox1.Text.LastIndexOf(this.Findword, startIndex);
            else
                findIndex = this.textBox1.Text.IndexOf(this.Findword, startIndex);

            if (findIndex < 0)
            {
                // 文字が見つからなかった
                MessageBox.Show(Properties.Resources.InfoMessageFindNotFound); return;
            }

            // 文字を選択にし、スクロール
            this.textBox1.Select(findIndex, this.Findword.Length);
            this.textBox1.ScrollToCaret();
            this.textBox1.Focus();
        }
        #endregion

        #region コンテキストメニュー
        /// <summary>
        /// コンテキストメニュー コピー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectedText != "")
                textBox1.Copy();
        }

        /// <summary>
        /// コンテキストメニュー 貼り付け
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text))
                textBox1.Paste();
        }

        /// <summary>
        /// コンテキストメニュー 切り取り
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectedText != "")
                textBox1.Cut();
        }
        
        /// <summary>
        /// コンテキストメニュー すべて選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.textBox1.SelectAll();
        }
        #endregion
    }
}
