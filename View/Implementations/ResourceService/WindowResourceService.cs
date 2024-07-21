using System.Windows;
using System.Windows.Media.Imaging;

namespace View.Implementations.ResourceService
{
    /// <summary>
    /// Класс сервиса ресурсов окна с методами получения ресурсов, строк, иконок и заголовков.
    /// Реализует <see cref="IWindowResourceService"/>.
    /// </summary>
    public class WindowResourceService : IWindowResourceService
    {
        /// <summary>
        /// Конец ключа заголовка.
        /// </summary>
        private static string _headerEndKey = "Header";

        /// <summary>
        /// Конец ключа иконки.
        /// </summary>
        private static string _iconEndKey = "Icon";

        /// <summary>
        /// Конец ключа стиля.
        /// </summary>
        private static string _styleEndKey = "Style";

        /// <inheritdoc/>
        public object GetResource(string resourceKey) =>
            Application.Current.Resources[resourceKey];

        /// <inheritdoc/>
        public string GetString(string stringKey) => GetResource(stringKey) as string;

        /// <inheritdoc/>
        public string GetHeader(string headerKey) => GetString(headerKey + _headerEndKey);

        /// <inheritdoc/>
        public BitmapSource GetIcon(string iconKey) =>
            GetResource(iconKey + _iconEndKey) as BitmapSource;

        /// <inheritdoc/>
        public Style GetStyle(string styleKey) =>
            GetResource(styleKey + _styleEndKey) as Style;
    }
}