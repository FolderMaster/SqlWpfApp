using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.ObjectModel;
using System.ComponentModel;

using Model.Independent;

namespace Model.Dependent
{
    /// <summary>
    /// Класс специальности с номером, названием и названием факультета.
    /// </summary>
    [Table("Specialties")]
    [PrimaryKey(nameof(Number))]
    public class Specialty
    {
        /// <summary>
        /// Генератор идентификаторов.
        /// </summary>
        private static IdGenerator _idGenerator = new(1);

        /// <summary>
        /// Возвращает и задаёт все специальности.
        /// </summary>
        public static ObservableCollection<Specialty> Specialties { get; set; } = new();

        /// <summary>
        /// Возвращает и задаёт номер.
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Возвращает и задаёт название.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Возвращает и задаёт название факультета.
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// Возвращает и задаёт связанный факультет.
        /// </summary>
        [Browsable(false)]
        public virtual Department Department { get; set; }

        /// <summary>
        /// Возвращает и задаёт связанные дисциплины.
        /// </summary>
        [Browsable(false)]
        public virtual ObservableCollection<Discipline> Disciplines { get; set; }

        /// <summary>
        /// Возвращает и задаёт связанные группы.
        /// </summary>
        [Browsable(false)]
        public virtual ObservableCollection<Group> Groups { get; set; }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="Specialty"/> по умолчанию.
        /// </summary>
        public Specialty() => ValuesGenerator.GenerateValues(_idGenerator, () =>
            Specialties.FirstOrDefault((d) => d.Number == Number) != null, (id) => {
                Number = nameof(Number) + "_" + id;
                Name = nameof(Name) + "_" + id;
            }, () => Specialties.Add(this));
    }
}