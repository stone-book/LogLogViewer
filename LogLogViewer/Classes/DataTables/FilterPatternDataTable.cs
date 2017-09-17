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
    public class FilterPatternTable : GridDataTableBase
    {

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FilterPatternTable() : base()
        {

        }

        /// <summary>
        /// DataTableにデータを投入する
        /// </summary>
        /// <param name="list"></param>
        public void SetPatterns(List<FilterPattern> list)
        {
            foreach (FilterPattern ptn in list)
            {
                FilterPattern fp = (FilterPattern)ptn;
                DataRow dr = this.DataSource.NewRow();
                dr[0] = this.DataSource.Rows.Count + 1;
                dr[1] = fp.Enable;
                dr[2] = fp.RegularExp;
                dr[3] = fp.Comment;
                this.DataSource.Rows.Add(dr);
            }
        }

        /// <summary>
        /// DataTableからデータを取り出す
        /// </summary>
        /// <returns></returns>
        public new List<FilterPattern> GetPatterns()
        {
            List<FilterPattern> list = new List<FilterPattern>();

            foreach (DataRow dr in this.DataSource.Rows)
            {
                string[] strs = new string[3];
                strs[0] = dr[1].ToString(); // フラグ
                strs[1] = dr[2].ToString().Trim(); // 正規表現                
                strs[2] = dr[3].ToString().Trim(); // コメント

                // 正規表現が設定されていないものは無視
                if (string.IsNullOrEmpty(strs[1]))
                    continue;

                FilterPattern ptn = new FilterPattern(strs);
                list.Add(ptn);
            }

            return list;
        }
    }
}
