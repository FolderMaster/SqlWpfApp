using System.Windows;

using View.Windows;

using ViewModel.Interfaces;

namespace View.Implementations.Proces
{
    public class RequestsWindowProc : WindowProc
    {
        public RequestsWindowProc(IDbContextCreator dbContextCreator,
            IMessageService messageService) : base(dbContextCreator, messageService) { }

        protected override Window CreateWindow(IDbContextCreator dbContextCreator,
            IMessageService messageService) =>
            new RequestsWindow(dbContextCreator, messageService);
    }
}