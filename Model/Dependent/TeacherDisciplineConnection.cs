using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.ObjectModel;

using Model.Independent;

namespace Model.Dependent
{
    [Table("TeacherDisciplineConnections")]
    [PrimaryKey(nameof(TeacherID), new string[] { nameof(DisciplineID) })]
    public class TeacherDisciplineConnection
    {
        private static IdGenerator _idGenerator = new(1);

        public static ObservableCollection<TeacherDisciplineConnection>
            TeacherDisciplineConnections { get; set; } = new();

        public long TeacherID { get; set; }

        public virtual Teacher Teacher { get; set; }

        public long DisciplineID { get; set; }

        public virtual Discipline Discipline { get; set; }

        public string RoleName { get; set; }

        public virtual Role Role { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string? PositionName { get; set; }

        public TeacherDisciplineConnection() => ValuesGenerator.GenerateValues(_idGenerator, () =>
            TeacherDisciplineConnections.FirstOrDefault((d) => d.TeacherID == TeacherID &&
            d.DisciplineID == DisciplineID) != null, (id) => {
                TeacherID = id;
                DisciplineID = id;
            }, () => TeacherDisciplineConnections.Add(this));
    }
}