using System;
using System.Windows;

using ViewModel.Interfaces;
using ViewModel.Interfaces.DataBase;
using ViewModel.Interfaces.Services;
using ViewModel.Interfaces.Services.Messages;
using ViewModel.VMs;

namespace View.Windows
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class ConnectionWindow : Window
    {
        public ConnectionWindow(IDbContextBuilder dbContextBuilder, IResourceService resourceService,
            IConfiguration configuration, IMessageService errorMessageService)
        {
            InitializeComponent();

            DataContext = new ConnectionVM(dbContextBuilder, resourceService, configuration,
                errorMessageService);
        }
    }
}
