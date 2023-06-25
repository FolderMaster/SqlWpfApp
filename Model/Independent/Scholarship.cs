using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

using Model.Dependent;

namespace Model.Independent
{
    [Table("Scholarships")]
    [PrimaryKey(nameof(Name))]
    public class Scholarship
    {
        private static IdGenerator _idGenerator = new(1);

        public static ObservableCollection<Scholarship> Scholarships { get; set; } = new();

        public string Name { get; set; }

        public string Symbol { get; set; }

        public double Coefficient { get; set; } = 1;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public double Amount { get; set; }

        public virtual ObservableCollection<Student> Students { get; set; }

        public Scholarship() => ValuesGenerator.GenerateValues(_idGenerator, () =>
            Scholarships.FirstOrDefault((d) => d.Name == Name) != null, (id) => {
                Name = nameof(Name) + "_" + id;
                Symbol = nameof(Symbol) + "_" + id;
            }, () => Scholarships.Add(this));
    }
}