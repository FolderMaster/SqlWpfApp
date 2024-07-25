using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using ViewModel.Interfaces;
using ViewModel.Interfaces.Services;
using ViewModel.Services;

namespace ViewModel.VMs
{
    /// <summary>
    /// Класс основного представления модели с командами.
    /// </summary>
    public partial class MainVM : ObservableObject
    {
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
        /// <param name="configuration">Конфигурация.</param>
        public MainVM(IMessageService messengerService, IResourceService resourceService,
            IConfiguration configuration)
        {
            SaveCommand = new RelayCommand(() =>
                MessengerService.ExecuteWithExceptionMessage(resourceService,
                messengerService, configuration.Save));
            LoadCommand = new RelayCommand(() =>
                MessengerService.ExecuteWithExceptionMessage(resourceService,
                messengerService, configuration.Load));

            LoadCommand.Execute(null);
        }
    }
}