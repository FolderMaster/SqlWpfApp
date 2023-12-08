using System;
using System.Configuration;
using System.Windows;

using ViewModel.Interfaces;

namespace View.Implementations
{
    /// <summary>
    /// Класс конфигурации окна cо строкой подключения к базе данных, положение слева, сверху,
    /// шириной, высотой, состоянием окна. Реализует <see cref="IConfiguration"/>.
    /// </summary>
    public class WindowConfiguration : IConfiguration
    {
        /// <summary>
        /// Конфигурация.
        /// </summary>
        private Configuration _configuration;

        /// <summary>
        /// Настройки.
        /// </summary>
        private KeyValueConfigurationCollection _settings;

        /// <summary>
        /// Окно.
        /// </summary>
        private Window _window;

        /// <summary>
        ///  Возвращает и задаёт строку подключения к базе данных.
        /// </summary>
        public string DataBaseConnectionString { get; set; } =
            //"Data Source=UniversityDataBase.db;Mode=ReadWrite"
            "Server=(localdb)\\mssqllocaldb;Database=UniversityDb;User Id=deaneryEmployee1;Password=1056";

        /// <summary>
        /// Возвращает и задаёт положение окна слева.
        /// </summary>
        public double Left
        {
            get => _window.Left;
            set => _window.Left = value;
        }

        /// <summary>
        /// Возвращает и задаёт положение окна сверху.
        /// </summary>
        public double Top
        {
            get => _window.Top;
            set => _window.Top = value;
        }

        /// <summary>
        /// Возвращает и задаёт ширину окна.
        /// </summary>
        public double Width
        {
            get => _window.Width;
            set => _window.Width = value;
        }

        /// <summary>
        /// Возвращает и задаёт высоту окна.
        /// </summary>
        public double Height
        {
            get => _window.Height;
            set => _window.Height = value;
        }

        /// <summary>
        /// Возвращает и задаёт состояние окна.
        /// </summary>
        public WindowState WindowState
        {
            get => _window.WindowState;
            set => _window.WindowState = value;
        }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="WindowConfiguration"/>.
        /// </summary>
        /// <param name="window">Окно.</param>
        public WindowConfiguration(Window window)
        {
            _configuration =
                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            _settings = _configuration.AppSettings.Settings;
            _window = window;
        }

        /// <summary>
        /// Задаёт конфигурационное значение.
        /// </summary>
        /// <param name="key">Ключ.</param>
        /// <param name="value">Значение.</param>
        private void SetConfigurationValue(string key, object value)
        {
            if (_settings[key] == null)
            {
                _settings.Add(key, value.ToString());
            }
            else
            {
                _settings[key].Value = value.ToString();
            }
        }

        /// <summary>
        /// Присваивает конфигурационным значением.
        /// </summary>
        /// <param name="key">Ключ.</param>
        /// <param name="assign">Действие присваивания.</param>
        private void AssignByConfigurationValue(string key, Action<string> assign)
        {
            string? value = _settings[key] != null ? _settings[key].Value : null;
            if (value != null)
            {
                assign(value);
            }
        }

        /// <summary>
        /// Сохраняет.
        /// </summary>
        public void Save()
        {
            SetConfigurationValue(nameof(DataBaseConnectionString), DataBaseConnectionString);
            SetConfigurationValue(nameof(Left), Left);
            SetConfigurationValue(nameof(Top), Top);
            SetConfigurationValue(nameof(Width), Width);
            SetConfigurationValue(nameof(Height), Height);
            SetConfigurationValue(nameof(WindowState), WindowState);
            _configuration.Save();
        }

        /// <summary>
        /// Загружает.
        /// </summary>
        public void Load()
        {
            AssignByConfigurationValue(nameof(DataBaseConnectionString),
                (value) => DataBaseConnectionString = value);
            AssignByConfigurationValue(nameof(Left), (value) => Left = double.Parse(value));
            AssignByConfigurationValue(nameof(Top), (value) => Top = double.Parse(value));
            AssignByConfigurationValue(nameof(Width), (value) => Width = double.Parse(value));
            AssignByConfigurationValue(nameof(Height), (value) => Height = double.Parse(value));
            AssignByConfigurationValue(nameof(WindowState), (value) =>
                WindowState = (WindowState)Enum.Parse(typeof(WindowState), value));
        }
    }
}