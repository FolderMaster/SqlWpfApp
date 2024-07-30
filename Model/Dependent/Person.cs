using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.ObjectModel;
using System.ComponentModel;

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
        /// Возвращает и задаёт все персоны.
        /// </summary>
        public static ObservableCollection<Person> Persons { get; set; } = new();

        /// <summary>
        /// Возвращает и задаёт идентификатор.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        [Browsable(false)]
        public virtual Passport Passport { get; set; }

        /// <summary>
        /// Возвращает и задаёт связанные студенты.
        /// </summary>
        [Browsable(false)]
        public virtual ObservableCollection<Student> Students { get; set; }

        /// <summary>
        /// Возвращает и задаёт связанные преподаватели.
        /// </summary>
        [Browsable(false)]
        public virtual ObservableCollection<Teacher> Teachers { get; set; }

        /// <summary>
        /// Возвращает и задаёт ФИО.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="Person"/> по умолчанию.
        /// </summary>
        public Person() { }
    }
}