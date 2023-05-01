using System.Windows;

using SQLiteWpfApp.ViewModels;

namespace SQLiteWpfApp.Views.MessageBoxes
{
    class InformationMessageBoxService : IMessageService
    {
        public bool ShowMessage(string message, string title) => MessageBox.Show(message, title,
            MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK;
    }
}