using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace SQLiteWpfApp.Models
{
    [Table("StudyForms")]
    [PrimaryKey(nameof(Name))]
    public class StudyForm
    {
        public string Name { get; set; }

        public GradeMode GradeMode { get; set; }
    }
}