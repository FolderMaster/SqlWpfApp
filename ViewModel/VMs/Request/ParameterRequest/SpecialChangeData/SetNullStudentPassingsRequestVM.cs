using System;
using System.Collections.Generic;

namespace ViewModel.VMs.Request.ParameterRequest.SpecialChangeData
{
    public class SetNullStudentPassingsRequestVM : ParameterRequestVM
    {
        public override string Table => "StudentDisciplineConnections";

        public string GroupNumber { get; set; } = "%";

        public int MinGroupFormationYear { get; set; } = DateTime.Now.Year;

        public int MaxGroupFormationYear { get; set; } = DateTime.Now.Year;

        public override string GetRequest() =>
            "UPDATE StudentDisciplineConnections " +
            "SET IsPassed = NULL " +
            "WHERE StudentID IN " +
            "(SELECT ID " +
            "FROM Students " +
            "WHERE (GroupFormationYear BETWEEN @MinGroupFormationYear AND " +
            "@MaxGroupFormationYear) AND GroupNumber LIKE @GroupNumber)";

        public override Dictionary<string, object> GetParameters() => new()
        {
            ["@GroupNumber"] = GroupNumber,
            ["@MinGroupFormationYear"] = MinGroupFormationYear,
            ["@MaxGroupFormationYear"] = MaxGroupFormationYear
        };
    }
}
