using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ViewModel.VMs.Request.ParameterRequest.SpecialViewData
{
    public class DepartmentScholarshipCountsViewRequestVM : OrderParameterRequestVM
    {
        public override ObservableCollection<string> Columns => new()
        { "Department", "Scholarship", "Count" };

        public string DisciplineName { get; set; } = "%";

        public string StudyFormName { get; set; } = "%";

        public override string GetMainPartRequest() =>
            "SELECT sp.DepartmentName AS Department, " +
            "s.ScholarshipName AS Scholarship, COUNT(s.ScholarshipName) AS Count " +
            "FROM Students s, Persons p, Groups g, Specialties sp " +
            "WHERE p.ID = s.ID AND g.Number = s.GroupNumber AND " +
            "sp.Number = g.SpecialtyNumber " +
            "GROUP BY sp.DepartmentName, s.ScholarshipName " +
            "HAVING sp.DepartmentName LIKE @DepartmentName AND " +
            "s.ScholarshipName LIKE @ScholarshipName";

        public override Dictionary<string, object> GetParameters() => new()
        {
            ["@DisciplineName"] = DisciplineName,
            ["@StudyFormName"] = StudyFormName
        };
    }
}
