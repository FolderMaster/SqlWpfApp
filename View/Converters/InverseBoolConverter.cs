using System;
using System.Globalization;
using System.Windows.Data;

namespace View.Converters
{
    /// <summary>
    /// Класс инверсивного конвертора <see cref="bool"/>.
    /// </summary>
    public class InverseBoolConverter : IValueConverter
    {
        /// <summary>
        /// Конвертирует значение <see cref="bool"/> к инверсии <see cref="bool"/>.
        /// </summary>
        /// <param name="value">Значение конвертации.</param>
        /// <param name="targetType">Тип назначения конвертации.</param>
        /// <param name="parameter">Дополнительный параметр.</param>
        /// <param name="culture">Сведения о культуре.</param>
        /// <returns>Если true, то false, иначе true.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => (bool?)value == true ? false : true;

        /// <summary>
        /// Конвертирует инверсивное значение <see cref="bool"/> к <see cref="bool"/>.
        /// </summary>
        /// <param name="value">Значение конвертации.</param>
        /// <param name="targetType">Тип назначения конвертации.</param>
        /// <param name="parameter">Дополнительный параметр.</param>
        /// <param name="culture">Сведения о культуре.</param>
        /// <returns>Если true, то false, иначе true.</returns>
        public object ConvertBack(object value, Type targetType, object parameter,
            CultureInfo culture) => (bool?)value == true ? false : true;
    }
}