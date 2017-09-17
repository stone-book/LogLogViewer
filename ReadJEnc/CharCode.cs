using System.Text;

namespace Hnx8.ReadJEnc
{
    /// <summary>
    /// ReadJEnc 文字コード種類定義(Rev.20150309)
    /// </summary>
    public class CharCode
    {   ////////////////////////////////////////////////////////////////////////
        // <CharCode.cs> ReadJEnc 文字コード種類定義(Rev.20150309)
        //  Copyright (C) 2014-2015 hnx8(H.Takahashi)
        //  http://hp.vector.co.jp/authors/VA055804/
        //
        //  Released under the MIT license
        //  http://opensource.org/licenses/mit-license.php
        ////////////////////////////////////////////////////////////////////////

        //Unicode系文字コード

        /// <summary>UTF8(BOMあり)</summary>
        public static readonly Text UTF8 = new Text("UTF-8", new UTF8Encoding(true, true)); //BOM : 0xEF, 0xBB, 0xBF
        /// <summary>UTF32(BOMありLittleEndian)</summary>
        public static readonly Text UTF32 = new Text("UTF-32", new UTF32Encoding(false, true, true)); //BOM : 0xFF, 0xFE, 0x00, 0x00
        /// <summary>UTF32(BOMありBigEndian)</summary>
        public static readonly Text UTF32B = new Text("UTF-32B", new UTF32Encoding(true, true, true)); //BOM : 0x00, 0x00, 0xFE, 0xFF
        /// <summary>UTF16(BOMありLittleEndian)</summary><remarks>Windows標準のUnicode</remarks>
        public static readonly Text UTF16 = new Text("UTF-16", new UnicodeEncoding(false, true, true)); //BOM : 0xFF, 0xFE
        /// <summary>UTF16(BOMありBigEndian)</summary>
        public static readonly Text UTF16B = new Text("UTF-16B", new UnicodeEncoding(true, true, true)); //BOM : 0xFE, 0xFF

        /// <summary>UTF16(BOM無しLittleEndian)</summary>
        public static readonly Text UTF16LE = new Text("UTF-16LE", new UnicodeEncoding(false, false, true));
        /// <summary>UTF16(BOM無しBigEndian)</summary>
        public static readonly Text UTF16BE = new Text("UTF-16BE", new UnicodeEncoding(true, false, true));
        /// <summary>UTF8(BOM無し)</summary>
        public static readonly Text UTF8N = new Text("UTF-8N", new UTF8Encoding(false, true));

        //１バイト文字コード

        /// <summary>Ascii</summary><remarks>デコードはUTF8Encodingを流用</remarks>
        public static readonly Text ASCII = new Text("ASCII", UTF8N.Encoding);
        /// <summary>1252 ISO8859 西ヨーロッパ言語</summary>
        public static readonly Text ANSI = new Text("ANSI欧米", 1252);

        //ISO-2022文字コード

        /// <summary>50221 iso-2022-jp 日本語 (JIS-Allow 1 byte Kana) ※MS版</summary>
        public static readonly Text JIS = new Text("JIS", 50221);
        /// <summary>50222 iso-2022-jp 日本語 (JIS-Allow 1 byte Kana - SO/SI)</summary><remarks>SO/SIによるカナシフトのみのファイルもCP50222とみなす</remarks>
        public static readonly Text JIS50222 = new Text("JIS50222", 50222);
        /// <summary>50221(MS版JIS) + 20932(JIS補助漢字を無理やりデコード)</summary><remarks>JIS補助漢字はデコードのみ対応、エンコードは未対応</remarks>
        public static readonly Text JISH = new Text("JIS補漢", 0);
        /// <summary>JISのように見えるがデコード不能な箇所あり、実質非テキストファイル</summary>
        public static readonly Text JISNG = new Text("JIS破損", -50221);
        /// <summary>50225 iso-2022-kr 韓国語(ISO)</summary><remarks>SO/SIカナシフトファイルの判定ロジックに流れ込まないようにするため定義</remarks>
        public static readonly Text ISOKR = new Text("ISO-KR", 50225);

        //日本語文字コード

        /// <summary>EUC補助漢字(0x8F)あり ※MS-CP20932を利用し強引にデコードする</summary><remarks>エンコードするとファイルが壊れるので注意</remarks>
        public static readonly Text EUCH = new EucH("EUC補漢");
        /// <summary>51932 euc-jp 日本語 (EUC) ※MS版</summary>
        public static readonly Text EUC = new Text("EUCJP", 51932);
        /// <summary>932 shift_jis 日本語 (シフト JIS) ※MS独自</summary>
        public static readonly Text SJIS = new Text("ShiftJIS", 932);

#if (!JPONLY) //漢字圏テキスト文字コード各種（日本語判別以外使用しないなら定義省略可）

        /// <summary>20000 x-Chinese-CNS 繁体字中国語(EUC-TW)</summary>
        public static readonly Text EUCTW = new Text("EUC(台)", 20000);
        /// <summary>950 big5 繁体字中国語 (BIG5)</summary>
        public static readonly Text BIG5TW = new Text("Big5(台)", 950);

        /// <summary>51936 EUC-CN 簡体字中国語 (=GB2312)</summary>
        public static readonly Text EUCCN = new Text("EUC(中)", 51936);
        /// <summary>54936 GB18030 簡体字中国語 (GB2312/GBKの拡張)</summary>
        public static readonly Text GB18030 = new Text("GB18030", 54936);

        /// <summary>51949 euc-kr 韓国語 (=KSX1001)</summary>
        public static readonly Text EUCKR = new Text("EUC(韓)", 51949);
        /// <summary>949 ks_c_5601-1987 韓国語 (UHC=EUC-KRの拡張)</summary>
        public static readonly Text UHCKR = new Text("UHC(韓)", 949);
#endif

        // 文字コード（ファイル種類）判定メソッド

        /// <summary>BOMありUTFファイルの文字コードを判定する</summary>
        /// <param name="bytes">判定対象のバイト配列</param>
        /// <param name="Read">バイト配列先頭の読み込み済バイト数（LEASTREADSIZEのバイト数以上読み込んでおくこと）</param>
        /// <returns>BOMから判定できた文字コード種類、合致なしの場合null</returns>
        public static CharCode GetPreamble(byte[] bytes, int Read)
        {   //BOM一致判定
            return GetPreamble(bytes, Read,
                UTF8, UTF32, UTF32B, UTF16, UTF16B);
        }

        #region 基本クラス定義--------------------------------------------------
        /// <summary>ファイル文字コード種類名</summary>
        public readonly string Name;
        /// <summary>先頭バイト識別データ（BOM/マジックナンバー）</summary>
        protected readonly byte[] Bytes = null;
        /// <summary>エンコーディング</summary>
        private Encoding Encoding;
        /// <summary>エンコーディング遅延初期化用のコードページ番号退避変数</summary>
        private int enc = 0;

        /// <summary>基本コンストラクタ</summary>
        /// <param name="Name">ファイル文字コード種類名を定義する</param>
        /// <param name="enc">デコード時に使用するCodePageを指定(正値ならDecoderExceptionFallback、マイナス値ならDecoderReplacementFallBackを設定)</param>
        /// <param name="Bytes">先頭バイト識別データを指定する</param>
        protected CharCode(string Name, int enc, byte[] Bytes)
        {
            this.Name = Name;
            this.enc = enc;
            //GetEncoding(); //Encodingを実際に使用するまで初期化を遅らせる
            this.Bytes = Bytes;
        }
        /// <summary>基本コンストラクタ</summary>
        /// <param name="Name">ファイル文字コード種類名を定義する</param>
        /// <param name="Encoding">デコード時に使用するEncodingを指定する</param>
        /// <param name="Bytes">先頭バイト識別データを指定する</param>
        protected CharCode(string Name, Encoding Encoding, byte[] Bytes)
        {
            this.Name = Name;
            this.Encoding = Encoding;
            this.Bytes = Bytes;
        }

        /// <summary>このファイル文字コード種類のEncodingオブジェクトを返す</summary>
        public Encoding GetEncoding()
        {
            if (this.Encoding == null)
            {   //Encodingオブジェクトがまだ用意されていなければ初期化する
                this.Encoding =
                    (enc > 0 ? System.Text.Encoding.GetEncoding(enc, EncoderFallback.ExceptionFallback, DecoderFallback.ExceptionFallback)
                    : enc < 0 ? System.Text.Encoding.GetEncoding(-enc, EncoderFallback.ExceptionFallback, DecoderFallback.ReplacementFallback)
                    : null);
            }
            return Encoding;
        }

        /// <summary>引数のバイト配列から文字列を取り出す。失敗時はnullを返す</summary>
        /// <param name="bytes">判定対象のバイト配列</param>
        /// <param name="len">ファイルサイズ(バイト配列先頭からの先頭からのデコード対象バイト数)</param>
        public virtual string GetString(byte[] bytes, int len)
        {
            Encoding enc = GetEncoding();
            if (enc == null) { return null; }
            try
            {   //BOMサイズを把握し、BOMを除いた部分を文字列として取り出す
                int bomBytes = (this.Bytes == null ? 0 : this.Bytes.Length);
                return enc.GetString(bytes, bomBytes, len - bomBytes);
            }
            catch (DecoderFallbackException)
            {   //読み出し失敗(マッピングされていない文字があった場合など)
                return null;
            }
        }

        /// <summary>このファイル文字コード種類の名前を返す</summary>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>判定対象のファイル文字コード種類一覧から、BOM/マジックナンバーが一致するものを探索して返す</summary>
        /// <param name="bytes">判定対象のバイト配列</param>
        /// <param name="Read">バイト配列先頭の読み込み済バイト数（LEASTREADSIZEのバイト数以上読み込んでおくこと）</param>
        /// <param name="Array">判定対象とするファイル文字コード種類の一覧</param>
        /// <returns>先頭バイトが一致したファイル文字コード種類、合致なしの場合null</returns>
        protected static CharCode GetPreamble(byte[] bytes, int Read, params CharCode[] Array)
        {
            foreach (CharCode c in Array)
            {   //読み込み済バイト配列内容をもとにファイル種類の一致を確認
                byte[] bom = c.Bytes;
                int i = (bom != null ? bom.Length : int.MaxValue); //BOM・マジックナンバー末尾から調べる
                if (Read < i) { continue; } //そもそもファイルサイズが小さい場合は不一致
                do
                {   //全バイト一致ならその文字コードとみなす
                    if (i == 0) { return c; }
                    i--;
                } while (bytes[i] == bom[i]); //BOM・マジックナンバー不一致箇所ありならdo脱出
            }
            return null; //ファイル種類決定できず
        }

        #endregion

        #region テキスト基本クラス定義------------------------------------------
        /// <summary>文字コード種類：テキスト
        /// </summary>
        public class Text : CharCode
        {
            internal Text(string Name, Encoding Encoding) : base(Name, Encoding, Encoding.GetPreamble()) { }
            internal Text(string Name, int enc) : base(Name, enc, null) { }
        }
        #endregion

        #region JIS補助漢字対応デコーダ-----------------------------------------
        /// <summary>
        /// EUC補助漢字特殊処理(MS版CP20932の特異なコード体系によりデコードする)
        /// </summary>
        private class EucH : Text
        {
            internal EucH(string Name) : base(Name, 20932) { }

            public override string GetString(byte[] bytes, int len)
            {
                byte[] bytesForCP20932 = new byte[len]; //CP20932でのデコード用にバイト配列を補正
                int cp20932Len = 0;
                int shiftPos = int.MinValue;
                byte b;
                for (int i = 0; i < len; i++)
                {
                    if ((b = bytes[i]) == 0x8F)
                    {   //3byteの補助漢字を検出、補正箇所を把握(0x8Fは読み捨て、補正後配列には設定しない)
                        shiftPos = i + 2;
                    }
                    else
                    {   //補助漢字3byte目ならば0x21-0x7Eへシフト(CP20932におけるEUCの2byte目として設定)
                        bytesForCP20932[cp20932Len] = (i == shiftPos ? (byte)(b & 0x7F) : b);
                        cp20932Len++;
                    }
                }
                try
                {   //補正後配列を用い、CP20932でのデコードを試みる
                    return GetEncoding().GetString(bytesForCP20932, 0, cp20932Len);
                }
                catch (DecoderFallbackException)
                {   //読み出し失敗(マッピングされていない文字があった場合など)
                    return null;
                }
            }
        }
        #endregion
    }
}