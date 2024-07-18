using CommunityToolkit.Mvvm.ComponentModel;

using System.Collections.Generic;
using System.Data;
using ViewModel.Interfaces;
using ViewModel.Interfaces.Services;
using ViewModel.Interfaces.Services.Messages;
using ViewModel.Services;

namespace ViewModel.VMs.Request
{
    /// <summary>
    /// Класс представления модели для выполнения запросов с результатом выполнения и методом
    /// выполнения.
    /// </summary>
    public partial class RequestsVM : ObservableObject
    {
        /// <summary>
        /// Результат выполнения.
        /// </summary>
        [ObservableProperty]
        private DataTable executingResult;

        /// <summary>
        /// Создатель контекста базы данных.
        /// </summary>
        private ISession _dbContextCreator;

        /// <summary>
        /// Сервис послания сообщений.
        /// </summary>
        protected IMessengerService _messengerService;

        /// <summary>
        /// Сервис ресурсов.
        /// </summary>
        protected IResourceService _resourceService;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="RequestsVM"/>.
        /// </summary>
        /// <param name="dbContextBuilder">Создатель контекста базы данных.</param>
        /// <param name="resourceService">Сервис ресурсов.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        public RequestsVM(ISession dbContextBuilder, IResourceService resourceService,
            IMessageService messageService)
        {
            _resourceService = resourceService;
            _messengerService = new MessengerService(_resourceService, messageService);
            _dbContextCreator = dbContextBuilder;
        }

        /// <summary>
        /// Выполняет команду.
        /// </summary>
        /// <param name="сommandString">Строка команды.</param>
        protected void ExecuteCommand(string сommandString, Dictionary<string, object>? parameters = null) =>
            _messengerService.ExecuteWithExceptionMessage(() =>
                ExecutingResult = _dbContextCreator.DbContext.ExecuteCommand(сommandString, parameters));
    }
}