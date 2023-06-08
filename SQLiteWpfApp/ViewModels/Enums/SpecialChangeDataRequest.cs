namespace SQLiteWpfApp.ViewModels.Enums
{
    public enum SpecialChangeDataRequest
    {
        None,
        SetNullStudentPassings,
        SetNullStudentDeductings,
        SetStudentPassingsByLastGrades,
        SetStudentPassingsWithoutGrades,
        SetStudentDeductings,
        SetStudentScholarshipsByGrades,
        SetStudentScholarshipsByPassings
    }
}