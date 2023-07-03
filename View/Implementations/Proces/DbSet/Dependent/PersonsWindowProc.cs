using System;
using System.Collections.Generic;
using System.Windows;

using View.Services;
using View.Windows.DbSet.Dependent;

using ViewModel.Interfaces;
using ViewModel.VMs.DbSet;

using Model.Dependent;
using Model.Independent;

namespace View.Implementations.Proces.DbSet.Dependent
{
    public class PersonsWindowProc : WindowProc
    {
        public PersonsWindowProc(IDbContextCreator dbContextCreator,
            IMessageService messageService) : base(dbContextCreator, messageService) { }

        protected override Window CreateWindow(IDbContextCreator dbContextCreator,
            IMessageService messageService)
        {
            var mainVM = new DbSetVM<Person>(dbContextCreator, messageService);
            var dependentVM = new DbSetVM<Passport>(dbContextCreator, messageService);

            mainVM.ItemChanged += (object? sender, EventArgs e) =>
            {
                dependentVM.SelectedItem = mainVM.SelectedItem?.Passport;
            };

            return new TwoGridDbSetWindow()
            {
                Title = AppResourceService.GetHeader(nameof(Person) + "s"),
                Icon = AppResourceService.GetIcon(nameof(Person) + "s"),
                DataContext = new List<object>()
                {
                    mainVM, dependentVM,
                    (string nameProperty) => nameProperty != nameof(Person.Passport) &&
                    nameProperty != nameof(Person.Teachers) &&
                    nameProperty != nameof(Person.Students),
                    (string nameProperty) => nameProperty != nameof(Passport.Persons) &&
                    nameProperty != nameof(Passport.Scan)
                }
            };
        }
    }
}