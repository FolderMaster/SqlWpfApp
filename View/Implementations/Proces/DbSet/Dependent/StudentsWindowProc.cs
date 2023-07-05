using System.Windows;

using View.Implementations.ResourceService;
using View.Windows.DbSet.Dependent;

using ViewModel.Interfaces;

namespace View.Implementations.Proces.DbSet.Dependent
{
    public class StudentsWindowProc : WindowProc
    {
        public StudentsWindowProc(IDbContextBuilder dbContextCreator,
            IWindowResourceService windowResourceService, IMessageService messageService) :
            base(dbContextCreator, windowResourceService, messageService) { }

        protected override Window CreateWindow(IDbContextBuilder dbContextCreator,
            IWindowResourceService windowResourceService, IMessageService messageService) =>
            new StudentsWindow(dbContextCreator, windowResourceService, messageService);
    }
}