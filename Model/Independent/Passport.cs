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
    public class Passport : PropertyUpdaterService
    {
        private string _serialNumberValue = "";

        private string _nameValue = "";

        private string _permanentResidenceAddressValue = "";

        private bool _sexValue = false;

        private DateTime _birthDateValue = DateTime.Now;

        /// <summary>
        /// Скан.
        /// </summary>
        private byte[]? _scanValue = null;

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
        public string SerialNumber
        {
            get => _serialNumberValue;
            set => UpdateProperty(ref _serialNumberValue, value,
                (v, n) => ValueValidator.AssertStringOnEqualLength(v, 10, n));
        }

        /// <summary>
        /// Возвращает и задаёт ФИО.
        /// </summary>
        public string Name
        {
            get => _nameValue;
            set => UpdateProperty(ref _nameValue, value, (v, n) =>
                ValueValidator.AssertStringOnLessLength(v, 64, n));
        }

        /// <summary>
        /// Возвращает и задаёт адрес постоянной прописки.
        /// </summary>
        public string PermanentResidenceAddress
        {
            get => _permanentResidenceAddressValue;
            set => UpdateProperty(ref _permanentResidenceAddressValue, value,
                (v, n) => ValueValidator.AssertStringOnLessLength(v, 64, n));
        }

        /// <summary>
        /// Возвращает и задаёт пол.
        /// </summary>
        public bool Sex
        {
            get => _sexValue;
            set => UpdateProperty(ref _sexValue, value);
        }

        /// <summary>
        /// Возвращает и задаёт скан.
        /// </summary>
        public byte[]? Scan
        {
            get => _scanValue;
            set => UpdateProperty(ref _scanValue, value,
                ValueValidator.AssertOnNotNullValue);
        } 

        /// <summary>
        /// Возвращает и задаёт дата рождения.
        /// </summary>
        public DateTime BirthDate
        {
            get => _birthDateValue;
            set => UpdateProperty(ref _birthDateValue, value, null,
                (v) => PropertyChangedEventInvoke(nameof(Age)));
        }

        /// <summary>
        /// Возвращает и задаёт возраст.
        /// </summary>
        [NotMapped]
        public int Age => new DateTime(DateTime.Now.Subtract(BirthDate).Ticks).Year - 1;

        /// <summary>
        /// Возвращает и задаёт связанные персоны.
        /// </summary>
        public virtual ObservableCollection<Person> Persons { get; set; }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="Passport"/> по умолчанию.
        /// </summary>
        public Passport()
        {
            ValuesGenerator.GenerateValues(_idGenerator, () =>
                Passports.FirstOrDefault((p) => p.SerialNumber == SerialNumber) != null, (id) =>
                {
                    SerialNumber = id.ToString();
                    Name = nameof(Name) + "_" + id;
                    PermanentResidenceAddress = nameof(PermanentResidenceAddress) + "_" + id;
                }, () => Passports.Add(this));
            AddError("Scan is null!", nameof(Scan));
        }
    }
}