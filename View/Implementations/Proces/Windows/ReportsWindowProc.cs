using System.Windows;

using View.Implementations.Dialogs;
using View.Implementations.ResourceService;
using View.Windows;

using ViewModel.Interfaces;
using ViewModel.Interfaces.Services;
using ViewModel.Interfaces.Services.Document;
using ViewModel.Interfaces.Services.Files;

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
        private readonly IPrintService _printService;

        private readonly IGettingFileService _openGettingFileService;

        private readonly IDocumentService _documentService;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="ReportsWindowProc"/>.
        /// </summary>
        /// <param name="session">Создатель контекста базы данных.</param>
        /// <param name="windowResourceService">Сервис ресурсов окна.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        /// <param name="printService">Сервис печати.</param>
        public ReportsWindowProc(ISession session,
            IWindowResourceService windowResourceService, IMessageService messageService,
            IPrintService printService, OpenFileDialogService openGettingFileService,
            IDocumentService documentService) :
            base("Reports", session, windowResourceService, messageService)
        {
            _printService = printService;
            _openGettingFileService = openGettingFileService;
            _documentService = documentService;
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
            new ReportsWindow(session, windowResourceService, messageService,
                _printService, _openGettingFileService, _documentService);
    }
}