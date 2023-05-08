using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

using SQLiteWpfApp.Models.Independent;

namespace SQLiteWpfApp.Models.Dependent
{
    [Table("StudyForms")]
    [PrimaryKey(nameof(Name))]
    public class StudyForm
    {
        public string Name { get; set; }

        public string GradeModeName { get; set; }

        public virtual GradeMode GradeMode { get; set; }
    }
}