using System.Collections.Generic;
using System.Windows;

using View.Windows.DbSet.Independent;
using View.Implementations.ResourceService;

using ViewModel.VMs.DbSet;
using ViewModel.Interfaces;
using ViewModel.Interfaces.Services;
using ViewModel.Interfaces.Services.Data;

using Model.Independent;

namespace View.Implementations.Proces.Windows.DbSet.Independent
{
    /// <summary>
    /// Класс оконной процедуры для работы с режимами оценивания с методами вызова и создания окна.
    /// Реализует <see cref="WindowProc"/>.
    /// </summary>
    public class GradeModesWindowProc : DbWindowProc
    {
        private readonly ISearchService _searchService;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="GradeModesWindowProc"/>.
        /// </summary>
        /// <param name="session">Создатель контекста базы данных.</param>
        /// <param name="windowResourceService">Сервис ресурсов окна.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        public GradeModesWindowProc(ISession session, IWindowResourceService windowResourceService,
            IMessageService messageService, ISearchService searchService) :
            base("GradeModes", session, windowResourceService, messageService) =>
            _searchService = searchService;

        /// <summary>
        /// Создаёт окно.
        /// </summary>
        /// <param name="session">Создатель контекста базы данных.</param>
        /// <param name="windowResourceService">Сервис ресурсов окна.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        /// <returns>Окно.</returns>
        protected override Window CreateWindow(ISession session,
            IWindowResourceService windowResourceService, IMessageService messageService) =>
            new GridDbSetWindow(windowResourceService, _name, _name,
                new List<object>()
                {
                    new DbSetVM<GradeMode>(session, windowResourceService,
                        messageService, _searchService)
                });
    }
}
