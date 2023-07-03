using System;
using System.Windows;

using ViewModel.Interfaces;

namespace View.Implementations.Proces
{
    public abstract class WindowProc : IProc
    {
        private Window? _window = null;

        private IDbContextCreator _dbContextCreator;

        private IMessageService _messageService;

        private Window Window
        {
            get => _window ??= CreateConfiguredWindow();
        }

        protected WindowProc(IDbContextCreator dbContextCreator,
            IMessageService messageService)
        {
            _dbContextCreator = dbContextCreator;
            _messageService = messageService;
        } 

        private Window CreateConfiguredWindow()
        {
            var window = CreateWindow(_dbContextCreator, _messageService);
            window.Closed += Window_Closed;
            return window;
        }

        protected abstract Window CreateWindow(IDbContextCreator dbContextCreator,
            IMessageService messageService);

        public void Invoke()
        {
            var window = Window;
            if (window.IsVisible)
            {
                window.Activate();
            }
            else
            {
                window.Show();
            }
        }

        private void Window_Closed(object? sender, EventArgs e) => _window = null;
    }
}