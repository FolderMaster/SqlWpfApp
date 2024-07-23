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

namespace View.Implementations.Configuration
{
    /// <summary>
    /// Класс конфигурации окна cо строкой подключения к базе данных, положение слева, сверху,
    /// шириной, высотой, состоянием окна. Реализует <see cref="IConfiguration"/>.
    /// </summary>
    public class WindowConfiguration : IConfiguration
    {
        private static readonly string _filePath = "settings.txt";

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
        private Window _window;

        private IFileService _fileService;

        private IEnumerable<IDbConnection> _connections;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="WindowConfiguration"/>.
        /// </summary>
        /// <param name="window">Окно.</param>
        public WindowConfiguration(MainWindow window, IFileService fileService,
            IEnumerable<IDbConnection> connections)
        {
            _window = window;
            _fileService = fileService;
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
            _fileService.Save(_filePath, bytes);
        }

        /// <summary>
        /// Загружает.
        /// </summary>
        public void Load()
        {
            var bytes = _fileService.Load(_filePath);
            var text = Encoding.Default.GetString(bytes);
            var data = JsonConvert.DeserializeObject<FileData>(text, _jsonSerializerSettings);

            data.WindowData.SetData(_window);
            foreach (var connection in _connections)
            {
                var connectionData = data.ConnectionData.FirstOrDefault
                    (d => d.ConnectionType == connection.GetType());
                if(connectionData != null)
                {
                    connection.Data = connectionData.Data;
                }
            }
        }
    }
}