using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace SQLiteWpfApp.Models
{
    [Table("Students")]
    [PrimaryKey(nameof(Person))]
    public class Student
    {
        public Person Person { get; set; }

        public int RecordBookNumber { get; set; }

        public int IsDeductible { get; set; }

        public Group Group { get; set; }

        public Scholarship Scholarship { get; set; }
    }
}