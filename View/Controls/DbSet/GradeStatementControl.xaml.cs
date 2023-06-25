using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

using Model.Dependent;

namespace View.Controls.DbSet
{
    public partial class GradeStatementControl : UserControl
    {
        public GradeStatement GradeStatement
        {
            get => (GradeStatement)GetValue(GradeStatementProperty);
            set => SetValue(GradeStatementProperty, value);
        }

        public ObservableCollection<Teacher> Teachers
        {
            get => (ObservableCollection<Teacher>)GetValue(TeachersProperty);
            set => SetValue(TeachersProperty, value);
        }

        public ObservableCollection<Student> Students
        {
            get => (ObservableCollection<Student>)GetValue(StudentsProperty);
            set => SetValue(StudentsProperty, value);
        }

        public ObservableCollection<Discipline> Disciplines
        {
            get => (ObservableCollection<Discipline>)GetValue(DisciplinesProperty);
            set => SetValue(DisciplinesProperty, value);
        }

        public ObservableCollection<Grade> Grades
        {
            get => (ObservableCollection<Grade>)GetValue(GradesProperty);
            set => SetValue(GradesProperty, value);
        }

        public bool IsReadOnly
        {
            get => (bool)GetValue(IsReadOnlyProperty);
            set => SetValue(IsReadOnlyProperty, value);
        }

        public static DependencyProperty GradeStatementProperty =
            DependencyProperty.Register(nameof(GradeStatement), typeof(GradeStatement),
                typeof(GradeStatementControl), new FrameworkPropertyMetadata());

        public static DependencyProperty TeachersProperty =
            DependencyProperty.Register(nameof(Teachers), typeof(ObservableCollection<Teacher>),
                typeof(GradeStatementControl), new FrameworkPropertyMetadata());

        public static DependencyProperty StudentsProperty =
            DependencyProperty.Register(nameof(Students), typeof(ObservableCollection<Student>),
                typeof(GradeStatementControl), new FrameworkPropertyMetadata());

        public static DependencyProperty DisciplinesProperty =
            DependencyProperty.Register(nameof(Disciplines),
                typeof(ObservableCollection<Discipline>), typeof(GradeStatementControl),
                new FrameworkPropertyMetadata());

        public static DependencyProperty GradesProperty =
            DependencyProperty.Register(nameof(Grades), typeof(ObservableCollection<Grade>),
                typeof(GradeStatementControl), new FrameworkPropertyMetadata());

        public static DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register(nameof(IsReadOnly), typeof(bool),
                typeof(GradeStatementControl), new FrameworkPropertyMetadata());

        public GradeStatementControl()
        {
            InitializeComponent();
        }
    }
}