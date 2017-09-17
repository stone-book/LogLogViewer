using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogLogViewer.Classes.Patterns;

namespace LogLogViewer.Classes.DataTables
{
    /// <summary>
    /// DataGridTable用のクラス
    /// </summary>
    public class GridDataTableBase
    {
        /// <summary>
        /// データテーブル
        /// </summary>
        public DataTable DataSource { set; get; }

        /// <summary>
        /// データの移動方向
        /// </summary>
        protected enum RowDirection
        {
            Up,
            Down,
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GridDataTableBase()
        {
            this.DataSource = new DataTable();            
            this.DataSource.Columns.Add("No." , typeof(int));
            this.DataSource.Columns.Add("Enable", typeof(bool));
            this.DataSource.Columns.Add("Regular Exp.", typeof(string));
            this.DataSource.Columns.Add("Comment",typeof(string));
        }

        /// <summary>
        /// 行追加する
        /// </summary>
        public virtual void AddNewRow()
        {
            DataRow dr = this.DataSource.NewRow();
            dr[0] = this.DataSource.Rows.Count + 1;
            this.DataSource.Rows.Add(dr);
        }

        /// <summary>
        /// 行を消す
        /// </summary>
        /// <param name="index"></param>
        public void DeleteRow(int index)
        {
            if(0 <= index && index < this.DataSource.Rows.Count)
                this.DataSource.Rows.RemoveAt(index);
        }

        /// <summary>
        /// 指定した行を上にする
        /// </summary>
        /// <param name="index"></param>
        public int MoveUpRow(int index)
        {
            return MoveRow(index, RowDirection.Up);
        }

        /// <summary>
        /// 指定した行を下にする
        /// </summary>
        /// <param name="index"></param>
        public int MoveDownRow(int index)
        {
            return MoveRow(index, RowDirection.Down);
        }

        /// <summary>
        /// 指定した位置に行を移動する
        /// </summary>
        /// <param name="index"></param>
        /// <param name="dir"></param>
        protected int MoveRow(int index, RowDirection dir)
        {
            // 最上部と最下部の移動制限
            if (dir == RowDirection.Up && index == 0)
                return -1;
            if (dir == RowDirection.Down && index == this.DataSource.Rows.Count - 1)
                return -1;

            // インデックス判定後、再度居並びなおす
            if (0 <= index && index < this.DataSource.Rows.Count)
            {
                DataRow dr = this.DataSource.NewRow();

                for (int i = 0; i < this.DataSource.Columns.Count; i++)
                    dr[i] = this.DataSource.Rows[index][i];

                this.DataSource.Rows.RemoveAt(index);

                if (dir == RowDirection.Up)
                    index--;
                else if (dir == RowDirection.Down)
                    index++;

                this.DataSource.Rows.InsertAt(dr, index);
            }

            // ナンバーを振りなおす
            int j = 1;
            foreach( DataRow dr in this.DataSource.Rows)
            {
                dr[0] = j;
                j++;
            }

            return index;
        }

        /// <summary>
        /// DataTableにデータを投入する
        /// </summary>
        /// <param name="list"></param>
        public virtual void SetPatterns(List<Pattern> list)
        {
            foreach (Pattern ptn in list)
            {
                DataRow dr = this.DataSource.NewRow();                
                dr[0] = this.DataSource.Rows.Count + 1;
                dr[1] = ptn.Enable;                
                this.DataSource.Rows.Add(dr);
            }
        }

        /// <summary>
        /// DataTableからデータを取り出す
        /// </summary>
        /// <returns></returns>
        public virtual List<Pattern> GetPatterns()
        {
            List<Pattern> list = new List<Pattern>();

            foreach (DataRow dr in this.DataSource.Rows)
            {
                string[] strs = new string[3];
                strs[0] = dr[1].ToString(); // フラグ
                
                Pattern ptn = new Pattern(strs);
                list.Add(ptn);
            }
            return list;
        }

    }
}
