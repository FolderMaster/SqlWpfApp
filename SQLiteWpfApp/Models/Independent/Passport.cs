using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

using SQLiteWpfApp.Models.Dependent;

namespace SQLiteWpfApp.Models.Independent
{
    [Table("Passports")]
    [PrimaryKey(nameof(SerialNumber))]
    public class Passport
    {
        private static IdGenerator _idGenerator = new(0);

        public static ObservableCollection<Passport> Passports { get; set; } = new();

        public long SerialNumber { get; set; }

        public string Name { get; set; }

        public string PermanentResidenceAddress { get; set; }

        public Sex Sex { get; set; } = Sex.Male;

        public byte[] Scan { get; set; } = null;

        public DateTime BirthDate { get; set; } = DateTime.Now;

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Age { get; set; }

        public virtual ObservableCollection<Person> Persons { get; set; }

        public Passport() => ValuesGenerator.GenerateValues(_idGenerator, () =>
            Passports.FirstOrDefault((p) => p.SerialNumber == SerialNumber) != null, (id) => {
                SerialNumber = id;
                Name = nameof(Name) + "_" + id;
                PermanentResidenceAddress = nameof(PermanentResidenceAddress) + "_" + id;
            }, () => Passports.Add(this));
    }
}