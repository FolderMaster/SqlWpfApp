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
    public class SpecialtiesWindow : TwoGridDbSetWindow
    {
        private static SpecialtiesWindow? _instance = null;

        private static Action _action = () =>
        {
            var instance = Instance;
            instance.Show();
        };

        public static SpecialtiesWindow Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SpecialtiesWindow();
                }
                return _instance;
            }
        }

        public static Action Action => _action;

        private SpecialtiesWindow() : base()
        {
            Title = Application.Current.Resources["SpecialtiesHeader"] as string;
            Icon = Application.Current.Resources["SpecialtiesIcon"] as BitmapSource;

            var messageService = new ErrorMessageBoxService();

            var mainVM = new DbSetVM<Specialty>(messageService);
            var dependentVM = new DbSetVM<Department>(messageService);

            mainVM.ItemChanged += (object? sender, EventArgs e) =>
            {
                dependentVM.SelectedItem = mainVM.SelectedItem?.Department;
            };

            DataContext = new List<object>()
            {
                mainVM, dependentVM, (string nameProperty) =>
                nameProperty != nameof(Specialty.Groups) &&
                nameProperty != nameof(Specialty.Disciplines) &&
                nameProperty != nameof(Specialty.Department), (string nameProperty) =>
                nameProperty != nameof(Department.Specialties) &&
                nameProperty != nameof(Department.Teachers),
                (Action)(() => _instance = null)
            };
        }
    }
}