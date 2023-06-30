using System;
using System.Collections.Generic;

using Model.Dependent;
using Model.Independent;
using ViewModel.VMs.DbSet;
using View.Implementations.MessageBoxes;
using View.Services;
using ViewModel.Interfaces;

namespace View.Windows.DbSet.Dependent
{
    public class SpecialtiesWindow : TwoGridDbSetWindow
    {
        private static SpecialtiesWindow? _instance = null;

        private static Action _call = () =>
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

        public static Action Call => _call;

        public static IDataBaseContextCreator? DataBaseContextCreator { get; set; }

        private SpecialtiesWindow() : base()
        {
            Title = AppResourceService.GetHeader("Specialtie");
            Icon = AppResourceService.GetIcon("Specialtie");

            var messageService = new ErrorMessageBoxService();

            var mainVM = new DbSetVM<Specialty>(DataBaseContextCreator, messageService);
            var dependentVM = new DbSetVM<Department>(DataBaseContextCreator, messageService);

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