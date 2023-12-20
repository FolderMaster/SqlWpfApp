using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ViewModel.VMs.Request.ParameterRequest.SpecialViewData
{
    public class PassingDisciplineCountsViewRequestVM : OrderParameterRequestVM
    {
        public override ObservableCollection<string> Columns => new()
        { "ID", "Name", "StudyForm", "Count" };

        public string DisciplineName { get; set; } = "%";

        public string StudyFormName { get; set; } = "%";

        public bool? IsPassed { get; set; } = false;

        public override string GetMainPartRequest() =>
            "SELECT d.ID AS ID, d.Name AS Name, d.StudyFormName StudyForm, " +
            "COUNT(c.IsPassed) AS Count " +
            "FROM StudentDisciplineConnections c, Disciplines d " +
            "WHERE c.IsPassed = @IsPassed  AND c.DisciplineID = d.ID AND " +
            "d.Name LIKE @DisciplineName AND d.StudyFormName LIKE @StudyFormName " +
            "GROUP BY d.ID, d.Name, d.StudyFormName";

        public override Dictionary<string, object> GetParameters() => new()
        {
            ["@DisciplineName"] = DisciplineName,
            ["@StudyFormName"] = StudyFormName,
            ["@IsPassed"] = IsPassed
        };
    }
}
