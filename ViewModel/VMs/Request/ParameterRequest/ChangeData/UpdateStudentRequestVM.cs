using System;
using System.Collections.Generic;

namespace ViewModel.VMs.Request.ParameterRequest.ChangeData
{
    public class UpdateStudentRequestVM : ParameterRequestVM
    {
        public override string Table => "Students";

        public long ID { get; set; }

        public int RecordBookNumber { get; set; }

        public bool IsDeductible { get; set; }

        public string GroupNumber { get; set; } = "";

        public int GroupFormationYear { get; set; } = DateTime.Now.Year;

        public string? ScholarshipName { get; set; }

        public override string GetRequest() =>
            "UPDATE Students " +
            "SET RecordBookNumber = @RecordBookNumber, " +
            "IsDeductible = @IsDeductible, GroupNumber = @GroupNumber, " +
            "GroupFormationYear = @GroupFormationYear, " +
            "ScholarshipName = @ScholarshipName " +
            "WHERE ID = @ID";

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
