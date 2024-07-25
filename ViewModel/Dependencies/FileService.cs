using System.IO;

using ViewModel.Interfaces.Services.Files;

namespace ViewModel.Dependencies
{
    /// <summary>
    /// Класс файлового сервиса с методами сохранения и загрузки, получения полного пути,
    /// проверки пути. Реализует <see cref="IFileService"/>.
    /// </summary>
    public class FileService : IFileService
    {
        /// <inheritdoc/>
        public byte[] Load(string path) => File.ReadAllBytes(path);

        /// <inheritdoc/>
        public void Save(string path, byte[] data) => File.WriteAllBytes(path, data);

        /// <inheritdoc/>
        public string GetFullPath(string path) => Path.GetFullPath(path);

        /// <inheritdoc/>
        public bool IsPathExists(string path) => Path.Exists(path);
    }
}