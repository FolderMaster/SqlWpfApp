using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.ObjectModel;

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

        public virtual GradeMode GradeMode { get; set; }

        public virtual ObservableCollection<GradeStatement> GradeStatements { get; set; }
    }
}