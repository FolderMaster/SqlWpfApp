using Microsoft.EntityFrameworkCore;

using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

using Model.Dependent;

namespace Model.Independent
{
    /// <summary>
    /// Класс должностью с названием.
    /// </summary>
    [Table("Positions")]
    [PrimaryKey(nameof(Name))]
    public class Position
    {
        /// <summary>
        /// Генератор идентификаторов.
        /// </summary>
        private static IdGenerator _idGenerator = new(1);

        /// <summary>
        /// Возвращает и задаёт все должности.
        /// </summary>
        public static ObservableCollection<Position> Positions { get; set; } = new();

        /// <summary>
        /// Возвращает и задаёт название.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Возвращает и задаёт связанные преподаватели.
        /// </summary>
        [Browsable(false)]
        public virtual ObservableCollection<Teacher> Teachers { get; set; }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="Position"/> по умолчанию.
        /// </summary>
        public Position() => ValuesGenerator.GenerateValues(_idGenerator, () =>
            Positions.FirstOrDefault((p) => p.Name == Name) != null, (id) =>
            Name = nameof(Name) + "_" + id, () => Positions.Add(this));
    }
}