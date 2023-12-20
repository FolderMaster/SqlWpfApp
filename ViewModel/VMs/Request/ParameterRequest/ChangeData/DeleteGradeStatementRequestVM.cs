using System.Collections.Generic;

namespace ViewModel.VMs.Request.ParameterRequest.ChangeData
{
    public class DeleteGradeStatementRequestVM : ParameterRequestVM
    {
        public override string Table => "GradeStatements";

        public long ID { get; set; }

        public override string GetRequest() =>
            "DELETE FROM GradeStatements " +
            "WHERE ID = @ID";

        public override Dictionary<string, object> GetParameters() => new() { ["@ID"] = ID };
    }
}
