using System.Windows;

using View.Implementations.ResourceService;
using View.Windows.DbSet.Dependent;

using ViewModel.Interfaces.DbContext;
using ViewModel.Interfaces.Services.Messages;

namespace View.Implementations.Proces.DbSet.Dependent
{
    /// <summary>
    /// Класс оконной процедуры для работы с преподавателями с методами вызова и создания окна.
    /// Реализует <see cref="WindowProc"/>.
    /// </summary>
    public class TeachersWindowProc : WindowProc
    {
        /// <summary>
        /// Создаёт экземпляр класса <see cref="TeachersWindowProc"/>.
        /// </summary>
        /// <param name="dbContextBuilder">Создатель контекста базы данных.</param>
        /// <param name="windowResourceService">Сервис ресурсов окна.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        public TeachersWindowProc(IDbContextBuilder dbContextBuilder,
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
            new TeachersWindow(dbContextBuilder, windowResourceService, messageService);
    }
}