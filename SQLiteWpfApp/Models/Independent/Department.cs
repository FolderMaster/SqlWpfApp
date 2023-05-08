using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SQLiteWpfApp.Models.Independent
{
    [Table("Departments")]
    [PrimaryKey(nameof(Name))]
    public class Department : INotifyPropertyChanged
    {
        private static IdGenerator _idGenerator = new(1);

        private string _name = "";

        private string _symbol = "";

        public static ObservableCollection<Department> Departments { get; set; } = new();

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

        public string Symbol
        {
            get => _symbol;
            set
            {
                if (Symbol != value)
                {
                    _symbol = value;
                    InvokePropertyChanged(nameof(Symbol));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public Department() => ValuesGenerator.GenerateValues(_idGenerator, () =>
            Departments.FirstOrDefault((d) => d.Name == Name) != null, (id) => {
                Name = nameof(Name) + "_" + id;
                Symbol = nameof(Symbol) + "_" + id;
            }, () => Departments.Add(this));

        private void InvokePropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}