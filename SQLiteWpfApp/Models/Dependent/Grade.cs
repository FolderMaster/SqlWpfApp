using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

using SQLiteWpfApp.Models.Independent;

namespace SQLiteWpfApp.Models.Dependent
{
    [Table("Grades")]
    [PrimaryKey(nameof(Name))]
    public class Grade
    {
        public string Name { get; set; }

        public string Symbol { get; set; }

        public string GradeModeName { get; set; }

        public GradeMode GradeMode { get; set; }
    }
}