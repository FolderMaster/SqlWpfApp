using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.ObjectModel;

using Model.Independent;

namespace Model.Dependent
{
    /// <summary>
    /// Класс студенты с идентификатором, номером зачётной книжки, логическим значением, 
    /// указывающее, что отчислен, годом образования группы, номером группы и названием стипендии.
    /// </summary>
    [Table("Students")]
    [PrimaryKey(nameof(ID))]
    public class Student
    {
        /// <summary>
        /// Возвращает и задаёт всех студентов.
        /// </summary>
        public static ObservableCollection<Student> Students { get; set; } = new();

        /// <summary>
        /// Возвращает и задаёт идентификатор.
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// Возвращает и задаёт связанная персона.
        /// </summary>
        [ForeignKey(nameof(ID))]
        public virtual Person Person { get; set; }

        /// <summary>
        /// Возвращает и задаёт номер зачётной книжки.
        /// </summary>
        public int RecordBookNumber { get; set; }

        /// <summary>
        /// Возвращает и задаёт логическое значение, указывающее, что отчислен.
        /// </summary>
        public bool? IsDeductible { get; set; } = null;

        /// <summary>
        /// Возвращает и задаёт год образования группы.
        /// </summary>
        public int GroupFormationYear { get; set; }

        /// <summary>
        /// Возвращает и задаёт номер группы.
        /// </summary>
        public string GroupNumber { get; set; }

        /// <summary>
        /// Возвращает и задаёт связанная группа.
        /// </summary>
        public virtual Group Group { get; set; }

        /// <summary>
        /// Возвращает и задаёт название стипендии.
        /// </summary>
        public string? ScholarshipName { get; set; }

        /// <summary>
        /// Возвращает и задаёт связанная стипендия.
        /// </summary>
        public virtual Scholarship? Scholarship { get; set; }

        /// <summary>
        /// Возвращает и задаёт связанные ведомости оценивания.
        /// </summary>
        public virtual ObservableCollection<GradeStatement> GradeStatements { get; set; }

        /// <summary>
        /// Возвращает и задаёт связанные связи между дисциплинами и студентами.
        /// </summary>
        public virtual ObservableCollection<StudentDisciplineConnection> Connections { get; set; }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="Student"/> по умолчанию.
        /// </summary>
        public Student() {}
    }
}