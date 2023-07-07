using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace View.Converters
{
    /// <summary>
    /// Класс конвертора перечислений и <see cref="Visibility"/>.
    /// </summary>
    public class EnumToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Конвертирует значение перечисления к <see cref="Visibility"/>.
        /// </summary>
        /// <param name="value">Значение конвертации.</param>
        /// <param name="targetType">Тип назначения конвертации.</param>
        /// <param name="parameter">Дополнительный параметр.</param>
        /// <param name="culture">Сведения о культуре.</param>
        /// <returns>Если параметр и значение совпадают, то true, иначе false.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value != null && parameter != null && value.ToString() == parameter.ToString() ?
            Visibility.Visible : Visibility.Hidden;

        /// <summary>
        /// Конвертирует значение <see cref="Visibility"/> к перечислению (не используется).
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