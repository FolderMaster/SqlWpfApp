using System.Collections.Generic;
using System.Windows;

using ViewModel.Interfaces;
using ViewModel.VMs.Request;

namespace View.Windows
{
    public partial class RequestsWindow : Window
    {
        public RequestsWindow(IDbContextBuilder dbContextCreator, IResourceService resourceService,
            IMessageService messageService)
        {
            InitializeComponent();

            DataContext = new List<object>()
            {
                new ChangeDataRequestsVM(dbContextCreator, resourceService, messageService),
                new SpecialChangeDataRequestsVM(dbContextCreator, resourceService, messageService),
                new ViewDataRequestsVM(dbContextCreator, resourceService, messageService),
                new SpecialViewDataRequestsVM(dbContextCreator, resourceService, messageService)
            };
        }
    }
}