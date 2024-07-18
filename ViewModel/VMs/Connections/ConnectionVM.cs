using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Linq;

using ViewModel.Interfaces;
using ViewModel.Interfaces.DataBase;
using ViewModel.Interfaces.Services;
using ViewModel.Interfaces.Services.Messages;
using ViewModel.Services;

namespace ViewModel.VMs.Connections
{
    public partial class ConnectionVM : ObservableObject
    {
        private MessengerService _messengerService;

        [ObservableProperty]
        private IDbConnection? selectedConnection;

        public IEnumerable<IDbConnection> Connections { get; private set; }

        public RelayCommand ConnectCommand { get; private set; }

        public ConnectionVM(ISession session, IResourceService resourceService,
            IConfiguration configuration, IMessageService errorMessageService, IEnumerable<IDbConnection> connections)
        {
            _messengerService = new MessengerService(resourceService, errorMessageService);
            Connections = connections;
            SelectedConnection = Connections.Any() ? connections.First() : null;

            ConnectCommand = new RelayCommand(() =>
                _messengerService.ExecuteWithExceptionMessage(() =>
                {
                    session.DbContext = SelectedConnection?.Connect();
                }), () => SelectedConnection?.CanConnect == true);
        }
    }
}
