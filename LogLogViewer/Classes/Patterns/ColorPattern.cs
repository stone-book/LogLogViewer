using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LogLogViewer.Classes.Patterns
{
    public class ColorPattern : Pattern
    {
        public static readonly new int PropetyNum = 4;

        /// <summary>
        /// テキストのフォント
        /// ColorオブジェクトはシリアライズできないのでXMLからは外す
        /// </summary>
        [XmlIgnore]
        public Color TextColor { set; get; }

        /// <summary>
        /// シリアライズ用のダミー
        /// （シリアライズされるときに呼ばれる
        /// Browsable(false)は通常使わないメソッドのため
        /// </summary>
        [Browsable(false)]
        public string TextColorString
        {
            set { this.TextColor = ColorTranslator.FromHtml(value); }
            get { return ColorTranslator.ToHtml(this.TextColor); }
        }

        public ColorPattern()
        {

        }

        public ColorPattern(string[] setting) : base(setting)
        {
            if(setting != null && setting.Length >= PropetyNum)
            {
                this.TextColor = ColorTranslator.FromHtml(setting[3]);                
            }
        }      
    }
}
