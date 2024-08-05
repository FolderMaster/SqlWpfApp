using System.Collections.Generic;
using System.Windows;

using View.Implementations.ResourceService;
using View.Windows;

using ViewModel.Interfaces;
using ViewModel.Interfaces.DataBase;
using ViewModel.Interfaces.Services;

namespace View.Implementations.Proces.Windows
{
    public class ConnectionWindowProc : WindowProc
    {
        private readonly IEnumerable<IDbConnection> _connections;

        public ConnectionWindowProc(ISession session, IEnumerable<IDbConnection> connections,
            IWindowResourceService windowResourceService, IMessageService messageService) :
            base("Connection", session, windowResourceService, messageService)
        {
            _connections = connections;
        }

        protected override Window CreateWindow(ISession session,
            IWindowResourceService windowResourceService, IMessageService messageService) =>
            new ConnectionWindow(session, _connections, windowResourceService, messageService);
    }
}
