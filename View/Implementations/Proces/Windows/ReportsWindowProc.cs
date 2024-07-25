﻿using System.Windows;

using View.Implementations.ResourceService;
using View.Windows;

using ViewModel.Interfaces;
using ViewModel.Interfaces.Services;

namespace View.Implementations.Proces.Windows
{
    /// <summary>
    /// Класс оконной процедуры для работы с отчётами с методами вызова и создания окна. Реализует
    /// <see cref="WindowProc"/>.
    /// </summary>
    public class ReportsWindowProc : DbWindowProc
    {
        /// <summary>
        /// Сервис печати.
        /// </summary>
        private IPrintService _printService;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="ReportsWindowProc"/>.
        /// </summary>
        /// <param name="session">Создатель контекста базы данных.</param>
        /// <param name="windowResourceService">Сервис ресурсов окна.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        /// <param name="printService">Сервис печати.</param>
        public ReportsWindowProc(ISession session,
            IWindowResourceService windowResourceService, IMessageService messageService,
            IPrintService printService) :
            base("Reports", session, windowResourceService, messageService) =>
            _printService = printService;

        /// <summary>
        /// Создаёт окно.
        /// </summary>
        /// <param name="session">Создатель контекста базы данных.</param>
        /// <param name="windowResourceService">Сервис ресурсов окна.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        /// <returns>Окно.</returns>
        protected override Window CreateWindow(ISession session,
            IWindowResourceService windowResourceService, IMessageService messageService) =>
            new ReportsWindow(session, windowResourceService, messageService,
                _printService);
    }
}