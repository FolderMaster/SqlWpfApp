using System.Windows;

using SQLiteWpfApp.ViewModels.Services;

namespace SQLiteWpfApp.Views.MessageBoxes
{
    public class QuestionMessageBoxService : IMessageService
    {
        public bool ShowMessage(string message, string title) => MessageBox.Show(message, title,
            MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
    }
}