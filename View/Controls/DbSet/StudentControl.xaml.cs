using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

using Model.Dependent;
using Model.Independent;

namespace View.Controls.DbSet
{
    /// <summary>
    /// Класс элемента управления для работы со студентами с студентом, коллекциями стипендий,
    /// групп, персон и логическим значением, указывающее только ли для чтения элемент управления.
    /// </summary>
    public partial class StudentControl : UserControl
    {
        /// <summary>
        /// Возвращает и задаёт студента.
        /// </summary>
        public Student Student
        {
            get => (Student)GetValue(StudentProperty);
            set => SetValue(StudentProperty, value);
        }

        /// <summary>
        /// Возвращает и задаёт коллекцию стипендий.
        /// </summary>
        public ObservableCollection<Scholarship> Scholarships
        {
            get => (ObservableCollection<Scholarship>)GetValue(ScholarshipsProperty);
            set => SetValue(ScholarshipsProperty, value);
        }

        /// <summary>
        /// Возвращает и задаёт коллекцию группы.
        /// </summary>
        public ObservableCollection<Group> Groups
        {
            get => (ObservableCollection<Group>)GetValue(GroupsProperty);
            set => SetValue(GroupsProperty, value);
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
        /// Свойство зависимости <see cref="Student"/>.
        /// </summary>
        public static DependencyProperty StudentProperty =
            DependencyProperty.Register(nameof(Student), typeof(Student),
                typeof(StudentControl), new FrameworkPropertyMetadata());

        /// <summary>
        /// Свойство зависимости <see cref="Scholarships"/>.
        /// </summary>
        public static DependencyProperty ScholarshipsProperty =
            DependencyProperty.Register(nameof(Scholarships),
                typeof(ObservableCollection<Scholarship>), typeof(StudentControl),
                new FrameworkPropertyMetadata());

        /// <summary>
        /// Свойство зависимости <see cref="Groups"/>.
        /// </summary>
        public static DependencyProperty GroupsProperty =
            DependencyProperty.Register(nameof(Groups), typeof(ObservableCollection<Group>),
                typeof(StudentControl), new FrameworkPropertyMetadata());

        /// <summary>
        /// Свойство зависимости <see cref="Persons"/>.
        /// </summary>
        public static DependencyProperty PersonsProperty =
            DependencyProperty.Register(nameof(Persons), typeof(ObservableCollection<Person>),
                typeof(StudentControl), new FrameworkPropertyMetadata());

        /// <summary>
        /// Свойство зависимости <see cref="IsReadOnly"/>.
        /// </summary>
        public static DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register(nameof(IsReadOnly), typeof(bool),
                typeof(StudentControl), new FrameworkPropertyMetadata());

        /// <summary>
        /// Создаёт экземпляр класса <see cref="StudentControl"/> по умолчанию.
        /// </summary>
        public StudentControl()
        {
            InitializeComponent();
        }
    }
}