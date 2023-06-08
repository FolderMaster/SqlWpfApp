using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SQLiteWpfApp.Models.Dependent
{
    [Table("Disciplines")]
    [PrimaryKey(nameof(ID))]
    public class Discipline
    {
        private static IdGenerator _idGenerator = new(1);

        public static ObservableCollection<Discipline> Disciplines { get; set; } = new();

        public long ID { get; set; }

        public string Name { get; set; }

        public int HoursCount { get; set; } = 0;

        public string SpecialtyNumber { get; set; }

        public virtual Specialty Specialty { get; set; }

        public string StudyFormName { get; set; }

        public virtual StudyForm StudyForm { get; set; }

        public virtual ObservableCollection<GradeStatement> GradeStatements { get; set; }

        public virtual ObservableCollection<StudentDisciplineConnection> StudentConnections
        { get; set; }

        public virtual ObservableCollection<TeacherDisciplineConnection> TeacherConnections
        { get; set; }

        public Discipline() => ValuesGenerator.GenerateValues(_idGenerator, () =>
            Disciplines.FirstOrDefault((d) => d.ID == ID) != null, (id) => {
                ID = id;
                Name = nameof(Name) + "_" + id;
            }, () => Disciplines.Add(this));
    }
}