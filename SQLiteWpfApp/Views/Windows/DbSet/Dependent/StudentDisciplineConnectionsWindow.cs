using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;

using SQLiteWpfApp.Models.Dependent;
using SQLiteWpfApp.ViewModels.VMs.DbSet;
using SQLiteWpfApp.Views.MessageBoxes;

namespace SQLiteWpfApp.Views.Windows.DbSet.Dependent
{
    public class StudentDisciplineConnectionsWindow : ThreeGridDbSetWindow
    {
        private static StudentDisciplineConnectionsWindow? _instance = null;

        private static Action _action = () =>
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

        public static Action Action => _action;

        private StudentDisciplineConnectionsWindow() : base()
        {
            Title = Application.Current.Resources[nameof(StudentDisciplineConnection) + "sHeader"]
                as string;
            Icon = Application.Current.Resources[nameof(StudentDisciplineConnection) + "sIcon"]
                as BitmapSource;

            var messageService = new ErrorMessageBoxService();

            var mainVM = new DbSetVM<StudentDisciplineConnection>(messageService);
            var dependentVM = new DbSetVM<Student>(messageService);
            var dependent2VM = new DbSetVM<Discipline>(messageService);

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