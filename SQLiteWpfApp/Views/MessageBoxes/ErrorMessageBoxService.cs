using System.Windows;

using SQLiteWpfApp.ViewModels.Services;

namespace SQLiteWpfApp.Views.MessageBoxes
{
    public class ErrorMessageBoxService : IMessageService
    {
        public bool ShowMessage(string message, string title) => MessageBox.Show(message, title,
            MessageBoxButton.OK, MessageBoxImage.Error) == MessageBoxResult.OK;
    }
}