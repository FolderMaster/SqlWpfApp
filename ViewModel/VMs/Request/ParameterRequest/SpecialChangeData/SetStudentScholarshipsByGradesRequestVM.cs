using System;
using System.Collections.Generic;

namespace ViewModel.VMs.Request.ParameterRequest.SpecialChangeData
{
    public class SetStudentScholarshipsByGradesRequestVM : ParameterRequestVM
    {
        public override string Table => "Students";

        public string FutureScholarshipName { get; set; } = "";

        public int MinLastGradesCount { get; set; } = 1;

        public int MinLastGradeCoefficient { get; set; } = 3;

        public string CurrentScholarshipName { get; set; } = "";

        public string PermanentResidenceAddress { get; set; } = "%";

        public override string GetRequest() =>
            "UPDATE Students " +
            "SET ScholarshipName = @FutureScholarshipName " +
            "WHERE ID IN" +
            "(SELECT minLastGrades.StudentID " +
            "FROM Students st, Persons p, Passports pa, (" +
            "SELECT StudentID, MIN(lastGrades.Coefficient) AS MinCoefficient " +
            "FROM (" +
            "SELECT StudentID, DisciplineID, Coefficient " +
            "FROM GradeStatements gs, Grades gr " +
            "WHERE gr.Name = gs.GradeName AND NOT EXISTS" +
            "(SELECT 1 " +
            "FROM GradeStatements grs " +
            "WHERE grs.StudentID = gs.StudentID AND grs.DisciplineID = gs.DisciplineID AND " +
            "grs.PassingDate > gs.PassingDate)" +
            ") AS lastGrades " +
            "GROUP BY StudentID" +
            ") AS minLastGrades, (" +
            "SELECT StudentID, DisciplineID, Coefficient " +
            "FROM GradeStatements gs, Grades gr " +
            "WHERE gr.Name = gs.GradeName AND NOT EXISTS " +
            "(SELECT 1 " +
            "FROM GradeStatements grs " +
            "WHERE grs.StudentID = gs.StudentID AND grs.DisciplineID = gs.DisciplineID AND " +
            "grs.PassingDate > gs.PassingDate)" +
            ") AS lastGrades " +
            "WHERE lastGrades.Coefficient = MinCoefficient AND " +
            "st.ID = minLastGrades.StudentID AND st.ID = lastGrades.StudentID AND " +
            "st.ID = p.ID AND p.PassportSerialNumber = pa.SerialNumber AND " +
            "pa.PermanentResidenceAddress LIKE @PermanentResidenceAddress AND " +
            "st.ScholarshipName LIKE @CurrentScholarshipName AND " +
            "MinCoefficient = @MinLastGradeCoefficient " +
            "GROUP BY minLastGrades.StudentID, MinCoefficient " +
            "HAVING COUNT(Coefficient) = @MinLastGradesCount)";

        public override Dictionary<string, object> GetParameters() => new()
        {
            ["@FutureScholarshipName"] = FutureScholarshipName,
            ["@MinLastGradesCount"] = MinLastGradesCount,
            ["@MinLastGradeCoefficient"] = MinLastGradeCoefficient,
            ["@CurrentScholarshipName"] = CurrentScholarshipName,
            ["@PermanentResidenceAddress"] = PermanentResidenceAddress
        };
    }
}
