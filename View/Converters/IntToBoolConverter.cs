using System;
using System.Globalization;
using System.Windows.Data;

namespace View.Converters
{
    /// <summary>
    /// Класс конвертора <see cref="int"/> и <see cref="bool"/>.
    /// </summary>
    public class IntToBoolConverter : IValueConverter
    {
        /// <summary>
        /// Конвертирует значение <see cref="int"/> к <see cref="bool"/>.
        /// </summary>
        /// <param name="value">Значение конвертации.</param>
        /// <param name="targetType">Тип назначения конвертации.</param>
        /// <param name="parameter">Дополнительный параметр.</param>
        /// <param name="culture">Сведения о культуре.</param>
        /// <returns>Если число больше 0, то true, иначе false.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => (int)value > 0 ? true : false;

        /// <summary>
        /// Конвертирует значение <see cref="bool"/> к <see cref="int"/>.
        /// </summary>
        /// <param name="value">Значение конвертации.</param>
        /// <param name="targetType">Тип назначения конвертации.</param>
        /// <param name="parameter">Дополнительный параметр.</param>
        /// <param name="culture">Сведения о культуре.</param>
        /// <returns>Если true, то 1, иначе 0.</returns>
        public object ConvertBack(object value, Type targetType, object parameter,
            CultureInfo culture) => (bool)value ? 1 : 0;
    }
}