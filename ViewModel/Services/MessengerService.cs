using System;

using ViewModel.Interfaces.Services;

namespace ViewModel.Services
{
    /// <summary>
    /// Класс сервиса послания сообщений с методами показа сообщения и исполнения действия с
    /// сообщением о исключении.
    /// </summary>
    public static class MessengerService
    {
        /// <summary>
        /// Ключ к ресурсу заголовка ошибки.
        /// </summary>
        private static string _errorTitleResourceKey = "ErrorMessageTitle";

        /// <summary>
        /// Показывает сообщение.
        /// </summary>
        /// <param name="messageService">Сервис сообщений.</param>
        /// <param name="titleResourceKey">Ключ к ресурсу заголовка.</param>
        /// <param name="descriptionResourceKey">Ключ к ресурсу описания.</param>
        /// <returns>Результат показа сообщения.</returns>
        public static bool ShowErrorMessage(IMessageService messageService,
            IResourceService resourceService, string descriptionResourceKey) =>
            messageService.ShowMessage(resourceService.GetString(descriptionResourceKey),
                resourceService.GetString(_errorTitleResourceKey));

        /// <summary>
        /// Исполняет действие с сообщением о исключении.
        /// </summary>
        /// <param name="action">Действие.</param>
        /// <param name="errorAction">Действие после исключения.</param>
        public static void ExecuteWithExceptionMessage(IResourceService resourceService,
            IMessageService errorMessageService, Action action, Action? errorAction = null)
        {
            try
            {
                action?.Invoke();
            }
            catch (Exception ex)
            {
                errorMessageService.ShowMessage($"{ex.Message}\n{ex.InnerException}",
                    resourceService.GetString(_errorTitleResourceKey));
                errorAction?.Invoke();
            }
        }
    }
}