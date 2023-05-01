using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

using SQLiteWpfApp.Models.Independent;

namespace SQLiteWpfApp.Models.Dependent
{
    [Table("TeacherDisciplineConnections")]
    [PrimaryKey(nameof(TeacherID), new string[] { nameof(DisciplineID) })]
    public class TeacherDisciplineConnection
    {
        public int TeacherID { get; set; }

        public Teacher Teacher { get; set; }

        public int DisciplineID { get; set; }

        public Discipline Discipline { get; set; }

        public string RoleName { get; set; }

        public Role Role { get; set; }
    }
}