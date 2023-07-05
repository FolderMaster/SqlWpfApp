namespace ViewModel.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса получения файла.
    /// </summary>
    public interface IGettingFileService
    {
        /// <summary>
        /// Получает путь к файлу.
        /// </summary>
        /// <param name="filter">Фильтр для файлов.</param>
        /// <returns>Путь к файлу.</returns>
        public string? GetFilePath(string? filter = null);
    }
}