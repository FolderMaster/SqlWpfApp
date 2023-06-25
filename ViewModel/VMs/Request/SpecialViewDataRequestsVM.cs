using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using ViewModel.Classes;
using ViewModel.Enums;
using ViewModel.Services;
using ViewModel.VMs.Request;

namespace ViewModel.VMs
{
    public partial class SpecialViewDataRequestsVM : ViewRequestsVM
    {
        [ObservableProperty]
        private SpecialViewDataRequest _request =
            SpecialViewDataRequest.AverageDisciplineLastGrades;

        [ObservableProperty]
        private ObservableCollection<ParametersVM> parametersVMs = new()
        {
            new ParametersVM(new object[] {
                new ObservableCollection<string>() { "ID", "Name", "StudyForm", "AverageGrade" },
                new ObservableCollection<SortItem>(), "%%", "%%"
            }),
            new ParametersVM(new object[] {
                new ObservableCollection<string>() { "Department", "Scholarship", "Count" },
                new ObservableCollection<SortItem>(), "%%", "%%"
            }),
            new ParametersVM(new object[] {
                new ObservableCollection<string>() { "Department", "ID", "Name" },
                new ObservableCollection<SortItem>(), "%%"
            }),
            new ParametersVM(new object[] {
                new ObservableCollection<string>() { "ID", "Name", "StudyForm", "Count" },
                new ObservableCollection<SortItem>(), 0, "%%", "%%"
            }),
            new ParametersVM(new object[] {
                new ObservableCollection<string>() { "Department", "GroupNumber",
                    "GroupFormationYear", "AverageGrade" },
                new ObservableCollection<SortItem>(), "%%", "%%", DateTime.Now.Year,
                DateTime.Now.Year
            }),
            new ParametersVM(new object[] {
                new ObservableCollection<string>() { "Department", "GroupNumber",
                    "GroupFormationYear", "StudentID", "StudentName", "DisciplineID",
                    "DisciplineName" }, new ObservableCollection<SortItem>(), 0
            })
        };

        public RelayCommand ExecuteSqlCommand { get; private set; }

        public SpecialViewDataRequestsVM(IMessageService messageService) : base(messageService)
        {
            ExecuteSqlCommand = new RelayCommand(() => ExecuteSqlCommand(CreateSpecialCommand()));
        }
        
        private string CreateSpecialCommand()
        {
            var selectContent = "";
            var fromContent = "";
            var whereContent = "";
            var groupByContent = "";
            var havingContent = "";
            var orderByContent = "";

            var parametersIndex = (int)Request - 1;
            var parametersVM = ParametersVMs[parametersIndex];
            switch (Request)
            {
                case SpecialViewDataRequest.AverageDisciplineLastGrades:
                    selectContent = "d.ID AS ID, d.Name AS Name, d.StudyFormName AS StudyForm, " +
                        "AVG(studentGrades.Coefficient) AS AverageGrade";
                    fromContent = "Disciplines d " +
                        "JOIN (" +
                        CreateSelectCommand("gs.StudentID, gs.DisciplineID, MAX(gs.PassingDate), " +
                        "g.Coefficient",
                        "Grades g, GradeStatements gs",
                        "g.Name = gs.GradeName",
                        "gs.StudentID, gs.DisciplineID") +
                        ") AS studentGrades";
                    whereContent = "studentGrades.DisciplineID = d.ID AND d.Name LIKE " +
                        $"'{parametersVM.Parameters[2]}' " +
                        "AND d.StudyFormName LIKE " +
                        $"'{parametersVM.Parameters[3]}'";
                    groupByContent = "d.ID"; break;
                case SpecialViewDataRequest.DepartmentScholarshipCounts:
                    selectContent = "sp.DepartmentName AS Department, s.ScholarshipName AS " +
                        "Scholarship, COUNT(s.ScholarshipName) AS Count";
                    fromContent = "Students s, Persons p, Groups g, Specialties sp";
                    whereContent = "p.ID = s.ID AND g.Number = s.GroupNumber AND sp.Number = " +
                        "g.SpecialtyNumber";
                    groupByContent = "sp.DepartmentName, s.ScholarshipName";
                    havingContent = "sp.DepartmentName LIKE " +
                        $"'{parametersVM.Parameters[2]}' " +
                        "AND s.ScholarshipName LIKE " +
                        $"'{parametersVM.Parameters[3]}'"; break;
                case SpecialViewDataRequest.DeductibleDepartmentStudents:
                    selectContent = "sp.DepartmentName AS Department, s.ID AS ID, p.Name AS Name";
                    fromContent = "Students s, Persons p, Groups g, Specialties sp";
                    whereContent = "p.ID = s.ID AND g.Number = s.GroupNumber AND sp.Number = " +
                        "g.SpecialtyNumber AND s.IsDeductible = 1 AND sp.DepartmentName LIKE " +
                        $"'{parametersVM.Parameters[2]}'"; break;
                case SpecialViewDataRequest.PassingDisciplineCounts:
                    selectContent = "d.ID AS ID, d.Name AS Name, d.StudyFormName StudyForm, " +
                        "COUNT(c.IsPassed) AS Count";
                    fromContent = "StudentDisciplineConnections c, Disciplines d";
                    whereContent = "c.IsPassed = " +
                        $"{parametersVM.Parameters[2]} " +
                        "AND c.DisciplineID = d.ID AND d.Name LIKE " +
                        $"'{parametersVM.Parameters[3]}' " +
                        "AND d.StudyFormName LIKE " +
                        $"'{parametersVM.Parameters[4]}'";
                    groupByContent = "d.ID"; break;
                case SpecialViewDataRequest.AverageDepartmentGroupLastGrades:
                    selectContent = "sp.DepartmentName AS Department, g.Number AS GroupNumber, " +
                        "g.FormationYear AS GroupFormationYear, " +
                        "AVG(studentGrades.Coefficient) AS AverageGrade";
                    fromContent = "Students s, Groups g, Specialties sp " +
                        "JOIN (" +
                        CreateSelectCommand("gs.StudentID, gs.DisciplineID, MAX(gs.PassingDate), " +
                        "g.Coefficient",
                        "Grades g, GradeStatements gs",
                        "g.Name = gs.GradeName",
                        "gs.StudentID, gs.DisciplineID") +
                        ") AS studentGrades";
                    whereContent = "studentGrades.StudentID = s.ID AND " +
                        "s.GroupFormationYear = g.FormationYear AND s.GroupNumber = g.Number AND " +
                        "g.SpecialtyNumber = sp.Number";
                    groupByContent = "sp.DepartmentName, g.Number, g.FormationYear";
                    havingContent = "sp.DepartmentName LIKE " +
                        $"'{parametersVM.Parameters[2]}' " +
                        "AND g.Number LIKE " +
                        $"'{parametersVM.Parameters[3]}' " +
                        "AND g.FormationYear BETWEEN " +
                        $"{parametersVM.Parameters[4]} " +
                        "AND " +
                        $"{parametersVM.Parameters[5]}"; break;
                case SpecialViewDataRequest.PassedDepartmentStudentDisciplines:
                    selectContent = "s.DepartmentName AS Department, g.Number AS GroupNumber, " +
                        "g.FormationYear AS GroupFormationYear, c.StudentID AS StudentID, " +
                        "p.Name AS StudentName, c.DisciplineID AS DisciplineID, " +
                        "d.Name AS DisciplineName";
                    fromContent = "Specialties s, Groups g, Students s, Persons p, " +
                        "StudentDisciplineConnections c, Disciplines d";
                    whereContent = "s.Number = g.SpecialtyNumber AND s.GroupNumber = g.Number AND " +
                        "s.GroupFormationYear = g.FormationYear AND s.ID = p.ID AND " +
                        "c.StudentID = s.ID AND c.DisciplineID = d.ID AND c.IsPassed = " +
                        $"{parametersVM.Parameters[2]}"; break;
            }

            var sortCollection =
                (ObservableCollection<SortItem>)parametersVM.Parameters[1];
            if (sortCollection.Count > 0)
            {
                List<string> sortCollectionStrings = new();
                foreach (var sortItem in sortCollection)
                {
                    sortCollectionStrings.Add($"{sortItem.ColumnName} {sortItem.SortMode}");
                }
                orderByContent = string.Join(',', sortCollectionStrings);
            }

            return CreateSelectCommand(selectContent, fromContent, whereContent, groupByContent,
                havingContent, orderByContent);
        }
    }
}