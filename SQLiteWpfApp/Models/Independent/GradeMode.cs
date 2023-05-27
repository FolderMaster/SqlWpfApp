using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

using SQLiteWpfApp.Models.Dependent;

namespace SQLiteWpfApp.Models.Independent
{
    [Table("GradeModes")]
    [PrimaryKey(nameof(Name))]
    public class GradeMode : INotifyPropertyChanged
    {
        private static IdGenerator _idGenerator = new(1);

        private string _name = "";

        public static ObservableCollection<GradeMode> GradeModes { get; set; } = new();

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

        public virtual ObservableCollection<Grade> Grades { get; set; }

        public virtual ObservableCollection<StudyForm> StudyForms { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public GradeMode() => ValuesGenerator.GenerateValues(_idGenerator, () =>
            GradeModes.FirstOrDefault((g) => g.Name == Name) != null, (id) =>
            Name = nameof(Name) + "_" + id, () => GradeModes.Add(this));

        private void InvokePropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}