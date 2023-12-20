using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ViewModel.VMs.Request.ParameterRequest.SpecialViewData
{
    public class PassedDepartmentStudentDisciplinesViewRequestVM : OrderParameterRequestVM
    {
        public override ObservableCollection<string> Columns => new()
        { "Department", "GroupNumber", "GroupFormationYear", "StudentID", "StudentName",
            "DisciplineID", "DisciplineName" };

        public bool IsPassed { get; set; } = false;

        public override string GetMainPartRequest() =>
            "SELECT s.DepartmentName AS Department, g.Number AS GroupNumber, " +
            "g.FormationYear AS GroupFormationYear, c.StudentID AS StudentID, " +
            "p.Name AS StudentName, c.DisciplineID AS DisciplineID, " +
            "d.Name AS DisciplineName " +
            "FROM Specialties s, Groups g, Students st, Persons p, " +
            "StudentDisciplineConnections c, Disciplines d " +
            "WHERE s.Number = g.SpecialtyNumber AND st.GroupNumber = g.Number AND " +
            "st.GroupFormationYear = g.FormationYear AND st.ID = p.ID AND " +
            "c.StudentID = st.ID AND c.DisciplineID = d.ID AND c.IsPassed = @IsPassed";

        public override Dictionary<string, object> GetParameters() => new()
        {
            ["@IsPassed"] = IsPassed
        };
    }
}
