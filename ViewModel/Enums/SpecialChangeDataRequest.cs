namespace ViewModel.Enums
{
    /// <summary>
    /// Перечисление специализированных запросов изменения данных.
    /// </summary>
    public enum SpecialChangeDataRequest
    {
        /// <summary>
        /// Никакой запрос.
        /// </summary>
        None,
        /// <summary>
        /// Установление нулевого значения в сдачах дисциплин у студентов.
        /// </summary>
        SetNullStudentPassings,
        /// <summary>
        /// Установление нулевого значения в отчислениях у студентов.
        /// </summary>
        SetNullStudentDeductings,
        /// <summary>
        /// Установление сдач дисциплин у студентов по последним оценкам.
        /// </summary>
        SetStudentPassingsByLastGrades,
        /// <summary>
        /// Установление сдач дисциплин у студентов без оценок.
        /// </summary>
        SetStudentPassingsWithoutGrades,
        /// <summary>
        /// Установление отчислений у студентов.
        /// </summary>
        SetStudentDeductings,
        /// <summary>
        /// Установление стипендии у студентов по оценкам.
        /// </summary>
        SetStudentScholarshipsByGrades,
        /// <summary>
        /// Установление стипендии у студентов по сдачам дисциплин.
        /// </summary>
        SetStudentScholarshipsByPassings
    }
}