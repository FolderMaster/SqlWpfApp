namespace ViewModel.Interfaces
{
    /// <summary>
    /// Интерфейс файлового сервиса с методами сохранения и загрузки.
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
    }
}