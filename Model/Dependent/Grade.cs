using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.ObjectModel;

using Model.Independent;

namespace Model.Dependent
{
    [Table("Grades")]
    [PrimaryKey(nameof(Name))]
    public class Grade
    {
        private static IdGenerator _idGenerator = new(1);

        public static ObservableCollection<Grade> Grades { get; set; } = new();

        public string Name { get; set; }

        public string Symbol { get; set; }

        public string GradeModeName { get; set; }

        public virtual GradeMode GradeMode { get; set; }

        public int Coefficient { get; set; } = 2;

        public virtual ObservableCollection<GradeStatement> GradeStatements { get; set; }

        public Grade() => ValuesGenerator.GenerateValues(_idGenerator, () =>
            Grades.FirstOrDefault((d) => d.Name == Name) != null, (id) => {
                Name = nameof(Name) + "_" + id;
                Symbol = nameof(Symbol) + "_" + id;
            }, () => Grades.Add(this));
    }
}