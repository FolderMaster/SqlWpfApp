namespace ViewModel.Interfaces
{
    /// <summary>
    /// Интерфейс конфигурации с методами сохранения и загрузки, строкой подключения к базе данных.
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        /// Возращает и задаёт строку подключения к базе данных.
        /// </summary>
        public string DataBaseConnectionString { get; set; }

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