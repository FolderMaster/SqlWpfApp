using System.Collections.Generic;

namespace ViewModel.VMs.Request.ParameterRequest.SpecialChangeData
{
    public class SetStudentPassingsByLastGradesRequestVM : ParameterRequestVM
    {
        public override string Table => "StudentDisciplineConnections";

        public bool IsPassed { get; set; } = true;

        public int MinGradeCoefficient { get; set; } = 3;

        public int MaxGradeCoefficient { get; set; } = 5;

        public override string GetRequest() =>
            "UPDATE sdc " +
            "SET IsPassed = @IsPassed " +
            "FROM StudentDisciplineConnections sdc " +
            "WHERE EXISTS " +
            "(SELECT 1 " +
            "FROM (" +
            "SELECT Coefficient, StudentID, DisciplineID " +
            "FROM GradeStatements gst, Grades gr " +
            "WHERE gr.Name = gst.GradeName AND StudentID = gst.StudentID AND " +
            "DisciplineID = gst.DisciplineID AND NOT EXISTS (" +
            "SELECT 1 " +
            "FROM GradeStatements gs " +
            "WHERE gs.StudentID = gst.StudentID AND gs.DisciplineID = gst.DisciplineID AND " +
            "gs.PassingDate > gst.PassingDate)" +
            ") AS lastGrades " +
            "WHERE lastGrades.StudentID = sdc.StudentID AND " +
            "lastGrades.DisciplineID = sdc.DisciplineID AND " +
            "(lastGrades.Coefficient BETWEEN @MinGradeCoefficient AND @MaxGradeCoefficient))";

        public override Dictionary<string, object> GetParameters() => new()
        {
            ["@IsPassed"] = IsPassed,
            ["@MinGradeCoefficient"] = MinGradeCoefficient,
            ["@MaxGradeCoefficient"] = MaxGradeCoefficient
        };
    }
}
