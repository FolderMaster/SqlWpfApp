using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using System.Windows.Documents;

using ViewModel.Interfaces;
using ViewModel.Services;

namespace ViewModel.VMs.Request
{
    public partial class ReportsVM : ViewRequestsVM
    {
        private static string _deductingsStudentsParagraphResourceKey =
            "DeductingsStudentsParagraph";

        private static string _studentsAverageGradesParagraphResourceKey =
            "StudentsAverageGradesParagraph";

        private static string _studentsGradesParagraphResourceKey = "StudentsGradesParagraph";

        private static string _departmentsScholarshipsParagraphResourceKey =
            "DepartmentsScholarshipsParagraph";

        private static string _departmentsAverageGradesParagraphResourceKey =
            "DepartmentsAverageGradesParagraph";

        private static string _documentDescriptionResourceKey = "DocumentDescription";

        private IPrintService _printDialogService;

        private DocumentEditService _documentEditService;

        [ObservableProperty]
        private FlowDocument document = new();

        public RelayCommand PrintCommand { get; private set; }

        public RelayCommand DeductingsCommand { get; private set; }

        public RelayCommand StudentsCommand { get; private set; }

        public RelayCommand DepartmentsCommand { get; private set; }

        public ReportsVM(IDbContextBuilder dataBaseContextBuilder,
            IResourceService resourceService, IMessageService messageService,
            IPrintService printDialog) :
            base(dataBaseContextBuilder, resourceService, messageService)
        {
            _printDialogService = printDialog;
            _documentEditService = new DocumentEditService(Document);

            DeductingsCommand = new RelayCommand(() =>
            {
                _documentEditService.Clear();
                ExecuteSqlCommand(CreateSelectCommand("s.ID, p.Name, s.GroupNumber, " +
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
                ExecuteSqlCommand(CreateSelectCommand("s.ID, p.Name, " +
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
                ExecuteSqlCommand(CreateSelectCommand("s.ID, p.Name, " +
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
                ExecuteSqlCommand(CreateSelectCommand("sp.DepartmentName AS Department, " +
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
                ExecuteSqlCommand(CreateSelectCommand("sp.DepartmentName AS Department, " +
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
                _printDialogService.Print(source.DocumentPaginator,
                    _resourceService.GetString(_documentDescriptionResourceKey));
            });
        }
    }
}