using Microsoft.EntityFrameworkCore;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Dependent
{
    /// <summary>
    /// Класс ведомости оценивания с идентификатором, идентификатором студента, идентификатором 
    /// дисциплины, идентификатором преподавателя, названием оценки и датой сдачи.
    /// </summary>
    [Table("GradeStatements")]
    [PrimaryKey(nameof(ID))]
    public class GradeStatement
    {
        /// <summary>
        /// Возвращает и задаёт все ведомости оценивания.
        /// </summary>
        public static ObservableCollection<GradeStatement> GradeStatements { get; set; } = new();

        /// <summary>
        /// Возвращает и задаёт идентификатор.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }

        /// <summary>
        /// Возвращает и задаёт идентификатор студента.
        /// </summary>
        public long StudentID { get; set; }

        /// <summary>
        /// Возвращает и задаёт связанный студент.
        /// </summary>
        [Browsable(false)]
        public virtual Student Student { get; set; }

        /// <summary>
        /// Возвращает и задаёт идентификатор дисциплины.
        /// </summary>
        public long DisciplineID { get; set; }

        /// <summary>
        /// Возвращает и задаёт связанная дисциплина.
        /// </summary>
        [Browsable(false)]
        public virtual Discipline Discipline { get; set; }

        /// <summary>
        /// Возвращает и задаёт идентификатор преподавателя.
        /// </summary>
        public long TeacherID { get; set; }

        /// <summary>
        /// Возвращает и задаёт связанный преподаватель.
        /// </summary>
        [Browsable(false)]
        public virtual Teacher Teacher { get; set; }

        /// <summary>
        /// Возвращает и задаёт название оценки.
        /// </summary>
        public string GradeName { get; set; }

        /// <summary>
        /// Возвращает и задаёт связанная оценка.
        /// </summary>
        [Browsable(false)]
        public virtual Grade Grade { get; set; }

        /// <summary>
        /// Возвращает и задаёт дата сдачи.
        /// </summary>
        public DateTime PassingDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="GradeStatement"/> по умолчанию.
        /// </summary>
        public GradeStatement() { }
    }
}