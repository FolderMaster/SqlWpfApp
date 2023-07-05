using Microsoft.EntityFrameworkCore;

using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Dependent
{
    /// <summary>
    /// Класс ведомости оценивания с идентификатором, индентификатором студента, индентификатором 
    /// дисциплины, индентификатором преподавателя, названием оценки и датой сдачи.
    /// </summary>
    [Table("GradeStatements")]
    [PrimaryKey(nameof(ID))]
    public class GradeStatement
    {
        /// <summary>
        /// Генератор идентификаторов.
        /// </summary>
        private static IdGenerator _idGenerator = new(1);

        /// <summary>
        /// Возвращает и задаёт все ведомости оценивания.
        /// </summary>
        public static ObservableCollection<GradeStatement> GradeStatements { get; set; } = new();

        /// <summary>
        /// Возвращает и задаёт индентификатор.
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// Возвращает и задаёт индентификатор студента.
        /// </summary>
        public long StudentID { get; set; }

        /// <summary>
        /// Возвращает и задаёт связанный студент.
        /// </summary>
        public virtual Student Student { get; set; }

        /// <summary>
        /// Возвращает и задаёт индентификатор дисциплины.
        /// </summary>
        public long DisciplineID { get; set; }

        /// <summary>
        /// Возвращает и задаёт связанная дисциплина.
        /// </summary>
        public virtual Discipline Discipline { get; set; }

        /// <summary>
        /// Возвращает и задаёт индентификатор преподавателя.
        /// </summary>
        public long TeacherID { get; set; }

        /// <summary>
        /// Возвращает и задаёт связанный преподаватель.
        /// </summary>
        public virtual Teacher Teacher { get; set; }

        /// <summary>
        /// Возвращает и задаёт название оценки.
        /// </summary>
        public string GradeName { get; set; }

        /// <summary>
        /// Возвращает и задаёт связанная оценка.
        /// </summary>
        public virtual Grade Grade { get; set; }

        /// <summary>
        /// Возвращает и задаёт дата сдачи.
        /// </summary>
        public DateTime PassingDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="GradeStatement"/> по умолчанию.
        /// </summary>
        public GradeStatement() => ValuesGenerator.GenerateValues(_idGenerator, () =>
            GradeStatements.FirstOrDefault((d) => d.ID == ID) != null, (id) =>  ID = id,
            () => GradeStatements.Add(this));
    }
}