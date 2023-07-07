using System;
using System.Globalization;
using System.Windows.Data;

namespace View.Converters
{
    /// <summary>
    /// Класс конвертора перечислений и <see cref="bool"/>.
    /// </summary>
    class EnumToBoolConverter : IValueConverter
    {
        /// <summary>
        /// Конвертирует значение перечисления к <see cref="bool"/>.
        /// </summary>
        /// <param name="value">Значение конвертации.</param>
        /// <param name="targetType">Тип назначения конвертации.</param>
        /// <param name="parameter">Дополнительный параметр.</param>
        /// <param name="culture">Сведения о культуре.</param>
        /// <returns>Если параметр и значение совпадают, то true, иначе false.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value != null && parameter != null && value.ToString() == parameter.ToString();

        /// <summary>
        /// Конвертирует значение <see cref="bool"/> к перечислению.
        /// </summary>
        /// <param name="value">Значение конвертации.</param>
        /// <param name="targetType">Тип назначения конвертации.</param>
        /// <param name="parameter">Дополнительный параметр.</param>
        /// <param name="culture">Сведения о культуре.</param>
        /// <returns>Если значение true, то параметр, иначе значение по умолчанию.</returns>
        public object ConvertBack(object value, Type targetType, object parameter,
            CultureInfo culture) =>
            (bool)value ? Enum.Parse(targetType, parameter.ToString()) : default;
    }
}