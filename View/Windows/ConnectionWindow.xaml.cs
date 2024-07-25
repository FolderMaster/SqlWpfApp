using System.Collections.Generic;
using System.Windows;

using ViewModel.Interfaces;
using ViewModel.Interfaces.DataBase;
using ViewModel.Interfaces.Services;
using ViewModel.VMs.Connections;

namespace View.Windows
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class ConnectionWindow : Window
    {
        public ConnectionWindow(ISession session,
            IEnumerable<IDbConnection> connections, IResourceService resourceService,
            IMessageService errorMessageService)
        {
            InitializeComponent();

            DataContext = new ConnectionVM(session, resourceService,
                errorMessageService, connections);
        }
    }
}
