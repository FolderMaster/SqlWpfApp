using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.VMs.Request.ParameterRequest.SpecialChangeData
{
    public class SetStudentScholarshipsByGradesRequestVM : ParameterRequestVM
    {
        public override string Table => "Students";

        public override Dictionary<string, object> GetParameters()
        {
            throw new NotImplementedException();
        }

        public override string GetRequest()
        {
            throw new NotImplementedException();
            /**table = "Students";
                    command = CreateUpdateCommand(table,
                        "ScholarshipName = " +
                        $"\"{ParametersVMs[parametersIndex].Parameters[0]}\"",
                        "ID IN (" +
                        CreateSelectCommand("s.ID",
                        "Students s " +
                        "JOIN (" +
                        CreateSelectCommand("s.ID, " +
                        "COUNT(MinLastGradeCoefficient) AS CountMinLastGrades, " +
                        "MIN(MinLastGradeCoefficient) AS MinLastGradeCoefficient, " +
                        "pa.PermanentResidenceAddress, " +
                        "s.ScholarshipName",
                        "Students s, Persons p, Passports pa " +
                        "LEFT JOIN (" +
                        CreateSelectCommand("gs.StudentID, " +
                        "MIN(g.Coefficient) AS MinLastGradeCoefficient",
                        "GradeStatements gs " +
                        "INNER JOIN Grades g ON gs.GradeName = g.Name",
                        "gs.PassingDate = (" +
                        CreateSelectCommand("MAX(PassingDate)",
                        "GradeStatements",
                        "StudentID = gs.StudentID") + ")",
                        "gs.StudentID") + ")" +
                        "t ON s.ID = t.StudentID",
                        "s.ID = p.ID AND p.PassportSerialNumber = pa.SerialNumber",
                        "S.ID") + ") AS minStatements",
                        "minStatements.ID = s.ID AND " +
                        "minStatements.CountMinLastGrades <= " +
                        $"{ParametersVMs[parametersIndex].Parameters[1]} " +
                        "AND minStatements.MinLastGradeCoefficient = " +
                        $"{ParametersVMs[parametersIndex].Parameters[2]} " +
                        "AND minStatements.ScholarshipName LIKE" +
                        $"'{ParametersVMs[parametersIndex].Parameters[3]}' " +
                        "AND minStatements.PermanentResidenceAddress NOT LIKE " +
                        $"'{ParametersVMs[parametersIndex].Parameters[4]}'") + ")");
            **/
        }
    }
}
