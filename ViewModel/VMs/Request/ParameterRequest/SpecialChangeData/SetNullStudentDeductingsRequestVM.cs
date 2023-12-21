using System;
using System.Collections.Generic;

namespace ViewModel.VMs.Request.ParameterRequest.SpecialChangeData
{
    public class SetNullStudentDeductingsRequestVM : ParameterRequestVM
    {
        public override string Table => "Students";

        public string GroupNumber { get; set; } = "%";

        public int MinGroupFormationYear { get; set; } = DateTime.Now.Year;

        public int MaxGroupFormationYear { get; set; } = DateTime.Now.Year;

        public override string GetRequest() =>
            "UPDATE Students " +
            "SET IsDeductible = NULL " +
            "WHERE ID IN " +
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
