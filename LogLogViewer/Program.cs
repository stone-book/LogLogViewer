using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using LogLogViewer.Forms;

namespace LogLogViewer
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            string path = null;

            // 引数チェック
            if (args.Length > 0)
            {
                List<string> argslist = args.ToList<string>();

                int enIndx = argslist.IndexOf("en");

                // 英語モード
                if (enIndx >= 0)
                {
                    System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en", false);
                    argslist.RemoveAt(enIndx);
                }

                if (argslist.Count > 0)
                    path = argslist[0];
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // メインフォーム
            MainForm main;

            // パスの引数があるかないか
            if (path != null)
            {
                main = new MainForm(path);
            }
            else
            {
                main = new MainForm();
            }
            Application.Run(main);
        }
    }
}
