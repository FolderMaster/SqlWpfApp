using System;
using System.Collections.Generic;

using Model.Dependent;
using ViewModel.VMs.DbSet;
using View.Implementations.MessageBoxes;
using View.Services;
using ViewModel.Interfaces;

namespace View.Windows.DbSet.Dependent
{
    public class GroupsWindow : TwoGridDbSetWindow
    {
        private static GroupsWindow? _instance = null;

        private static Action _call = () =>
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

        public static IDataBaseContextCreator? DataBaseContextCreator { get; set; }

        public static Action Call => _call;

        private GroupsWindow() : base()
        {
            Title = AppResourceService.GetHeader(nameof(Group));
            Icon = AppResourceService.GetIcon(nameof(Group));

            var messageService = new ErrorMessageBoxService();

            var mainVM = new DbSetVM<Group>(DataBaseContextCreator, messageService);
            var dependentVM = new DbSetVM<Specialty>(DataBaseContextCreator, messageService);

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
                nameProperty != nameof(Specialty.Disciplines),
                (Action)(() => _instance = null)
            };
        }
    }
}