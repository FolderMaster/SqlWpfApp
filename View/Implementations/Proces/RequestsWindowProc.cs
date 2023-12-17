using System.Windows;

using View.Implementations.ResourceService;
using View.Windows;

using ViewModel.Interfaces.DataBase;
using ViewModel.Interfaces.Services.Messages;

namespace View.Implementations.Proces
{
    /// <summary>
    /// Класс оконной процедуры для работы с запросами с методами вызова и создания окна. Реализует
    /// <see cref="WindowProc"/>.
    /// </summary>
    public class RequestsWindowProc : WindowProc
    {
        /// <summary>
        /// Создаёт экземпляр класса <see cref="RequestsWindowProc"/>.
        /// </summary>
        /// <param name="dbContextBuilder">Создатель контекста базы данных.</param>
        /// <param name="windowResourceService">Сервис ресурсов окна.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        public RequestsWindowProc(IDbContextBuilder dbContextBuilder,
            IWindowResourceService windowResourceService, IMessageService messageService) :
            base(dbContextBuilder, windowResourceService, messageService) { }

        /// <summary>
        /// Создаёт окно.
        /// </summary>
        /// <param name="dbContextBuilder">Создатель контекста базы данных.</param>
        /// <param name="windowResourceService">Сервис ресурсов окна.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        /// <returns>Окно.</returns>
        protected override Window CreateWindow(IDbContextBuilder dbContextBuilder,
            IWindowResourceService windowResourceService, IMessageService messageService) =>
            new RequestsWindow(dbContextBuilder, windowResourceService, messageService);
    }
}