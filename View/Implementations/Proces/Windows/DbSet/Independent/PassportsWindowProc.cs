using System.Windows;
using View.Implementations.ResourceService;
using View.Windows.DbSet.Independent;
using ViewModel.Interfaces;
using ViewModel.Interfaces.Services.Files;
using ViewModel.Interfaces.Services.Messages;

namespace View.Implementations.Proces.Windows.DbSet.Independent
{
    /// <summary>
    /// Класс оконной процедуры для работы с паспортами с методами вызова и создания окна.
    /// Реализует <see cref="WindowProc"/>.
    /// </summary>
    public class PassportsWindowProc : DbWindowProc
    {
        /// <summary>
        /// Сервис получения файла открытия.
        /// </summary>
        private IGettingFileService _gettingOpenFileService;

        /// <summary>
        /// Сервис получения файла сохранения.
        /// </summary>
        private IGettingFileService _gettingSaveFileService;

        /// <summary>
        /// Файловый сервис.
        /// </summary>
        private IFileService _fileService;

        /// <summary>
        /// Сервис путей.
        /// </summary>
        private IPathService _pathService;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="PassportsWindowProc"/>.
        /// </summary>
        /// <param name="session">Создатель контекста базы данных.</param>
        /// <param name="windowResourceService">Сервис ресурсов окна.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        /// <param name="gettingOpenFileService">Сервис получения файла открытия.</param>
        /// <param name="gettingSaveFileService">Сервис получения файла сохранения.</param>
        /// <param name="fileService">Файловый сервис.</param>
        /// <param name="pathService">Сервис путей.</param>
        public PassportsWindowProc(ISession session,
            IWindowResourceService windowResourceService, IMessageService messageService,
            IGettingFileService gettingOpenFileService, IGettingFileService gettingSaveFileService,
            IFileService fileService, IPathService pathService) :
            base("Passports", session, windowResourceService, messageService)
        {
            _gettingOpenFileService = gettingOpenFileService;
            _gettingSaveFileService = gettingSaveFileService;
            _fileService = fileService;
            _pathService = pathService;
        }

        /// <summary>
        /// Создаёт окно.
        /// </summary>
        /// <param name="session">Создатель контекста базы данных.</param>
        /// <param name="windowResourceService">Сервис ресурсов окна.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        /// <returns>Окно.</returns>
        protected override Window CreateWindow(ISession session,
            IWindowResourceService windowResourceService, IMessageService messageService) =>
            new PassportsWindow(session, windowResourceService, messageService,
                _gettingOpenFileService, _gettingSaveFileService, _fileService, _pathService);
    }
}