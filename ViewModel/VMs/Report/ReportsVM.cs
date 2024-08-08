using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using System;
using System.Linq;

using ViewModel.Enums;
using ViewModel.Interfaces;
using ViewModel.Interfaces.Services;
using ViewModel.Interfaces.Services.Data;
using ViewModel.Interfaces.Services.Document;
using ViewModel.Interfaces.Services.Files;
using ViewModel.Services;
using ViewModel.VMs.Request;

namespace ViewModel.VMs.Report
{
    /// <summary>
    /// Класс представления модели для создания отчётов с документом и команды печати и вывода
    /// отчётов.
    /// </summary>
    public partial class ReportsVM : ObservableObject
    {
        private static string _requestPatternKey = "RequestPattern";

        /// <summary>
        /// Ключ ресурса параграфа о студентах в отчёте об отчислениях.
        /// </summary>
        private static string _deductingsStudentsParagraphKey = "DeductingsStudentsParagraph";

        private static string _deductingsStudentsRequestKey = "DeductingsStudentsRequest";

        /// <summary>
        /// Ключ ресурса параграфа о средне статистических оценках в отчёте о студентах.
        /// </summary>
        private static string _studentsAverageGradesParagraphKey =
            "StudentsAverageGradesParagraph";

        private static string _studentsAverageGradesRequestKey = "StudentsAverageGradesRequest";

        /// <summary>
        /// Ключ ресурса параграфа о оценках в отчёте о студентах.
        /// </summary>
        private static string _studentsGradesParagraphKey = "StudentsGradesParagraph";

        private static string _studentsGradesRequestKey = "StudentsGradesRequest";

        /// <summary>
        /// Ключ ресурса параграфа о стипендиях в отчёте о факультетах.
        /// </summary>
        private static string _departmentsScholarshipsParagraphKey =
            "DepartmentsScholarshipsParagraph";

        private static string _departmentsScholarshipsRequestKey =
            "DepartmentsScholarshipsRequest";

        /// <summary>
        /// Ключ ресурса параграфа о средне статистических оценках в отчёте о факультетах.
        /// </summary>
        private static string _departmentsAverageGradesParagraphKey =
            "DepartmentsAverageGradesParagraph";

        private static string _departmentsAverageGradesRequestKey =
            "DepartmentsAverageGradesRequest";

        /// <summary>
        /// Ключ ресурса описания документа.
        /// </summary>
        private static string _documentDescriptionKey = "DocumentDescription";

        /// <summary>
        /// Создатель контекста базы данных.
        /// </summary>
        private ISession _session;

        /// <summary>
        /// Сервис послания сообщений.
        /// </summary>
        protected IMessageService _messageService;

        /// <summary>
        /// Сервис ресурсов.
        /// </summary>
        protected IResourceService _resourceService;

        /// <summary>
        /// Сервис печати.
        /// </summary>
        private IPrintService _printService;

        /// <summary>
        /// Документ.
        /// </summary>
        private IDocument? _document;

        public IGettingFileService OpenGettingFileService { get; private set; }

        public IDocumentService DocumentService { get; private set; }

        public ISearchService SearchService { get; private set; }

        public IDocument? Document
        {
            get => _document;
            set
            {
                if (SetProperty(ref _document, value))
                {
                    PrintCommand.NotifyCanExecuteChanged();
                    DeductingsCommand.NotifyCanExecuteChanged();
                    StudentsCommand.NotifyCanExecuteChanged();
                    DepartmentsCommand.NotifyCanExecuteChanged();
                    ExecuteCommand.NotifyCanExecuteChanged();
                }
            }
        }

        /// <summary>
        /// Возвращает и задаёт команду печати.
        /// </summary>
        public RelayCommand PrintCommand { get; private set; }

        /// <summary>
        /// Возвращает и задаёт команду вывода отчёта по отчислениям.
        /// </summary>
        public RelayCommand DeductingsCommand { get; private set; }

        /// <summary>
        /// Возвращает и задаёт команду вывода отчёта по студентам.
        /// </summary>
        public RelayCommand StudentsCommand { get; private set; }

        /// <summary>
        /// Возвращает и задаёт команду вывода отчёта по факультетам.
        /// </summary>
        public RelayCommand DepartmentsCommand { get; private set; }

        public RelayCommand ExecuteCommand { get; private set; }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="ChangeDataRequestsVM"/>.
        /// </summary>
        /// <param name="session">Создатель контекста базы данных.</param>
        /// <param name="resourceService">Сервис ресурсов.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        /// <param name="printService">Сервис печати.</param>
        public ReportsVM(ISession session, IResourceService resourceService,
            IMessageService messageService, IPrintService printService,
            IGettingFileService openGettingFileService, IDocumentService documentService,
            ISearchService searchService)
        {
            _session = session;
            _resourceService = resourceService;
            _messageService = messageService;
            _printService = printService;
            OpenGettingFileService = openGettingFileService;
            DocumentService = documentService;
            SearchService = searchService;

            DeductingsCommand = new RelayCommand(() =>
            {
                DocumentEditService.ApplyTemplate(Document, new object[]
                {
                    _resourceService.GetString(_deductingsStudentsParagraphKey),
                    _resourceService.GetString(_deductingsStudentsRequestKey)
                });
            }, () => Document != null);
            StudentsCommand = new RelayCommand(() =>
            {
                DocumentEditService.ApplyTemplate(Document, new object[]
                {
                    _resourceService.GetString(_studentsAverageGradesParagraphKey),
                    _resourceService.GetString(_studentsAverageGradesRequestKey),
                    _resourceService.GetString(_studentsGradesParagraphKey),
                    _resourceService.GetString(_studentsGradesRequestKey)
                });
            }, () => Document != null);
            DepartmentsCommand = new RelayCommand(() =>
            {
                DocumentEditService.ApplyTemplate(Document, new object[]
                {
                    _resourceService.GetString(_departmentsScholarshipsParagraphKey),
                    _resourceService.GetString(_departmentsScholarshipsRequestKey),
                    _resourceService.GetString(_departmentsAverageGradesParagraphKey),
                    _resourceService.GetString(_departmentsAverageGradesRequestKey),
                });
            }, () => Document != null);
            ExecuteCommand = new RelayCommand(() =>
            {
                MessengerService.ExecuteWithExceptionMessage(resourceService, messageService, () =>
                {
                    var pattern = _resourceService.GetString(_requestPatternKey);
                    var ranges = Document.Search(pattern, Document.ContentRange, SearchService);
                    foreach (var range in ranges)
                    {
                        var group = SearchService.GetGroups(range.Text, pattern).First();
                        var command = (string)group.ElementAt(1);
                        DataFormat format;
                        if (Enum.TryParse((string)group.ElementAt(2), true, out format))
                        {
                            var table = _session.DbContext.ExecuteCommand(command);
                            DocumentEditService.InsertDataTable(Document, range, table, format);
                        }
                    }
                });
            }, () => Document != null);
            PrintCommand = new RelayCommand(() =>
            {
                _printService.Print(Document.DocumentPaginator,
                    _resourceService.GetString(_documentDescriptionKey));
            }, () => Document != null);
        }
    }
}