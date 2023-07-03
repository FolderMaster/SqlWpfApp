using System.Collections.Generic;
using System.Windows;

using ViewModel.Interfaces;
using ViewModel.VMs.Request;

namespace View.Windows
{
    public partial class RequestsWindow : Window
    {
        public RequestsWindow(IDbContextCreator dbContextCreator, IMessageService messageService)
        {
            InitializeComponent();

            DataContext = new List<object>()
            {
                new ChangeDataRequestsVM(dbContextCreator, messageService),
                new SpecialChangeDataRequestsVM(dbContextCreator, messageService),
                new ViewDataRequestsVM(dbContextCreator, messageService),
                new SpecialViewDataRequestsVM(dbContextCreator, messageService)
            };
        }
    }
}