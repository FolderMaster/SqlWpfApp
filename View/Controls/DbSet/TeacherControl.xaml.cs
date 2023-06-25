using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

using Model.Dependent;
using Model.Independent;

namespace View.Controls.DbSet
{
    public partial class TeacherControl : UserControl
    {
        public Teacher Teacher
        {
            get => (Teacher)GetValue(TeacherProperty);
            set => SetValue(TeacherProperty, value);
        }

        public ObservableCollection<Position> Positions
        {
            get => (ObservableCollection<Position>)GetValue(PositionsProperty);
            set => SetValue(PositionsProperty, value);
        }

        public ObservableCollection<Department> Departments
        {
            get => (ObservableCollection<Department>)GetValue(DepartmentsProperty);
            set => SetValue(DepartmentsProperty, value);
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

        public static DependencyProperty TeacherProperty =
            DependencyProperty.Register(nameof(Teacher), typeof(Teacher),
                typeof(TeacherControl), new FrameworkPropertyMetadata());

        public static DependencyProperty PositionsProperty =
            DependencyProperty.Register(nameof(Positions), typeof(ObservableCollection<Position>),
                typeof(TeacherControl), new FrameworkPropertyMetadata());

        public static DependencyProperty DepartmentsProperty =
            DependencyProperty.Register(nameof(Departments),
                typeof(ObservableCollection<Department>), typeof(TeacherControl),
                new FrameworkPropertyMetadata());

        public static DependencyProperty PersonsProperty =
            DependencyProperty.Register(nameof(Persons), typeof(ObservableCollection<Person>),
                typeof(TeacherControl), new FrameworkPropertyMetadata());

        public static DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register(nameof(IsReadOnly), typeof(bool),
                typeof(TeacherControl), new FrameworkPropertyMetadata());

        public TeacherControl()
        {
            InitializeComponent();
        }
    }
}