using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

using SQLiteWpfApp.Models.Dependent;
using SQLiteWpfApp.Models.Independent;

namespace SQLiteWpfApp.Views.Controls
{
    public partial class StudentControl : UserControl
    {
        public Student Student
        {
            get => (Student)GetValue(StudentProperty);
            set => SetValue(StudentProperty, value);
        }

        public ObservableCollection<Scholarship> Scholarships
        {
            get => (ObservableCollection<Scholarship>)GetValue(ScholarshipsProperty);
            set => SetValue(ScholarshipsProperty, value);
        }

        public ObservableCollection<Group> Groups
        {
            get => (ObservableCollection<Group>)GetValue(GroupsProperty);
            set => SetValue(GroupsProperty, value);
        }

        public ObservableCollection<Person> Persons
        {
            get => (ObservableCollection<Person>)GetValue(PersonsProperty);
            set => SetValue(PersonsProperty, value);
        }

        public bool IsReadOnly
        {
            get => (bool)GetValue(IsReadOnlyProperty);
            set => SetValue(IsReadOnlyProperty, value);
        }

        public static DependencyProperty StudentProperty =
            DependencyProperty.Register(nameof(Student), typeof(Student),
                typeof(StudentControl), new FrameworkPropertyMetadata());

        public static DependencyProperty ScholarshipsProperty =
            DependencyProperty.Register(nameof(Scholarships),
                typeof(ObservableCollection<Scholarship>), typeof(StudentControl),
                new FrameworkPropertyMetadata());

        public static DependencyProperty GroupsProperty =
            DependencyProperty.Register(nameof(Groups), typeof(ObservableCollection<Group>),
                typeof(StudentControl), new FrameworkPropertyMetadata());

        public static DependencyProperty PersonsProperty =
            DependencyProperty.Register(nameof(Persons), typeof(ObservableCollection<Person>),
                typeof(StudentControl), new FrameworkPropertyMetadata());

        public static DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register(nameof(IsReadOnly), typeof(bool),
                typeof(StudentControl), new FrameworkPropertyMetadata());

        public StudentControl()
        {
            InitializeComponent();
        }
    }
}