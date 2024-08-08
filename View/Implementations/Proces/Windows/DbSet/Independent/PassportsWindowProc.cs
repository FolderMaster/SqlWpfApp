using System.Windows;

using View.Implementations.Dialogs;
using View.Implementations.ResourceService;
using View.Windows.DbSet.Independent;

using ViewModel.Interfaces;
using ViewModel.Interfaces.Services;
using ViewModel.Interfaces.Services.Data;
using ViewModel.Interfaces.Services.Files;

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
        private readonly IGettingFileService _gettingOpenFileService;

        /// <summary>
        /// Сервис получения файла сохранения.
        /// </summary>
        private readonly IGettingFileService _gettingSaveFileService;

        /// <summary>
        /// Файловый сервис.
        /// </summary>
        private readonly IFileService _fileService;

        private readonly IImageService _imageService;

        private readonly ISearchService _searchService;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="PassportsWindowProc"/>.
        /// </summary>
        /// <param name="session">Создатель контекста базы данных.</param>
        /// <param name="windowResourceService">Сервис ресурсов окна.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        /// <param name="gettingOpenFileService">Сервис получения файла открытия.</param>
        /// <param name="gettingSaveFileService">Сервис получения файла сохранения.</param>
        /// <param name="fileService">Файловый сервис.</param>
        public PassportsWindowProc(ISession session,
            IWindowResourceService windowResourceService, IMessageService messageService,
            OpenFileDialogService gettingOpenFileService,
            SaveFileDialogService gettingSaveFileService,
            IFileService fileService, IImageService imageService, ISearchService searchService) :
            base("Passports", session, windowResourceService, messageService)
        {
            _gettingOpenFileService = gettingOpenFileService;
            _gettingSaveFileService = gettingSaveFileService;
            _fileService = fileService;
            _imageService = imageService;
            _searchService = searchService;
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
            new PassportsWindow(session, windowResourceService, messageService, _searchService,
                _gettingOpenFileService, _gettingSaveFileService, _fileService, _imageService);
    }
}