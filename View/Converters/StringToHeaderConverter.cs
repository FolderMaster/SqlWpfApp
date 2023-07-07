using System;
using System.Globalization;
using System.Windows.Data;

namespace View.Converters
{
    /// <summary>
    /// Класс конвертора <see cref="string"/> и заголовка.
    /// </summary>
    public class StringToHeaderConverter : IValueConverter
    {
        /// <summary>
        /// Конвертирует значение <see cref="string"/> к заголовку.
        /// </summary>
        /// <param name="value">Значение конвертации.</param>
        /// <param name="targetType">Тип назначения конвертации.</param>
        /// <param name="parameter">Дополнительный параметр.</param>
        /// <param name="culture">Сведения о культуре.</param>
        /// <returns>Заголовок.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => (string)value + ":";

        /// <summary>
        /// Конвертирует значение заголовка к <see cref="string"/> (не используется).
        /// </summary>
        /// <param name="value">Значение конвертации.</param>
        /// <param name="targetType">Тип назначения конвертации.</param>
        /// <param name="parameter">Дополнительный параметр.</param>
        /// <param name="culture">Сведения о культуре.</param>
        /// <returns>Исключение (не используется).</returns>
        /// <exception cref="NotImplementedException"></exception>
        public object ConvertBack(object value, Type targetType, object parameter,
            CultureInfo culture) => throw new NotImplementedException();
    }
}