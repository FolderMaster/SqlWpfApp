using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

using View.Implementations.Configuration.Data;
using View.Windows;

using ViewModel.Interfaces;
using ViewModel.Interfaces.DataBase;
using ViewModel.Interfaces.Services;
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
        private static readonly string _settingsPath = "settings.cfg";

        private static readonly string _savePath = "save.aes";

        /// <summary>
        /// Настройки Json-сериализатора.
        /// </summary>
        private static readonly JsonSerializerSettings _jsonSerializerSettings =
            new JsonSerializerSettings()
        {
            ObjectCreationHandling = ObjectCreationHandling.Replace,
            TypeNameHandling = TypeNameHandling.All,
            Formatting = Formatting.Indented
        };

        /// <summary>
        /// Окно.
        /// </summary>
        private readonly Window _window;

        private readonly IFileService _fileService;

        private readonly IMessageService _messageService;

        private readonly IResourceService _resourceService;

        private readonly IEncryptionService _encryptionService;

        private readonly IEnumerable<IDbConnection> _connections;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="WindowConfiguration"/>.
        /// </summary>
        /// <param name="window">Окно.</param>
        public WindowConfiguration(MainWindow window, IFileService fileService,
            IMessageService messageService, IResourceService resourceService,
            IEncryptionService encryptionService, IEnumerable<IDbConnection> connections)
        {
            _window = window;
            _fileService = fileService;
            _messageService = messageService;
            _resourceService = resourceService;
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

        private void SaveData(object data, string filePath, bool isEncrypt = false)
        {
            MessengerService.ExecuteWithExceptionMessage(_resourceService, _messageService, () =>
            {
                var text = JsonConvert.SerializeObject(data, _jsonSerializerSettings);
                var bytes = Encoding.Default.GetBytes(text);
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
                var text = Encoding.Default.GetString(bytes);
                result = JsonConvert.DeserializeObject<T>(text, _jsonSerializerSettings);
            });
            return result;
        }
    }
}