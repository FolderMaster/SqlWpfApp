using System;
using System.Globalization;
using System.Windows.Data;

namespace View.Converters
{
    public class IntToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => (int)value > 0 ? true : false;

        public object ConvertBack(object value, Type targetType, object parameter,
            CultureInfo culture) => (bool)value ? 1 : 0;
    }
}