using System.Windows;

using View.Windows;

using ViewModel.Interfaces;

namespace View.Implementations.Proces
{
    public class ReportsWindowProc : WindowProc
    {
        private IPrintDialogService _printDialogService;

        public ReportsWindowProc(IDbContextCreator dbContextCreator,
            IMessageService messageService, IPrintDialogService printDialogService) :
            base(dbContextCreator, messageService) => _printDialogService = printDialogService;

        protected override Window CreateWindow(IDbContextCreator dbContextCreator,
            IMessageService messageService) =>
            new ReportsWindow(dbContextCreator, messageService, _printDialogService);
    }
}