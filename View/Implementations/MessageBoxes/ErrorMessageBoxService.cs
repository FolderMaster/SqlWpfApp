using System.Windows;
using ViewModel.Interfaces.Services;

namespace View.Implementations.MessageBoxes
{
    /// <summary>
    /// Класс сервиса окна сообщения ошибки с методом показа сообщения. Реализует
    /// <see cref="IMessageService"/>.
    /// </summary>
    public class ErrorMessageBoxService : BaseMessageBoxService
    {
        public ErrorMessageBoxService() : base(MessageBoxButton.OK,
            MessageBoxImage.Error, MessageBoxResult.OK) { }
    }
}