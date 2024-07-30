using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.ObjectModel;
using System.ComponentModel;

using Model.Independent;

namespace Model.Dependent
{
    /// <summary>
    /// Класс связи между дисциплинами и преподавателями с идентификатором преподавателя, 
    /// идентификатором дисциплины и названием роли.
    /// </summary>
    [Table("TeacherDisciplineConnections")]
    [PrimaryKey(nameof(TeacherID), new string[] { nameof(DisciplineID) })]
    public class TeacherDisciplineConnection
    {
        /// <summary>
        /// Генератор идентификаторов.
        /// </summary>
        private static IdGenerator _idGenerator = new(1);

        /// <summary>
        /// Возвращает и задаёт все связи между дисциплинами и преподавателями.
        /// </summary>
        public static ObservableCollection<TeacherDisciplineConnection>
            TeacherDisciplineConnections { get; set; } = new();

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
        /// Возвращает и задаёт идентификатор дисциплины.
        /// </summary>
        public long DisciplineID { get; set; }

        /// <summary>
        /// Возвращает и задаёт связанную дисциплину.
        /// </summary>
        [Browsable(false)]
        public virtual Discipline Discipline { get; set; }

        /// <summary>
        /// Возвращает и задаёт название роли.
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// Возвращает и задаёт связанную роль.
        /// </summary>
        [Browsable(false)]
        public virtual Role Role { get; set; }

        /// <summary>
        /// Возвращает и задаёт название должности.
        /// </summary>
        [NotMapped]
        public string? PositionName => Teacher?.PositionName;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="TeacherDisciplineConnection"/> по умолчанию.
        /// </summary>
        public TeacherDisciplineConnection() => ValuesGenerator.GenerateValues(_idGenerator, () =>
            TeacherDisciplineConnections.FirstOrDefault((d) => d.TeacherID == TeacherID &&
            d.DisciplineID == DisciplineID) != null, (id) => {
                TeacherID = id;
                DisciplineID = id;
            }, () => TeacherDisciplineConnections.Add(this));
    }
}