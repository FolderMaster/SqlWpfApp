using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

using SQLiteWpfApp.Models.Dependent;

namespace SQLiteWpfApp.Models.Independent
{
    [Table("Scholarships")]
    [PrimaryKey(nameof(Name))]
    public class Scholarship : INotifyPropertyChanged
    {
        private static IdGenerator _idGenerator = new(1);

        private string _name = "";

        private string _symbol = "";

        private double _coefficient = 1;

        public static ObservableCollection<Scholarship> Scholarships { get; set; } = new();

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

        public double Coefficient
        {
            get => _coefficient;
            set
            {
                if (Coefficient != value)
                {
                    _coefficient = value;
                    InvokePropertyChanged(nameof(Coefficient));
                }
            }
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public double Amount { get; set; }

        public virtual ObservableCollection<Student> Students { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public Scholarship() => ValuesGenerator.GenerateValues(_idGenerator, () =>
            Scholarships.FirstOrDefault((d) => d.Name == Name) != null, (id) => {
                Name = nameof(Name) + "_" + id;
                Symbol = nameof(Symbol) + "_" + id;
            }, () => Scholarships.Add(this));

        private void InvokePropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}