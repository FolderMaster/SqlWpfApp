using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

using Model.Dependent;
using Model.Independent;

namespace View.Controls.DbSet
{
    public partial class TeacherDisciplineConnectionControl : UserControl
    {
        public TeacherDisciplineConnection TeacherDisciplineConnection
        {
            get => (TeacherDisciplineConnection)GetValue(TeacherDisciplineConnectionProperty);
            set => SetValue(TeacherDisciplineConnectionProperty, value);
        }

        public ObservableCollection<Teacher> Teachers
        {
            get => (ObservableCollection<Teacher>)GetValue(TeachersProperty);
            set => SetValue(TeachersProperty, value);
        }

        public ObservableCollection<Discipline> Disciplines
        {
            get => (ObservableCollection<Discipline>)GetValue(DisciplinesProperty);
            set => SetValue(DisciplinesProperty, value);
        }

        public ObservableCollection<Role> Roles
        {
            get => (ObservableCollection<Role>)GetValue(RolesProperty);
            set => SetValue(RolesProperty, value);
        }

        public bool IsReadOnly
        {
            get => (bool)GetValue(IsReadOnlyProperty);
            set => SetValue(IsReadOnlyProperty, value);
        }

        public static DependencyProperty TeacherDisciplineConnectionProperty =
            DependencyProperty.Register(nameof(TeacherDisciplineConnection),
                typeof(TeacherDisciplineConnection), typeof(TeacherDisciplineConnectionControl),
                new FrameworkPropertyMetadata());

        public static DependencyProperty TeachersProperty =
            DependencyProperty.Register(nameof(Teachers), typeof(ObservableCollection<Teacher>),
                typeof(TeacherDisciplineConnectionControl), new FrameworkPropertyMetadata());

        public static DependencyProperty DisciplinesProperty =
            DependencyProperty.Register(nameof(Disciplines),
                typeof(ObservableCollection<Discipline>),
                typeof(TeacherDisciplineConnectionControl), new FrameworkPropertyMetadata());

        public static DependencyProperty RolesProperty =
            DependencyProperty.Register(nameof(Roles), typeof(ObservableCollection<Role>),
                typeof(TeacherDisciplineConnectionControl), new FrameworkPropertyMetadata());

        public static DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register(nameof(IsReadOnly), typeof(bool),
                typeof(TeacherDisciplineConnectionControl), new FrameworkPropertyMetadata());

        public TeacherDisciplineConnectionControl()
        {
            InitializeComponent();
        }
    }
}