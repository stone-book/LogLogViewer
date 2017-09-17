using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LogLogViewer.Forms;
using System.Collections.Specialized;

namespace LogLogViewer.Classes
{
    public class HistoryControl
    {
        /// <summary>
        /// ヒストリーパス
        /// </summary>
        private StringCollection historyPaths;

        /// <summary>
        /// ヒストリーメニュー
        /// </summary>
        private List<ToolStripMenuItem> historyMenus;

        private MainForm mainForm;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="mainForm"></param>
        public HistoryControl(MainForm mainForm)
        {
            this.mainForm = mainForm;
            this.getHistory();
        }

        /// <summary>
        /// ファイル履歴
        /// </summary>
        /// <returns></returns>
        private void getHistory()
        {
            this.historyPaths = new StringCollection();
            this.historyMenus = new List<ToolStripMenuItem>();

            if (Properties.Settings.Default.HisotryFiles == null)
                return;

            foreach (string s in Properties.Settings.Default.HisotryFiles)
            {
                if (!string.IsNullOrEmpty(s))
                {
                    this.addHistory(s);

                    if (this.historyMenus.Count > 9)
                        break;
                }
            }
        }

        /// <summary>
        ///  ファイル履歴リスト追加操作
        /// </summary>
        /// <param name="path"></param>
        private void addHistory(string path)
        {
            ToolStripMenuItem item = new System.Windows.Forms.ToolStripMenuItem();
            item.Text = path;
            item.Click += new System.EventHandler(mainForm.HistoryFile_Click);
            this.historyMenus.Insert(0, item);
            this.historyPaths.Insert(0, path);
        }

        /// <summary>
        /// ファイル履歴リスト削除操作
        /// </summary>
        /// <param name="index"></param>
        private void deleteHistory(int index)
        {
            ToolStripMenuItem item = this.historyMenus[index];
            this.historyMenus.RemoveAt(index);
            item.Dispose();

            this.historyPaths.RemoveAt(index);
           
        }

        /// <summary>
        /// ファイル履歴挿入
        /// </summary>
        /// <param name="path"></param>
        public void IntoHistory(string path)
        {
            // パスが含まれるか
            int index = this.historyPaths.IndexOf(path);
            if (index >= 0)
            {
                this.deleteHistory(index);
                this.addHistory(path);                
            }
            else
            {
                if (this.historyPaths.Count > 9)
                {
                    this.deleteHistory(0);
                    this.addHistory(path);
                }
                else
                {
                    this.addHistory(path);
                }
            }

        }

        /// <summary>
        /// ファイル履歴返却
        /// </summary>
        /// <returns></returns>
        public ToolStripMenuItem[] GetHistoryMenu()
        {
            return this.historyMenus.ToArray<ToolStripMenuItem>();
        }

        /// <summary>
        /// ファイル履歴を保存
        /// </summary>
        public void SaveHistory(bool isSave)
        {
            if(isSave)
                Properties.Settings.Default.HisotryFiles = this.historyPaths;
            else
                Properties.Settings.Default.HisotryFiles = new StringCollection();

            Properties.Settings.Default.Save();
        }

    }
}
