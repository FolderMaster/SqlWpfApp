using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

using SQLiteWpfApp.Models.Dependent;

namespace SQLiteWpfApp.Models.Independent
{
    [Table("Departments")]
    [PrimaryKey(nameof(Name))]
    public class Department
    {
        private static IdGenerator _idGenerator = new(1);

        public static ObservableCollection<Department> Departments { get; set; } = new();

        public string Name { get; set; }

        public string Symbol { get; set; }

        public virtual ObservableCollection<Specialty> Specialties { get; set; }

        public virtual ObservableCollection<Teacher> Teachers { get; set; }

        public Department() => ValuesGenerator.GenerateValues(_idGenerator, () =>
            Departments.FirstOrDefault((d) => d.Name == Name) != null, (id) => {
                Name = nameof(Name) + "_" + id;
                Symbol = nameof(Symbol) + "_" + id;
            }, () => Departments.Add(this));
    }
}