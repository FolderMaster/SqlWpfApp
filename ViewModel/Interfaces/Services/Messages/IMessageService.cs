namespace ViewModel.Interfaces.Services.Messages
{
    /// <summary>
    /// Интерфейс сервиса сообщений с методом показа сообщения.
    /// </summary>
    public interface IMessageService
    {
        /// <summary>
        /// Показывает сообщение.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <param name="title">Заголовок.</param>
        /// <returns>Результат показа сообщения.</returns>
        public bool ShowMessage(string message, string title);
    }
}