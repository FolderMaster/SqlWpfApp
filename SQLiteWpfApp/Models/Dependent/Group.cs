using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SQLiteWpfApp.Models.Dependent
{
    [Table("Groups")]
    [PrimaryKey(nameof(Number), new string[] { nameof(FormationYear) })]
    public class Group
    {
        private static IdGenerator _idGenerator = new(1);

        public static ObservableCollection<Group> Groups { get; set; } = new();

        public string Number { get; set; }

        public int FormationYear { get; set; } = DateTime.Now.Year;

        public string SpecialtyNumber { get; set; }

        public virtual Specialty Specialty { get; set; }

        public virtual ObservableCollection<Student> Students { get; set; }

        public Group() => ValuesGenerator.GenerateValues(_idGenerator, () =>
            Groups.FirstOrDefault((d) => d.Number == Number && d.FormationYear == FormationYear) 
            != null, (id) => Number = nameof(Number) + "_" + id, () => Groups.Add(this));
    }
}