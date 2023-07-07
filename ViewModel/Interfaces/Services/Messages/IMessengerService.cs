using System;

namespace ViewModel.Interfaces.Services.Messages
{
    /// <summary>
    /// Интерфейс сервиса послания сообщений с методами показа сообщения и исполнения действия с
    /// сообщением о исключении.
    /// </summary>
    public interface IMessengerService
    {
        /// <summary>
        /// Показывает сообщение.
        /// </summary>
        /// <param name="messageService">Сервис сообщений.</param>
        /// <param name="titleResourceKey">Ключ к ресурсу заголовка.</param>
        /// <param name="descriptionResourceKey">Ключ к ресурсу описания.</param>
        /// <returns>Результат показа сообщения.</returns>
        public bool ShowMessage(IMessageService messageService, string titleResourceKey,
            string descriptionResourceKey);

        /// <summary>
        /// Исполняет действие с сообщением о исключении.
        /// </summary>
        /// <param name="action">Действие.</param>
        /// <param name="errorAction">Действие после исключения.</param>
        public void ExecuteWithExceptionMessage(Action action, Action? errorAction = null);
    }
}