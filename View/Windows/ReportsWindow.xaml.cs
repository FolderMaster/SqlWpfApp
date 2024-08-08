using System.Windows;
using System.Windows.Documents;

using ViewModel.Interfaces.Services;
using ViewModel.Interfaces;
using ViewModel.VMs.Report;
using ViewModel.Interfaces.Services.Files;
using ViewModel.Interfaces.Services.Document;
using ViewModel.Interfaces.Services.Data;

using View.Implementations.Document;

namespace View.Windows
{
    /// <summary>
    /// Класс окна для работы с отчётами.
    /// </summary>
    public partial class ReportsWindow : Window
    {
        /// <summary>
        /// Создаёт экземпляр класса <see cref="ReportsWindow"/>.
        /// </summary>
        /// <param name="session">Создатель контекста базы данных.</param>
        /// <param name="resourceService">Сервис ресурсов.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        /// <param name="printDialogService">Сервис печати.</param>
        public ReportsWindow(ISession session, IResourceService resourceService,
            IMessageService messageService, IPrintService printDialogService,
            IGettingFileService openGettingFileService, IDocumentService documentService,
            ISearchService searchService)
        {
            InitializeComponent();

            var vm = new ReportsVM(session, resourceService, messageService,
                printDialogService, openGettingFileService, documentService, searchService);
            vm.Document = new Document(new FlowDocument());
            DataContext = vm;
        }
    }
}