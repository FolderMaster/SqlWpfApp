using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace SQLiteWpfApp.Models
{
    [Table("Specialties")]
    [PrimaryKey(nameof(Number))]
    public class Specialty
    {
        public string Number { get; set; }

        public string Name { get; set; }

        public Department Department { get; set; }
    }
}