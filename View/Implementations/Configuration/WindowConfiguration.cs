using Newtonsoft.Json;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

using View.Windows;
using View.Implementations.Configuration.Data;

using ViewModel.Interfaces;
using ViewModel.Interfaces.DataBase;
using ViewModel.Interfaces.Services.Files;
using ViewModel.Interfaces.Services;
using ViewModel.Services;

namespace View.Implementations.Configuration
{
    /// <summary>
    /// Класс конфигурации окна cо строкой подключения к базе данных, положение слева, сверху,
    /// шириной, высотой, состоянием окна. Реализует <see cref="IConfiguration"/>.
    /// </summary>
    public class WindowConfiguration : IConfiguration
    {
        private static readonly string _filePath = "settings.cfg";

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
            var connectionsData = new List<ConnectionData>();
            foreach (var connection in _connections)
            {
                connectionsData.Add(new ConnectionData(connection.GetType(), connection.Data));
            }
            var data = new FileData(windowData, connectionsData);

            var text = JsonConvert.SerializeObject(data, _jsonSerializerSettings);
            var bytes = Encoding.Default.GetBytes(text);
            var encryptedBytes = _encryptionService.Encrypt(bytes);
            MessengerService.ExecuteWithExceptionMessage(_resourceService, _messageService,
                () => _fileService.Save(_filePath, encryptedBytes));
        }

        /// <summary>
        /// Загружает.
        /// </summary>
        public void Load()
        {
            if (_fileService.IsPathExists(_filePath))
            {
                FileData? data = null;
                MessengerService.ExecuteWithExceptionMessage
                    (_resourceService, _messageService, () =>
                {
                    var bytes = _fileService.Load(_filePath);
                    var decryptedBytes = _encryptionService.Decrypt(bytes);
                    var text = Encoding.Default.GetString(decryptedBytes);
                    data = JsonConvert.DeserializeObject<FileData>
                        (text, _jsonSerializerSettings);
                });

                if (data != null)
                {
                    data.WindowData.SetData(_window);
                    foreach (var connection in _connections)
                    {
                        var connectionData = data.ConnectionData.FirstOrDefault
                            (d => d.ConnectionType == connection.GetType());
                        if (connectionData != null)
                        {
                            connection.Data = connectionData.Data;
                        }
                    }
                }
            }
        }
    }
}