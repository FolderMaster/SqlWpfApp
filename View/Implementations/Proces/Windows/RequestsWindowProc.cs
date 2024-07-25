using System.Windows;

using View.Implementations.ResourceService;
using View.Windows;

using ViewModel.Interfaces;
using ViewModel.Interfaces.Services;

namespace View.Implementations.Proces.Windows
{
    /// <summary>
    /// Класс оконной процедуры для работы с запросами с методами вызова и создания окна. Реализует
    /// <see cref="WindowProc"/>.
    /// </summary>
    public class RequestsWindowProc : DbWindowProc
    {
        /// <summary>
        /// Создаёт экземпляр класса <see cref="RequestsWindowProc"/>.
        /// </summary>
        /// <param name="session">Создатель контекста базы данных.</param>
        /// <param name="windowResourceService">Сервис ресурсов окна.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        public RequestsWindowProc(ISession session,
            IWindowResourceService windowResourceService, IMessageService messageService) :
            base("Requests", session, windowResourceService, messageService)
        { }

        /// <summary>
        /// Создаёт окно.
        /// </summary>
        /// <param name="session">Создатель контекста базы данных.</param>
        /// <param name="windowResourceService">Сервис ресурсов окна.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        /// <returns>Окно.</returns>
        protected override Window CreateWindow(ISession session,
            IWindowResourceService windowResourceService, IMessageService messageService) =>
            new RequestsWindow(session, windowResourceService, messageService);
    }
}