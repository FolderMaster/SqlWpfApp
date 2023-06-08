using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SQLiteWpfApp.Views.Converters
{
    public class EnumToVisibilityMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter,
            CultureInfo culture)
        {
            var parameterString = parameter.ToString();
            var parameters = parameterString.Split(' ');
            if(parameters.Length != values.Length)
            {
                throw new NotImplementedException();
            }

            for(int n = 0; n < parameters.Length; n++)
            {
                if (parameters[n] != values[n].ToString())
                {
                    return Visibility.Hidden;
                }
            }
            return Visibility.Visible;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter,
            CultureInfo culture) => throw new NotImplementedException();
    }
}