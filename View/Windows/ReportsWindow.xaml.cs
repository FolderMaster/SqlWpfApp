using System.Windows;

using ViewModel.VMs.Request;
using ViewModel.Interfaces;

namespace View.Windows
{
    public partial class ReportsWindow : Window
    {
        public ReportsWindow(IDbContextCreator dbContextCreator, IMessageService messageService,
            IPrintDialogService printDialogService)
        {
            InitializeComponent();

            DataContext = new ReportsVM(dbContextCreator, messageService, printDialogService);
        }
    }
}