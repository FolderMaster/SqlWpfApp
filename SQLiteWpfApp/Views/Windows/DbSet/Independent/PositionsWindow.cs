using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;

using SQLiteWpfApp.Models.Independent;
using SQLiteWpfApp.ViewModels.VMs.DbSet;
using SQLiteWpfApp.Views.MessageBoxes;

namespace SQLiteWpfApp.Views.Windows.DbSet.Independent
{
    public class PositionsWindow : GridDbSetWindow
    {
        private static PositionsWindow? _instance = null;

        private static Action _actionService = () =>
            {
                var instance = Instance;
                instance.Show();
            };

        public static PositionsWindow Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PositionsWindow();
                }
                return _instance;
            }
        }

        public static Action ActionService => _actionService;

        private PositionsWindow() : base()
        {
            Title = Application.Current.Resources[nameof(Position) + "sHeader"] as string;
            Icon = Application.Current.Resources[nameof(Position) + "sIcon"] as BitmapSource;

            DataContext = new List<object>()
            {
                new DbSetVM<Position>(new ErrorMessageBoxService()),
                (string nameProperty) => nameProperty != nameof(Position.Teachers),
                (Action)(() => _instance = null)
            };
        }
    }
}