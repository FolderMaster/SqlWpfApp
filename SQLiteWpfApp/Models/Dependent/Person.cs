using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

using SQLiteWpfApp.Models.Independent;

namespace SQLiteWpfApp.Models.Dependent
{
    [Table("Persons")]
    [PrimaryKey(nameof(ID))]
    public class Person
    {
        public int ID { get; set; }

        public string ResidentialAddress { get; set; }

        public int PassportSerialNumber { get; set; }

        public virtual Passport Passport { get; set; }
    }
}