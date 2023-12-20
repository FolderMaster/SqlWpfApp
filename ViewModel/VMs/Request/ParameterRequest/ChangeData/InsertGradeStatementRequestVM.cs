using System;
using System.Collections.Generic;

namespace ViewModel.VMs.Request.ParameterRequest.ChangeData
{
    public class InsertGradeStatementRequestVM : ParameterRequestVM
    {
        public override string Table => "GradeStatements";

        public long StudentID { get; set; }

        public long DisciplineID { get; set; }

        public long TeacherID { get; set; }

        public string GradeName { get; set; } = "";

        public DateTime PassingDate { get; set; } = DateTime.Now;

        public override string GetRequest() =>
            "INSERT INTO GradeStatements " +
            "(StudentID, DisciplineID, TeacherID, GradeName, PassingDate) " +
            "VALUES (@StudentID, @DisciplineID, @TeacherID, @GradeName, @PassingDate)";

        public override Dictionary<string, object> GetParameters() => new()
        {
            ["@StudentID"] = StudentID,
            ["@DisciplineID"] = DisciplineID,
            ["@TeacherID"] = TeacherID,
            ["@GradeName"] = GradeName,
            ["@PassingDate"] = PassingDate
        };
    }
}
