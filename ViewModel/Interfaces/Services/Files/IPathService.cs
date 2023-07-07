namespace ViewModel.Interfaces.Services.Files
{
    /// <summary>
    /// Интерфейс сервиса путей с методом получения полного пути.
    /// </summary>
    public interface IPathService
    {
        /// <summary>
        /// Возвращает полный путь.
        /// </summary>
        /// <param name="path">Путь.</param>
        /// <returns>Полный путь.</returns>
        public string GetFullPath(string path);
    }
}