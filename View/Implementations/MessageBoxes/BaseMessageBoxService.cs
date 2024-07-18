using System.Windows;

using ViewModel.Interfaces.Services.Messages;

namespace View.Implementations.MessageBoxes
{
    public class BaseMessageBoxService : IMessageService
    {
        private readonly MessageBoxButton _buttons;

        private readonly MessageBoxImage _icon;

        private readonly MessageBoxOptions _options;

        private readonly MessageBoxResult _trueResult;

        public BaseMessageBoxService(MessageBoxButton buttons, MessageBoxImage icon,
            MessageBoxResult trueResult, MessageBoxOptions options = MessageBoxOptions.None)
        {
            _buttons = buttons;
            _icon = icon;
            _options = options;
            _trueResult = trueResult;
        }

        public bool ShowMessage(string message, string title) =>
            MessageBox.Show(message, title, _buttons, _icon) == _trueResult;
    }
}
