using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.ObjectModel;

using Model.Independent;

namespace Model.Dependent
{
    [Table("Specialties")]
    [PrimaryKey(nameof(Number))]
    public class Specialty
    {
        private static IdGenerator _idGenerator = new(1);

        public static ObservableCollection<Specialty> Specialties { get; set; } = new();

        public string Number { get; set; }

        public string Name { get; set; }

        public string DepartmentName { get; set; }

        public virtual Department Department { get; set; }

        public virtual ObservableCollection<Discipline> Disciplines { get; set; }

        public virtual ObservableCollection<Group> Groups { get; set; }

        public Specialty() => ValuesGenerator.GenerateValues(_idGenerator, () =>
            Specialties.FirstOrDefault((d) => d.Number == Number) != null, (id) => {
                Number = nameof(Number) + "_" + id;
                Name = nameof(Name) + "_" + id;
            }, () => Specialties.Add(this));
    }
}