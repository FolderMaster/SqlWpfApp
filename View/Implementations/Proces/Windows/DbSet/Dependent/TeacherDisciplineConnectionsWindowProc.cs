using System.Windows;

using View.Implementations.ResourceService;
using View.Windows.DbSet.Dependent;

using ViewModel.Interfaces;
using ViewModel.Interfaces.Services;

namespace View.Implementations.Proces.Windows.DbSet.Dependent
{
    /// <summary>
    /// Класс оконной процедуры для работы со связями между дисциплинами и преподавателями с
    /// методами вызова и создания окна. Реализует <see cref="WindowProc"/>.
    /// </summary>
    public class TeacherDisciplineConnectionsWindowProc : DbWindowProc
    {
        /// <summary>
        /// Создаёт экземпляр класса <see cref="TeacherDisciplineConnectionsWindowProc"/>.
        /// </summary>
        /// <param name="session">Создатель контекста базы данных.</param>
        /// <param name="windowResourceService">Сервис ресурсов окна.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        public TeacherDisciplineConnectionsWindowProc(ISession session,
            IWindowResourceService windowResourceService, IMessageService messageService) :
            base("TeacherDisciplineConnections", session, windowResourceService, messageService)
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
            new TeacherDisciplineConnectionsWindow(session, windowResourceService,
                messageService);
    }
}