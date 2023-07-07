namespace ViewModel.Enums
{
    /// <summary>
    /// Перечисление специализированных запросов просмотра данных.
    /// </summary>
    public enum SpecialViewDataRequest
    {
        /// <summary>
        /// Никакой запрос.
        /// </summary>
        None,
        /// <summary>
        /// Средне статические последние оценки по дисциплинам.
        /// </summary>
        AverageDisciplineLastGrades,
        /// <summary>
        /// Количество стипендий по факультетам.
        /// </summary>
        DepartmentScholarshipCounts,
        /// <summary>
        /// Студенты на отчисление по факультетам.
        /// </summary>
        DeductibleDepartmentStudents,
        /// <summary>
        /// Количество сданных дисциплин.
        /// </summary>
        PassingDisciplineCounts,
        /// <summary>
        /// Средне статические последние оценки по группе и факультету.
        /// </summary>
        AverageDepartmentGroupLastGrades,
        /// <summary>
        /// Сданные дисциплины студентами по факультету.
        /// </summary>
        PassedDepartmentStudentDisciplines
    }
}