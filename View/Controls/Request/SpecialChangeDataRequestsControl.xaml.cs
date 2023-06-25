using System.Windows.Controls;

using ViewModel.VMs.Request;
using View.MessageBoxes;

namespace View.Controls.Request
{
    public partial class SpecialChangeDataRequestsControl : UserControl
    {
        public SpecialChangeDataRequestsControl()
        {
            InitializeComponent();

            DataContext = new SpecialChangeDataRequestsVM(new ErrorMessageBoxService());
        }
    }
}