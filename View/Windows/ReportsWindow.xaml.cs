using System.Windows;

using ViewModel.VMs.Request;
using ViewModel.Interfaces;

namespace View.Windows
{
    public partial class ReportsWindow : Window
    {
        public ReportsWindow(IDbContextBuilder dbContextCreator, IResourceService resourceService,
            IMessageService messageService, IPrintService printDialogService)
        {
            InitializeComponent();

            DataContext = new ReportsVM(dbContextCreator, resourceService, messageService,
                printDialogService);
        }
    }
}