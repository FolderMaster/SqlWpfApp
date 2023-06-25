using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

using Model.Dependent;

namespace Model.Independent
{
    [Table("Positions")]
    [PrimaryKey(nameof(Name))]
    public class Position
    {
        private static IdGenerator _idGenerator = new(1);

        public static ObservableCollection<Position> Positions { get; set; } = new();

        public string Name { get; set; }

        public virtual ObservableCollection<Teacher> Teachers { get; set; }

        public Position() => ValuesGenerator.GenerateValues(_idGenerator, () =>
            Positions.FirstOrDefault((p) => p.Name == Name) != null, (id) =>
            Name = nameof(Name) + "_" + id, () => Positions.Add(this));
    }
}