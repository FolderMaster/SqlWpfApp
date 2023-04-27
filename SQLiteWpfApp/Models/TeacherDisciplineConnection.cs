using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace SQLiteWpfApp.Models
{
    [Table("TeacherDisciplineConnections")]
    [PrimaryKey(nameof(Teacher), new string[] { nameof(Discipline) })]
    public class TeacherDisciplineConnection
    {
        public Teacher Teacher { get; set; }

        public Discipline Discipline { get; set; }

        public Role Role { get; set; }
    }
}