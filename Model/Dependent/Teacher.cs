using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.ObjectModel;

using Model.Independent;

namespace Model.Dependent
{
    /// <summary>
    /// Класс преподавателя с идентификатором, названием факультета и названием должности.
    /// </summary>
    [Table("Teachers")]
    [PrimaryKey(nameof(ID))]
    public class Teacher
    {
        /// <summary>
        /// Генератор идентификаторов.
        /// </summary>
        private static IdGenerator _idGenerator = new(1);

        /// <summary>
        /// Возвращает и задаёт всех преподавателей.
        /// </summary>
        public static ObservableCollection<Teacher> Teachers { get; set; } = new();

        /// <summary>
        /// Возвращает и задаёт индентификатор.
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// Возвращает и задаёт связанная персона.
        /// </summary>
        [ForeignKey(nameof(ID))]
        public virtual Person Person { get; set; }

        /// <summary>
        /// Возвращает и задаёт название факультета.
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// Возвращает и задаёт связанный факультет.
        /// </summary>
        public virtual Department Department { get; set; }

        /// <summary>
        /// Возвращает и задаёт название должности.
        /// </summary>
        public string PositionName { get; set; }

        /// <summary>
        /// Возвращает и задаёт связанная должность.
        /// </summary>
        public virtual Position Position { get; set; }

        /// <summary>
        /// Возвращает и задаёт связанные ведомости оценивания.
        /// </summary>
        public virtual ObservableCollection<GradeStatement> GradeStatements { get; set; }

        /// <summary>
        /// Возвращает и задаёт связанные связи между дисциплинами и преподавателями.
        /// </summary>
        public virtual ObservableCollection<TeacherDisciplineConnection> Connections { get; set; }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="Teacher"/> по умолчанию.
        /// </summary>
        public Teacher() => ValuesGenerator.GenerateValues(_idGenerator, () =>
            Teachers.FirstOrDefault((d) => d.ID == ID) != null, (id) => ID = id,
            () => Teachers.Add(this));
    }
}