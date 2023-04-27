using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace SQLiteWpfApp.Models
{
    [Table("StudentDisciplineConnections")]
    [PrimaryKey(nameof(Student), new string[] { nameof(Discipline) })]
    public class StudentDisciplineConnection
    {
        public Student Student { get; set; }

        public Discipline Discipline { get; set; }

        public int isPassed { get; set; }
    }
}