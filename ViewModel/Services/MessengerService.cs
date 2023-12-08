using System;

using ViewModel.Interfaces.Services;
using ViewModel.Interfaces.Services.Messages;

using SystemSqlException = System.Data.SqlClient.SqlException;

namespace ViewModel.Services
{
    /// <summary>
    /// Класс сервиса послания сообщений с методами показа сообщения и исполнения действия с
    /// сообщением о исключении. Реализует <see cref="IMessengerService"/>.
    /// </summary>
    public class MessengerService : IMessengerService
    {
        /// <summary>
        /// Ключ к ресурсу заголовка ошибки.
        /// </summary>
        private static string _errorTitleResourceKey = "ErrorMessageTitle";

        /// <summary>
        /// Сервис ресурсов.
        /// </summary>
        private IResourceService _resourceService;

        /// <summary>
        /// Сервис сообщений об ошибках.
        /// </summary>
        private IMessageService _errorMessageService;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="MessengerService"/>.
        /// </summary>
        /// <param name="resourceService">Сервис ресурсов.</param>
        /// <param name="errorMessageService">Сервис сообщений об ошибках.</param>
        public MessengerService(IResourceService resourceService,
            IMessageService errorMessageService)
        {
            _resourceService = resourceService;
            _errorMessageService = errorMessageService;
        }

        /// <summary>
        /// Показывает сообщение.
        /// </summary>
        /// <param name="messageService">Сервис сообщений.</param>
        /// <param name="titleResourceKey">Ключ к ресурсу заголовка.</param>
        /// <param name="descriptionResourceKey">Ключ к ресурсу описания.</param>
        /// <returns>Результат показа сообщения.</returns>
        public bool ShowMessage(IMessageService messageService, string titleResourceKey,
            string descriptionResourceKey) =>
            messageService.ShowMessage(_resourceService.GetString(descriptionResourceKey),
                _resourceService.GetString(titleResourceKey));

        /// <summary>
        /// Исполняет действие с сообщением о исключении.
        /// </summary>
        /// <param name="action">Действие.</param>
        /// <param name="errorAction">Действие после исключения.</param>
        public void ExecuteWithExceptionMessage(Action action, Action? errorAction = null)
        {

            try
            {
                action?.Invoke();
            }
            catch (Exception ex)
            {
                _errorMessageService.ShowMessage(ex.Message + ex.InnerException,
                    _resourceService.GetString(_errorTitleResourceKey));
                errorAction?.Invoke();
            }
        }
    }
}