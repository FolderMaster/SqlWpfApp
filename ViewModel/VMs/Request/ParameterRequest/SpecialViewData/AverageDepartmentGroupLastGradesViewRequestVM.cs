using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ViewModel.VMs.Request.ParameterRequest.SpecialViewData
{
    public class AverageDepartmentGroupLastGradesViewRequestVM : OrderParameterRequestVM
    {
        public override ObservableCollection<string> Columns => new()
        { "Department", "GroupNumber", "GroupFormationYear", "AverageGrade" };

        public string DepartmentName { get; set; } = "%";

        public string GroupNumber { get; set; } = "%";

        public int MinGroupFormationYear { get; set; } = DateTime.Now.Year;

        public int MaxGroupFormationYear { get; set; } = DateTime.Now.Year;

        public override string GetMainPartRequest() =>
            "SELECT sp.DepartmentName AS Department, g.Number AS GroupNumber, " +
            "g.FormationYear AS GroupFormationYear, " +
            "AVG(CAST(studentGrades.Coefficient AS REAL)) AS AverageGrade " +
            "FROM Students s, Groups g, Specialties sp , (" +
            "SELECT StudentID, DisciplineID, PassingDate, Coefficient " +
            "FROM GradeStatements gs, Grades gr " +
            "WHERE gr.Name = gs.GradeName AND NOT EXISTS (" +
            "SELECT 1 " +
            "FROM GradeStatements grs " +
            "WHERE grs.StudentID = gs.StudentID AND " +
            "grs.DisciplineID = gs.DisciplineID AND " +
            "grs.PassingDate > gs.PassingDate)" +
            ") AS studentGrades " +
            "WHERE studentGrades.StudentID = s.ID AND " +
            "s.GroupFormationYear = g.FormationYear AND s.GroupNumber = g.Number AND " +
            "g.SpecialtyNumber = sp.Number " +
            "GROUP BY sp.DepartmentName, g.Number, g.FormationYear " +
            "HAVING sp.DepartmentName LIKE @DepartmentName AND " +
            "g.Number LIKE @GroupNumber AND " +
            "g.FormationYear BETWEEN @MinGroupFormationYear AND @MaxGroupFormationYear";

        public override Dictionary<string, object> GetParameters() => new()
        {
            ["@DepartmentName"] = DepartmentName,
            ["@GroupNumber"] = GroupNumber,
            ["@MinGroupFormationYear"] = MinGroupFormationYear,
            ["@MaxGroupFormationYear"] = MaxGroupFormationYear
        };
    }
}
