namespace ViewModel.Interfaces.Services.Files
{
    /// <summary>
    /// Интерфейс файлового сервиса с методами сохранения и загрузки, получения полного пути,
    /// проверки пути.
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Сохраняет данные.
        /// </summary>
        /// <param name="path">Путь к сохранению.</param>
        /// <param name="data">Данные.</param>
        public void Save(string path, byte[] data);

        /// <summary>
        /// Загружает данные.
        /// </summary>
        /// <param name="path">Путь к данным.</param>
        /// <returns>Загруженные данные.</returns>
        public byte[] Load(string path);

        public void Delete(string path);

        /// <summary>
        /// Возвращает полный путь.
        /// </summary>
        /// <param name="path">Путь.</param>
        /// <returns>Полный путь.</returns>
        public string GetFullPath(string path);

        /// <summary>
        /// Проверяет на существование заданного пути.
        /// </summary>
        /// <param name="path">Путь.</param>
        /// <returns>Возвращает true, когда путь существует,
        /// false, когда его не существует.</returns>
        public bool IsPathExists(string path);
    }
}