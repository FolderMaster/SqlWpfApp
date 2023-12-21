using System.Collections.Generic;

namespace ViewModel.VMs.Request.ParameterRequest.SpecialChangeData
{
    public class SetStudentScholarshipsByPassingsRequestVM : ParameterRequestVM
    {
        public override string Table => "Students";

        public bool IsPassed { get; set; } = false;

        public string FutureScholarshipName { get; set; } = "";

        public int MinCountIsPassed { get; set; } = 1;

        public string CurrentScholarshipName { get; set; } = "";

        public override string GetRequest() =>
            "UPDATE Students " +
            "SET ScholarshipName = @FutureScholarshipName " +
            "WHERE ID IN " +
            "(SELECT s.ID " +
            "FROM Students s, " +
            "(SELECT c.StudentID, COUNT(c.IsPassed) AS CountIsPassed, " +
            "s.ScholarshipName " +
            "FROM StudentDisciplineConnections c, Students s " +
            "WHERE s.ID = c.StudentID AND c.IsPassed = @IsPassed " +
            "GROUP BY c.StudentID, s.ScholarshipName) AS passings " +
            "WHERE passings.StudentID = s.ID AND " +
            "passings.CountIsPassed >= @MinCountIsPassed AND " +
            "passings.ScholarshipName LIKE @CurrentScholarshipName)";

        public override Dictionary<string, object> GetParameters() => new()
        {
            ["@FutureScholarshipName"] = FutureScholarshipName,
            ["@IsPassed"] = IsPassed,
            ["@MinCountIsPassed"] = MinCountIsPassed,
            ["@CurrentScholarshipName"] = CurrentScholarshipName
        };
    }
}
