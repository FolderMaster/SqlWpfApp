using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace SQLiteWpfApp.Models
{
    [Table("Scholarships")]
    [PrimaryKey(nameof(Name))]
    public class Scholarship
    {
        public string Name { get; set; }

        public string Symbol { get; set; }

        public double Coefficient { get; set; }
    }
}