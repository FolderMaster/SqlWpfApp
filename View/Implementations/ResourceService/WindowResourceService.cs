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
        /// Возвращает ресурс.
        /// </summary>
        /// <param name="resourceKey">Ключ ресурса.</param>
        /// <returns>Ресурс.</returns>
        public object GetResource(string resourceKey) =>
            Application.Current.Resources[resourceKey];

        /// <summary>
        /// Возвращает строку.
        /// </summary>
        /// <param name="stringKey">Ключ строки.</param>
        /// <returns>Строка.</returns>
        public string GetString(string stringKey) => GetResource(stringKey) as string;

        /// <summary>
        /// Возвращает заголовок.
        /// </summary>
        /// <param name="headerKey">Ключ заголовка.</param>
        /// <returns>Заголовок.</returns>
        public string GetHeader(string headerKey) => GetString(headerKey + _headerEndKey);

        /// <summary>
        /// Возвращает иконку.
        /// </summary>
        /// <param name="iconKey">Ключ иконки.</param>
        /// <returns>Иконка.</returns>
        public BitmapSource GetIcon(string iconKey) =>
            GetResource(iconKey + _iconEndKey) as BitmapSource;
    }
}