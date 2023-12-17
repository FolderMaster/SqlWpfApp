using System.Windows;

using View.Implementations.ResourceService;
using View.Windows;

using ViewModel.Interfaces.DataBase;
using ViewModel.Interfaces.Services.Messages;

namespace View.Implementations.Proces
{
    public class MainWindowProc : WindowProc
    {
        public MainWindowProc(IDbContextBuilder dbContextBuilder,
            IWindowResourceService windowResourceService, IMessageService messageService) :
            base(dbContextBuilder, windowResourceService, messageService) { }

        protected override Window CreateWindow(IDbContextBuilder dbContextBuilder,
            IWindowResourceService windowResourceService, IMessageService messageService) =>
            new MainWindow();
    }
}
