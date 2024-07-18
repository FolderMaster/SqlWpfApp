using System.Collections.Generic;
using System.Windows;

using View.Windows.DbSet.Dependent;
using View.Implementations.ResourceService;

using ViewModel.VMs.DbSet;
using ViewModel.Interfaces.Services.Messages;

using Model.Dependent;
using Model.Independent;
using ViewModel.Interfaces;

namespace View.Implementations.Proces.Windows.DbSet.Dependent
{
    /// <summary>
    /// Класс оконной процедуры для работы со специальностями с методами вызова и создания окна.
    /// Реализует <see cref="WindowProc"/>.
    /// </summary>
    public class SpecialtiesWindowProc : DbWindowProc
    {
        /// <summary>
        /// Ключ ресурсов.
        /// </summary>
        private static string _keyResource = "Specialties";

        /// <summary>
        /// Создаёт экземпляр класса <see cref="SpecialtiesWindowProc"/>.
        /// </summary>
        /// <param name="session">Создатель контекста базы данных.</param>
        /// <param name="windowResourceService">Сервис ресурсов окна.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        public SpecialtiesWindowProc(ISession session,
            IWindowResourceService windowResourceService, IMessageService messageService) :
            base("Specialties", session, windowResourceService, messageService)
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
            var mainVM = new DbSetVM<Specialty>(session, windowResourceService,
                messageService);
            var dependentVM = new DbSetVM<Department>(session, windowResourceService,
                messageService);

            mainVM.SelectedItemChanged += (sender, e) =>
            {
                dependentVM.SelectedItem = mainVM.SelectedItem?.Department;
            };

            return new TwoGridDbSetWindow(windowResourceService, _keyResource, _keyResource,
                new List<object>()
                {
                    mainVM, dependentVM, (string nameProperty) =>
                    nameProperty != nameof(Specialty.Groups) &&
                    nameProperty != nameof(Specialty.Disciplines) &&
                    nameProperty != nameof(Specialty.Department), (string nameProperty) =>
                    nameProperty != nameof(Department.Specialties) &&
                    nameProperty != nameof(Department.Teachers)
                });
        }
    }
}