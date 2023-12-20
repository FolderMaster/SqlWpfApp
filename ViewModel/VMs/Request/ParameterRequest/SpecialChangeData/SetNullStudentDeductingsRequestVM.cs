using System;
using System.Collections.Generic;

namespace ViewModel.VMs.Request.ParameterRequest.SpecialChangeData
{
    public class SetNullStudentDeductingsRequestVM : ParameterRequestVM
    {
        public override string Table => "Students";

        public override string GetRequest() =>
            $"UPDATE Students " +
            "SET IsDeductible = NULL " +
            "WHERE StudentID IN " +
            "(SELECT ID " +
            "FROM Students " +
            "(GroupFormationYear BETWEEN @MinGroupFormationYear AND " +
            "@MaxGroupFormationYear) AND GroupNumber LIKE @GroupNumber)";

        public override Dictionary<string, object> GetParameters() => new()
        {
        };
    }
}
