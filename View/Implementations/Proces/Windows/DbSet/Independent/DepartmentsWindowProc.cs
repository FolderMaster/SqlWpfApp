using System.Collections.Generic;
using System.Windows;

using View.Windows.DbSet.Independent;
using View.Implementations.ResourceService;

using ViewModel.VMs.DbSet;
using ViewModel.Interfaces.Services.Messages;

using Model.Independent;
using ViewModel.Interfaces;

namespace View.Implementations.Proces.Windows.DbSet.Independent
{
    /// <summary>
    /// Класс оконной процедуры для работы с факультетами с методами вызова и создания окна.
    /// Реализует <see cref="WindowProc"/>.
    /// </summary>
    public class DepartmentsWindowProc : DbWindowProc
    {
        /// <summary>
        /// Ключ ресурсов.
        /// </summary>
        private static string _keyResource = nameof(Department) + "s";

        /// <summary>
        /// Создаёт экземпляр класса <see cref="DepartmentsWindowProc"/>.
        /// </summary>
        /// <param name="session">Создатель контекста базы данных.</param>
        /// <param name="windowResourceService">Сервис ресурсов окна.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        public DepartmentsWindowProc(ISession session,
            IWindowResourceService windowResourceService, IMessageService messageService) :
            base("Departments", session, windowResourceService, messageService) { }

        /// <summary>
        /// Создаёт окно.
        /// </summary>
        /// <param name="session">Создатель контекста базы данных.</param>
        /// <param name="windowResourceService">Сервис ресурсов окна.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        /// <returns>Окно.</returns>
        protected override Window CreateWindow(ISession session,
            IWindowResourceService windowResourceService, IMessageService messageService) =>
            new GridDbSetWindow(windowResourceService, _keyResource, _keyResource,
                new List<object>()
                {
                    new DbSetVM<Department>(session, windowResourceService,
                        messageService),
                    (string nameProperty) => nameProperty != nameof(Department.Specialties) &&
                    nameProperty != nameof(Department.Teachers)
                });
    }
}