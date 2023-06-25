using System.Windows;

using ViewModel.Services;

namespace View.MessageBoxes
{
    public class ErrorMessageBoxService : IMessageService
    {
        public bool ShowMessage(string message, string title) => MessageBox.Show(message, title,
            MessageBoxButton.OK, MessageBoxImage.Error) == MessageBoxResult.OK;
    }
}