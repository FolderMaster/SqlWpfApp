using Microsoft.EntityFrameworkCore;

using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

using Model.Dependent;
using Model.ObservableObjects;

namespace Model.Independent
{
    /// <summary>
    /// Класс паспорта с серией и номером, ФИО, адресом постоянной прописки, полом, датой 
    /// рождения, возрастом, сканом.
    /// </summary>
    [Table("Passports")]
    [PrimaryKey(nameof(SerialNumber))]
    public class Passport : ObservableObject
    {
        public static ObservableProperty SerialNumberProperty =
            RegisterProperty(typeof(Passport), nameof(SerialNumber), "",
                [(o) => ValueValidator.AssertStringOnEqualLength((string)o.NewValue, 10,
                    o.Property.Name)]);

        public static ObservableProperty NameProperty =
            RegisterProperty(typeof(Passport), nameof(Name), "",
                [(o) => ValueValidator.AssertStringOnLessLength((string)o.NewValue, 64,
                    o.Property.Name)]);

        public static ObservableProperty PermanentResidenceAddressProperty =
            RegisterProperty(typeof(Passport), nameof(PermanentResidenceAddress), "",
                [(o) => ValueValidator.AssertStringOnLessLength((string)o.NewValue, 64,
                    o.Property.Name)]);

        public static ObservableProperty ScanProperty =
            RegisterProperty(typeof(Passport), nameof(Scan), null,
                [(o) => ValueValidator.AssertOnNotNullValue(o.NewValue, o.Property.Name)]);

        public static ObservableProperty BirthDateProperty =
            RegisterProperty(typeof(Passport), nameof(BirthDate), DateTime.Now, null,
                (o) => o.Owner.PropertyChangedEventInvoke(nameof(Age)));

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
            get => (string)GetValue(SerialNumberProperty);
            set => SetValue(value, SerialNumberProperty);
        }

        /// <summary>
        /// Возвращает и задаёт ФИО.
        /// </summary>
        public string Name
        {
            get => (string)GetValue(NameProperty);
            set => SetValue(value, NameProperty);
        }

        /// <summary>
        /// Возвращает и задаёт адрес постоянной прописки.
        /// </summary>
        public string PermanentResidenceAddress
        {
            get => (string)GetValue(PermanentResidenceAddressProperty);
            set => SetValue(value, PermanentResidenceAddressProperty);
        }

        /// <summary>
        /// Возвращает и задаёт пол.
        /// </summary>
        public bool Sex { get; set; } = false;

        /// <summary>
        /// Возвращает и задаёт скан.
        /// </summary>
        [Browsable(false)]
        public byte[]? Scan
        {
            get => (byte[]?)GetValue(ScanProperty);
            set => SetValue(value, ScanProperty);
        } 

        /// <summary>
        /// Возвращает и задаёт дата рождения.
        /// </summary>
        public DateTime BirthDate
        {
            get => (DateTime)GetValue(BirthDateProperty);
            set => SetValue(value, BirthDateProperty);
        }

        /// <summary>
        /// Возвращает и задаёт возраст.
        /// </summary>
        [NotMapped]
        public int Age => new DateTime(DateTime.Now.Subtract(BirthDate).Ticks).Year - 1;

        /// <summary>
        /// Возвращает и задаёт связанные персоны.
        /// </summary>
        [Browsable(false)]
        public virtual ObservableCollection<Person> Persons { get; set; }

        static Passport() { }

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
        }
    }
}