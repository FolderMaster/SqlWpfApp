using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Microsoft.Data.SqlClient;

using System.Collections.ObjectModel;
using System.Linq;

using ViewModel.Classes;
using ViewModel.Interfaces;
using ViewModel.Interfaces.DataBase;
using ViewModel.Interfaces.Services;
using ViewModel.Interfaces.Services.Messages;
using ViewModel.Services;

namespace ViewModel.VMs
{
    public partial class ConnectionVM : ObservableObject
    {
        private IConfiguration _configuration;

        private IMessengerService _messengerService;

        private Connection? _selectedConnection;

        [ObservableProperty]
        private Credential? selectedCredential;

        [ObservableProperty]
        private ObservableCollection<Connection> connections = new()
        {
            new Connection() {
                Credentials = new() {
                    new Credential() { User = "dean1", Password = "dean1" },
                    new Credential() { User = "deaneryEmployee1", Password = "deaneryEmployee1" },
                    new Credential() { User = "deaneryEmployee2", Password = "deaneryEmployee2" },
                    new Credential() { User = "teacher1", Password = "teacher1" },
                    new Credential() { User = "teacher2", Password = "teacher2" }
                },
                DataSource = "(localdb)\\mssqllocaldb",
                InitialCatalog = "UniversityDb",
                IsTlsConnection = false,
                IsColumnEncryption = true,
                IsTrustServerCertificate = false
            },
            new Connection() {
                Credentials = new() {
                    new Credential() { User = "dean1", Password = "dean1" },
                    new Credential() { User = "deaneryEmployee1", Password = "deaneryEmployee1" },
                    new Credential() { User = "deaneryEmployee2", Password = "deaneryEmployee2" },
                    new Credential() { User = "teacher1", Password = "teacher1" },
                    new Credential() { User = "teacher2", Password = "teacher2" }
                },
                DataSource = "127.0.0.1",
                InitialCatalog = "UniversityDb",
                IsTlsConnection = true,
                IsColumnEncryption = true,
                IsTrustServerCertificate = true
            }
        };

        [ObservableProperty]
        private string connectionText;

        [ObservableProperty]
        private string credentialText;

        public Connection? SelectedConnection
        {
            get => _selectedConnection;
            set
            {
                if(SetProperty(ref _selectedConnection, value))
                {
                    if(value != null)
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

        public RelayCommand ConnectCommand { get; private set; }

        public RelayCommand ExitCommand { get; private set; }

        public ConnectionVM(IDbContextBuilder dbContextBuilder, IResourceService resourceService,
            IConfiguration configuration, IMessageService errorMessageService)
        {
            _configuration = configuration;
            _messengerService = new MessengerService(resourceService, errorMessageService);

            _messengerService.ExecuteWithExceptionMessage(_configuration.Load, () => { });
            if(_configuration.Connections != null)
            {
                Connections.Clear();
                foreach (var connection in _configuration.Connections)
                {
                    Connections.Add(connection);
                }
            }
            _configuration.Connections = Connections;
            SelectedConnection = Connections.Count > 0 ? Connections[0] : null;
            if(SelectedConnection != null)
            {
                SelectedCredential = SelectedConnection.Credentials.Count > 0 ?
                    SelectedConnection.Credentials[0] : null;
            }

            AddConnectionCommand = new RelayCommand(() => {
                var newConnection = new Connection()
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
                        Connections[selectedIndex] : Connections[Connections.Count - 1];
                }
                else
                {
                    SelectedConnection = null;
                }
            });

            AddCredentialCommand = new RelayCommand(() =>
            {
                var newAuthorization = new Credential()
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
                        authorizations[selectedIndex] : authorizations[authorizations.Count - 1];
                }
                else
                {
                    SelectedCredential = null;
                }
            });

            ConnectCommand = new RelayCommand(() =>
                _messengerService.ExecuteWithExceptionMessage(() =>
                {
                    var builder = new SqlConnectionStringBuilder()
                    {
                        DataSource = SelectedConnection?.DataSource,
                        InitialCatalog = SelectedConnection?.InitialCatalog,
                        UserID = SelectedCredential?.User,
                        Password = SelectedCredential?.Password,
                        Encrypt = SelectedConnection?.IsTlsConnection,
                        ColumnEncryptionSetting = SelectedConnection?.IsColumnEncryption == true ?
                            SqlConnectionColumnEncryptionSetting.Enabled :
                            SqlConnectionColumnEncryptionSetting.Disabled,
                        TrustServerCertificate = SelectedConnection?.IsTrustServerCertificate == true
                    };
                    dbContextBuilder.Create(builder.ConnectionString);
                }));
        }
    }
}
