using System.Windows;

using SQLiteWpfApp.ViewModels.Services;

namespace SQLiteWpfApp.Views.MessageBoxes
{
    public class InformationMessageBoxService : IMessageService
    {
        public bool ShowMessage(string message, string title) => MessageBox.Show(message, title,
            MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK;
    }
}