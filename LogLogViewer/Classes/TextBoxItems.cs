using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogLogViewer.Classes
{
    /// <summary>
    /// テキストボックスに表示するアイテム
    /// </summary>
    public class TextBoxItems
    {
        public string Text;
        public bool HasColor = false;
        public Color TextColor = Color.Empty;
    }
}
