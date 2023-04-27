using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace SQLiteWpfApp.Models
{
    [Table("Teachers")]
    [PrimaryKey(nameof(Person))]
    public class Teacher
    {
        public Person Person { get; set; }

        public Department Department { get; set; }

        public Position Position { get; set; }
    }
}