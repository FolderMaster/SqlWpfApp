using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;

using Model.Dependent;
using Model.Independent;
using ViewModel.VMs.DbSet;
using View.MessageBoxes;

namespace View.Windows.DbSet.Dependent
{
    public class PersonsWindow : TwoGridDbSetWindow
    {
        private static PersonsWindow? _instance = null;

        private static Action _action = () =>
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

        public static Action Action => _action;

        private PersonsWindow() : base()
        {
            Title = Application.Current.Resources[nameof(Person) + "sHeader"] as string;
            Icon = Application.Current.Resources[nameof(Person) + "sIcon"] as BitmapSource;

            var messageService = new ErrorMessageBoxService();

            var mainVM = new DbSetVM<Person>(messageService);
            var dependentVM = new DbSetVM<Passport>(messageService);

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