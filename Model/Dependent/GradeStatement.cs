using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Dependent
{
    [Table("GradeStatements")]
    [PrimaryKey(nameof(ID))]
    public class GradeStatement
    {
        private static IdGenerator _idGenerator = new(1);

        public static ObservableCollection<GradeStatement> GradeStatements { get; set; } = new();

        public long ID { get; set; }

        public long StudentID { get; set; }

        public virtual Student Student { get; set; }

        public long DisciplineID { get; set; }

        public virtual Discipline Discipline { get; set; }

        public long TeacherID { get; set; }

        public virtual Teacher Teacher { get; set; }

        public string GradeName { get; set; }

        public virtual Grade Grade { get; set; }

        public DateTime PassingDate { get; set; } = DateTime.Now;

        public GradeStatement() => ValuesGenerator.GenerateValues(_idGenerator, () =>
            GradeStatements.FirstOrDefault((d) => d.ID == ID) != null, (id) =>  ID = id,
            () => GradeStatements.Add(this));
    }
}