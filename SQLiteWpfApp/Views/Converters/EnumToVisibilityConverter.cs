using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SQLiteWpfApp.Views.Converters
{
    public class EnumToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value != null && parameter != null && value.ToString() == parameter.ToString() ?
            Visibility.Visible : Visibility.Hidden;

        public object ConvertBack(object value, Type targetType, object parameter,
            CultureInfo culture) => throw new NotImplementedException();
    }
}