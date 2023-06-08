using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;

using SQLiteWpfApp.Models.Independent;
using SQLiteWpfApp.ViewModels.VMs.DbSet;
using SQLiteWpfApp.Views.MessageBoxes;

namespace SQLiteWpfApp.Views.Windows.DbSet.Independent
{
    public class DepartmentsWindow : GridDbSetWindow
    {
        private static DepartmentsWindow? _instance = null;

        private static Action _action = () =>
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

        public static Action Action => _action;

        private DepartmentsWindow() : base()
        {
            Title = Application.Current.Resources[nameof(Department) + "sHeader"] as string;
            Icon = Application.Current.Resources[nameof(Department) + "sIcon"] as BitmapSource;

            DataContext = new List<object>()
            {
                new DbSetVM<Department>(new ErrorMessageBoxService()),
                (string nameProperty) => nameProperty != nameof(Department.Specialties) &&
                nameProperty != nameof(Department.Teachers),
                (Action)(() => _instance = null)
            };
        }
    }
}