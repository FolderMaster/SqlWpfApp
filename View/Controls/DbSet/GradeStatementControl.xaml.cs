using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

using Model.Dependent;

namespace View.Controls.DbSet
{
    /// <summary>
    /// Класс элемента управления для работы с ведомостями оценивания с ведомостью оценивания,
    /// коллекциями преподавателей, студентов, дисциплин, оценок и логическим значением,
    /// указывающее только ли для чтения элемент управления.
    /// </summary>
    public partial class GradeStatementControl : UserControl
    {
        /// <summary>
        /// Возвращает и задаёт ведомость оценивания.
        /// </summary>
        public GradeStatement GradeStatement
        {
            get => (GradeStatement)GetValue(GradeStatementProperty);
            set => SetValue(GradeStatementProperty, value);
        }

        /// <summary>
        /// Возвращает и задаёт коллекцию преподавателей.
        /// </summary>
        public ObservableCollection<Teacher> Teachers
        {
            get => (ObservableCollection<Teacher>)GetValue(TeachersProperty);
            set => SetValue(TeachersProperty, value);
        }

        /// <summary>
        /// Возвращает и задаёт коллекцию студентов.
        /// </summary>
        public ObservableCollection<Student> Students
        {
            get => (ObservableCollection<Student>)GetValue(StudentsProperty);
            set => SetValue(StudentsProperty, value);
        }

        /// <summary>
        /// Возвращает и задаёт коллекцию дисциплин.
        /// </summary>
        public ObservableCollection<Discipline> Disciplines
        {
            get => (ObservableCollection<Discipline>)GetValue(DisciplinesProperty);
            set => SetValue(DisciplinesProperty, value);
        }

        /// <summary>
        /// Возвращает и задаёт коллекцию оценок.
        /// </summary>
        public ObservableCollection<Grade> Grades
        {
            get => (ObservableCollection<Grade>)GetValue(GradesProperty);
            set => SetValue(GradesProperty, value);
        }

        /// <summary>
        /// Возвращает и задаёт логическое значение, указывающее только ли для чтения элемент
        /// управления.
        /// </summary>
        public bool IsReadOnly
        {
            get => (bool)GetValue(IsReadOnlyProperty);
            set => SetValue(IsReadOnlyProperty, value);
        }

        /// <summary>
        /// Свойство зависимости <see cref="GradeStatement"/>.
        /// </summary>
        public static DependencyProperty GradeStatementProperty =
            DependencyProperty.Register(nameof(GradeStatement), typeof(GradeStatement),
                typeof(GradeStatementControl), new FrameworkPropertyMetadata());

        /// <summary>
        /// Свойство зависимости <see cref="Teachers"/>.
        /// </summary>
        public static DependencyProperty TeachersProperty =
            DependencyProperty.Register(nameof(Teachers), typeof(ObservableCollection<Teacher>),
                typeof(GradeStatementControl), new FrameworkPropertyMetadata());

        /// <summary>
        /// Свойство зависимости <see cref="Students"/>.
        /// </summary>
        public static DependencyProperty StudentsProperty =
            DependencyProperty.Register(nameof(Students), typeof(ObservableCollection<Student>),
                typeof(GradeStatementControl), new FrameworkPropertyMetadata());

        /// <summary>
        /// Свойство зависимости <see cref="Disciplines"/>.
        /// </summary>
        public static DependencyProperty DisciplinesProperty =
            DependencyProperty.Register(nameof(Disciplines),
                typeof(ObservableCollection<Discipline>), typeof(GradeStatementControl),
                new FrameworkPropertyMetadata());

        /// <summary>
        /// Свойство зависимости <see cref="Grades"/>.
        /// </summary>
        public static DependencyProperty GradesProperty =
            DependencyProperty.Register(nameof(Grades), typeof(ObservableCollection<Grade>),
                typeof(GradeStatementControl), new FrameworkPropertyMetadata());

        /// <summary>
        /// Свойство зависимости <see cref="IsReadOnly"/>.
        /// </summary>
        public static DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register(nameof(IsReadOnly), typeof(bool),
                typeof(GradeStatementControl), new FrameworkPropertyMetadata());

        /// <summary>
        /// Создаёт экземпляр класса <see cref="GradeStatementControl"/> по умолчанию.
        /// </summary>
        public GradeStatementControl()
        {
            InitializeComponent();
        }
    }
}