using System.Windows;

using View.Implementations.ResourceService;
using View.Windows.DbSet.Dependent;

using ViewModel.Interfaces;
using ViewModel.Interfaces.Services;

namespace View.Implementations.Proces.Windows.DbSet.Dependent
{
    /// <summary>
    /// Класс оконной процедуры для работы с ведомостями оценивания с методами вызова и создания
    /// окна. Реализует <see cref="WindowProc"/>.
    /// </summary>
    public class GradeStatementsWindowProc : DbWindowProc
    {
        /// <summary>
        /// Создаёт экземпляр класса <see cref="GradeStatementsWindowProc"/>.
        /// </summary>
        /// <param name="session">Создатель контекста базы данных.</param>
        /// <param name="windowResourceService">Сервис ресурсов окна.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        public GradeStatementsWindowProc(ISession session,
            IWindowResourceService windowResourceService, IMessageService messageService) :
            base("GradeStatements", session, windowResourceService, messageService)
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
            new GradeStatementsWindow(session, windowResourceService, messageService);
    }
}