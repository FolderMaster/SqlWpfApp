using CommunityToolkit.Mvvm.ComponentModel;

using System.Collections.Generic;
using System.Data;

using ViewModel.Interfaces;
using ViewModel.Interfaces.Services;
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
        private ISession _session;

        /// <summary>
        /// Сервис послания сообщений.
        /// </summary>
        protected IMessageService _messageService;

        /// <summary>
        /// Сервис ресурсов.
        /// </summary>
        protected IResourceService _resourceService;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="RequestsVM"/>.
        /// </summary>
        /// <param name="session">Создатель контекста базы данных.</param>
        /// <param name="resourceService">Сервис ресурсов.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        public RequestsVM(ISession session, IResourceService resourceService,
            IMessageService messageService)
        {
            _session = session;
            _messageService = messageService;
            _resourceService = resourceService;
        }

        /// <summary>
        /// Выполняет команду.
        /// </summary>
        /// <param name="сommandString">Строка команды.</param>
        protected void ExecuteCommand(string сommandString,
            IDictionary<string, object>? parameters = null) =>
            MessengerService.ExecuteWithExceptionMessage(_resourceService, _messageService, () =>
                ExecutingResult = _session.DbContext.ExecuteCommand(сommandString, parameters));
    }
}