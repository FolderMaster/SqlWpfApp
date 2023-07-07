using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using System.Windows.Documents;
using ViewModel.Interfaces.DbContext;
using ViewModel.Interfaces.Services;
using ViewModel.Interfaces.Services.Messages;
using ViewModel.Services;

namespace ViewModel.VMs.Request
{
    /// <summary>
    /// Класс представления модели для создания отчётов с документом и команды печати и вывода
    /// отчётов.
    /// </summary>
    public partial class ReportsVM : ViewRequestsVM
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
        private DocumentEditService _documentEditService;

        /// <summary>
        /// Документ.
        /// </summary>
        [ObservableProperty]
        private FlowDocument document = new();

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
        /// <param name="dbContextBuilder">Создатель контекста базы данных.</param>
        /// <param name="resourceService">Сервис ресурсов.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        /// <param name="printService">Сервис печати.</param>
        public ReportsVM(IDbContextBuilder dbContextBuilder,
            IResourceService resourceService, IMessageService messageService,
            IPrintService printService) :
            base(dbContextBuilder, resourceService, messageService)
        {
            _printService = printService;
            _documentEditService = new DocumentEditService(Document);

            DeductingsCommand = new RelayCommand(() =>
            {
                _documentEditService.Clear();
                ExecuteCommand(CreateSelectCommand("s.ID, p.Name, s.GroupNumber, " +
                    "s.GroupFormationYear, sp.DepartmentName AS Department",
                    "Students s, Persons p, Groups g, Specialties sp",
                    "IsDeductible = 1 AND s.ID = p.ID AND s.GroupNumber = g.Number AND " +
                    "s.GroupFormationYear = g.FormationYear AND g.SpecialtyNumber = sp.Number",
                    orderByContent: "s.ID ASC, p.Name ASC"));
                _documentEditService.AddParagraph(_resourceService.GetString
                    (_deductingsStudentsParagraphResourceKey));
                _documentEditService.AddTable(ExecutingResult);
            });
            StudentsCommand = new RelayCommand(() =>
            {
                _documentEditService.Clear();
                ExecuteCommand(CreateSelectCommand("s.ID, p.Name, " +
                    "sp.DepartmentName AS Department, g.Number AS GroupNumber, " +
                    "g.FormationYear AS GroupFormationYear, " +
                    "AVG(studentGrades.Coefficient) AS AverageGrade",
                    "Students s, Persons p, Groups g, Specialties sp " +
                    "JOIN (" +
                    CreateSelectCommand("gs.StudentID, gs.DisciplineID, MAX(gs.PassingDate), " +
                    "g.Coefficient",
                    "Grades g, GradeStatements gs",
                    "g.Name = gs.GradeName",
                    "gs.StudentID, gs.DisciplineID") +
                    ") AS studentGrades",
                    "studentGrades.StudentID = s.ID AND s.GroupFormationYear = g.FormationYear AND " +
                    "s.GroupNumber = g.Number AND g.SpecialtyNumber = sp.Number AND p.ID = s.ID",
                    "s.ID",
                    orderByContent: "s.ID ASC, p.Name ASC"));
                _documentEditService.AddParagraph(_resourceService.GetString
                    (_studentsAverageGradesParagraphResourceKey));
                _documentEditService.AddTable(ExecutingResult);
                ExecuteCommand(CreateSelectCommand("s.ID, p.Name, " +
                    "sp.DepartmentName AS Department, g.Number AS GroupNumber, " +
                    "g.FormationYear AS GroupFormationYear, d.ID AS DisciplineID, " +
                    "d.Name AS DisciplineName, studentGrades.GradeName AS Grade",
                    "Students s, Persons p, Groups g, Specialties sp, Disciplines d " +
                    "JOIN (" +
                    CreateSelectCommand("gs.StudentID, gs.DisciplineID, MAX(gs.PassingDate), " +
                    "gs.GradeName",
                    "GradeStatements gs",
                    groupByContent: "gs.StudentID, gs.DisciplineID") +
                    ") AS studentGrades",
                    "studentGrades.StudentID = s.ID AND s.GroupFormationYear = g.FormationYear AND " +
                    "s.GroupNumber = g.Number AND g.SpecialtyNumber = sp.Number AND p.ID = s.ID AND " +
                    "studentGrades.DisciplineID = d.ID",
                    "s.ID, d.ID",
                    orderByContent: "s.ID ASC, p.Name ASC"));
                _documentEditService.AddParagraph(_resourceService.GetString
                    (_studentsGradesParagraphResourceKey));
                _documentEditService.AddTable(ExecutingResult);
            });
            DepartmentsCommand = new RelayCommand(() =>
            {
                _documentEditService.Clear();
                ExecuteCommand(CreateSelectCommand("sp.DepartmentName AS Department, " +
                    "s.GroupNumber, s.GroupFormationYear, s.ScholarshipName AS Scholarship, " +
                    "Count(s.ID) AS Count",
                    "Students s, Groups g, Specialties sp",
                    "s.GroupNumber = g.Number AND s.GroupFormationYear = g.FormationYear AND " +
                    "g.SpecialtyNumber = sp.Number",
                    "sp.DepartmentName, s.GroupNumber, s.GroupFormationYear, s.ScholarshipName",
                    orderByContent: "sp.DepartmentName ASC, s.GroupNumber ASC, " +
                    "s.GroupFormationYear ASC"));
                _documentEditService.AddParagraph(_resourceService.GetString
                    (_departmentsScholarshipsParagraphResourceKey));
                _documentEditService.AddTable(ExecutingResult);
                ExecuteCommand(CreateSelectCommand("sp.DepartmentName AS Department, " +
                    "g.Number AS GroupNumber, g.FormationYear AS GroupFormationYear, " +
                    "AVG(studentGrades.Coefficient) AS AverageGrade",
                    "Students s, Groups g, Specialties sp " +
                    "JOIN (" +
                    CreateSelectCommand("gs.StudentID, gs.DisciplineID, MAX(gs.PassingDate), " +
                    "g.Coefficient",
                    "Grades g, GradeStatements gs",
                    "g.Name = gs.GradeName",
                    "gs.StudentID, gs.DisciplineID") +
                    ") AS studentGrades",
                    "studentGrades.StudentID = s.ID AND " +
                    "s.GroupFormationYear = g.FormationYear AND s.GroupNumber = g.Number AND " +
                    "g.SpecialtyNumber = sp.Number",
                    "sp.DepartmentName, g.Number, g.FormationYear",
                    orderByContent: "sp.DepartmentName ASC, s.GroupNumber ASC, " +
                    "s.GroupFormationYear ASC"));
                _documentEditService.AddParagraph(_resourceService.GetString
                    (_departmentsAverageGradesParagraphResourceKey));
                _documentEditService.AddTable(ExecutingResult);
            });
            PrintCommand = new RelayCommand(() =>
            {
                var source = Document as IDocumentPaginatorSource;
                _printService.Print(source.DocumentPaginator,
                    _resourceService.GetString(_documentDescriptionResourceKey));
            });
        }
    }
}