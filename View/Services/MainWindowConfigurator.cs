﻿using System.Collections.Generic;

using View.Implementations.MessageBoxes;
using View.Windows;

using ViewModel.Interfaces;
using ViewModel.Interfaces.Proces;
using ViewModel.Interfaces.Services;
using ViewModel.Interfaces.Services.Messages;
using ViewModel.Interfaces.Technicals;
using ViewModel.VMs;

namespace View.Services
{
    /// <summary>
    /// Класс конфигуратора основного окна <seealso cref="MainWindow"/> с аргументами конфигурации
    /// и методом конфигурации. Реализует <see cref="IConfigurator"/>.
    /// </summary>
    public class MainWindowConfigurator : IConfigurator
    {
        /// <summary>
        /// Возвращает и задаёт основное окно.
        /// </summary>
        public MainWindow MainWindow { set; get; }

        /// <summary>
        /// Возвращает и задаёт создателя контекста базы данных.
        /// </summary>
        public ISession DbContextBuilder { get; set; }

        /// <summary>
        /// Возвращает и задаёт сервис ресурсов.
        /// </summary>
        public IResourceService ResourceService { get; set; }

        /// <summary>
        /// Возвращает и задаёт конфигурацию.
        /// </summary>
        public IConfiguration Configuration { get; set; }

        /// <summary>
        /// Возвращает и задаёт сервис сообщений об ошибках.
        /// </summary>
        public IMessageService ErrorMessageService { get; set; }

        public IEnumerable<IProc> Proces { get; set; }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="MainWindowConfigurator"/>.
        /// </summary>
        /// <param name="mainWindow">Основное окно.</param>
        /// <param name="dbContextBuilder">Создатель контекста базы данных.</param>
        /// <param name="resourceService">Сервис ресурсов.</param>
        /// <param name="configuration">Конфигурация.</param>
        /// <param name="questionMessageService">Сервис сообщений о вопросах.</param>
        /// <param name="informationMessageService">Сервис информационных сообщений.</param>
        /// <param name="errorMessageService">Сервис сообщений об ошибках.</param>
        /// <param name="appCloseable">Сервис закрытия приложения.</param>
        /// <param name="proces">Процедуры.</param>
        public MainWindowConfigurator(MainWindow mainWindow, ISession dbContextBuilder,
            IResourceService resourceService, IConfiguration configuration,
            ErrorMessageBoxService errorMessageService, IAppCloseable appCloseable,
            IEnumerable<IProc> proces)
        {
            MainWindow = mainWindow;
            DbContextBuilder = dbContextBuilder;
            ResourceService = resourceService;
            Configuration = configuration;
            ErrorMessageService = errorMessageService;
            Proces = proces;
        }

        /// <summary>
        /// Конфигурирует основное окно <seealso cref="MainWindow"/>.
        /// </summary>
        public void Configure() => MainWindow.DataContext = new MainVM(DbContextBuilder,
            ResourceService, Configuration, ErrorMessageService, Proces);
    }
}