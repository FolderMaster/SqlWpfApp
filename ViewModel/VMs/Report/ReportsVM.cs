using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using System.Windows.Documents;

using ViewModel.Interfaces;
using ViewModel.Interfaces.Services;
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
    public partial class ReportsVM : RequestsVM
    {
        /// <summary>
        /// Ключ ресурса параграфа о студентах в отчёте об отчислениях.
        /// </summary>
        private static string _deductingsStudentsParagraphResourceKey =
            "DeductingsStudentsParagraph";

        /// <summary>
        /// Ключ ресурса параграфа о средне статистических оценках в отчёте о студентах.
        /// </summary>
        private static string _studentsAverageGradesParagraphResourceKey =
            "StudentsAverageGradesParagraph";

        /// <summary>
        /// Ключ ресурса параграфа о оценках в отчёте о студентах.
        /// </summary>
        private static string _studentsGradesParagraphResourceKey = "StudentsGradesParagraph";

        /// <summary>
        /// Ключ ресурса параграфа о стипендиях в отчёте о факультетах.
        /// </summary>
        private static string _departmentsScholarshipsParagraphResourceKey =
            "DepartmentsScholarshipsParagraph";

        /// <summary>
        /// Ключ ресурса параграфа о средне статистических оценках в отчёте о факультетах.
        /// </summary>
        private static string _departmentsAverageGradesParagraphResourceKey =
            "DepartmentsAverageGradesParagraph";

        /// <summary>
        /// Ключ ресурса описания документа.
        /// </summary>
        private static string _documentDescriptionResourceKey = "DocumentDescription";

        /// <summary>
        /// Сервис печати.
        /// </summary>
        private IPrintService _printService;

        /// <summary>
        /// Сервис изменения документа.
        /// </summary>
        private DocumentEditService _documentEditService = new();

        /// <summary>
        /// Документ.
        /// </summary>
        [ObservableProperty]
        private object? document;

        public IGettingFileService OpenGettingFileService { get; private set; }

        public IDocumentService DocumentService { get; private set; }

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

        /// <summary>
        /// Создаёт экземпляр класса <see cref="ChangeDataRequestsVM"/>.
        /// </summary>
        /// <param name="session">Создатель контекста базы данных.</param>
        /// <param name="resourceService">Сервис ресурсов.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        /// <param name="printService">Сервис печати.</param>
        public ReportsVM(ISession session, IResourceService resourceService,
            IMessageService messageService, IPrintService printService,
            IGettingFileService openGettingFileService, IDocumentService documentService) :
            base(session, resourceService, messageService)
        {
            _printService = printService;
            OpenGettingFileService = openGettingFileService;
            DocumentService = documentService;

            DeductingsCommand = new RelayCommand(() =>
            {
                _documentEditService.Clear((FlowDocument)Document);
                ExecuteCommand("SELECT s.ID, p.Name, s.GroupNumber, s.GroupFormationYear, " +
                    "sp.DepartmentName AS Department " +
                    "FROM Students s, Persons p, Groups g, Specialties sp " +
                    "WHERE IsDeductible = 1 AND s.ID = p.ID AND s.GroupNumber = g.Number " +
                    "AND s.GroupFormationYear = g.FormationYear AND " +
                    "g.SpecialtyNumber = sp.Number " +
                    "ORDER BY s.ID ASC, p.Name ASC");
                _documentEditService.AddParagraph((FlowDocument)Document, _resourceService.GetString
                    (_deductingsStudentsParagraphResourceKey));
                _documentEditService.AddTable((FlowDocument)Document, ExecutingResult);
            });
            StudentsCommand = new RelayCommand(() =>
            {
                _documentEditService.Clear((FlowDocument)Document);
                ExecuteCommand("SELECT s.ID, p.Name, sp.DepartmentName AS Department, " +
                    "g.Number AS GroupNumber, g.FormationYear AS GroupFormationYear, " +
                    "AVG(CAST(studentGrades.Coefficient AS REAL)) AS AverageGrade " +
                    "FROM Students s, Persons p, Groups g, Specialties sp, (" +
                    "SELECT StudentID, DisciplineID, PassingDate, Coefficient " +
                    "FROM GradeStatements gs, Grades gr " +
                    "WHERE gr.Name = gs.GradeName AND NOT EXISTS (" +
                    "SELECT 1 " +
                    "FROM GradeStatements grs " +
                    "WHERE grs.StudentID = gs.StudentID AND grs.DisciplineID = gs.DisciplineID " +
                    "AND grs.PassingDate > gs.PassingDate)" +
                    ") AS studentGrades " +
                    "WHERE studentGrades.StudentID = s.ID AND " +
                    "s.GroupFormationYear = g.FormationYear AND s.GroupNumber = g.Number AND " +
                    "g.SpecialtyNumber = sp.Number AND p.ID = s.ID " +
                    "GROUP BY sp.DepartmentName, g.Number, g.FormationYear, s.ID, p.Name " +
                    "ORDER BY s.ID ASC");
                _documentEditService.AddParagraph((FlowDocument)Document, _resourceService.GetString
                    (_studentsAverageGradesParagraphResourceKey));
                _documentEditService.AddTable((FlowDocument)Document, ExecutingResult);
                ExecuteCommand("SELECT s.ID, p.Name, sp.DepartmentName AS Department, " +
                    "g.Number AS GroupNumber, g.FormationYear AS GroupFormationYear, " +
                    "d.ID AS DisciplineID, d.Name AS DisciplineName, " +
                    "studentGrades.GradeName AS Grade " +
                    "FROM Students s, Persons p, Groups g, Specialties sp, Disciplines d, (" +
                    "SELECT StudentID, DisciplineID, PassingDate, GradeName " +
                    "FROM GradeStatements gs " +
                    "WHERE  NOT EXISTS (" +
                    "SELECT 1 " +
                    "FROM GradeStatements grs " +
                    "WHERE grs.StudentID = gs.StudentID AND grs.DisciplineID = gs.DisciplineID " +
                    "AND grs.PassingDate > gs.PassingDate)" +
                    ") AS studentGrades " +
                    "WHERE studentGrades.StudentID = s.ID AND " +
                    "s.GroupFormationYear = g.FormationYear AND s.GroupNumber = g.Number AND " +
                    "g.SpecialtyNumber = sp.Number AND p.ID = s.ID AND " +
                    "studentGrades.DisciplineID = d.ID " +
                    "ORDER BY s.ID ASC");
                _documentEditService.AddParagraph((FlowDocument)Document, _resourceService.GetString
                    (_studentsGradesParagraphResourceKey));
                _documentEditService.AddTable((FlowDocument)Document, ExecutingResult);
            });
            DepartmentsCommand = new RelayCommand(() =>
            {
                _documentEditService.Clear((FlowDocument)Document);
                ExecuteCommand("SELECT sp.DepartmentName AS Department, s.GroupNumber, " +
                    "s.GroupFormationYear, s.ScholarshipName AS Scholarship, " +
                    "Count(s.ID) AS Count " +
                    "FROM Students s, Groups g, Specialties sp " +
                    "WHERE s.GroupNumber = g.Number AND s.GroupFormationYear = g.FormationYear " +
                    "AND g.SpecialtyNumber = sp.Number " +
                    "GROUP BY sp.DepartmentName, s.GroupNumber, s.GroupFormationYear, " +
                    "s.ScholarshipName " +
                    "ORDER BY sp.DepartmentName ASC, s.GroupNumber ASC, s.GroupFormationYear ASC");
                _documentEditService.AddParagraph((FlowDocument)Document, _resourceService.GetString
                    (_departmentsScholarshipsParagraphResourceKey));
                _documentEditService.AddTable((FlowDocument)Document, ExecutingResult);
                ExecuteCommand("SELECT sp.DepartmentName AS Department, s.GroupNumber, " +
                    "s.GroupFormationYear, " +
                    "AVG(CAST(studentGrades.Coefficient AS REAL)) AS AverageGrade " +
                    "FROM Students s, Groups g, Specialties sp, (" +
                    "SELECT StudentID, DisciplineID, PassingDate, Coefficient " +
                    "FROM GradeStatements gs, Grades gr " +
                    "WHERE gr.Name = gs.GradeName AND NOT EXISTS (" +
                    "SELECT 1 " +
                    "FROM GradeStatements grs " +
                    "WHERE grs.StudentID = gs.StudentID AND " +
                    "grs.DisciplineID = gs.DisciplineID AND grs.PassingDate > gs.PassingDate)" +
                    ") AS studentGrades " +
                    "WHERE studentGrades.StudentID = s.ID AND " +
                    "s.GroupFormationYear = g.FormationYear AND s.GroupNumber = g.Number AND " +
                    "g.SpecialtyNumber = sp.Number " +
                    "GROUP BY sp.DepartmentName, s.GroupNumber, s.GroupFormationYear " +
                    "ORDER BY sp.DepartmentName ASC, s.GroupNumber ASC, s.GroupFormationYear ASC");
                _documentEditService.AddParagraph((FlowDocument)Document, _resourceService.GetString
                    (_departmentsAverageGradesParagraphResourceKey));
                _documentEditService.AddTable((FlowDocument)Document, ExecutingResult);
            });
            PrintCommand = new RelayCommand(() =>
            {
                _printService.Print(DocumentService.GetDocumentPaginator(Document),
                    _resourceService.GetString(_documentDescriptionResourceKey));
            });
        }
    }
}