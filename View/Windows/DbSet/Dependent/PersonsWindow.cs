using System;
using System.Collections.Generic;

using Model.Dependent;
using Model.Independent;
using ViewModel.VMs.DbSet;
using View.Implementations.MessageBoxes;
using View.Services;
using ViewModel.Interfaces;

namespace View.Windows.DbSet.Dependent
{
    public class PersonsWindow : TwoGridDbSetWindow
    {
        private static PersonsWindow? _instance = null;

        private static Action _call = () =>
        {
            var instance = Instance;
            instance.Show();
        };

        public static PersonsWindow Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PersonsWindow();
                }
                return _instance;
            }
        }

        public static Action Call => _call;

        public static IDataBaseContextCreator? DataBaseContextCreator { get; set; }

        private PersonsWindow() : base()
        {
            Title = AppResourceService.GetHeader(nameof(Person));
            Icon = AppResourceService.GetIcon(nameof(Person));

            var messageService = new ErrorMessageBoxService();

            var mainVM = new DbSetVM<Person>(DataBaseContextCreator, messageService);
            var dependentVM = new DbSetVM<Passport>(DataBaseContextCreator, messageService);

            mainVM.ItemChanged += (object? sender, EventArgs e) =>
            {
                dependentVM.SelectedItem = mainVM.SelectedItem?.Passport;
            };

            DataContext = new List<object>()
            {
                mainVM, dependentVM,
                (string nameProperty) => nameProperty != nameof(Person.Passport) &&
                nameProperty != nameof(Person.Teachers) && nameProperty != nameof(Person.Students),
                (string nameProperty) => nameProperty != nameof(Passport.Persons) &&
                nameProperty != nameof(Passport.Scan),
                (Action)(() => _instance = null)
            };
        }
    }
}