using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.ObjectModel;
using System.Linq;

using SQLiteWpfApp.Models.Independent;

namespace SQLiteWpfApp.Models.Dependent
{
    [Table("StudyForms")]
    [PrimaryKey(nameof(Name))]
    public class StudyForm
    {
        private static IdGenerator _idGenerator = new(1);

        public static ObservableCollection<StudyForm> StudyForms { get; set; } = new();

        public string Name { get; set; }

        public string GradeModeName { get; set; }

        public virtual GradeMode GradeMode { get; set; }

        public virtual ObservableCollection<Discipline> Disciplines { get; set; }

        public StudyForm() => ValuesGenerator.GenerateValues(_idGenerator, () =>
            StudyForms.FirstOrDefault((d) => d.Name == Name) != null, (id) =>
            Name = nameof(Name) + "_" + id, () => StudyForms.Add(this));
    }
}