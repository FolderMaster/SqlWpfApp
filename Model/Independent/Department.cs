using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

using Model.Dependent;

namespace Model.Independent
{
    /// <summary>
    /// Класс факультета с названием и обозначением.
    /// </summary>
    [Table("Departments")]
    [PrimaryKey(nameof(Name))]
    public class Department
    {
        /// <summary>
        /// Генератор идентификаторов.
        /// </summary>
        private static IdGenerator _idGenerator = new(1);

        /// <summary>
        /// Возвращает и задаёт все факультеты.
        /// </summary>
        public static ObservableCollection<Department> Departments { get; set; } = new();

        /// <summary>
        /// Возвращает и задаёт название.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Возвращает и задаёт обозначение.
        /// </summary>
        public string Symbol { get; set; }

        /// <summary>
        /// Возвращает и задаёт связанные специальности.
        /// </summary>
        public virtual ObservableCollection<Specialty> Specialties { get; set; }

        /// <summary>
        /// Возвращает и задаёт связанные преподаватели.
        /// </summary>
        public virtual ObservableCollection<Teacher> Teachers { get; set; }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="Department"/> по умолчанию.
        /// </summary>
        public Department() => ValuesGenerator.GenerateValues(_idGenerator, () =>
            Departments.FirstOrDefault((d) => d.Name == Name) != null, (id) => {
                Name = nameof(Name) + "_" + id;
                Symbol = nameof(Symbol) + "_" + id;
            }, () => Departments.Add(this));
    }
}