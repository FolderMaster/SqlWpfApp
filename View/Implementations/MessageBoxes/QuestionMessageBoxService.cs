using System.Windows;
using ViewModel.Interfaces.Services.Messages;

namespace View.Implementations.MessageBoxes
{
    /// <summary>
    /// Класс сервиса окна сообщения вопроса с методом показа сообщения. Реализует
    /// <see cref="IMessageService"/>.
    /// </summary>
    public class QuestionMessageBoxService : IMessageService
    {
        /// <summary>
        /// Показывает сообщение.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <param name="title">Заголовок.</param>
        /// <returns>Результат показа сообщения.</returns>
        public bool ShowMessage(string message, string title) => MessageBox.Show(message, title,
            MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
    }
}