using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;

using SQLiteWpfApp.Models.Dependent;
using SQLiteWpfApp.Models.Independent;
using SQLiteWpfApp.ViewModels.VMs.DbSet;
using SQLiteWpfApp.Views.MessageBoxes;

namespace SQLiteWpfApp.Views.Windows.DbSet.Dependent
{
    public class StudyFormsWindow : TwoGridDbSetWindow
    {
        private static StudyFormsWindow? _instance = null;

        private static Action _action = () =>
        {
            var instance = Instance;
            instance.Show();
        };

        public static StudyFormsWindow Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new StudyFormsWindow();
                }
                return _instance;
            }
        }

        public static Action Action => _action;

        private StudyFormsWindow() : base()
        {
            Title = Application.Current.Resources[nameof(StudyForm) + "sHeader"] as string;
            Icon = Application.Current.Resources[nameof(StudyForm) + "sIcon"] as BitmapSource;

            var messageService = new ErrorMessageBoxService();

            var mainVM = new DbSetVM<StudyForm>(messageService);
            var dependentVM = new DbSetVM<GradeMode>(messageService);

            mainVM.ItemChanged += (object? sender, EventArgs e) =>
            {
                dependentVM.SelectedItem = mainVM.SelectedItem?.GradeMode;
            };

            DataContext = new List<object>()
            {
                mainVM, dependentVM, (string nameProperty) =>
                nameProperty != nameof(StudyForm.GradeMode) &&
                nameProperty != nameof(StudyForm.Disciplines), (string nameProperty) =>
                nameProperty != nameof(GradeMode.Grades) &&
                nameProperty != nameof(GradeMode.StudyForms),
                (Action)(() => _instance = null)
            };
        }
    }
}