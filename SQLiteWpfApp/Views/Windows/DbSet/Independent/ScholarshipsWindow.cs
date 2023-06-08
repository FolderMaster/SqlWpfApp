using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;

using SQLiteWpfApp.Models.Independent;
using SQLiteWpfApp.ViewModels.VMs.DbSet;
using SQLiteWpfApp.Views.MessageBoxes;

namespace SQLiteWpfApp.Views.Windows.DbSet.Independent
{
    public class ScholarshipsWindow : GridDbSetWindow
    {
        private static ScholarshipsWindow? _instance = null;

        private static Action _action = () =>
            {
                var instance = Instance;
                instance.Show();
            };

        public static ScholarshipsWindow Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ScholarshipsWindow();
                }
                return _instance;
            }
        }

        public static Action ActionService => _action;

        private ScholarshipsWindow() : base()
        {
            Title = Application.Current.Resources[nameof(Scholarship) + "sHeader"] as string;
            Icon = Application.Current.Resources[nameof(Scholarship) + "sIcon"] as BitmapSource;

            DataContext = new List<object>()
            {
                new DbSetVM<Scholarship>(new ErrorMessageBoxService()),
                (string nameProperty) => nameProperty != nameof(Scholarship.Students),
                (Action)(() => _instance = null)
            };
        }
    }
}