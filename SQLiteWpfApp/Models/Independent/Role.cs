using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

using SQLiteWpfApp.Models.Dependent;

namespace SQLiteWpfApp.Models.Independent
{
    [Table("Roles")]
    [PrimaryKey(nameof(Name))]
    public class Role : INotifyPropertyChanged
    {
        private static IdGenerator _idGenerator = new(1);

        private string _name = "";

        public static ObservableCollection<Role> Roles { get; set; } = new();

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

        public virtual ObservableCollection<TeacherDisciplineConnection> Connections { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public Role() => ValuesGenerator.GenerateValues(_idGenerator, () =>
            Roles.FirstOrDefault((r) => r.Name == Name) != null, (id) =>
            Name = nameof(Name) + "_" + id, () => Roles.Add(this));

        private void InvokePropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}