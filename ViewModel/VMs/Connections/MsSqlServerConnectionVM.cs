using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using System.Collections.ObjectModel;
using System.Linq;

using ViewModel.Classes.Connections.MsSqlServer;
using ViewModel.Interfaces;
using ViewModel.Interfaces.Services;
using ViewModel.Interfaces.Services.Messages;

namespace ViewModel.VMs.Connections
{
    public partial class MsSqlServerConnectionVM : ObservableObject
    {
        private MsSqlServerConnection? _selectedConnection;

        [ObservableProperty]
        private MsSqlServerCredential? selectedCredential;

        [ObservableProperty]
        private ObservableCollection<MsSqlServerConnection> connections;

        [ObservableProperty]
        private string connectionText;

        [ObservableProperty]
        private string credentialText;

        public MsSqlServerConnection? SelectedConnection
        {
            get => _selectedConnection;
            set
            {
                if (SetProperty(ref _selectedConnection, value))
                {
                    if (value != null)
                    {
                        if (value.Credentials.Any())
                        {
                            SelectedCredential = value.Credentials[0];
                        }
                    }
                }
            }
        }

        public RelayCommand AddConnectionCommand { get; private set; }

        public RelayCommand RemoveSelectedConnectionCommand { get; private set; }

        public RelayCommand RemoveSelectedCredentialCommand { get; private set; }

        public RelayCommand AddCredentialCommand { get; private set; }

        public MsSqlServerConnectionVM(ISession dbContextBuilder,
            IResourceService resourceService, IConfiguration configuration,
            IMessageService errorMessageService)
        {
            SelectedConnection = Connections.Count > 0 ? Connections[0] : null;
            if (SelectedConnection != null)
            {
                SelectedCredential = SelectedConnection.Credentials.Count > 0 ?
                    SelectedConnection.Credentials[0] : null;
            }

            AddConnectionCommand = new RelayCommand(() => {
                var newConnection = new MsSqlServerConnection()
                {
                    DataSource = ConnectionText
                };
                Connections.Add(newConnection);
                SelectedConnection = newConnection;
            });
            RemoveSelectedConnectionCommand = new RelayCommand(() =>
            {
                int selectedIndex = Connections.IndexOf(SelectedConnection);
                Connections.Remove(SelectedConnection);
                if (Connections.Count > 0)
                {
                    SelectedConnection = selectedIndex < Connections.Count ?
                        Connections[selectedIndex] : Connections[^1];
                }
                else
                {
                    SelectedConnection = null;
                }
            });

            AddCredentialCommand = new RelayCommand(() =>
            {
                var newAuthorization = new MsSqlServerCredential()
                {
                    User = CredentialText
                };
                SelectedConnection.Credentials.Add(newAuthorization);
                SelectedCredential = newAuthorization;
            });
            RemoveSelectedCredentialCommand = new RelayCommand(() =>
            {
                var authorizations = SelectedConnection.Credentials;
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
