﻿using System.Windows;

using View.Implementations.ResourceService;
using View.Windows.DbSet.Dependent;

using ViewModel.Interfaces;
using ViewModel.Interfaces.Services;
using ViewModel.Interfaces.Services.Data;

namespace View.Implementations.Proces.Windows.DbSet.Dependent
{
    /// <summary>
    /// Класс оконной процедуры для работы с преподавателями с методами вызова и создания окна.
    /// Реализует <see cref="WindowProc"/>.
    /// </summary>
    public class TeachersWindowProc : DbWindowProc
    {
        private readonly ISearchService _searchService;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="TeachersWindowProc"/>.
        /// </summary>
        /// <param name="session">Создатель контекста базы данных.</param>
        /// <param name="windowResourceService">Сервис ресурсов окна.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        public TeachersWindowProc(ISession session, IWindowResourceService windowResourceService,
            IMessageService messageService, ISearchService searchService) :
            base("Teachers", session, windowResourceService, messageService) =>
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
            new TeachersWindow(session, windowResourceService, messageService, _searchService);
    }
}