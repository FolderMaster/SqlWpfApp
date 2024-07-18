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
    /// Класс оконной процедуры для работы с персонами с методами вызова и создания окна. Реализует
    /// <see cref="WindowProc"/>.
    /// </summary>
    public class PersonsWindowProc : DbWindowProc
    {
        /// <summary>
        /// Ключ ресурсов.
        /// </summary>
        private static string _keyResource = nameof(Person) + "s";

        /// <summary>
        /// Создаёт экземпляр класса <see cref="PersonsWindowProc"/>.
        /// </summary>
        /// <param name="session">Создатель контекста базы данных.</param>
        /// <param name="windowResourceService">Сервис ресурсов окна.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        public PersonsWindowProc(ISession session,
            IWindowResourceService windowResourceService, IMessageService messageService) :
            base("Persons", session, windowResourceService, messageService) { }

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
            var mainVM = new DbSetVM<Person>(session, windowResourceService,
                messageService);
            var dependentVM = new DbSetVM<Passport>(session, windowResourceService,
                messageService);

            mainVM.SelectedItemChanged += (sender, e) =>
            {
                dependentVM.SelectedItem = mainVM.SelectedItem?.Passport;
            };

            return new TwoGridDbSetWindow(windowResourceService, _keyResource, _keyResource,
                new List<object>()
                {
                    mainVM, dependentVM,
                    (string nameProperty) => nameProperty != nameof(Person.Passport) &&
                    nameProperty != nameof(Person.Teachers) &&
                    nameProperty != nameof(Person.Students),
                    (string nameProperty) => nameProperty != nameof(Passport.Persons) &&
                    nameProperty != nameof(Passport.Scan)
                });
        }
    }
}