using System.Windows;

using View.Implementations.ResourceService;
using View.Windows;

using ViewModel.Interfaces;

namespace View.Implementations.Proces
{
    public class ReportsWindowProc : WindowProc
    {
        private IPrintService _printDialogService;

        public ReportsWindowProc(IDbContextBuilder dbContextCreator,
            IWindowResourceService windowResourceService, IMessageService messageService,
            IPrintService printDialogService) :
            base(dbContextCreator, windowResourceService, messageService) =>
            _printDialogService = printDialogService;

        protected override Window CreateWindow(IDbContextBuilder dbContextCreator,
            IWindowResourceService windowResourceService, IMessageService messageService) =>
            new ReportsWindow(dbContextCreator, windowResourceService, messageService,
                _printDialogService);
    }
}