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
    public partial class FindDialog : Form
    {
        /// <summary>
        /// 制御するメインフォーム
        /// </summary>
        private MainForm mainform;

        /// <summary>
        ///　コンストラスタ
        /// </summary>
        /// <param name="findword"></param>
        public FindDialog(MainForm mainform)
        {
            InitializeComponent();

            this.mainform = mainform;
            this.textBoxFindWord.Text = mainform.Findword;
        }

        /// <summary>
        /// キャンセルボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.CancelButton.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// 検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonFind_Click(object sender, EventArgs e)
        {
            try
            {
                // 検索文字取得
                string word = this.textBoxFindWord.Text;

                if (word.Length > 0)
                {
                    // 検索文字設定
                    this.mainform.Findword = word;
                    // 検索メソッド実行
                    this.mainform.FindTextBox();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
