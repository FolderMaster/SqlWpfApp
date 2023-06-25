using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

using Model.Dependent;

namespace Model.Independent
{
    [Table("GradeModes")]
    [PrimaryKey(nameof(Name))]
    public class GradeMode
    {
        private static IdGenerator _idGenerator = new(1);

        public static ObservableCollection<GradeMode> GradeModes { get; set; } = new();

        public string Name { get; set; }

        public virtual ObservableCollection<Grade> Grades { get; set; }

        public virtual ObservableCollection<StudyForm> StudyForms { get; set; }

        public GradeMode() => ValuesGenerator.GenerateValues(_idGenerator, () =>
            GradeModes.FirstOrDefault((g) => g.Name == Name) != null, (id) =>
            Name = nameof(Name) + "_" + id, () => GradeModes.Add(this));
    }
}