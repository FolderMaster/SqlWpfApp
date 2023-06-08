using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;

using SQLiteWpfApp.Models.Dependent;
using SQLiteWpfApp.ViewModels.VMs.DbSet;
using SQLiteWpfApp.Views.MessageBoxes;

namespace SQLiteWpfApp.Views.Windows.DbSet.Dependent
{
    public class DisciplinesWindow : ThreeGridDbSetWindow
    {
        private static DisciplinesWindow? _instance = null;

        private static Action _action = () =>
        {
            var instance = Instance;
            instance.Show();
        };

        public static DisciplinesWindow Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DisciplinesWindow();
                }
                return _instance;
            }
        }

        public static Action Action => _action;

        private DisciplinesWindow() : base()
        {
            Title = Application.Current.Resources[nameof(Discipline) + "sHeader"] as string;
            Icon = Application.Current.Resources[nameof(Discipline) + "sIcon"] as BitmapSource;

            var messageService = new ErrorMessageBoxService();

            var mainVM = new DbSetVM<Discipline>(messageService);
            var dependentVM = new DbSetVM<StudyForm>(messageService);
            var dependent2VM = new DbSetVM<Specialty>(messageService);

            mainVM.ItemChanged += (object? sender, EventArgs e) =>
            {
                dependentVM.SelectedItem = mainVM.SelectedItem?.StudyForm;
                dependent2VM.SelectedItem = mainVM.SelectedItem?.Specialty;
            };

            DataContext = new List<object>()
            {
                mainVM, dependentVM, dependent2VM,
                (string nameProperty) => nameProperty != nameof(Discipline.Specialty) &&
                nameProperty != nameof(Discipline.StudentConnections) &&
                nameProperty != nameof(Discipline.TeacherConnections) &&
                nameProperty != nameof(Discipline.StudyForm) &&
                nameProperty != nameof(Discipline.GradeStatements),
                (string nameProperty) => nameProperty != nameof(StudyForm.Disciplines) &&
                nameProperty != nameof(StudyForm.GradeMode),
                (string nameProperty) => nameProperty != nameof(Specialty.Groups) &&
                nameProperty != nameof(Specialty.Disciplines) &&
                nameProperty != nameof(Specialty.Department),
                (Action)(() => _instance = null)
            };
        }
    }
}