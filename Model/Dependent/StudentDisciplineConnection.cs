using Microsoft.EntityFrameworkCore;

using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Dependent
{
    /// <summary>
    /// Класс связи между дисциплинами и студентами с идентификатором студентами, 
    /// идентификатором дисциплины и логическим значением, указывающее, что дисциплина сдана.
    /// </summary>
    [Table("StudentDisciplineConnections")]
    [PrimaryKey(nameof(StudentID), new string[] { nameof(DisciplineID) })]
    public class StudentDisciplineConnection
    {
        /// <summary>
        /// Генератор идентификаторов.
        /// </summary>
        private static IdGenerator _idGenerator = new(1);

        /// <summary>
        /// Возвращает и задаёт все связи между дисциплинами и студентами.
        /// </summary>
        public static ObservableCollection<StudentDisciplineConnection>
            StudentDisciplineConnections { get; set; } = new();

        /// <summary>
        /// Возвращает и задаёт идентификатор студента.
        /// </summary>
        public long StudentID { get; set; }

        /// <summary>
        /// Возвращает и задаёт связанный студент.
        /// </summary>
        public virtual Student Student { get; set; }

        /// <summary>
        /// Возвращает и задаёт идентификатор дисциплины.
        /// </summary>
        public long DisciplineID { get; set; }

        /// <summary>
        /// Возвращает и задаёт связанная дисциплина.
        /// </summary>
        public virtual Discipline Discipline { get; set; }

        /// <summary>
        /// Возвращает и задаёт логическое значение, указывающее, что дисциплина сдана.
        /// </summary>
        public bool? IsPassed { get; set; } = null;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="StudentDisciplineConnection"/> по умолчанию.
        /// </summary>
        public StudentDisciplineConnection() => ValuesGenerator.GenerateValues(_idGenerator, () =>
            StudentDisciplineConnections.FirstOrDefault((d) => d.StudentID == StudentID &&
            d.DisciplineID == DisciplineID) != null, (id) => {
                StudentID = id;
                DisciplineID = id;
            }, () => StudentDisciplineConnections.Add(this));
    }
}