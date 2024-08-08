using System.Collections.Generic;
using System.Windows;

using View.Windows.DbSet.Dependent;
using View.Implementations.ResourceService;

using ViewModel.Interfaces;
using ViewModel.Interfaces.Services;
using ViewModel.VMs.DbSet;
using ViewModel.Interfaces.Services.Data;

using Model.Dependent;

namespace View.Implementations.Proces.Windows.DbSet.Dependent
{
    /// <summary>
    /// Класс оконной процедуры для работы с дисциплинами с методами вызова и создания окна.
    /// Реализует <see cref="WindowProc"/>.
    /// </summary>
    public class DisciplinesWindowProc : DbWindowProc
    {
        private readonly ISearchService _searchService;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="DisciplinesWindowProc"/>.
        /// </summary>
        /// <param name="session">Создатель контекста базы данных.</param>
        /// <param name="windowResourceService">Сервис ресурсов окна.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        public DisciplinesWindowProc(ISession session,
            IWindowResourceService windowResourceService, IMessageService messageService,
            ISearchService searchService) :
            base("Disciplines", session, windowResourceService, messageService) =>
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
            var mainVM = new DbSetVM<Discipline>(session, windowResourceService,
                messageService, _searchService);
            var dependentVM = new DbSetVM<StudyForm>(session, windowResourceService,
                messageService, _searchService);
            var dependent2VM = new DbSetVM<Specialty>(session, windowResourceService,
                messageService, _searchService);

            mainVM.SelectedItemChanged += (sender, e) =>
            {
                dependentVM.SelectedItem = mainVM.SelectedItem?.StudyForm;
                dependent2VM.SelectedItem = mainVM.SelectedItem?.Specialty;
            };

            return new ThreeGridDbSetWindow(windowResourceService, _name, _name,
                new List<object>()
                {
                    mainVM, dependentVM, dependent2VM
                });
        }
    }
}