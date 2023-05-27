using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.ObjectModel;

using SQLiteWpfApp.Models.Independent;

namespace SQLiteWpfApp.Models.Dependent
{
    [Table("Teachers")]
    [PrimaryKey(nameof(ID))]
    public class Teacher
    {
        public int ID { get; set; }

        [ForeignKey(nameof(ID))]
        public virtual Person Person { get; set; }

        public string DepartmentName { get; set; }

        public virtual Department Department { get; set; }

        public string PositionName { get; set; }

        public virtual Position Position { get; set; }

        public virtual ObservableCollection<GradeStatement> GradeStatements { get; set; }

        public virtual ObservableCollection<TeacherDisciplineConnection> Connections { get; set; }
    }
}