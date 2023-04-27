using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace SQLiteWpfApp.Models
{
    [Table("Persons")]
    [PrimaryKey(nameof(ID))]
    public class Person
    {
        public int ID { get; set; }

        public string ResidentialAddress { get; set; }

        public Passport Passport { get; set; }
    }
}