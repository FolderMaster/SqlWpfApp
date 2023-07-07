using System.Windows;
using ViewModel.Interfaces.Services.Messages;

namespace View.Implementations.MessageBoxes
{
    /// <summary>
    /// Класс сервиса окна сообщения ошибки с методом показа сообщения. Реализует
    /// <see cref="IMessageService"/>.
    /// </summary>
    public class ErrorMessageBoxService : IMessageService
    {
        /// <summary>
        /// Показывает сообщение.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <param name="title">Заголовок.</param>
        /// <returns>Результат показа сообщения.</returns>
        public bool ShowMessage(string message, string title) => MessageBox.Show(message, title,
            MessageBoxButton.OK, MessageBoxImage.Error) == MessageBoxResult.OK;
    }
}