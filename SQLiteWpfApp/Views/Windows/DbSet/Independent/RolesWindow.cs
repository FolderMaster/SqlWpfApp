using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;

using SQLiteWpfApp.Models.Independent;
using SQLiteWpfApp.ViewModels.VMs.DbSet;
using SQLiteWpfApp.Views.MessageBoxes;

namespace SQLiteWpfApp.Views.Windows.DbSet.Independent
{
    public class RolesWindow : GridDbSetWindow
    {
        private static RolesWindow? _instance = null;

        private static Action _action = () =>
            {
                var instance = Instance;
                instance.Show();
            };

        public static RolesWindow Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RolesWindow();
                }
                return _instance;
            }
        }

        public static Action Action => _action;

        private RolesWindow() : base()
        {
            Title = Application.Current.Resources[nameof(Role) + "sHeader"] as string;
            Icon = Application.Current.Resources[nameof(Role) + "sIcon"] as BitmapSource;

            DataContext = new List<object>()
            {
                new DbSetVM<Role>(new ErrorMessageBoxService()),
                (string nameProperty) => nameProperty != nameof(Role.Connections),
                (Action)(() => _instance = null)
            };
        }
    }
}