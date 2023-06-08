using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

using SQLiteWpfApp.Models.Dependent;

namespace SQLiteWpfApp.Models.Independent
{
    [Table("Roles")]
    [PrimaryKey(nameof(Name))]
    public class Role
    {
        private static IdGenerator _idGenerator = new(1);

        public static ObservableCollection<Role> Roles { get; set; } = new();

        public string Name { get; set; }

        public virtual ObservableCollection<TeacherDisciplineConnection> Connections { get; set; }

        public Role() => ValuesGenerator.GenerateValues(_idGenerator, () =>
            Roles.FirstOrDefault((r) => r.Name == Name) != null, (id) =>
            Name = nameof(Name) + "_" + id, () => Roles.Add(this));
    }
}