using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;

using SQLiteWpfApp.Models.Independent;
using SQLiteWpfApp.ViewModels.VMs.DbSet;
using SQLiteWpfApp.Views.MessageBoxes;

namespace SQLiteWpfApp.Views.Windows.DbSet.Independent
{
    public class GradeModesWindow : GridDbSetWindow
    {
        private static GradeModesWindow? _instance = null;

        private static Action _action = () =>
        {
            var instance = Instance;
            instance.Show();
        };

        public static GradeModesWindow Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GradeModesWindow();
                }
                return _instance;
            }
        }

        public static Action Action => _action;

        private GradeModesWindow() : base()
        {
            Title = Application.Current.Resources[nameof(GradeMode) + "sHeader"] as string;
            Icon = Application.Current.Resources[nameof(GradeMode) + "sIcon"] as BitmapSource;

            DataContext = new List<object>()
            {
                new DbSetVM<GradeMode>(new ErrorMessageBoxService()),
                (string nameProperty) => nameProperty != nameof(GradeMode.Grades) &&
                nameProperty != nameof(GradeMode.StudyForms),
                (Action)(() => _instance = null)
            };
        }
    }
}