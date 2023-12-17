using System;
using System.Windows;

using View.Implementations.ResourceService;

using ViewModel.Interfaces;
using ViewModel.Interfaces.DataBase;
using ViewModel.Interfaces.Services.Messages;

namespace View.Implementations.Proces
{
    /// <summary>
    /// Класс оконной процедуры с методами вызова и создания окна. Реализует <see cref="IProc"/>.
    /// </summary>
    public abstract class WindowProc : IProc
    {
        /// <summary>
        /// Окно.
        /// </summary>
        private Window? _window = null;

        /// <summary>
        /// Создатель контекста базы данных.
        /// </summary>
        private IDbContextBuilder _dbContextCreator;

        /// <summary>
        /// Сервис ресурсов окна.
        /// </summary>
        private IWindowResourceService _windowResourceService;

        /// <summary>
        /// Сервис сообщений.
        /// </summary>
        private IMessageService _messageService;

        /// <summary>
        /// Возвращает окно.
        /// </summary>
        private Window Window
        {
            get => _window ??= CreateConfiguredWindow();
        }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="WindowProc"/>.
        /// </summary>
        /// <param name="dbContextBuilder">Создатель контекста базы данных.</param>
        /// <param name="windowResourceService">Сервис ресурсов окна.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        protected WindowProc(IDbContextBuilder dbContextBuilder,
            IWindowResourceService windowResourceService, IMessageService messageService)
        {
            _dbContextCreator = dbContextBuilder;
            _windowResourceService = windowResourceService;
            _messageService = messageService;
        }

        /// <summary>
        /// Создаёт сконфигурированное окно.
        /// </summary>
        /// <returns>Сконфигурированное окно.</returns>
        private Window CreateConfiguredWindow()
        {
            var window = CreateWindow(_dbContextCreator, _windowResourceService, _messageService);
            window.Closed += Window_Closed;
            return window;
        }

        /// <summary>
        /// Создаёт окно.
        /// </summary>
        /// <param name="dbContextBuilder">Создатель контекста базы данных.</param>
        /// <param name="windowResourceService">Сервис ресурсов окна.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        /// <returns>Окно.</returns>
        protected abstract Window CreateWindow(IDbContextBuilder dbContextBuilder,
            IWindowResourceService windowResourceService, IMessageService messageService);

        /// <summary>
        /// Вызывает процедуру.
        /// </summary>
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