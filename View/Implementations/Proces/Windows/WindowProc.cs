using System;
using System.Windows;

using View.Implementations.ResourceService;

using ViewModel.Interfaces;
using ViewModel.Interfaces.Services;

namespace View.Implementations.Proces.Windows
{
    /// <summary>
    /// Класс оконной процедуры с методами вызова и создания окна. Реализует <see cref="IProc"/>.
    /// </summary>
    public abstract class WindowProc : BaseProc
    {
        /// <summary>
        /// Окно.
        /// </summary>
        private Window? _window = null;

        /// <summary>
        /// Создатель контекста базы данных.
        /// </summary>
        protected ISession _session;

        /// <summary>
        /// Сервис сообщений.
        /// </summary>
        protected IMessageService _messageService;

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
        /// <param name="name">Название.</param>
        /// <param name="session">Создатель контекста базы данных.</param>
        /// <param name="windowResourceService">Сервис ресурсов окна.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        protected WindowProc(string name, ISession session,
            IWindowResourceService windowResourceService, IMessageService messageService) :
            base(name, windowResourceService)
        {
            _session = session;
            _messageService = messageService;
        }

        /// <summary>
        /// Создаёт сконфигурированное окно.
        /// </summary>
        /// <returns>Сконфигурированное окно.</returns>
        private Window CreateConfiguredWindow()
        {
            var window = CreateWindow(_session, _windowResourceService, _messageService);
            window.Closed += Window_Closed;
            return window;
        }

        /// <summary>
        /// Создаёт окно.
        /// </summary>
        /// <param name="session">Создатель контекста базы данных.</param>
        /// <param name="windowResourceService">Сервис ресурсов окна.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        /// <returns>Окно.</returns>
        protected abstract Window CreateWindow(ISession session,
            IWindowResourceService windowResourceService, IMessageService messageService);

        /// <inheritdoc/>
        public override void Invoke()
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

        public override void Abort()
        {
            var window = Window;
            if (window.IsVisible)
            {
                window.Close();
            }
        }

        private void Window_Closed(object? sender, EventArgs e) => _window = null;
    }
}