using System.Windows.Controls;

using ViewModel.VMs.Request;
using View.MessageBoxes;

namespace View.Controls.Request
{
    public partial class ViewDataRequestsControl : UserControl
    {
        public ViewDataRequestsControl()
        {
            InitializeComponent();

            DataContext = new ViewDataRequestsVM(new ErrorMessageBoxService());
        }
    }
}