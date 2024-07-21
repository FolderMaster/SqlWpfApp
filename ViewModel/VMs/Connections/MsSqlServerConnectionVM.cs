using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using ViewModel.Classes.Connections.MsSqlServer;

namespace ViewModel.VMs.Connections
{
    public partial class MsSqlServerConnectionVM : ObservableObject
    {
        private IList<MsSqlServerConnectionDataSet>? _connectionsData =
            new ObservableCollection<MsSqlServerConnectionDataSet>();

        private MsSqlServerConnectionDataSet? _selectedConnectionData;

        [ObservableProperty]
        private MsSqlServerCredentialData? selectedCredential;

        [ObservableProperty]
        private MsSqlServerConnectionData? selectedConnection;

        [ObservableProperty]
        private string connectionText;

        [ObservableProperty]
        private string credentialText;

        public IList<MsSqlServerConnectionDataSet>? ConnectionsData
        {
            get => _connectionsData;
            set
            {
                if (SetProperty(ref _connectionsData, value))
                {
                    SelectedConnectionData = value != null && value.Any() ? value[0] : null;
                }
            }
        }

        public MsSqlServerConnectionDataSet? SelectedConnectionData
        {
            get => _selectedConnectionData;
            set
            {
                if (SetProperty(ref _selectedConnectionData, value))
                {
                    if (value != null)
                    {
                        SelectedConnection = value.Connection;
                        SelectedCredential = value.Credentials.Any() ?
                            value.Credentials[0] : null;
                    }
                    else
                    {
                        SelectedConnection = null;
                        SelectedCredential = null;
                    }
                }
            }
        }

        public RelayCommand AddConnectionCommand { get; private set; }

        public RelayCommand RemoveSelectedConnectionCommand { get; private set; }

        public RelayCommand RemoveSelectedCredentialCommand { get; private set; }

        public RelayCommand AddCredentialCommand { get; private set; }

        public MsSqlServerConnectionVM()
        {
            AddConnectionCommand = new RelayCommand(() => {
                var newConnectionData = new MsSqlServerConnectionDataSet()
                {
                    Connection = new MsSqlServerConnectionData()
                    {
                        DataSource = ConnectionText
                    }
                };
                ConnectionsData.Add(newConnectionData);
                SelectedConnectionData = newConnectionData;
            });
            RemoveSelectedConnectionCommand = new RelayCommand(() =>
            {
                int selectedIndex = ConnectionsData.IndexOf(SelectedConnectionData);
                ConnectionsData.Remove(SelectedConnectionData);
                if (ConnectionsData.Count > 0)
                {
                    SelectedConnectionData = selectedIndex < ConnectionsData.Count ?
                        ConnectionsData[selectedIndex] : ConnectionsData[^1];
                }
                else
                {
                    SelectedConnectionData = null;
                }
            });

            AddCredentialCommand = new RelayCommand(() =>
            {
                var newAuthorization = new MsSqlServerCredentialData()
                {
                    User = CredentialText
                };
                SelectedConnectionData.Credentials.Add(newAuthorization);
                SelectedCredential = newAuthorization;
            });
            RemoveSelectedCredentialCommand = new RelayCommand(() =>
            {
                var authorizations = SelectedConnectionData.Credentials;
                int selectedIndex = authorizations.IndexOf(SelectedCredential);
                authorizations.Remove(SelectedCredential);
                if (authorizations.Count > 0)
                {
                    SelectedCredential = selectedIndex < authorizations.Count ?
                        authorizations[selectedIndex] : authorizations[^1];
                }
                else
                {
                    SelectedCredential = null;
                }
            });
        }
    }
}
