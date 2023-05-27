using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.ObjectModel;

using SQLiteWpfApp.Models.Independent;

namespace SQLiteWpfApp.Models.Dependent
{
    [Table("Persons")]
    [PrimaryKey(nameof(ID))]
    public class Person
    {
        public int ID { get; set; }

        public string ResidentialAddress { get; set; }

        public long PassportSerialNumber { get; set; }

        public virtual Passport Passport { get; set; }

        public virtual ObservableCollection<Student> Students { get; set; }

        public virtual ObservableCollection<Teacher> Teachers { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string? Name { get; set; }
    }
}