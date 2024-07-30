using Microsoft.EntityFrameworkCore;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Dependent
{
    /// <summary>
    /// Класс дисциплина с идентификатором, названием, количеством часов, номером специальности и
    /// названием режима обучения.
    /// </summary>
    [Table("Disciplines")]
    [PrimaryKey(nameof(ID))]
    public class Discipline
    {
        /// <summary>
        /// Возвращает и задаёт все дисциплины.
        /// </summary>
        public static ObservableCollection<Discipline> Disciplines { get; set; } = new();

        /// <summary>
        /// Возвращает и задаёт идентификатор.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }

        /// <summary>
        /// Возвращает и задаёт название.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Возвращает и задаёт количество часов.
        /// </summary>
        public int HoursCount { get; set; } = 0;

        /// <summary>
        /// Возвращает и задаёт номер специальности.
        /// </summary>
        public string SpecialtyNumber { get; set; }

        /// <summary>
        /// Возвращает и задаёт связанная специальность.
        /// </summary>
        [Browsable(false)]
        public virtual Specialty Specialty { get; set; }

        /// <summary>
        /// Возвращает и задаёт название режима обучения.
        /// </summary>
        public string StudyFormName { get; set; }

        /// <summary>
        /// Возвращает и задаёт связанный режимы обучения.
        /// </summary>
        [Browsable(false)]
        public virtual StudyForm StudyForm { get; set; }

        /// <summary>
        /// Возвращает и задаёт связанные ведомости оценивания.
        /// </summary>
        [Browsable(false)]
        public virtual ObservableCollection<GradeStatement> GradeStatements { get; set; }

        /// <summary>
        /// Возвращает и задаёт связанные связи между дисциплинами и студентами.
        /// </summary>
        [Browsable(false)]
        public virtual ObservableCollection<StudentDisciplineConnection> StudentConnections
        { get; set; }

        /// <summary>
        /// Возвращает и задаёт связанные связи между дисциплинами и преподавателями.
        /// </summary>
        [Browsable(false)]
        public virtual ObservableCollection<TeacherDisciplineConnection> TeacherConnections
        { get; set; }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="Discipline"/> по умолчанию.
        /// </summary>
        public Discipline() { }
    }
}