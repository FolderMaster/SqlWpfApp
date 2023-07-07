using System.Windows;

using View.Implementations.ResourceService;
using View.Windows.DbSet.Independent;

using ViewModel.Interfaces.DbContext;
using ViewModel.Interfaces.Services.Files;
using ViewModel.Interfaces.Services.Messages;

namespace View.Implementations.Proces.DbSet.Independent
{
    /// <summary>
    /// Класс оконной процедуры для работы с паспортами с методами вызова и создания окна.
    /// Реализует <see cref="WindowProc"/>.
    /// </summary>
    public class PassportsWindowProc : WindowProc
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
        /// <param name="dbContextBuilder">Создатель контекста базы данных.</param>
        /// <param name="windowResourceService">Сервис ресурсов окна.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        /// <param name="gettingOpenFileService">Сервис получения файла открытия.</param>
        /// <param name="gettingSaveFileService">Сервис получения файла сохранения.</param>
        /// <param name="fileService">Файловый сервис.</param>
        /// <param name="pathService">Сервис путей.</param>
        public PassportsWindowProc(IDbContextBuilder dbContextBuilder,
            IWindowResourceService windowResourceService, IMessageService messageService,
            IGettingFileService gettingOpenFileService, IGettingFileService gettingSaveFileService,
            IFileService fileService, IPathService pathService) :
            base(dbContextBuilder, windowResourceService, messageService)
        {
            _gettingOpenFileService = gettingOpenFileService;
            _gettingSaveFileService = gettingSaveFileService;
            _fileService = fileService;
            _pathService = pathService;
        }

        /// <summary>
        /// Создаёт окно.
        /// </summary>
        /// <param name="dbContextBuilder">Создатель контекста базы данных.</param>
        /// <param name="windowResourceService">Сервис ресурсов окна.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        /// <returns>Окно.</returns>
        protected override Window CreateWindow(IDbContextBuilder dbContextBuilder,
            IWindowResourceService windowResourceService, IMessageService messageService) =>
            new PassportsWindow(dbContextBuilder, windowResourceService, messageService,
                _gettingOpenFileService, _gettingSaveFileService, _fileService, _pathService);
    }
}