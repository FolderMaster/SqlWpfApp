using System;
using System.Collections.Generic;

namespace ViewModel.VMs.Request.ParameterRequest.ChangeData
{
    public class UpdateGradeStatementRequestVM : ParameterRequestVM
    {
        public override string Table => "GradeStatements";

        public long ID { get; set; }

        public long StudentID { get; set; }

        public long DisciplineID { get; set; }

        public long TeacherID { get; set; }

        public string GradeName { get; set; } = "";

        public DateTime PassingDate { get; set; } = DateTime.Now;

        public override string GetRequest() =>
            "UPDATE GradeStatements " +
            "SET StudentID = @StudentID, DisciplineID = @DisciplineID, " +
            "TeacherID = @TeacherID, GradeName = @GradeName, " +
            "PassingDate = @PassingDate " +
            "WHERE ID = @ID";

        public override Dictionary<string, object> GetParameters() => new()
        {
            ["@ID"] = ID,
            ["@StudentID"] = StudentID,
            ["@DisciplineID"] = DisciplineID,
            ["@TeacherID"] = TeacherID,
            ["@GradeName"] = GradeName,
            ["@PassingDate"] = PassingDate
        };
    }
}
