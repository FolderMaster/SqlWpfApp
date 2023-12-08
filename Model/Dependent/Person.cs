using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.ObjectModel;

using Model.Independent;

namespace Model.Dependent
{
    /// <summary>
    /// Класс персоны с идентификатором, адресом проживания, серией и номером паспорта и ФИО.
    /// </summary>
    [Table("Persons")]
    [PrimaryKey(nameof(ID))]
    public class Person
    {
        /// <summary>
        /// Генератор идентификаторов.
        /// </summary>
        private static IdGenerator _idGenerator = new(1);

        /// <summary>
        /// Возвращает и задаёт все персоны.
        /// </summary>
        public static ObservableCollection<Person> Persons { get; set; } = new();

        /// <summary>
        /// Возвращает и задаёт идентификатор.
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// Возвращает и задаёт адрес проживания.
        /// </summary>
        public string ResidentialAddress { get; set; }

        /// <summary>
        /// Возвращает и задаёт серию и номер паспорта.
        /// </summary>
        public string PassportSerialNumber { get; set; }

        /// <summary>
        /// Возвращает и задаёт связанный паспорт.
        /// </summary>
        public virtual Passport Passport { get; set; }

        /// <summary>
        /// Возвращает и задаёт связанные студенты.
        /// </summary>
        public virtual ObservableCollection<Student> Students { get; set; }

        /// <summary>
        /// Возвращает и задаёт связанные преподаватели.
        /// </summary>
        public virtual ObservableCollection<Teacher> Teachers { get; set; }

        /// <summary>
        /// Возвращает и задаёт ФИО.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="Person"/> по умолчанию.
        /// </summary>
        public Person() => ValuesGenerator.GenerateValues(_idGenerator, () =>
            Persons.FirstOrDefault((d) => d.ID == ID) != null, (id) => {
                ID = id;
                ResidentialAddress = nameof(ResidentialAddress) + "_" + id;
            }, () => Persons.Add(this));
    }
}