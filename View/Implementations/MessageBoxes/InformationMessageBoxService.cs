using System.Windows;
using ViewModel.Interfaces.Services;

namespace View.Implementations.MessageBoxes
{
    /// <summary>
    /// Класс сервиса окна сообщения информации с методом показа сообщения. Реализует
    /// <see cref="IMessageService"/>.
    /// </summary>
    public class InformationMessageBoxService : BaseMessageBoxService
    {
        public InformationMessageBoxService() : base(MessageBoxButton.OK,
            MessageBoxImage.Information, MessageBoxResult.OK) { }
    }
}