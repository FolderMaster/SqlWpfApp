using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

using Model.Dependent;

namespace Model.Independent
{
    /// <summary>
    /// Класс паспорта с серией и номером, ФИО, адресом постоянной прописки, полом, датой 
    /// рождения, возрастом, сканом.
    /// </summary>
    [Table("Passports")]
    [PrimaryKey(nameof(SerialNumber))]
    public class Passport
    {
        /// <summary>
        /// Генератор идентификаторов.
        /// </summary>
        private static IdGenerator _idGenerator = new(0);

        /// <summary>
        /// Возвращает и задаёт все паспорты.
        /// </summary>
        public static ObservableCollection<Passport> Passports { get; set; } = new();

        /// <summary>
        /// Возвращает и задаёт серию и номер.
        /// </summary>
        public long SerialNumber { get; set; }

        /// <summary>
        /// Возвращает и задаёт ФИО.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Возвращает и задаёт адресс постоянной прописки.
        /// </summary>
        public string PermanentResidenceAddress { get; set; }

        /// <summary>
        /// Возвращает и задаёт пол.
        /// </summary>
        public Sex Sex { get; set; } = Sex.Male;

        /// <summary>
        /// Возвращает и задаёт скан.
        /// </summary>
        public byte[] Scan { get; set; } = null;

        /// <summary>
        /// Возвращает и задаёт дата рождения.
        /// </summary>
        public DateTime BirthDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Возвращает и задаёт возраст.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Age { get; set; }

        /// <summary>
        /// Возвращает и задаёт связанные персоны.
        /// </summary>
        public virtual ObservableCollection<Person> Persons { get; set; }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="Passport"/> по умолчанию.
        /// </summary>
        public Passport() => ValuesGenerator.GenerateValues(_idGenerator, () =>
            Passports.FirstOrDefault((p) => p.SerialNumber == SerialNumber) != null, (id) => {
                SerialNumber = id;
                Name = nameof(Name) + "_" + id;
                PermanentResidenceAddress = nameof(PermanentResidenceAddress) + "_" + id;
            }, () => Passports.Add(this));
    }
}