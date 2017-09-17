using LogLogViewer.Classes.Patterns;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogLogViewer.Classes.DataTables
{
    public class ColorPatternDataTable : GridDataTableBase
    {

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ColorPatternDataTable()
        {
            // Colorを前に持って期待から並べ直し
            this.DataSource = new DataTable();
            this.DataSource.Columns.Add("No.", typeof(int));
            this.DataSource.Columns.Add("Enable", typeof(bool));
            this.DataSource.Columns.Add("Color", typeof(string));
            this.DataSource.Columns.Add("Regular Exp.", typeof(string));
            this.DataSource.Columns.Add("Comment", typeof(string));
        }

        /// <summary>
        /// 行を新規追加する
        /// </summary>
        public override void AddNewRow()
        {
            DataRow dr = this.DataSource.NewRow();
            dr[0] = this.DataSource.Rows.Count + 1;
            dr[2] = "#000000";
            this.DataSource.Rows.Add(dr);
        }

        /// <summary>
        /// DataTableにデータを投入する
        /// </summary>
        /// <param name="list"></param>
        public void SetPatterns(List<ColorPattern> list)
        {         
            foreach (ColorPattern ptn in list)
            {
                DataRow dr = this.DataSource.NewRow();
                dr[0] = this.DataSource.Rows.Count + 1;
                dr[1] = ptn.Enable;
                dr[2] = ColorTranslator.ToHtml(ptn.TextColor);
                dr[3] = ptn.RegularExp;
                dr[4] = ptn.Comment;
                this.DataSource.Rows.Add(dr);
            }
        }

        /// <summary>
        /// DataTableからCloroPatternリストを返却する
        /// </summary>
        /// <returns></returns>
        public new List<ColorPattern> GetPatterns()
        {
            List<ColorPattern> list = new List<ColorPattern>();

            foreach (DataRow dr in this.DataSource.Rows)
            {
                string[] strs = new string[4];
                strs[0] = dr[1].ToString().Trim(); // フラグ
                strs[1] = dr[3].ToString().Trim(); // 正規表現                
                strs[2] = dr[4].ToString().Trim(); // コメント
                strs[3] = dr[2].ToString().Trim(); // 色

                // 色や正規表現が設定されていないものは無視
                if (string.IsNullOrEmpty(strs[1]) || string.IsNullOrEmpty(strs[3]))
                    continue;

                ColorPattern ptn = new ColorPattern(strs);
                list.Add(ptn);
            }

            return list;
        }

    }
}
