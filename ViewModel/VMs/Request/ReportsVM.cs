using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Documents;
using System.Data;
using System.Windows.Media;
using System.Windows;

using ViewModel.Interfaces;

namespace ViewModel.VMs.Request
{
    public partial class ReportsVM : ViewRequestsVM
    {
        private IPrintDialogService _printDialogService;

        [ObservableProperty]
        private FlowDocument document = new();

        public RelayCommand PrintCommand { get; private set; }

        public RelayCommand DeductingsCommand { get; private set; }

        public RelayCommand StudentsCommand { get; private set; }

        public RelayCommand DepartmentsCommand { get; private set; }

        public ReportsVM(IDbContextCreator dataBaseContextCreator,
            IMessageService messageService, IPrintDialogService printDialog) :
            base(dataBaseContextCreator, messageService)
        {
            _printDialogService = printDialog;

            DeductingsCommand = new RelayCommand(() =>
            {
                Document = new();
                ExecuteSqlCommand(CreateSelectCommand("s.ID, p.Name, s.GroupNumber, " +
                    "s.GroupFormationYear, sp.DepartmentName AS Department",
                    "Students s, Persons p, Groups g, Specialties sp",
                    "IsDeductible = 1 AND s.ID = p.ID AND s.GroupNumber = g.Number AND " +
                    "s.GroupFormationYear = g.FormationYear AND g.SpecialtyNumber = sp.Number",
                    orderByContent: "s.ID ASC, p.Name ASC"));
                Document.Blocks.Add(new Paragraph(new Run("Students:")));
                Document.Blocks.Add(CreateTable(ExecutingResult));
            });
            StudentsCommand = new RelayCommand(() =>
            {
                Document = new();
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
                Document.Blocks.Add(new Paragraph(new Run("Average grades:")));
                Document.Blocks.Add(CreateTable(ExecutingResult));
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
                Document.Blocks.Add(new Paragraph(new Run("Grades:")));
                Document.Blocks.Add(CreateTable(ExecutingResult));
            });
            DepartmentsCommand = new RelayCommand(() =>
            {
                Document = new();
                ExecuteSqlCommand(CreateSelectCommand("sp.DepartmentName AS Department, " +
                    "s.GroupNumber, s.GroupFormationYear, s.ScholarshipName AS Scholarship, " +
                    "Count(s.ID) AS Count",
                    "Students s, Groups g, Specialties sp",
                    "s.GroupNumber = g.Number AND s.GroupFormationYear = g.FormationYear AND " +
                    "g.SpecialtyNumber = sp.Number",
                    "sp.DepartmentName, s.GroupNumber, s.GroupFormationYear, s.ScholarshipName",
                    orderByContent: "sp.DepartmentName ASC, s.GroupNumber ASC, " +
                    "s.GroupFormationYear ASC"));
                Document.Blocks.Add(new Paragraph(new Run("Scholarships:")));
                Document.Blocks.Add(CreateTable(ExecutingResult));
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
                Document.Blocks.Add(new Paragraph(new Run("Average grades:")));
                Document.Blocks.Add(CreateTable(ExecutingResult));
            });
            PrintCommand = new RelayCommand(() =>
            {
                var source = Document as IDocumentPaginatorSource;
                _printDialogService.Print(source.DocumentPaginator, "Report");
            });
        }

        private Table CreateTable(DataTable dataTable)
        {
            var table = new Table();
            table.CellSpacing = 0;

            var rowGroup = new TableRowGroup();
            table.RowGroups.Add(rowGroup);

            var header = new TableRow();
            rowGroup.Rows.Add(header);

            foreach (DataColumn column in dataTable.Columns)
            {
                var tableColumn = new TableColumn();
                table.Columns.Add(tableColumn);
                var cell = new TableCell(new Paragraph(new Run(column.ColumnName)));
                cell.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                cell.BorderThickness = new Thickness(1);
                header.Cells.Add(cell);
            }
            foreach (DataRow row in dataTable.Rows)
            {
                var tableRow = new TableRow();
                rowGroup.Rows.Add(tableRow);

                foreach (DataColumn column in dataTable.Columns)
                {
                    var value = row[column].ToString();
                    var cell = new TableCell(new Paragraph(new Run(value)));
                    cell.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                    cell.BorderThickness = new Thickness(1);
                    tableRow.Cells.Add(cell);
                }
            }
            return table;
        }
    }
}