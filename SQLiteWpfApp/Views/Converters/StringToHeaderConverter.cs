using System;
using System.Globalization;
using System.Windows.Data;

namespace SQLiteWpfApp.Views.Converters
{
    public class StringToHeaderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => (string)value + ":";

        public object ConvertBack(object value, Type targetType, object parameter,
            CultureInfo culture) => throw new NotImplementedException();
    }
}