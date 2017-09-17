using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogLogViewer.Classes.Patterns;

namespace LogLogViewer.Classes.DataTables
{
    public class MailPatternTable : GridDataTableBase
    {

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MailPatternTable() : base()
        {
            this.DataSource.Columns.Add("ToAddress", typeof(string));
            this.DataSource.Columns.Add("Subject", typeof(string));
            this.DataSource.Columns.Add("Message", typeof(string));

            //this.DataSource = new DataTable();
            //this.DataSource.Columns.Add("No.", typeof(int));
            //this.DataSource.Columns.Add("Enable", typeof(bool));
            //this.DataSource.Columns.Add("Regular Exp.", typeof(string));
            //this.DataSource.Columns.Add("Message", typeof(string));
            //this.DataSource.Columns.Add("Comment", typeof(string));
        }

        /// <summary>
        /// DataTableにデータを投入する
        /// </summary>
        /// <param name="list"></param>
        public void SetPatterns(List<MailPattern> list)
        {
            foreach (MailPattern ptn in list)
            {
                MailPattern mp = (MailPattern)ptn;
                DataRow dr = this.DataSource.NewRow();
                dr[0] = this.DataSource.Rows.Count + 1;
                dr[1] = mp.Enable;
                dr[2] = mp.RegularExp;                
                dr[3] = mp.Comment;
                dr[4] = mp.ToAddress;
                dr[5] = mp.Subject;
                dr[6] = mp.Message;
                this.DataSource.Rows.Add(dr);
            }
        }

        /// <summary>
        /// DataTableからデータを取り出す
        /// </summary>
        /// <returns></returns>
        public new List<MailPattern> GetPatterns()
        {
            List<MailPattern> list = new List<MailPattern>();

            foreach (DataRow dr in this.DataSource.Rows)
            {
                string[] strs = new string[6];
                strs[0] = dr[1].ToString(); // フラグ
                strs[1] = dr[2].ToString().Trim(); // 正規表現                
                strs[2] = dr[3].ToString().Trim(); // コメント
                strs[3] = dr[4].ToString().Trim(); // アドレス
                strs[4] = dr[5].ToString().Trim(); // タイトル
                strs[5] = dr[6].ToString().Trim(); // メッセージ
                // 正規表現が設定されていないものは無視
                if (string.IsNullOrEmpty(strs[1]))
                    continue;

                MailPattern ptn = new MailPattern(strs);
                list.Add(ptn);
            }

            return list;
        }
    }
}
