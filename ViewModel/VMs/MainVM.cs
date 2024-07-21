using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using ViewModel.Interfaces;
using ViewModel.Interfaces.Services;
using ViewModel.Interfaces.Services.Messages;
using ViewModel.Services;

namespace ViewModel.VMs
{
    /// <summary>
    /// Класс основного представления модели с командами.
    /// </summary>
    public partial class MainVM : ObservableObject
    {
        /// <summary>
        /// Сервис послания сообщений.
        /// </summary>
        private IMessengerService _messengerService;

        /// <summary>
        /// Конфигурация.
        /// </summary>
        private IConfiguration _configuration;

        /// <summary>
        /// Возвращает и задаёт команду сохранения.
        /// </summary>
        public RelayCommand SaveCommand { get; private set; }

        /// <summary>
        /// Возвращает и задаёт команду загрузки.
        /// </summary>
        public RelayCommand LoadCommand { get; private set; }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="MainVM"/>.
        /// </summary>
        /// <param name="resourceService">Сервис ресурсов.</param>
        /// <param name="configuration">Конфигурация.</param>
        /// <param name="errorMessageService">Сервис сообщений об ошибках.</param>
        public MainVM(IResourceService resourceService,
            IConfiguration configuration, IMessageService errorMessageService)
        {
            _configuration = configuration;
            _messengerService = new MessengerService(resourceService, errorMessageService);

            SaveCommand = new RelayCommand(() =>
                _messengerService.ExecuteWithExceptionMessage(_configuration.Save));
            LoadCommand = new RelayCommand(() =>
                _messengerService.ExecuteWithExceptionMessage(_configuration.Load));

            LoadCommand.Execute(null);
        }
    }
}