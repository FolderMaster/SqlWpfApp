using System.Windows;
using ViewModel.Interfaces.Services;

namespace View.Implementations.MessageBoxes
{
    /// <summary>
    /// Класс сервиса окна сообщения вопроса с методом показа сообщения. Реализует
    /// <see cref="IMessageService"/>.
    /// </summary>
    public class QuestionMessageBoxService : BaseMessageBoxService
    {
        public QuestionMessageBoxService() : base(MessageBoxButton.YesNo,
            MessageBoxImage.Question, MessageBoxResult.Yes) { }
    }
}