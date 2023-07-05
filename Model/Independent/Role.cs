using Microsoft.EntityFrameworkCore;

using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

using Model.Dependent;

namespace Model.Independent
{
    /// <summary>
    /// Класс ролей с названием.
    /// </summary>
    [Table("Roles")]
    [PrimaryKey(nameof(Name))]
    public class Role
    {
        /// <summary>
        /// Генератор идентификаторов.
        /// </summary>
        private static IdGenerator _idGenerator = new(1);

        /// <summary>
        /// Возвращает и задаёт все роли.
        /// </summary>
        public static ObservableCollection<Role> Roles { get; set; } = new();

        /// <summary>
        /// Возвращает и задаёт название.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Возвращает и задаёт связанные связи между дисциплинами и преподавателями.
        /// </summary>
        public virtual ObservableCollection<TeacherDisciplineConnection> Connections { get; set; }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="Role"/> по умолчанию.
        /// </summary>
        public Role() => ValuesGenerator.GenerateValues(_idGenerator, () =>
            Roles.FirstOrDefault((r) => r.Name == Name) != null, (id) =>
            Name = nameof(Name) + "_" + id, () => Roles.Add(this));
    }
}