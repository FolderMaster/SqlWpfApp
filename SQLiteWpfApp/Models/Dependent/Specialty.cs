using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

using SQLiteWpfApp.Models.Independent;

namespace SQLiteWpfApp.Models.Dependent
{
    [Table("Specialties")]
    [PrimaryKey(nameof(Number))]
    public class Specialty
    {
        public string Number { get; set; }

        public string Name { get; set; }

        public string DepartmentName { get; set; }

        public virtual Department Department { get; set; }
    }
}