using System.Windows;
using ViewModel.Interfaces;

namespace View.Implementations.MessageBoxes
{
    public class QuestionMessageBoxService : IMessageService
    {
        public bool ShowMessage(string message, string title) => MessageBox.Show(message, title,
            MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
    }
}