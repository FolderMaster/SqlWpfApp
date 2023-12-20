using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.VMs.Request.ParameterRequest.SpecialChangeData
{
    public class SetStudentDeductingsRequestVM : ParameterRequestVM
    {
        public override string Table => "Students";

        public override string GetRequest() => "UPDATE Students " +
                        "SET IsDeductible = @IsDeductible " +
                        "WHERE ID IN" +
                        "(SELECT c.StudentID " +
                        "FROM StudentDisciplineConnections c, (" +
                        "SELECT c.StudentID, Count(c.IsPassed) AS Count " +
                        "FROM StudentDisciplineConnections c " +
                        "WHERE c.IsPassed = @IsPassed " +
                        "GROUP BY c.StudentID" +
                        ") AS passings " +
                        "WHERE c.StudentID = passings.StudentID AND " +
                        "passings.Count >= @MinCountIsPassed)";

        public override Dictionary<string, object> GetParameters() => new()
        {
        };
    }
}
