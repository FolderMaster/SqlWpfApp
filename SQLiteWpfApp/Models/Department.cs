using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace SQLiteWpfApp.Models
{
    [Table("Departments")]
    [PrimaryKey(nameof(Name))]
    public class Department
    {
        public string Name { get; set; }

        public string Symbol { get; set; }
    }
}