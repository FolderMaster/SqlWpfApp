using Model;
using System;
using System.Globalization;
using System.Windows.Data;

namespace View.Converters
{
    public class BoolToEnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => Enum.Parse(typeof(Sex),
                ((bool)value == true ? 1 : 0).ToString());

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => (int)value == 0 ? false : true;
    }
}
