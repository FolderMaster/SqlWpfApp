using System.Windows;

using View.Implementations.ResourceService;
using View.Windows.DbSet.Dependent;

using ViewModel.Interfaces;

namespace View.Implementations.Proces.DbSet.Dependent
{
    public class GradeStatementsWindowProc : WindowProc
    {
        public GradeStatementsWindowProc(IDbContextBuilder dbContextCreator,
            IWindowResourceService windowResourceService, IMessageService messageService) :
            base(dbContextCreator, windowResourceService, messageService) { }

        protected override Window CreateWindow(IDbContextBuilder dbContextCreator,
            IWindowResourceService windowResourceService, IMessageService messageService) =>
            new GradeStatementsWindow(dbContextCreator, windowResourceService, messageService);
    }
}