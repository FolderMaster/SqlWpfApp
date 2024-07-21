namespace ViewModel.Interfaces
{
    /// <summary>
    /// Интерфейс конфигурации с методами сохранения и загрузки.
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        /// Сохраняет.
        /// </summary>
        public void Save();

        /// <summary>
        /// Загружает.
        /// </summary>
        public void Load();
    }
}