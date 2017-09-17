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
using LogLogViewer.Classes.Patterns;
using LogLogViewer.Classes.DataTables;

namespace LogLogViewer.Forms
{
    public partial class SettingDialog : Form
    {
        public Setting setting { private set; get; } = null;

        // 配列で対応付け
        private RadioButton[] encodingRadios = null;

        private int[] encodingCodePage = null;

        /// <summary>
        /// ColorパターンのGridデータ
        /// </summary>
        private ColorPatternDataTable colorPtnDt = null;

        /// <summary>
        /// フィルタパターンのGridデータ
        /// </summary>
        private FilterPatternTable filterDt = null;

        /// <summary>
        /// アラートメールパターンのGridデータ
        /// </summary>
        private MailPatternTable mailDt = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="setting"></param>
        public SettingDialog(Setting setting)
        {
            InitializeComponent();
            
            this.encodingRadios = new RadioButton[]
            {
                this.radioButtonEucJP,
                this.radioButtonJIS,
                this.radioButtonShiftJIS,
                this.radioButtonUTF16BE,
                this.radioButtonUTF16LE,
                this.radioButtonUTF8
            };
            this.encodingCodePage = new int[]
            {
                (int)Const.EncodingCodePage.EucJP,
                (int)Const.EncodingCodePage.Iso2022Jp,
                (int)Const.EncodingCodePage.ShiftJIS,
                (int)Const.EncodingCodePage.UTF16BE,
                (int)Const.EncodingCodePage.UTF16LE,
                (int)Const.EncodingCodePage.UTF8,
            };

            // コントロールに合わせて初期化
            this.initControl(setting);
        }

        /// <summary>
        /// コントロールを設定に合わせて初期化
        /// </summary>
        /// <param name="setting"></param>
        private void initControl(Setting setting)
        {
            // 設定を格納
            this.setting = new Setting(setting);

            // 読み込みサイズ（1000倍）
            this.maxReadSize.Value = this.setting.MaxReadSize / 1000;
            // 表示文字数
            this.maxLineLength.Value = this.setting.MaxLineLength;

            // 色の初期表示
            this.drawPicColor(this.fontColorPic, this.setting.FontColor);
            this.drawPicColor(this.backColorPic, this.setting.BackColor);

            // 追記モード
            this.postScriptModeCheck.Checked = this.setting.OnlyPostscriptMode;

            // ラジオボタン
            if (this.setting.ManualCharacterEncoding != null)
            {
                this.radioButtonAuto.Checked = false;

                int codepage = this.setting.ManualCharacterEncoding.CodePage;

                for (int i = 0; i < this.encodingCodePage.Length; i++)
                {
                    if (this.encodingCodePage[i] == codepage)
                    {
                        this.encodingRadios[i].Checked = true;
                    }
                    else
                    {
                        this.encodingRadios[i].Checked = false;
                    }

                }
            }
            else
            {
                // 手動部分を初期化
                Array.ForEach<RadioButton>(this.encodingRadios, p => p.Checked = false);
                // 自動設定
                this.radioButtonAuto.Checked = true;
            }

            // フォントダイアログ
            this.fontDialog1.Font = this.setting.TextBoxFont;
            this.setFontInfo(this.setting.TextBoxFont);

            // ファイル履歴
            this.checkBoxIsSaveHistory.Checked = this.setting.IsSaveHistory;

            // ステータスバー
            this.checkBoxShowStatusBar.Checked = this.setting.IsShowStatusBar;

            // URLリンク
            this.checkBoxIsUrlLink.Checked = this.setting.IsUrlLink;

            // 正規表現の色分け
            this.checkBoxPtnEnable.Checked = this.setting.IsUseColor;
            this.setColorPattern(this.setting.ColorPatterns);
            this.setGroupBoxPtn();

            // 正規表現フィルタ
            this.checkBoxFilterEnable.Checked = this.setting.IsUseFilter;
            this.setFilterPattern(this.setting.FilterPatterns);
            this.setGroupBoxFilter();

            // 正規表現アラートメール
            this.checkBoxMailEnable.Checked = this.setting.IsUseAlertMail;
            this.setMailPattern(this.setting.MailPatterns);
            this.setGroupBoxMail();
        }

 

        private void fontColorPic_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.setting.FontColor = this.colorDialog1.Color;
                // 色の表示
                this.drawPicColor(this.fontColorPic, this.setting.FontColor);
            }
        }

        private void backColorPic_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.setting.BackColor = this.colorDialog1.Color;
                // 色の表示
                this.drawPicColor(this.backColorPic, this.setting.BackColor);
            }
        }

        /// <summary>
        /// 色を描く
        /// </summary>
        /// <param name="pb"></param>
        /// <param name="color"></param>
        private void drawPicColor(PictureBox pb , Color color)
        {
            Bitmap bmp = new Bitmap(pb.Size.Width, pb.Size.Height);
            
            using (Graphics g = Graphics.FromImage(bmp))
            using (Brush brush = new SolidBrush(color))
            {
                g.FillRectangle(brush, 0, 0, bmp.Width, bmp.Height);

            }

            pb.Image = bmp;

        }

        /// <summary>
        /// OKボタンイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try {
                this.setting.MaxReadSize = (int)this.maxReadSize.Value * 1000;
                this.setting.MaxLineLength = (int)this.maxLineLength.Value;

                // 追記のみ表示
                this.setting.OnlyPostscriptMode = this.postScriptModeCheck.Checked;

                if (this.radioButtonAuto.Checked)
                {
                    // 自動設定
                    this.setting.ManualCharacterEncoding = null;
                }
                else
                {
                    // ラジオボタンからコード設定
                    for (int i = 0; i < this.encodingRadios.Length; i++)
                    {
                        if (this.encodingRadios[i].Checked)
                        {
                            int codepage = this.encodingCodePage[i];
                            this.setting.ManualCharacterEncoding = Encoding.GetEncoding(codepage);
                            break;
                        }

                    }
                }

                // ファイル履歴
                this.setting.IsSaveHistory = this.checkBoxIsSaveHistory.Checked;

                // ステータスバー
                this.setting.IsShowStatusBar = this.checkBoxShowStatusBar.Checked;

                // URLリンク
                this.setting.IsUrlLink = this.checkBoxIsUrlLink.Checked;

                // 正規表現の色分け
                this.setting.IsUseColor = this.checkBoxPtnEnable.Checked;
                this.setting.ColorPatterns = this.colorPtnDt.GetPatterns();

                // フィルター設定
                this.setting.IsUseFilter = this.checkBoxFilterEnable.Checked;
                this.setting.FilterPatterns = this.filterDt.GetPatterns();

                // アラートメール設定
                this.setting.IsUseAlertMail = this.checkBoxMailEnable.Checked;
                this.setting.MailPatterns = this.mailDt.GetPatterns();

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// キャンセル
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// タブ移動時に初期選択無効
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 正規表現の初期選択を無効にする
            if (this.tabControl1.SelectedIndex == 1)
                this.dataGridViewPtn.CurrentCell = null;
            // 正規表現の初期選択を無効にする
            if (this.tabControl1.SelectedIndex == 2)
                this.dataGridViewFilter.CurrentCell = null;
        }

        /// <summary>
        /// フォント設定ダイアログ表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonFont_Click(object sender, EventArgs e)
        {
            try {
                if (this.fontDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.setFontInfo(this.fontDialog1.Font);
                    this.setting.TextBoxFont = this.fontDialog1.Font;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// フォント情報を表示
        /// </summary>
        /// <param name="font"></param>
        private void setFontInfo(Font font)
        {
            this.textBoxFontInfo.Text = font.Name + ", " + font.Size + "pt";
        }

        /// <summary>
        /// デフォルト設定を呼び出す
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDefault_Click(object sender, EventArgs e)
        {
            try
            {
                // デフォルトコンストラスタを渡す
                Setting setting = new Setting();
                // Autoのエンコーディングだけは戻しておく（保存ではないため）
                setting.AutoCharacterEncoding = this.setting.AutoCharacterEncoding;
                this.initControl(setting);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region カラーパターン設定

        /// <summary>
        /// カラーパターンの有効/無効切替
        /// </summary>
        private void setGroupBoxPtn()
        {
            this.groupBoxPtn.Enabled = this.checkBoxPtnEnable.Checked;
        }

        /// <summary>
        /// カラーパターンのDataGirdを設定
        /// </summary>
        /// <param name="list"></param>
        private void setColorPattern(List<ColorPattern> list)
        {
            // 初期化
            this.dataGridViewPtn.Columns.Clear();

            //データ投入
            this.colorPtnDt = new ColorPatternDataTable();
            this.colorPtnDt.SetPatterns(list);
            // バインディングする
            this.dataGridViewPtn.DataSource = this.colorPtnDt.DataSource;

            // noと色のカラムはリードオンリー
            this.dataGridViewPtn.Columns[0].ReadOnly = true;
            this.dataGridViewPtn.Columns[2].ReadOnly = true;
            this.dataGridViewPtn.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewPtn.Columns[0].Width = 30;
            
            // ソート禁止
            this.setDataGridSortEnable(this.dataGridViewPtn, DataGridViewColumnSortMode.NotSortable);

            // ヘッダのマルチ言語化
            this.dataGridViewPtn.Columns[0].HeaderText = Properties.Resources.gridViewColumnHeaderNo;
            this.dataGridViewPtn.Columns[1].HeaderText = Properties.Resources.gridViewColumnHeaderEnable;
            this.dataGridViewPtn.Columns[2].HeaderText = Properties.Resources.gridViewColumnHeaderColor;
            this.dataGridViewPtn.Columns[3].HeaderText = Properties.Resources.gridViewColumnHeaderRegExp;
            this.dataGridViewPtn.Columns[4].HeaderText = Properties.Resources.gridViewColumnHeaderComment;

        }

        /// <summary>
        /// カラーパターン有効のチェックボックスイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxPtnEnable_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.setGroupBoxPtn();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// カラーパターンのグリッド用の色塗り
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewPtn_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if(e.ColumnIndex == 2)
            { 
                Color color = ColorTranslator.FromHtml(e.Value.ToString());
                e.CellStyle.BackColor = color;
                e.CellStyle.ForeColor = gettFontColorOnBack(color);
            }
        }
      
        /// <summary>
        /// カラーパレットを表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewPtn_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                this.dataGridViewPtn[e.ColumnIndex, e.RowIndex].Selected = false;
                this.colorDialog1.Color = ColorTranslator.FromHtml(this.dataGridViewPtn[e.ColumnIndex, e.RowIndex].Value.ToString());
                if (this.colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.dataGridViewPtn[e.ColumnIndex, e.RowIndex].Value = ColorTranslator.ToHtml(Color.FromArgb(this.colorDialog1.Color.ToArgb()));
                }
            }
        }
        #endregion

        #region フィルタパターン設定
        /// <summary>
        /// フィルタパターンパターンの有効/無効切替
        /// </summary>
        private void setGroupBoxFilter()
        {
            this.groupBoxFilter.Enabled = this.checkBoxFilterEnable.Checked;
        }

        /// <summary>
        /// フィルタパターンのDataGirdを設定
        /// </summary>
        /// <param name="list"></param>
        private void setFilterPattern(List<FilterPattern> list)
        {
            // 初期化
            this.dataGridViewFilter.Columns.Clear();

            //データ投入
            this.filterDt = new FilterPatternTable();
            this.filterDt.SetPatterns(list);
            // バインディングする
            this.dataGridViewFilter.DataSource = this.filterDt.DataSource;

            // noカラムはリードオンリー
            this.dataGridViewFilter.Columns[0].ReadOnly = true;
            this.dataGridViewFilter.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewFilter.Columns[0].Width = 30;

            // ソート禁止
            this.setDataGridSortEnable(this.dataGridViewFilter, DataGridViewColumnSortMode.NotSortable);

            // ヘッダのマルチ言語化
            this.dataGridViewFilter.Columns[0].HeaderText = Properties.Resources.gridViewColumnHeaderNo;
            this.dataGridViewFilter.Columns[1].HeaderText = Properties.Resources.gridViewColumnHeaderEnable;
            this.dataGridViewFilter.Columns[2].HeaderText = Properties.Resources.gridViewColumnHeaderRegExp;
            this.dataGridViewFilter.Columns[3].HeaderText = Properties.Resources.gridViewColumnHeaderComment;
        }

        /// <summary>
        /// フィルタパターン有効のチェックボックスイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxFilterEnable_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.setGroupBoxFilter();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region アラートメール設定
        /// <summary>
        /// フィルタパターンパターンの有効/無効切替
        /// </summary>
        private void setGroupBoxMail()
        {
            this.groupBoxMail.Enabled = this.checkBoxMailEnable.Checked;
        }

        /// <summary>
        /// アラートメール有効のデータ貼り付け
        /// </summary>
        /// <param name="mailPatterns"></param>
        private void setMailPattern(List<MailPattern> list)
        {
            // 初期化
            this.dataGridViewMail.Columns.Clear();

            //データ投入
            this.mailDt = new MailPatternTable();
            this.mailDt.SetPatterns(list);
            // バインディングする
            this.dataGridViewMail.DataSource = this.mailDt.DataSource;

            // noカラムはリードオンリー
            this.dataGridViewMail.Columns[0].ReadOnly = true;
            this.dataGridViewMail.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewMail.Columns[0].Width = 30;
            // ソート禁止
            this.setDataGridSortEnable(this.dataGridViewMail, DataGridViewColumnSortMode.NotSortable);
            
            this.dataGridViewMail.Columns["ToAddress"].Visible = false;   //メールアドレス
            this.dataGridViewMail.Columns["Subject"].Visible = false;   //タイトル
            this.dataGridViewMail.Columns["Message"].Visible = false;   //本文

            this.dataGridViewMail.Columns[0].HeaderText = Properties.Resources.gridViewColumnHeaderNo;
            this.dataGridViewMail.Columns[1].HeaderText = Properties.Resources.gridViewColumnHeaderEnable;
            this.dataGridViewMail.Columns[2].HeaderText = Properties.Resources.gridViewColumnHeaderRegExp;
            this.dataGridViewMail.Columns[3].HeaderText = Properties.Resources.gridViewColumnHeaderComment;


            //ボタン追加 ここで追加するとなぜか、ボタンがインデックス0となる
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            
            btn.Name = Properties.Resources.gridViewColumnHeaderAlertMail;
            btn.Text = Properties.Resources.gridViewColumnHeaderMailEdit;
            btn.UseColumnTextForButtonValue = true;
            //btn.HeaderText = "EDIT";
            
            this.dataGridViewMail.Columns.Add(btn);
        }

        /// <summary>
        /// アラートメール有効のチェックボックスイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxMailEnable_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.setGroupBoxMail();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region カラーパターンのグリッドのボタン
        /// <summary>
        /// カラーパターンのグリッド追加ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                this.colorPtnDt.AddNewRow();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// カラーパターングリッドの削除ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dataGridViewPtn.SelectedCells != null && this.dataGridViewPtn.SelectedCells.Count > 0)
                    this.colorPtnDt.DeleteRow(this.dataGridViewPtn.SelectedCells[0].RowIndex);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// カラーパターングリッドの上ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPtnUp_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dataGridViewPtn.SelectedCells != null && this.dataGridViewPtn.SelectedCells.Count > 0)
                {
                    int index = this.dataGridViewPtn.SelectedCells[0].RowIndex;
                    int column = this.dataGridViewPtn.SelectedCells[0].ColumnIndex;
                    int movedIndex = this.colorPtnDt.MoveUpRow(index);
                    if (movedIndex >= 0)
                        this.dataGridViewPtn[column, movedIndex].Selected = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// カラーパターングリッドの下ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPtnDown_Click(object sender, EventArgs e)
        {
            try
            {
                int index = this.dataGridViewPtn.SelectedCells[0].RowIndex;
                int column = this.dataGridViewPtn.SelectedCells[0].ColumnIndex;
                int movedIndex = this.colorPtnDt.MoveDownRow(index);
                if (movedIndex >= 0)
                    this.dataGridViewPtn[column, movedIndex].Selected = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region フィルタパターンのグリッドのボタン
        /// <summary>
        /// フィルタパターンのグリッド追加ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttoFilterAdd_Click(object sender, EventArgs e)
        {
            try
            {
                this.filterDt.AddNewRow();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// フィルタパターングリッドの削除ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttoFilterDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dataGridViewFilter.SelectedCells != null && this.dataGridViewFilter.SelectedCells.Count > 0)
                    this.filterDt.DeleteRow(this.dataGridViewFilter.SelectedCells[0].RowIndex);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// フィルタパターングリッドの上ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttoFilterUp_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dataGridViewFilter.SelectedCells != null && this.dataGridViewFilter.SelectedCells.Count > 0)
                {
                    int index = this.dataGridViewFilter.SelectedCells[0].RowIndex;
                    int column = this.dataGridViewFilter.SelectedCells[0].ColumnIndex;
                    int movedIndex = this.filterDt.MoveUpRow(index);
                    if (movedIndex >= 0)
                        this.dataGridViewFilter[column, movedIndex].Selected = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// フィルタパターングリッドの下ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttoFilterDown_Click(object sender, EventArgs e)
        {
            try
            {
                int index = this.dataGridViewFilter.SelectedCells[0].RowIndex;
                int column = this.dataGridViewFilter.SelectedCells[0].ColumnIndex;
                int movedIndex = this.filterDt.MoveDownRow(index);
                if (movedIndex >= 0)
                    this.dataGridViewFilter[column, movedIndex].Selected = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region アラートメールのグリッドのボタン
        /// <summary>
        /// アラートメールのグリッド追加ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonMailAdd_Click(object sender, EventArgs e)
        {
            try
            {
                this.mailDt.AddNewRow();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// アラートメールのグリッドの削除ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonMailDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dataGridViewMail.SelectedCells != null && this.dataGridViewMail.SelectedCells.Count > 0)
                    this.mailDt.DeleteRow(this.dataGridViewMail.SelectedCells[0].RowIndex);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// アラートメールのグリッドの上ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonMailUp_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dataGridViewMail.SelectedCells != null && this.dataGridViewMail.SelectedCells.Count > 0)
                {
                    int index = this.dataGridViewMail.SelectedCells[0].RowIndex;
                    int column = this.dataGridViewMail.SelectedCells[0].ColumnIndex;
                    int movedIndex = this.mailDt.MoveUpRow(index);
                    if (movedIndex >= 0)
                        this.dataGridViewMail[column, movedIndex].Selected = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// アラートメールのグリッドの下ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonMailDown_Click(object sender, EventArgs e)
        {
            try
            {
                int index = this.dataGridViewMail.SelectedCells[0].RowIndex;
                int column = this.dataGridViewMail.SelectedCells[0].ColumnIndex;
                int movedIndex = this.mailDt.MoveDownRow(index);
                if (movedIndex >= 0)
                    this.dataGridViewMail[column, movedIndex].Selected = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        /// <summary>
        /// 背景色に合わせて適切な文字色を返却
        /// </summary>
        /// <param name="baackColor"></param>
        /// <returns></returns>
        private Color gettFontColorOnBack(Color baackColor)
        {
            if ((baackColor.R * 0.299 + baackColor.G * 0.587 + baackColor.B * 0.114) > 186)
                return Color.Black;
            return Color.White;
        }

        /// <summary>
        /// DataGridViewのソートモードの変更
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="mode"></param>
        private void setDataGridSortEnable(DataGridView dgv, DataGridViewColumnSortMode mode)
        {
            foreach (DataGridViewColumn c in dgv.Columns)
            {
                c.SortMode = mode;
            }
        }

        /// <summary>
        /// メールアカウントの設定ダイアログ表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonMailAccount_Click(object sender, EventArgs e)
        {
            try
            {
                MailAccountDialog md = new MailAccountDialog(this.setting.smtpSetting);
                if (md.ShowDialog() == DialogResult.OK)
                {
                    this.setting.smtpSetting = md.smtpSetting;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// メールの詳細設定画面を表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewMail_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (this.dataGridViewMail.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
                {
                    // TODO なぜかボタンのインデックスが0前に行った。
                    string toaddres = this.dataGridViewMail.Rows[e.RowIndex].Cells["ToAddress"].Value.ToString();
                    string subject = this.dataGridViewMail.Rows[e.RowIndex].Cells["Subject"].Value.ToString();
                    string message = this.dataGridViewMail.Rows[e.RowIndex].Cells["Message"].Value.ToString();
                    MailMessageDialog mmd = new MailMessageDialog(this.setting.smtpSetting, toaddres, subject, message);
                    mmd.ShowDialog();

                    if (mmd.DialogResult == DialogResult.OK)
                    {
                        this.dataGridViewMail.Rows[e.RowIndex].Cells["ToAddress"].Value = mmd.ToAddress;
                        this.dataGridViewMail.Rows[e.RowIndex].Cells["Subject"].Value = mmd.Subject;
                        this.dataGridViewMail.Rows[e.RowIndex].Cells["Message"].Value = mmd.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 現在の設定をファイルに保存します。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonExport_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog fd = new SaveFileDialog();

                fd.Filter = "XML file(*.xml)|*.xml|All Files(*.*)|*.*";

                if (fd.ShowDialog() == DialogResult.OK)
                {
                    Setting.SaveXmlFile(this.setting, fd.FileName);                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 設定の読み込み
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonInport_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fd = new OpenFileDialog();

                fd.Filter = "XML file(*.xml)|*.xml|All Files(*.*)|*.*";

                if (fd.ShowDialog() == DialogResult.OK)
                {
                    var setting = Setting.LoadXmlFile(fd.FileName);
                    this.initControl(setting);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
