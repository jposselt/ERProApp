using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ERProApp
{
    /// <summary>
    /// 
    /// </summary>
    //[ValueConversion(typeof(), typeof())]
    class HighlightDatesConverter : IValueConverter
    {
        private static Dictionary<DateTime, string> _highlight = new Dictionary<DateTime, string>();

        /// <summary>
        /// Gibt für ein Datum einen Textstring aus einem Dictionary zurueck. Falls ein Datum keinen
        /// Schluessel im Dictionary hat ist der Rueckgabewert null;
        /// </summary>
        public static Dictionary<DateTime, string> Highlight
        {
            get { return _highlight; }
            set { _highlight = value; }
        }
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string text;
            if (value == null)
                text = null;
            else if (!_highlight.TryGetValue((DateTime)value, out text))
                text = null;
            return text;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }




}
