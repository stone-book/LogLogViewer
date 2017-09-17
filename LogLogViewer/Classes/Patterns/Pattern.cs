using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LogLogViewer.Classes.Patterns
{
    public class Pattern
    {
        public static readonly int PropetyNum = 3;

        public bool Enable { set; get; } = false;

        public string RegularExp { set; get; }

        public string Comment { set; get; }

        public Pattern()
        {

        }

        public Pattern(string[] setting)
        {
            if (setting != null && setting.Length >= PropetyNum)
            {
                bool tmp = false;
                if (bool.TryParse(setting[0], out tmp))
                    this.Enable = tmp;
                else
                    this.Enable = false;
                this.RegularExp = setting[1];
                this.Comment = setting[2];
            }
        }
    }
}
