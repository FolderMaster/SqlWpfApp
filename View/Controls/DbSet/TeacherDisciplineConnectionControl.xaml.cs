using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

using Model.Dependent;
using Model.Independent;

namespace View.Controls.DbSet
{
    /// <summary>
    /// Класс элемента управления для работы со связями между дисциплинами и преподавателями с
    /// связью между дисциплинами и преподавателями, коллекциями преподавателей, дисциплин, ролей и
    /// логическим значением, указывающее только ли для чтения элемент управления.
    /// </summary>
    public partial class TeacherDisciplineConnectionControl : UserControl
    {
        /// <summary>
        /// Возвращает и задаёт связь между дисциплинами и преподавателями.
        /// </summary>
        public TeacherDisciplineConnection TeacherDisciplineConnection
        {
            get => (TeacherDisciplineConnection)GetValue(TeacherDisciplineConnectionProperty);
            set => SetValue(TeacherDisciplineConnectionProperty, value);
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
        /// Возвращает и задаёт коллекцию дисциплин.
        /// </summary>
        public ObservableCollection<Discipline> Disciplines
        {
            get => (ObservableCollection<Discipline>)GetValue(DisciplinesProperty);
            set => SetValue(DisciplinesProperty, value);
        }

        /// <summary>
        /// Возвращает и задаёт коллекцию ролей.
        /// </summary>
        public ObservableCollection<Role> Roles
        {
            get => (ObservableCollection<Role>)GetValue(RolesProperty);
            set => SetValue(RolesProperty, value);
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
        /// Свойство зависимости <see cref="TeacherDisciplineConnection"/>.
        /// </summary>
        public static DependencyProperty TeacherDisciplineConnectionProperty =
            DependencyProperty.Register(nameof(TeacherDisciplineConnection),
                typeof(TeacherDisciplineConnection), typeof(TeacherDisciplineConnectionControl),
                new FrameworkPropertyMetadata());

        /// <summary>
        /// Свойство зависимости <see cref="Teachers"/>.
        /// </summary>
        public static DependencyProperty TeachersProperty =
            DependencyProperty.Register(nameof(Teachers), typeof(ObservableCollection<Teacher>),
                typeof(TeacherDisciplineConnectionControl), new FrameworkPropertyMetadata());

        /// <summary>
        /// Свойство зависимости <see cref="Disciplines"/>.
        /// </summary>
        public static DependencyProperty DisciplinesProperty =
            DependencyProperty.Register(nameof(Disciplines),
                typeof(ObservableCollection<Discipline>),
                typeof(TeacherDisciplineConnectionControl), new FrameworkPropertyMetadata());

        /// <summary>
        /// Свойство зависимости <see cref="Roles"/>.
        /// </summary>
        public static DependencyProperty RolesProperty =
            DependencyProperty.Register(nameof(Roles), typeof(ObservableCollection<Role>),
                typeof(TeacherDisciplineConnectionControl), new FrameworkPropertyMetadata());

        /// <summary>
        /// Свойство зависимости <see cref="IsReadOnly"/>.
        /// </summary>
        public static DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register(nameof(IsReadOnly), typeof(bool),
                typeof(TeacherDisciplineConnectionControl), new FrameworkPropertyMetadata());

        /// <summary>
        /// Создаёт экземпляр класса <see cref="TeacherDisciplineConnectionControl"/> по умолчанию.
        /// </summary>
        public TeacherDisciplineConnectionControl()
        {
            InitializeComponent();
        }
    }
}