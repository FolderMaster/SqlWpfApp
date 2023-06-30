using System;
using System.Collections.Generic;

using Model.Dependent;
using ViewModel.VMs.DbSet;
using View.Implementations.MessageBoxes;
using View.Services;
using ViewModel.Interfaces;

namespace View.Windows.DbSet.Dependent
{
    public class StudentDisciplineConnectionsWindow : ThreeGridDbSetWindow
    {
        private static StudentDisciplineConnectionsWindow? _instance = null;

        private static Action _call = () =>
        {
            var instance = Instance;
            instance.Show();
        };

        public static StudentDisciplineConnectionsWindow Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new StudentDisciplineConnectionsWindow();
                }
                return _instance;
            }
        }

        public static Action Call => _call;

        public static IDataBaseContextCreator? DataBaseContextCreator { get; set; }

        private StudentDisciplineConnectionsWindow() : base()
        {
            Title = AppResourceService.GetHeader(nameof(StudentDisciplineConnection));
            Icon = AppResourceService.GetIcon(nameof(StudentDisciplineConnection));

            var messageService = new ErrorMessageBoxService();

            var mainVM = new DbSetVM<StudentDisciplineConnection>(DataBaseContextCreator,
                messageService);
            var dependentVM = new DbSetVM<Student>(DataBaseContextCreator, messageService);
            var dependent2VM = new DbSetVM<Discipline>(DataBaseContextCreator, messageService);

            mainVM.ItemChanged += (object? sender, EventArgs e) =>
            {
                dependentVM.SelectedItem = mainVM.SelectedItem?.Student;
                dependent2VM.SelectedItem = mainVM.SelectedItem?.Discipline;
            };

            DataContext = new List<object>()
            {
                mainVM, dependentVM, dependent2VM, (string nameProperty) =>
                nameProperty != nameof(StudentDisciplineConnection.Student) &&
                nameProperty != nameof(StudentDisciplineConnection.Discipline),
                (string nameProperty) => nameProperty != nameof(Student.Group) &&
                nameProperty != nameof(Student.Person) &&
                nameProperty != nameof(Student.Scholarship) &&
                nameProperty != nameof(Student.Connections) &&
                nameProperty != nameof(Student.GradeStatements), (string nameProperty) =>
                nameProperty != nameof(Discipline.Specialty) &&
                nameProperty != nameof(Discipline.StudentConnections) &&
                nameProperty != nameof(Discipline.TeacherConnections) &&
                nameProperty != nameof(Discipline.StudyForm) &&
                nameProperty != nameof(Discipline.GradeStatements),
                (Action)(() => _instance = null)
            };
        }
    }
}