using Microsoft.EntityFrameworkCore;

using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

using Model.Dependent;

namespace Model.Independent
{
    /// <summary>
    /// Класс стипендий с названием, обозначением, коэффициентом и размером.
    /// </summary>
    [Table("Scholarships")]
    [PrimaryKey(nameof(Name))]
    public class Scholarship
    {
        /// <summary>
        /// Генератор идентификаторов.
        /// </summary>
        private static IdGenerator _idGenerator = new(1);

        /// <summary>
        /// Возвращает и задаёт все стипендии.
        /// </summary>
        public static ObservableCollection<Scholarship> Scholarships { get; set; } = new();

        /// <summary>
        /// Возвращает и задаёт название.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Возвращает и задаёт обозначение.
        /// </summary>
        public string Symbol { get; set; }

        /// <summary>
        /// Возвращает и задаёт коэффициент.
        /// </summary>
        public float Coefficient { get; set; } = 1;

        /// <summary>
        /// Возвращает и задаёт размер.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public float Amount { get; set; }

        /// <summary>
        /// Возвращает и задаёт связанные студенты.
        /// </summary>
        [Browsable(false)]
        public virtual ObservableCollection<Student> Students { get; set; }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="Scholarship"/> по умолчанию.
        /// </summary>
        public Scholarship() => ValuesGenerator.GenerateValues(_idGenerator, () =>
            Scholarships.FirstOrDefault((d) => d.Name == Name) != null, (id) => {
                Name = nameof(Name) + "_" + id;
                Symbol = nameof(Symbol) + "_" + id;
            }, () => Scholarships.Add(this));
    }
}