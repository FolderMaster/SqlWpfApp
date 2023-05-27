using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace SQLiteWpfApp.Models.Dependent
{
    [Table("Disciplines")]
    [PrimaryKey(nameof(ID))]
    public class Discipline
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int HoursCount { get; set; }

        public string SpecialtyNumber { get; set; }

        public virtual Specialty Specialty { get; set; }

        public string StudyFormName { get; set; }

        public virtual StudyForm StudyForm { get; set; }

        public virtual ObservableCollection<GradeStatement> GradeStatements { get; set; }

        public virtual ObservableCollection<StudentDisciplineConnection> StudentConnections
        { get; set; }

        public virtual ObservableCollection<TeacherDisciplineConnection> TeacherConnections
        { get; set; }
    }
}