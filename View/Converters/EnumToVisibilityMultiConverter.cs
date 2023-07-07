using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace View.Converters
{
    /// <summary>
    /// Класс мульти конвертора перечислений и <see cref="Visibility"/>.
    /// </summary>
    public class EnumToVisibilityMultiConverter : IMultiValueConverter
    {
        /// <summary>
        /// Конвертирует значения перечисления к <see cref="Visibility"/>.
        /// </summary>
        /// <param name="values">Значение конвертации.</param>
        /// <param name="targetType">Тип назначения конвертации.</param>
        /// <param name="parameter">Дополнительный параметр.</param>
        /// <param name="culture">Сведения о культуре.</param>
        /// <returns>Если параметр и значения совпадают, то true, иначе false.</returns>
        /// <exception cref="NotImplementedException"></exception>
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

        /// <summary>
        /// Конвертирует значения <see cref="Visibility"/> к перечислению (не используется).
        /// </summary>
        /// <param name="value">Значение конвертации.</param>
        /// <param name="targetTypes">Тип назначения конвертации.</param>
        /// <param name="parameter">Дополнительный параметр.</param>
        /// <param name="culture">Сведения о культуре.</param>
        /// <returns>Исключение (не используется).</returns>
        /// <exception cref="NotImplementedException"></exception>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter,
            CultureInfo culture) => throw new NotImplementedException();
    }
}