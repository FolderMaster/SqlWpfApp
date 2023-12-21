using System.Collections.Generic;

namespace ViewModel.VMs.Request.ParameterRequest.SpecialChangeData
{
    public class SetStudentPassingsWithoutGradesRequestVM : ParameterRequestVM
    {
        public override string Table => "StudentDisciplineConnections";

        public bool IsPassed { get; set; } = false;

        public override string GetRequest() =>
            "UPDATE sdc " +
            "SET sdc.IsPassed = @IsPassed " +
            "FROM StudentDisciplineConnections sdc " +
            "WHERE NOT EXISTS" +
            "(SELECT 1 " +
            "FROM GradeStatements gs " +
            "WHERE gs.StudentID = sdc.StudentID AND gs.DisciplineID  = sdc.DisciplineID)";

        public override Dictionary<string, object> GetParameters() => new()
        {
            ["@IsPassed"] = IsPassed
        };
    }
}
