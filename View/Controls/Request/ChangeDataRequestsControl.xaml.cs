using System.Windows.Controls;

using ViewModel.VMs.Request;
using View.MessageBoxes;

namespace View.Controls.Request
{
    public partial class ChangeDataRequestsControl : UserControl
    {
        public ChangeDataRequestsControl()
        {
            InitializeComponent();

            DataContext = new ChangeDataRequestsVM(new ErrorMessageBoxService());
        }
    }
}