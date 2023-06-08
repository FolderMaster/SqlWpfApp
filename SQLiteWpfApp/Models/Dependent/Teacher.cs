using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.ObjectModel;
using System.Linq;

using SQLiteWpfApp.Models.Independent;

namespace SQLiteWpfApp.Models.Dependent
{
    [Table("Teachers")]
    [PrimaryKey(nameof(ID))]
    public class Teacher
    {
        private static IdGenerator _idGenerator = new(1);

        public static ObservableCollection<Teacher> Teachers { get; set; } = new();

        public long ID { get; set; }

        [ForeignKey(nameof(ID))]
        public virtual Person Person { get; set; }

        public string DepartmentName { get; set; }

        public virtual Department Department { get; set; }

        public string PositionName { get; set; }

        public virtual Position Position { get; set; }

        public virtual ObservableCollection<GradeStatement> GradeStatements { get; set; }

        public virtual ObservableCollection<TeacherDisciplineConnection> Connections { get; set; }

        public Teacher() => ValuesGenerator.GenerateValues(_idGenerator, () =>
            Teachers.FirstOrDefault((d) => d.ID == ID) != null, (id) => ID = id,
            () => Teachers.Add(this));
    }
}