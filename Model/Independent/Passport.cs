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

        private int _ageValue = 0;

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
            set
            {
                if (_serialNumberValue != value)
                {
                    _serialNumberValue = value;
                    ClearErrors();
                    AddError(ValueValidator.AssertStringOnEqualLength(value, 10));
                    PropertyChangedEventInvoke();
                    ErrorsChangedEventInvoke();
                }
            }
        }

        /// <summary>
        /// Возвращает и задаёт ФИО.
        /// </summary>
        public string Name
        {
            get => _nameValue;
            set
            {
                if (_nameValue != value)
                {
                    _nameValue = value;
                    ClearErrors();
                    AddError(ValueValidator.AssertStringOnLessLength(value, 64));
                    PropertyChangedEventInvoke();
                    ErrorsChangedEventInvoke();
                }
            }
        }

        /// <summary>
        /// Возвращает и задаёт адрес постоянной прописки.
        /// </summary>
        public string PermanentResidenceAddress
        {
            get => _permanentResidenceAddressValue;
            set
            {
                if (_permanentResidenceAddressValue != value)
                {
                    _permanentResidenceAddressValue = value;
                    ClearErrors();
                    AddError(ValueValidator.AssertStringOnLessLength(value, 64));
                    PropertyChangedEventInvoke();
                    ErrorsChangedEventInvoke();
                }
            }
        }

        /// <summary>
        /// Возвращает и задаёт пол.
        /// </summary>
        public bool Sex
        {
            get => _sexValue;
            set
            {
                if (_sexValue != value)
                {
                    _sexValue = value;
                    PropertyChangedEventInvoke();
                }
            }
        }

        /// <summary>
        /// Возвращает и задаёт скан.
        /// </summary>
        public byte[]? Scan
        {
            get => _scanValue;
            set
            {
                if(_scanValue != value)
                {
                    _scanValue = value;
                    ClearErrors();
                    AddError(ValueValidator.AssertOnNotNullValue(value));
                    PropertyChangedEventInvoke();
                    ErrorsChangedEventInvoke();
                }
            }
        } 

        /// <summary>
        /// Возвращает и задаёт дата рождения.
        /// </summary>
        public DateTime BirthDate
        {
            get => _birthDateValue;
            set
            {
                if(_birthDateValue != value)
                {
                    _birthDateValue = value;
                    PropertyChangedEventInvoke();
                }
            }
        }

        /// <summary>
        /// Возвращает и задаёт возраст.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int Age
        {
            get => _ageValue;
            set
            {
                if(_ageValue != value)
                {
                    _ageValue = value;
                    PropertyChangedEventInvoke();
                }
            }
        }

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