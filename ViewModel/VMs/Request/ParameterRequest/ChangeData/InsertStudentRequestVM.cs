using System;
using System.Collections.Generic;

namespace ViewModel.VMs.Request.ParameterRequest.ChangeData
{
    public class InsertStudentRequestVM : ParameterRequestVM
    {
        public override string Table => "Students";

        public long ID { get; set; }

        public int RecordBookNumber { get; set; }

        public bool IsDeductible { get; set; }

        public string GroupNumber { get; set; } = "";

        public int GroupFormationYear { get; set; } = DateTime.Now.Year;

        public string? ScholarshipName { get; set; }

        public override string GetRequest() =>
            "INSERT INTO Students" +
            "(ID, RecordBookNumber, IsDeductible, GroupNumber, " +
            "GroupFormationYear, ScholarshipName) " +
            "VALUES (@ID, @RecordBookNumber, @IsDeductible, @GroupNumber, " +
            "@GroupFormationYear, @ScholarshipName)";

        public override Dictionary<string, object> GetParameters() => new()
        {
            ["@ID"] = ID,
            ["@RecordBookNumber"] = RecordBookNumber,
            ["@IsDeductible"] = IsDeductible,
            ["@GroupNumber"] = GroupNumber,
            ["@GroupFormationYear"] = GroupFormationYear,
            ["@ScholarshipName"] = ScholarshipName
        };
    }
}
