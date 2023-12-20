using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.VMs.Request.ParameterRequest.SpecialChangeData
{
    public class SetStudentPassingsWithoutGradesRequestVM : ParameterRequestVM
    {
        public override string Table => "StudentDisciplineConnections";

        public override string GetRequest()
        {
            /**
             * $"UPDATE {table} " +
                        "SET IsPassed = @IsPassed " +
                        "SET IsPassed = @IsPassed " +
                        $"{ParametersVMs[parametersIndex].Parameters[0]}",
                        "WHERE(StudentID, DisciplineID) NOT IN (" +
                        CreateSelectCommand("DISTINCT gs.StudentID, gs.DisciplineID",
                        "GradeStatements gs") + ")");
            **/
            throw new NotImplementedException();
        }

        public override Dictionary<string, object> GetParameters() => new()
        {
        };
    }
}
