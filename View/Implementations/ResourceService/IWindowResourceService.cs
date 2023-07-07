using System.Windows.Media.Imaging;
using ViewModel.Interfaces.Services;

namespace View.Implementations.ResourceService
{
    /// <summary>
    /// Интерфейс сервиса ресурсов окна с методами получения ресурсов, строк, иконок и заголовков.
    /// Дополняет <see cref="IResourceService"/>.
    /// </summary>
    public interface IWindowResourceService : IResourceService
    {
        /// <summary>
        /// Возвращает иконку.
        /// </summary>
        /// <param name="iconKey">Ключ иконки.</param>
        /// <returns>Иконка.</returns>
        public BitmapSource GetIcon(string iconKey);

        /// <summary>
        /// Возвращает заголовок.
        /// </summary>
        /// <param name="headerKey">Ключ заголовка.</param>
        /// <returns>Заголовок.</returns>
        public string GetHeader(string headerKey);
    }
}