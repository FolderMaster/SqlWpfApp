using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Dependent
{
    [Table("StudentDisciplineConnections")]
    [PrimaryKey(nameof(StudentID), new string[] { nameof(DisciplineID) })]
    public class StudentDisciplineConnection
    {
        private static IdGenerator _idGenerator = new(1);

        public static ObservableCollection<StudentDisciplineConnection>
            StudentDisciplineConnections { get; set; } = new();

        public long StudentID { get; set; }

        public virtual Student Student { get; set; }

        public long DisciplineID { get; set; }

        public virtual Discipline Discipline { get; set; }

        public bool? IsPassed { get; set; } = null;

        public StudentDisciplineConnection() => ValuesGenerator.GenerateValues(_idGenerator, () =>
            StudentDisciplineConnections.FirstOrDefault((d) => d.StudentID == StudentID &&
            d.DisciplineID == DisciplineID) != null, (id) => {
                StudentID = id;
                DisciplineID = id;
            }, () => StudentDisciplineConnections.Add(this));
    }
}