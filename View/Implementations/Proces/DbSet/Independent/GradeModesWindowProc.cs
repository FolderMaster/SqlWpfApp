using System.Collections.Generic;
using System.Windows;

using View.Services;
using View.Windows.DbSet.Independent;

using ViewModel.Interfaces;
using ViewModel.VMs.DbSet;

using Model.Independent;

namespace View.Implementations.Proces.DbSet.Independent
{
    public class GradeModesWindowProc : WindowProc
    {
        public GradeModesWindowProc(IDbContextCreator dbContextCreator,
            IMessageService messageService) : base(dbContextCreator, messageService) { }

        protected override Window CreateWindow(IDbContextCreator dbContextCreator,
            IMessageService messageService) => new GridDbSetWindow()
            {
                Title = AppResourceService.GetHeader(nameof(GradeMode) + "s"),
                Icon = AppResourceService.GetIcon(nameof(GradeMode) + "s"),
                DataContext = new List<object>()
                {
                    new DbSetVM<GradeMode>(dbContextCreator, messageService),
                    (string nameProperty) => nameProperty != nameof(GradeMode.Grades) &&
                    nameProperty != nameof(GradeMode.StudyForms)
                }
            };
    }
}
