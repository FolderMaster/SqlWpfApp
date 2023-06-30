using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Dependent
{
    /// <summary>
    /// Класс группы с номером, годом образования и номером специальности.
    /// </summary>
    [Table("Groups")]
    [PrimaryKey(nameof(Number), new string[] { nameof(FormationYear) })]
    public class Group
    {
        /// <summary>
        /// Генератор идентификаторов.
        /// </summary>
        private static IdGenerator _idGenerator = new(1);

        /// <summary>
        /// Возвращает и задаёт все группы.
        /// </summary>
        public static ObservableCollection<Group> Groups { get; set; } = new();

        /// <summary>
        /// Возвращает и задаёт номер.
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Возвращает и задаёт год образования.
        /// </summary>
        public int FormationYear { get; set; } = DateTime.Now.Year;

        /// <summary>
        /// Возвращает и задаёт номер специальности.
        /// </summary>
        public string SpecialtyNumber { get; set; }

        /// <summary>
        /// Возвращает и задаёт связанная специальность.
        /// </summary>
        public virtual Specialty Specialty { get; set; }

        /// <summary>
        /// Возвращает и задаёт связанные студенты.
        /// </summary>
        public virtual ObservableCollection<Student> Students { get; set; }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="Group"/> по умолчанию.
        /// </summary>
        public Group() => ValuesGenerator.GenerateValues(_idGenerator, () =>
            Groups.FirstOrDefault((d) => d.Number == Number && d.FormationYear == FormationYear) 
            != null, (id) => Number = nameof(Number) + "_" + id, () => Groups.Add(this));
    }
}