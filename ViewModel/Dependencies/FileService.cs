using System.IO;
using ViewModel.Interfaces.Services.Files;

namespace ViewModel.Dependencies
{
    /// <summary>
    /// Класс файлового сервиса с методами сохранения и загрузки. Реализует 
    /// <see cref="IFileService"/>.
    /// </summary>
    public class FileService : IFileService
    {
        /// <summary>
        /// Загружает данные.
        /// </summary>
        /// <param name="path">Путь к данным.</param>
        /// <returns>Загруженные данные.</returns>
        public byte[] Load(string path) => File.ReadAllBytes(path);

        /// <summary>
        /// Сохраняет данные.
        /// </summary>
        /// <param name="path">Путь к сохранению.</param>
        /// <param name="data">Данные.</param>
        public void Save(string path, byte[] data) => File.WriteAllBytes(path, data);
    }
}