using System.IO;

using ViewModel.Interfaces;

namespace ViewModel.Dependencies
{
    /// <summary>
    /// Класс сервиса путей с методом получения полного пути. Реализует <see cref="IPathService"/>.
    /// </summary>
    public class PathService : IPathService
    {
        /// <summary>
        /// Возвращает полный путь.
        /// </summary>
        /// <param name="path">Путь.</param>
        /// <returns>Полный путь.</returns>
        public string GetFullPath(string path) => Path.GetFullPath(path);
    }
}
