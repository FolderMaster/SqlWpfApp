using System.Collections.Generic;
using System.Windows;

using ViewModel.Interfaces;
using ViewModel.Interfaces.Services;
using View.Windows.DbSet.Dependent;
using View.Implementations.ResourceService;

using ViewModel.VMs.DbSet;

using Model.Dependent;

namespace View.Implementations.Proces.Windows.DbSet.Dependent
{
    /// <summary>
    /// Класс оконной процедуры для работы со связями между дисциплинами и студентами с методами
    /// вызова и создания окна. Реализует <see cref="WindowProc"/>.
    /// </summary>
    public class StudentDisciplineConnectionsWindowProc : DbWindowProc
    {
        /// <summary>
        /// Ключ ресурсов.
        /// </summary>
        private static string _keyResource = nameof(StudentDisciplineConnection) + "s";

        /// <summary>
        /// Создаёт экземпляр класса <see cref="StudentDisciplineConnectionsWindowProc"/>.
        /// </summary>
        /// <param name="session">Создатель контекста базы данных.</param>
        /// <param name="windowResourceService">Сервис ресурсов окна.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        public StudentDisciplineConnectionsWindowProc(ISession session,
            IWindowResourceService windowResourceService, IMessageService messageService) :
            base("StudentDisciplineConnections", session, windowResourceService, messageService)
        { }

        /// <summary>
        /// Создаёт окно.
        /// </summary>
        /// <param name="session">Создатель контекста базы данных.</param>
        /// <param name="windowResourceService">Сервис ресурсов окна.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        /// <returns>Окно.</returns>
        protected override Window CreateWindow(ISession session,
            IWindowResourceService windowResourceService, IMessageService messageService)
        {
            var mainVM = new DbSetVM<StudentDisciplineConnection>(session,
                windowResourceService, messageService);
            var dependentVM = new DbSetVM<Student>(session, windowResourceService,
                messageService);
            var dependent2VM = new DbSetVM<Discipline>(session, windowResourceService,
                messageService);

            mainVM.SelectedItemChanged += (sender, e) =>
            {
                dependentVM.SelectedItem = mainVM.SelectedItem?.Student;
                dependent2VM.SelectedItem = mainVM.SelectedItem?.Discipline;
            };

            return new ThreeGridDbSetWindow(windowResourceService, _keyResource, _keyResource,
                new List<object>()
                {
                    mainVM, dependentVM, dependent2VM
                });
        }
    }
}