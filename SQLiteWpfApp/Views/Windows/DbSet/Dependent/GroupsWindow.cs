using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;

using SQLiteWpfApp.Models.Dependent;
using SQLiteWpfApp.ViewModels.VMs;
using SQLiteWpfApp.Views.MessageBoxes;

namespace SQLiteWpfApp.Views.Windows.DbSet.Dependent
{
    public class GroupsWindow : TwoGridDbSetWindow
    {
        private static GroupsWindow? _instance = null;

        private static Action _action = () =>
        {
            var instance = Instance;
            instance.Show();
        };

        public static GroupsWindow Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GroupsWindow();
                }
                return _instance;
            }
        }

        public static Action Action => _action;

        private GroupsWindow() : base()
        {
            Title = Application.Current.Resources[nameof(Group) + "sHeader"] as string;
            Icon = Application.Current.Resources[nameof(Group) + "sIcon"] as BitmapSource;

            var messageService = new ErrorMessageBoxService();

            var mainVM = new DbSetVM<Group>(messageService, () => _instance = null);
            var dependentVM = new DbSetVM<Specialty>(messageService);

            mainVM.ItemChanged += (object? sender, EventArgs e) =>
            {
                dependentVM.SelectedItem = mainVM.SelectedItem?.Specialty;
            };

            DataContext = new List<object>()
            {
                mainVM, dependentVM,
                (string nameProperty) => nameProperty != nameof(Group.Students) &&
                nameProperty != nameof(Group.Specialty), (string nameProperty) =>
                nameProperty != nameof(Specialty.Groups) &&
                nameProperty != nameof(Specialty.Department) &&
                nameProperty != nameof(Specialty.Disciplines)
            };
        }
    }
}