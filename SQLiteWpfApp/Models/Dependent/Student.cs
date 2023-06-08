using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.ObjectModel;
using System.Linq;

using SQLiteWpfApp.Models.Independent;

namespace SQLiteWpfApp.Models.Dependent
{
    [Table("Students")]
    [PrimaryKey(nameof(ID))]
    public class Student
    {
        private static IdGenerator _idGenerator = new(1);

        public static ObservableCollection<Student> Students { get; set; } = new();

        public long ID { get; set; }

        [ForeignKey(nameof(ID))]
        public virtual Person Person { get; set; }

        public long RecordBookNumber { get; set; }

        public bool? IsDeductible { get; set; } = null;

        public int GroupFormationYear { get; set; }

        public string GroupNumber { get; set; }

        public virtual Group Group { get; set; }

        public string? ScholarshipName { get; set; }

        public virtual Scholarship? Scholarship { get; set; }

        public virtual ObservableCollection<GradeStatement> GradeStatements { get; set; }

        public virtual ObservableCollection<StudentDisciplineConnection> Connections { get; set; }

        public Student() => ValuesGenerator.GenerateValues(_idGenerator, () =>
            Students.FirstOrDefault((d) => d.ID == ID) != null, (id) => {
                ID = id;
                RecordBookNumber = id;
            }, () => Students.Add(this));
    }
}