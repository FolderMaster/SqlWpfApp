using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ViewModel.VMs.Request.ParameterRequest.SpecialViewData
{
    public class DeductibleDepartmentStudentsViewRequestVM : OrderParameterRequestVM
    {
        public override ObservableCollection<string> Columns => new()
        { "Department", "ID", "Name" };

        public string DepartmentName { get; set; } = "%";

        public override string GetMainPartRequest() =>
            "SELECT sp.DepartmentName AS Department, s.ID AS ID, p.Name AS Name " +
            "FROM Students s, Persons p, Groups g, Specialties sp " +
            "WHERE p.ID = s.ID AND g.Number = s.GroupNumber AND " +
            "sp.Number = g.SpecialtyNumber AND s.IsDeductible = 1 AND " +
            "sp.DepartmentName LIKE @DepartmentName " +
            "GROUP BY sp.DepartmentName, s.ID, p.Name";

        public override Dictionary<string, object> GetParameters() => new()
        {
            ["@DepartmentName"] = DepartmentName
        };
    }
}
