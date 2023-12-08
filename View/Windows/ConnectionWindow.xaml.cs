using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ViewModel.Interfaces;
using ViewModel.Interfaces.DbContext;
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
