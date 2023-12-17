using System.Windows;

using View.Implementations.ResourceService;
using View.Windows;

using ViewModel.Interfaces;
using ViewModel.Interfaces.DataBase;
using ViewModel.Interfaces.Services.Messages;

namespace View.Implementations.Proces
{
    public class ConnectionWindowProc : WindowProc
    {
        private IConfiguration _configuration;

        public ConnectionWindowProc(IDbContextBuilder dbContextBuilder, IConfiguration configuration,
            IWindowResourceService windowResourceService, IMessageService messageService) :
            base(dbContextBuilder, windowResourceService, messageService) => _configuration = configuration;

        protected override Window CreateWindow(IDbContextBuilder dbContextBuilder,
            IWindowResourceService windowResourceService, IMessageService messageService) =>
            new ConnectionWindow(dbContextBuilder, windowResourceService, _configuration, messageService);
    }
}
