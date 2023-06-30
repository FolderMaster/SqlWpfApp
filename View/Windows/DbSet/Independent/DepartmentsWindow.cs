using System;
using System.Collections.Generic;

using Model.Independent;
using ViewModel.VMs.DbSet;
using View.Implementations.MessageBoxes;
using View.Services;
using ViewModel.Interfaces;

namespace View.Windows.DbSet.Independent
{
    public class DepartmentsWindow : GridDbSetWindow
    {
        private static DepartmentsWindow? _instance = null;

        private static Action _call = () =>
        {
            var instance = Instance;
            instance.Show();
        };

        public static DepartmentsWindow Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DepartmentsWindow();
                }
                return _instance;
            }
        }

        public static Action Call => _call;

        public static IDataBaseContextCreator? DataBaseContextCreator { get; set; }

        private DepartmentsWindow() : base()
        {
            Title = AppResourceService.GetHeader(nameof(Department));
            Icon = AppResourceService.GetIcon(nameof(Department));

            DataContext = new List<object>()
            {
                new DbSetVM<Department>(DataBaseContextCreator, new ErrorMessageBoxService()),
                (string nameProperty) => nameProperty != nameof(Department.Specialties) &&
                nameProperty != nameof(Department.Teachers),
                (Action)(() => _instance = null)
            };
        }
    }
}