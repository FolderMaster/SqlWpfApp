using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using System;
using System.Collections.Generic;

using ViewModel.Interfaces;
using ViewModel.Interfaces.Proces;
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
        /// Процедуры.
        /// </summary>
        public IEnumerable<IProc> Proces { get; private set; }

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
        /// <param name="dbContextBuilder">Создатель контекста базы данных.</param>
        /// <param name="resourceService">Сервис ресурсов.</param>
        /// <param name="configuration">Конфигурация.</param>
        /// <param name="errorMessageService">Сервис сообщений об ошибках.</param>
        public MainVM(ISession dbContextBuilder, IResourceService resourceService,
            IConfiguration configuration, IMessageService errorMessageService, IEnumerable<IProc> proces)
        {
            _configuration = configuration;
            _messengerService = new MessengerService(resourceService, errorMessageService);
            Proces = proces;

            /**
            private static string _errorTitleResourceKey = "ErrorMessageTitle";

            private static string _errorConnectionDescriptionResourceKey =
                "ErrorConnectionMessageDescription";
            dbContextBuilder.ResultConnectionCreated += (object? sender, EventArgs e) =>
            {
                if (dbContextBuilder.Result == null)
                {
                    IsDbConnected = false;
                }
                else
                {
                    IsDbConnected = dbContextBuilder.Result.CanConnect();
                    if (!IsDbConnected)
                    {
                        _messengerService.ShowMessage(errorMessageService, _errorTitleResourceKey,
                            _errorConnectionDescriptionResourceKey);
                    }
                }
            };**/

            SaveCommand = new RelayCommand(() =>
                _messengerService.ExecuteWithExceptionMessage(_configuration.Save));
            LoadCommand = new RelayCommand(() =>
                _messengerService.ExecuteWithExceptionMessage(_configuration.Load));

            LoadCommand.Execute(null);
        }
    }
}