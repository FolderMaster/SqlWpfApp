using System;
using System.Collections.Generic;

using Model.Independent;
using ViewModel.VMs.DbSet;
using View.Implementations.MessageBoxes;
using View.Services;
using ViewModel.Interfaces;

namespace View.Windows.DbSet.Independent
{
    public class RolesWindow : GridDbSetWindow
    {
        private static RolesWindow? _instance = null;

        private static Action _call = () =>
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

        public static Action Call => _call;

        public static IDataBaseContextCreator? DataBaseContextCreator { get; set; }

        private RolesWindow() : base()
        {
            Title = AppResourceService.GetHeader(nameof(Role));
            Icon = AppResourceService.GetIcon(nameof(Role));

            DataContext = new List<object>()
            {
                new DbSetVM<Role>(DataBaseContextCreator, new ErrorMessageBoxService()),
                (string nameProperty) => nameProperty != nameof(Role.Connections),
                (Action)(() => _instance = null)
            };
        }
    }
}