using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.ObjectModel;
using System.Linq;

using SQLiteWpfApp.Models.Independent;

namespace SQLiteWpfApp.Models.Dependent
{
    [Table("Persons")]
    [PrimaryKey(nameof(ID))]
    public class Person
    {
        private static IdGenerator _idGenerator = new(1);

        public static ObservableCollection<Person> Persons { get; set; } = new();

        public long ID { get; set; }

        public string ResidentialAddress { get; set; }

        public long PassportSerialNumber { get; set; }

        public virtual Passport Passport { get; set; }

        public virtual ObservableCollection<Student> Students { get; set; }

        public virtual ObservableCollection<Teacher> Teachers { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string? Name { get; set; }

        public Person() => ValuesGenerator.GenerateValues(_idGenerator, () =>
            Persons.FirstOrDefault((d) => d.ID == ID) != null, (id) => {
                ID = id;
                ResidentialAddress = nameof(ResidentialAddress) + "_" + id;
            }, () => Persons.Add(this));
    }
}