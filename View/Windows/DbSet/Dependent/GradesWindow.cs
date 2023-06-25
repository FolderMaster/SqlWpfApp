using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;

using Model.Dependent;
using Model.Independent;
using ViewModel.VMs.DbSet;
using View.MessageBoxes;

namespace View.Windows.DbSet.Dependent
{
    public class GradesWindow : TwoGridDbSetWindow
    {
        private static GradesWindow? _instance = null;

        private static Action _action = () =>
        {
            var instance = Instance;
            instance.Show();
        };

        public static GradesWindow Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GradesWindow();
                }
                return _instance;
            }
        }

        public static Action Action => _action;

        private GradesWindow() : base()
        {
            Title = Application.Current.Resources[nameof(Grade) + "sHeader"] as string;
            Icon = Application.Current.Resources[nameof(Grade) + "sIcon"] as BitmapSource;

            var messageService = new ErrorMessageBoxService();

            var mainVM = new DbSetVM<Grade>(messageService);
            var dependentVM = new DbSetVM<GradeMode>(messageService);

            mainVM.ItemChanged += (object? sender, EventArgs e) =>
            {
                dependentVM.SelectedItem = mainVM.SelectedItem?.GradeMode;
            };

            DataContext = new List<object>()
            {
                mainVM, dependentVM,
                (string nameProperty) => nameProperty != nameof(Grade.GradeStatements) &&
                nameProperty != nameof(Grade.GradeMode), (string nameProperty) =>
                nameProperty != nameof(GradeMode.Grades) &&
                nameProperty != nameof(GradeMode.StudyForms),
                (Action)(() => _instance = null)
            };
        }
    }
}