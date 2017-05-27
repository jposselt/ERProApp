using System;
using System.Globalization;
using System.Windows.Data;

namespace ERProApp
{
    /// <summary>
    /// Konvertiert ...
    /// </summary>
    [ValueConversion(typeof(bool), typeof(string))]
    class RentalTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
                return "Reservierungen: ";
            else
                return "Ausleihen: ";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
