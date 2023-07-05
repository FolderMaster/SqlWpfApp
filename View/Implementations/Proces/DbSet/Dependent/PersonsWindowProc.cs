using System;
using System.Collections.Generic;
using System.Windows;

using View.Windows.DbSet.Dependent;
using View.Implementations.ResourceService;

using ViewModel.Interfaces;
using ViewModel.VMs.DbSet;

using Model.Dependent;
using Model.Independent;

namespace View.Implementations.Proces.DbSet.Dependent
{
    public class PersonsWindowProc : WindowProc
    {
        private static string _keyResource = nameof(Person) + "s";

        public PersonsWindowProc(IDbContextBuilder dbContextCreator,
            IWindowResourceService windowResourceService, IMessageService messageService) :
            base(dbContextCreator, windowResourceService, messageService) { }

        protected override Window CreateWindow(IDbContextBuilder dbContextCreator,
            IWindowResourceService windowResourceService, IMessageService messageService)
        {
            var mainVM = new DbSetVM<Person>(dbContextCreator, windowResourceService,
                messageService);
            var dependentVM = new DbSetVM<Passport>(dbContextCreator, windowResourceService,
                messageService);

            mainVM.ItemChanged += (object? sender, EventArgs e) =>
            {
                dependentVM.SelectedItem = mainVM.SelectedItem?.Passport;
            };

            return new TwoGridDbSetWindow(windowResourceService, _keyResource, _keyResource,
                new List<object>()
                {
                    mainVM, dependentVM,
                    (string nameProperty) => nameProperty != nameof(Person.Passport) &&
                    nameProperty != nameof(Person.Teachers) &&
                    nameProperty != nameof(Person.Students),
                    (string nameProperty) => nameProperty != nameof(Passport.Persons) &&
                    nameProperty != nameof(Passport.Scan)
                });
        }
    }
}