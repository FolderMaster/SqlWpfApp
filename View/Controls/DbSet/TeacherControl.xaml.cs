using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

using Model.Dependent;
using Model.Independent;

namespace View.Controls.DbSet
{
    /// <summary>
    /// Класс элемента управления для работы с преподавателями с преподавателем, коллекциями
    /// должностями, факультетами, персонами и логическим значением, указывающее только ли для
    /// чтения элемент управления.
    /// </summary>
    public partial class TeacherControl : UserControl
    {
        /// <summary>
        /// Возвращает и задаёт преподавателя.
        /// </summary>
        public Teacher Teacher
        {
            get => (Teacher)GetValue(TeacherProperty);
            set => SetValue(TeacherProperty, value);
        }

        /// <summary>
        /// Возвращает и задаёт коллекцию должностей.
        /// </summary>
        public ObservableCollection<Position> Positions
        {
            get => (ObservableCollection<Position>)GetValue(PositionsProperty);
            set => SetValue(PositionsProperty, value);
        }

        /// <summary>
        /// Возвращает и задаёт коллекцию факультетов.
        /// </summary>
        public ObservableCollection<Department> Departments
        {
            get => (ObservableCollection<Department>)GetValue(DepartmentsProperty);
            set => SetValue(DepartmentsProperty, value);
        }

        /// <summary>
        /// Возвращает и задаёт коллекцию персоны.
        /// </summary>
        public ObservableCollection<Person> Persons
        {
            get => (ObservableCollection<Person>)GetValue(PersonsProperty);
            set => SetValue(PersonsProperty, value);
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
        /// Свойство зависимости <see cref="Teacher"/>.
        /// </summary>
        public static DependencyProperty TeacherProperty =
            DependencyProperty.Register(nameof(Teacher), typeof(Teacher),
                typeof(TeacherControl), new FrameworkPropertyMetadata());

        /// <summary>
        /// Свойство зависимости <see cref="Positions"/>.
        /// </summary>
        public static DependencyProperty PositionsProperty =
            DependencyProperty.Register(nameof(Positions), typeof(ObservableCollection<Position>),
                typeof(TeacherControl), new FrameworkPropertyMetadata());

        /// <summary>
        /// Свойство зависимости <see cref="Departments"/>.
        /// </summary>
        public static DependencyProperty DepartmentsProperty =
            DependencyProperty.Register(nameof(Departments),
                typeof(ObservableCollection<Department>), typeof(TeacherControl),
                new FrameworkPropertyMetadata());

        /// <summary>
        /// Свойство зависимости <see cref="Persons"/>.
        /// </summary>
        public static DependencyProperty PersonsProperty =
            DependencyProperty.Register(nameof(Persons), typeof(ObservableCollection<Person>),
                typeof(TeacherControl), new FrameworkPropertyMetadata());

        /// <summary>
        /// Свойство зависимости <see cref="IsReadOnly"/>.
        /// </summary>
        public static DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register(nameof(IsReadOnly), typeof(bool),
                typeof(TeacherControl), new FrameworkPropertyMetadata());

        /// <summary>
        /// Создаёт экземпляр класса <see cref="TeacherControl"/> по умолчанию.
        /// </summary>
        public TeacherControl()
        {
            InitializeComponent();
        }
    }
}