using System;
using System.Collections.Generic;

using Model.Independent;
using ViewModel.VMs.DbSet;
using View.Implementations.MessageBoxes;
using View.Services;
using ViewModel.Interfaces;

namespace View.Windows.DbSet.Independent
{
    public class PositionsWindow : GridDbSetWindow
    {
        private static PositionsWindow? _instance = null;

        private static Action _call = () =>
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

        public static Action Call => _call;

        public static IDataBaseContextCreator? DataBaseContextCreator { get; set; }

        private PositionsWindow() : base()
        {
            Title = AppResourceService.GetHeader(nameof(Position));
            Icon = AppResourceService.GetIcon(nameof(Position));

            DataContext = new List<object>()
            {
                new DbSetVM<Position>(DataBaseContextCreator, new ErrorMessageBoxService()),
                (string nameProperty) => nameProperty != nameof(Position.Teachers),
                (Action)(() => _instance = null)
            };
        }
    }
}