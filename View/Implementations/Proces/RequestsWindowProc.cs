using System.Windows;

using View.Implementations.ResourceService;
using View.Windows;

using ViewModel.Interfaces;

namespace View.Implementations.Proces
{
    public class RequestsWindowProc : WindowProc
    {
        public RequestsWindowProc(IDbContextBuilder dbContextCreator,
            IWindowResourceService windowResourceService, IMessageService messageService) :
            base(dbContextCreator, windowResourceService, messageService) { }

        protected override Window CreateWindow(IDbContextBuilder dbContextCreator,
            IWindowResourceService windowResourceService, IMessageService messageService) =>
            new RequestsWindow(dbContextCreator, windowResourceService, messageService);
    }
}