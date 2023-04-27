using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace SQLiteWpfApp.Models
{
    [Table("Grades")]
    [PrimaryKey(nameof(Name))]
    public class Grade
    {
        public string Name { get; set; }
        
        public string Symbol { get; set; }

        public GradeMode Mode { get; set; }
    }
}