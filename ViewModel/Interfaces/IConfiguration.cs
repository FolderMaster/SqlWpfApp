using System.Collections.ObjectModel;
using ViewModel.Classes;

namespace ViewModel.Interfaces
{
    /// <summary>
    /// Интерфейс конфигурации с методами сохранения и загрузки, строкой подключения к базе данных.
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        /// Возвращает и задаёт строку подключения к базе данных.
        /// </summary>
        public ObservableCollection<Connection> Connections { get; set; }

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