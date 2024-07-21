using System.Collections.Generic;
using System.Windows;

using ViewModel.Interfaces;
using ViewModel.Interfaces.DataBase;
using ViewModel.Interfaces.Services;
using ViewModel.Interfaces.Services.Messages;
using ViewModel.VMs.Connections;

namespace View.Windows
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class ConnectionWindow : Window
    {
        public ConnectionWindow(ISession dbContextBuilders,
            IEnumerable<IDbConnection> connections, IResourceService resourceService,
            IMessageService errorMessageService)
        {
            InitializeComponent();

            DataContext = new ConnectionVM(dbContextBuilders, resourceService,
                errorMessageService, connections);
        }
    }
}
