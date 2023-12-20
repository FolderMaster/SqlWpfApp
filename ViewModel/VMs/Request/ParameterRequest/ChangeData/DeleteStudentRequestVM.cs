using System.Collections.Generic;

namespace ViewModel.VMs.Request.ParameterRequest.ChangeData
{
    public class DeleteStudentRequestVM : ParameterRequestVM
    {
        public override string Table => "Students";

        public long ID { get; set; }

        public override string GetRequest() =>
            "DELETE FROM Students " +
            "WHERE ID = @ID";

        public override Dictionary<string, object> GetParameters() => new() { ["@ID"] = ID };
    }
}
