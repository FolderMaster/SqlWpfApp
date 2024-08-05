using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

using View.Windows;

using ViewModel.Interfaces;
using ViewModel.Interfaces.DataBase;
using ViewModel.Interfaces.Services;
using ViewModel.Interfaces.Services.Data;
using ViewModel.Interfaces.Services.Files;
using ViewModel.Services;

namespace View.Implementations.Configuration
{
    /// <summary>
    /// Класс конфигурации окна cо строкой подключения к базе данных, положение слева, сверху,
    /// шириной, высотой, состоянием окна. Реализует <see cref="IConfiguration"/>.
    /// </summary>
    public class WindowConfiguration : IConfiguration
    {
        private static readonly string _settingsPath = "settings.json";

        private static readonly string _savePath = "save.aes";

        /// <summary>
        /// Окно.
        /// </summary>
        private readonly Window _window;

        private readonly IFileService _fileService;

        private readonly IMessageService _messageService;

        private readonly IResourceService _resourceService;

        private readonly ISerializer _serializer;

        private readonly IEncryptionService _encryptionService;

        private readonly IEnumerable<IDbConnection> _connections;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="WindowConfiguration"/>.
        /// </summary>
        /// <param name="window">Окно.</param>
        public WindowConfiguration(MainWindow window, IFileService fileService,
            IMessageService messageService, IResourceService resourceService,
            ISerializer serializer, IEncryptionService encryptionService,
            IEnumerable<IDbConnection> connections)
        {
            _window = window;
            _fileService = fileService;
            _messageService = messageService;
            _resourceService = resourceService;
            _serializer = serializer;
            _encryptionService = encryptionService;
            _connections = connections;
            
        }

        /// <summary>
        /// Сохраняет.
        /// </summary>
        public void Save()
        {
            var windowData = new WindowData(_window);
            SaveData(windowData, _settingsPath);

            var connectionsData = new Dictionary<Type, object>();
            foreach (var connection in _connections)
            {
                connectionsData.Add(connection.GetType(), connection.Data);
            }
            SaveData(connectionsData, _savePath, true);
        }

        /// <summary>
        /// Загружает.
        /// </summary>
        public void Load()
        {
            if (_fileService.IsPathExists(_settingsPath))
            {
                var windowData = LoadData<WindowData>(_settingsPath);
                if (windowData != null)
                {
                    windowData.SetData(_window);
                }
            }

            if (_fileService.IsPathExists(_savePath))
            {
                var connectionsData = LoadData<IDictionary<Type, object>>(_savePath, true);
                if (connectionsData != null)
                {
                    foreach (var connection in _connections)
                    {
                        object data = default;
                        if (connectionsData.TryGetValue(connection.GetType(), out data))
                        {
                            connection.Data = data;
                        }
                    }
                }
            }
        }

        private void SaveData(object value, string filePath, bool isEncrypt = false)
        {
            MessengerService.ExecuteWithExceptionMessage(_resourceService, _messageService, () =>
            {
                var bytes = _serializer.Serialize(value);
                if (isEncrypt)
                {
                    var encryptedBytes = _encryptionService.Encrypt(bytes);
                    bytes = encryptedBytes;
                }
                _fileService.Save(filePath, bytes);
            });
        }

        private T? LoadData<T>(string filePath, bool isEncrypted = false)
        {
            T? result = default;
            MessengerService.ExecuteWithExceptionMessage(_resourceService, _messageService, () =>
            {
                var bytes = _fileService.Load(filePath);
                if (isEncrypted)
                {
                    var decryptedBytes = _encryptionService.Decrypt(bytes);
                    bytes = decryptedBytes;
                }
                result = _serializer.Deserialize<T>(bytes);
            });
            return result;
        }
    }
}