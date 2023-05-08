using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Xml.Linq;

namespace SQLiteWpfApp.Models.Independent
{
    [Table("Positions")]
    [PrimaryKey(nameof(Name))]
    public class Position : INotifyPropertyChanged
    {
        private static IdGenerator _idGenerator = new(1);

        private string _name = "";

        public static ObservableCollection<Position> Positions { get; set; } = new();

        public string Name
        {
            get => _name;
            set
            {
                if (Name != value)
                {
                    _name = value;
                    InvokePropertyChanged(nameof(Name));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public Position() => ValuesGenerator.GenerateValues(_idGenerator, () =>
            Positions.FirstOrDefault((p) => p.Name == Name) != null, (id) =>
            Name = nameof(Name) + "_" + id, () => Positions.Add(this));

        private void InvokePropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}