using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ViewModel.VMs.Request.ParameterRequest.SpecialViewData
{
    public class AverageDisciplineLastGradesViewRequestVM : OrderParameterRequestVM
    {
        public override ObservableCollection<string> Columns => new()
        { "ID", "Name", "StudyForm", "AverageGrade" };

        public string DisciplineName { get; set; } = "%";

        public string StudyFormName { get; set; } = "%";

        public override string GetMainPartRequest() =>
            "SELECT d.ID AS ID, d.Name AS Name, d.StudyFormName AS StudyForm, " +
            "AVG(CAST(studentGrades.Coefficient AS REAL)) AS AverageGrade " +
            "FROM Disciplines d, (" +
            "SELECT StudentID, DisciplineID, PassingDate, Coefficient " +
            "FROM GradeStatements gs, Grades gr " +
            "WHERE gr.Name = gs.GradeName AND NOT EXISTS (" +
            "SELECT 1 " +
            "FROM GradeStatements grs " +
            "WHERE grs.StudentID = gs.StudentID " +
            "AND grs.DisciplineID = gs.DisciplineID " +
            "AND grs.PassingDate > gs.PassingDate)" +
            ") AS studentGrades " +
            "WHERE studentGrades.DisciplineID = d.ID AND " +
            "d.Name LIKE @DisciplineName AND d.StudyFormName LIKE @StudyFormName";

        public override Dictionary<string, object> GetParameters() => new()
        {
            ["@DisciplineName"] = DisciplineName,
            ["@StudyFormName"] = StudyFormName
        };
    }
}
