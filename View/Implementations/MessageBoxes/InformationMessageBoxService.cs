using System.Windows;

using ViewModel.Interfaces;

namespace View.Implementations.MessageBoxes
{
    public class InformationMessageBoxService : IMessageService
    {
        public bool ShowMessage(string message, string title) => MessageBox.Show(message, title,
            MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK;
    }
}