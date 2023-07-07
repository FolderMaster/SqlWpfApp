using System.Windows;

using View.Implementations.ResourceService;
using View.Windows;

using ViewModel.Interfaces.DbContext;
using ViewModel.Interfaces.Services;
using ViewModel.Interfaces.Services.Messages;

namespace View.Implementations.Proces
{
    /// <summary>
    /// Класс оконной процедуры для работы с отчётами с методами вызова и создания окна. Реализует
    /// <see cref="WindowProc"/>.
    /// </summary>
    public class ReportsWindowProc : WindowProc
    {
        /// <summary>
        /// Сервис печати.
        /// </summary>
        private IPrintService _printService;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="ReportsWindowProc"/>.
        /// </summary>
        /// <param name="dbContextBuilder">Создатель контекста базы данных.</param>
        /// <param name="windowResourceService">Сервис ресурсов окна.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        /// <param name="printService">Сервис печати.</param>
        public ReportsWindowProc(IDbContextBuilder dbContextBuilder,
            IWindowResourceService windowResourceService, IMessageService messageService,
            IPrintService printService) :
            base(dbContextBuilder, windowResourceService, messageService) =>
            _printService = printService;

        /// <summary>
        /// Создаёт окно.
        /// </summary>
        /// <param name="dbContextBuilder">Создатель контекста базы данных.</param>
        /// <param name="windowResourceService">Сервис ресурсов окна.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        /// <returns>Окно.</returns>
        protected override Window CreateWindow(IDbContextBuilder dbContextBuilder,
            IWindowResourceService windowResourceService, IMessageService messageService) =>
            new ReportsWindow(dbContextBuilder, windowResourceService, messageService,
                _printService);
    }
}