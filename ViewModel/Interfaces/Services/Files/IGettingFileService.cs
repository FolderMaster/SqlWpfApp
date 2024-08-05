using System.Collections.Generic;

using ViewModel.Classes;

namespace ViewModel.Interfaces.Services.Files
{
    /// <summary>
    /// Интерфейс сервиса получения файла с методом получения пути к файлу.
    /// </summary>
    public interface IGettingFileService
    {
        /// <summary>
        /// Получает путь к файлу.
        /// </summary>
        /// <param name="filter">Фильтр для файлов.</param>
        /// <returns>Путь к файлу.</returns>
        public string? GetFilePath(IEnumerable<FileFormat>? filter = null);
    }
}