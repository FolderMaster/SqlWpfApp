using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.VMs.Request.ParameterRequest.SpecialChangeData
{
    public class SetStudentPassingsByLastGradesRequestVM : ParameterRequestVM
    {
        public override string Table => "StudentDisciplineConnections";

        public override Dictionary<string, object> GetParameters()
        {
            throw new NotImplementedException();
        }

        public override string GetRequest()
        {
            throw new NotImplementedException();
            /** table = "StudentDisciplineConnections";
                    command = $"UPDATE {table} " +
                        "SET IsPassed = @IsPassed " +
                        "(StudentID, DisciplineID) IN " +
                        "" +
                        "FROM Students " +
                        "(GroupFormationYear BETWEEN @MinGroupFormationYear AND " +
                        "@MaxGroupFormationYear) AND GroupNumber LIKE @GroupNumber)";
                    command = CreateUpdateCommand(table, "IsPassed = " +
                        $"{ParametersVMs[parametersIndex].Parameters[0]}",
                        "(StudentID, DisciplineID) IN (" +
                        CreateSelectCommand("gs.StudentID, gs.DisciplineID",
                        "GradeStatements gs " +
                        "INNER JOIN(" +
                        CreateSelectCommand("gs.StudentID, gs.DisciplineID, MAX(gs.PassingDate), " +
                        "g.Coefficient",
                        "Grades g, GradeStatements gs",
                        "g.Name = gs.GradeName",
                        "gs.StudentID, gs.DisciplineID") +
                        ") AS lastGrades " +
                        "ON gs.StudentID = lastGrades.StudentID AND " +
                        "gs.DisciplineID = lastGrades.DisciplineID AND " +
                        "lastGrades.Coefficient BETWEEN " +
                        $"{ParametersVMs[parametersIndex].Parameters[1]} " +
                        "AND " +
                        $"{ParametersVMs[parametersIndex].Parameters[2]}") + ")");
            **/
        }
    }
}
