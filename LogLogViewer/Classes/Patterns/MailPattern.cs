using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogLogViewer.Classes.Patterns
{
    public class MailPattern : Pattern
    {
        public static readonly new int PropetyNum = 6;

        public string ToAddress { set; get; }

        public string Subject { set; get; }

        public string Message { set; get; }        

        public MailPattern()
        {

        }

        public MailPattern(string[] setting) : base(setting)
        {
            if (setting != null && setting.Length >= PropetyNum)
            {
                this.ToAddress = setting[3];
                this.Subject = setting[4];
                this.Message = setting[5];
            }
        }

    }
}
