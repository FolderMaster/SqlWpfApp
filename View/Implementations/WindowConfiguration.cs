using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Windows;

using ViewModel.Classes;
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
        public ObservableCollection<Connection> Connections { get; set; }

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

        public void SaveConnections()
        {
            foreach (var key in _settings.AllKeys.Where((k) => k.StartsWith(nameof(Connections))))
            {
                _settings.Remove(key);
            }
            for (var i = 0; i < Connections.Count; ++i)
            {
                var connection = Connections[i];
                var connectionKey = $"{nameof(Connections)}[{i}].";

                SetConfigurationValue($"{connectionKey}{nameof(Connection.DataSource)}",
                    connection.DataSource);
                SetConfigurationValue($"{connectionKey}{nameof(Connection.InitialCatalog)}",
                    connection.InitialCatalog);
                SetConfigurationValue($"{connectionKey}{nameof(Connection.IsColumnEncryption)}",
                    connection.IsColumnEncryption);
                SetConfigurationValue($"{connectionKey}{nameof(Connection.IsTlsConnection)}",
                    connection.IsTlsConnection);
                SetConfigurationValue
                    ($"{connectionKey}{nameof(Connection.IsTrustServerCertificate)}",
                    connection.IsTrustServerCertificate);
                for(var n = 0; n < connection.Credentials.Count; ++n)
                {
                    var credential = connection.Credentials[n];
                    var credentialKey = $"{nameof(Connection.Credentials)}[{n}].";
                    SetConfigurationValue
                        ($"{connectionKey}{credentialKey}{nameof(Credential.User)}",
                        credential.User);
                    SetConfigurationValue
                        ($"{connectionKey}{credentialKey}{nameof(Credential.Password)}",
                        credential.Password);
                }
            }
        }

        public void LoadConnections()
        {
            Connections = new ObservableCollection<Connection>();
            var i = 0;
            var connectionKey = $"{nameof(Connections)}[{i}].";
            var isExistConnection = _settings.AllKeys.Any((k) => k.StartsWith(connectionKey));
            while (isExistConnection)
            {
                var connection = new Connection();
                AssignByConfigurationValue($"{connectionKey}{nameof(Connection.DataSource)}",
                    (v) => connection.DataSource = v);
                AssignByConfigurationValue($"{connectionKey}{nameof(Connection.InitialCatalog)}",
                    (v) => connection.InitialCatalog = v);
                AssignByConfigurationValue
                    ($"{connectionKey}{nameof(Connection.IsColumnEncryption)}",
                    (v) => connection.IsColumnEncryption = bool.Parse(v));
                AssignByConfigurationValue($"{connectionKey}{nameof(Connection.IsTlsConnection)}",
                        (v) => connection.IsTlsConnection = bool.Parse(v));
                AssignByConfigurationValue
                    ($"{connectionKey}{nameof(Connection.IsTrustServerCertificate)}",
                    (v) => connection.IsTrustServerCertificate = bool.Parse(v));
                
                var n = 0;
                var credentialKey = $"{nameof(Connection.Credentials)}[{n}].";
                var isExistCredential = _settings.AllKeys.Any
                    ((k) => k.StartsWith($"{connectionKey}{credentialKey}"));
                while (isExistCredential)
                {
                    var credential = new Credential();
                    AssignByConfigurationValue
                        ($"{connectionKey}{credentialKey}{nameof(Credential.User)}",
                        (v) => credential.User = v);
                    AssignByConfigurationValue
                        ($"{connectionKey}{credentialKey}{nameof(Credential.Password)}",
                        (v) => credential.Password = v);
                    connection.Credentials.Add(credential);

                    ++n;
                    credentialKey = $"{nameof(Connection.Credentials)}[{n}].";
                    isExistCredential = _settings.AllKeys.Any
                        ((k) => k.StartsWith($"{connectionKey}{credentialKey}"));
                }
                Connections.Add(connection);

                ++i;
                connectionKey = $"{nameof(Connections)}[{i}].";
                isExistConnection = _settings.AllKeys.Any((k) => k.StartsWith(connectionKey));
            }
        }

        /// <summary>
        /// Сохраняет.
        /// </summary>
        public void Save()
        {
            SaveConnections();
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
            LoadConnections();
            AssignByConfigurationValue(nameof(Left), (value) => Left = double.Parse(value));
            AssignByConfigurationValue(nameof(Top), (value) => Top = double.Parse(value));
            AssignByConfigurationValue(nameof(Width), (value) => Width = double.Parse(value));
            AssignByConfigurationValue(nameof(Height), (value) => Height = double.Parse(value));
            AssignByConfigurationValue(nameof(WindowState), (value) =>
                WindowState = (WindowState)Enum.Parse(typeof(WindowState), value));
        }
    }
}