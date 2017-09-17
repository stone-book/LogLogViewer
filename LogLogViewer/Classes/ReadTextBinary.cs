using Hnx8.ReadJEnc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogLogViewer.Classes
{
    /// <summary>
    /// ファイルをバイナリで読み込む
    /// </summary>
    class ReadTextBinary
    {
        /// <summary>
        /// バイナリファイルからテキストを読み込む
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="address"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string Read(string filePath , long address,  Encoding encoding , out long nextAddress)
        {
            byte[] bs = null;
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                fs.Seek(address, SeekOrigin.Begin);
                long size = fs.Length - address;
                //long size = readSize;

                // 次のファイルサイズを保持
                nextAddress = size + address;

                bs = new byte[size];
                fs.Read(bs, 0, bs.Length);
                                
                fs.Close();
            }

            // エンコードして返す
            return encoding.GetString(bs);
        }

        ///// <summary>
        ///// 簡易エンコード判定
        ///// </summary>
        ///// <param name="filePath"></param>
        ///// <returns></returns>
        //public static Encoding GetEncoding(string filePath)
        //{
        //    int codepage = (int)Const.EncodingCodePage.ShiftJIS;

        //    using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        //    {
        //        byte[] bs = new byte[1];
        //        fs.Read(bs, 0, bs.Length);

        //        if (bs.Length > 0)
        //        {
        //            if (bs[0] >= 161 && bs[0] <= 193)
        //                codepage = (int)Const.EncodingCodePage.EucJP;
        //            if (bs[0] >= 208 && bs[0] <= 239)
        //                codepage = (int)Const.EncodingCodePage.UTF8;
        //        }
        //        fs.Close();
        //    }
        //    // 判定できないときはshift-jisをとりあえず返す
        //    return Encoding.GetEncoding(codepage);
        //}

        /// <summary>
        /// ライブラリによるエンコード判定
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static Encoding GetEncoding(string filePath)
        {
            FileInfo file = new FileInfo(filePath);

            using (Hnx8.ReadJEnc.FileReader reader = new FileReader(file))
            {
                //判別読み出し実行
                Hnx8.ReadJEnc.CharCode c = reader.Read(file);
                Encoding enc = c.GetEncoding();

                // これでも取れなかった場合はしょうがない
                return enc != null ? enc : Encoding.Default;              
            }

        }
    }
}
