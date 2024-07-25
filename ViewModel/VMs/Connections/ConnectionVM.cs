using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using System;
using System.Collections.Generic;
using System.Linq;

using ViewModel.Interfaces;
using ViewModel.Interfaces.DataBase;
using ViewModel.Interfaces.Services;
using ViewModel.Services;

namespace ViewModel.VMs.Connections
{
    public partial class ConnectionVM : ObservableObject
    {
        private IDbConnection? _selectedConnection;

        public IDbConnection? SelectedConnection
        {
            get => _selectedConnection;
            set
            {
                var oldValue = _selectedConnection;
                if (SetProperty(ref _selectedConnection, value))
                {
                    ConnectCommand.NotifyCanExecuteChanged();
                    if (oldValue != null)
                    {
                        oldValue.CanConnectChanged -=
                            SelectedConnection_CanConnectChanged;
                    }
                    if(_selectedConnection != null)
                    {
                        _selectedConnection.CanConnectChanged +=
                            SelectedConnection_CanConnectChanged;
                    }
                }
            }
        }

        public IEnumerable<IDbConnection> Connections { get; private set; }

        public RelayCommand ConnectCommand { get; private set; }

        public ConnectionVM(ISession session, IResourceService resourceService,
            IMessageService messageService, IEnumerable<IDbConnection> connections)
        {
            ConnectCommand = new RelayCommand(() => MessengerService.ExecuteWithExceptionMessage
                (resourceService, messageService,
                () => session.DbContext = SelectedConnection?.Connect()),
                () => SelectedConnection?.CanConnect == true);

            Connections = connections;
            SelectedConnection = Connections.Any() ? connections.First() : null;
        }

        private void SelectedConnection_CanConnectChanged(object? sender, EventArgs e) =>
            ConnectCommand.NotifyCanExecuteChanged();
    }
}
