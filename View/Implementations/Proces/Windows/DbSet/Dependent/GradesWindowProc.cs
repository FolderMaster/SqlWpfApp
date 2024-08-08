using System.Collections.Generic;
using System.Windows;

using View.Windows.DbSet.Dependent;
using View.Implementations.ResourceService;

using ViewModel.Interfaces;
using ViewModel.Interfaces.Services;
using ViewModel.VMs.DbSet;
using ViewModel.Interfaces.Services.Data;

using Model.Dependent;
using Model.Independent;

namespace View.Implementations.Proces.Windows.DbSet.Dependent
{
    /// <summary>
    /// Класс оконной процедуры для работы с оценками с методами вызова и создания окна. Реализует
    /// <see cref="WindowProc"/>.
    /// </summary>
    public class GradesWindowProc : DbWindowProc
    {
        private readonly ISearchService _searchService;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="GradesWindowProc"/>.
        /// </summary>
        /// <param name="session">Создатель контекста базы данных.</param>
        /// <param name="windowResourceService">Сервис ресурсов окна.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        public GradesWindowProc(ISession session, IWindowResourceService windowResourceService,
            IMessageService messageService, ISearchService searchService) :
            base("Grades", session, windowResourceService, messageService) =>
            _searchService = searchService;

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
            var mainVM = new DbSetVM<Grade>(session, windowResourceService,
                messageService, _searchService);
            var dependentVM = new DbSetVM<GradeMode>(session, windowResourceService,
                messageService, _searchService);

            mainVM.SelectedItemChanged += (sender, e) =>
            {
                dependentVM.SelectedItem = mainVM.SelectedItem?.GradeMode;
            };

            return new TwoGridDbSetWindow(windowResourceService, _name, _name,
                new List<object>()
                {
                    mainVM, dependentVM
                });
        }
    }
}