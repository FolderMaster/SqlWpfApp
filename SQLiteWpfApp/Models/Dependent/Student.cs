using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

using SQLiteWpfApp.Models.Independent;

namespace SQLiteWpfApp.Models.Dependent
{
    [Table("Students")]
    [PrimaryKey(nameof(PersonID))]
    public class Student
    {
        public int PersonID { get; set; }

        public Person Person { get; set; }

        public int RecordBookNumber { get; set; }

        public bool? IsDeductible { get; set; }

        public int GroupFormationYear { get; set; }

        public string GroupNumber { get; set; }

        public Group Group { get; set; }

        public string? ScholarshipName { get; set; }

        public Scholarship? Scholarship { get; set; }
    }
}