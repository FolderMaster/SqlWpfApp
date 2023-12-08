using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Classes;
using ViewModel.Interfaces;
using ViewModel.Interfaces.DbContext;
using ViewModel.Interfaces.Services;
using ViewModel.Interfaces.Services.Messages;
using ViewModel.Services;

namespace ViewModel.VMs
{
    public partial class ConnectionVM : ObservableObject
    {
        private IConfiguration _configuration;

        private MessengerService _messengerService;

        [ObservableProperty]
        private ObservableCollection<Connection> connections = new()
        {
            new Connection() {
                Authorizations = new() {
                    new Authorization() { User = "dbo", Password = "" },
                    new Authorization() { User = "teacher1", Password = "1056" },
                    new Authorization() { User = "deaneryEmployee1", Password = "1056" }
                },
                Server = "(localdb)\\mssqllocaldb",
                DataBase = "UniversityDb"
            }
        };

        [ObservableProperty]
        private Connection? selectedConnection;

        [ObservableProperty]
        private Authorization? selectedAuthorization;

        public RelayCommand RemoveSelectedAuthorizationCommand { get; private set; }

        public RelayCommand AddAuthorizationCommand { get; private set; }

        public RelayCommand ConnectCommand { get; private set; }

        public RelayCommand ExitCommand { get; private set; }

        public ConnectionVM(IDbContextBuilder dbContextBuilder, IResourceService resourceService,
            IConfiguration configuration, IMessageService errorMessageService)
        {
            _configuration = configuration;
            _messengerService = new MessengerService(resourceService, errorMessageService);

            SelectedConnection = Connections.Count > 0 ? Connections[0] : null;
            if(SelectedConnection != null)
            {
                SelectedAuthorization = SelectedConnection.Authorizations.Count > 0 ?
                    SelectedConnection.Authorizations[0] : null;
            }

            RemoveSelectedAuthorizationCommand = new RelayCommand(() => { });
            AddAuthorizationCommand = new RelayCommand(() => { });
            ConnectCommand = new RelayCommand(() =>
                _messengerService.ExecuteWithExceptionMessage(() =>
                    dbContextBuilder.Create($"Server={selectedConnection?.Server};" +
                    $"Database={selectedConnection?.DataBase};" +
                    $"User Id={selectedAuthorization?.User};" +
                    $"Password={selectedAuthorization?.Password}")));
        }
    }
}
