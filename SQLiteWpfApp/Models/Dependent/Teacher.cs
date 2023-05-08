using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

using SQLiteWpfApp.Models.Independent;

namespace SQLiteWpfApp.Models.Dependent
{
    [Table("Teachers")]
    [PrimaryKey(nameof(PersonID))]
    public class Teacher
    {
        public int PersonID { get; set; }

        public virtual Person Person { get; set; }

        public string DepartmentName { get; set; }

        public virtual Department Department { get; set; }

        public string PositionName { get; set; }

        public virtual Position Position { get; set; }
    }
}