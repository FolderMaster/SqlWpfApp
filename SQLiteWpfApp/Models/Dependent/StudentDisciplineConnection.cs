using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace SQLiteWpfApp.Models.Dependent
{
    [Table("StudentDisciplineConnections")]
    [PrimaryKey(nameof(StudentID), new string[] { nameof(DisciplineID) })]
    public class StudentDisciplineConnection
    {
        public int StudentID { get; set; }

        public Student Student { get; set; }

        public int DisciplineID { get; set; }

        public Discipline Discipline { get; set; }

        public bool? isPassed { get; set; }
    }
}