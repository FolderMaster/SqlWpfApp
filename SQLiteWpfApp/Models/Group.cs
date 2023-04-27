using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace SQLiteWpfApp.Models
{
    [Table("Groups")]
    [PrimaryKey(nameof(Number), new string[] { nameof(FormationYear)})]
    public class Group
    {
        public string Number { get; set; }

        public int FormationYear { get; set; }

        public Specialty Specialty { get; set; }
    }
}