using System.Windows.Controls;

using ViewModel.VMs;
using View.MessageBoxes;

namespace View.Controls.Request
{
    public partial class SpecialViewDataRequestsControl : UserControl
    {
        public SpecialViewDataRequestsControl()
        {
            InitializeComponent();

            DataContext = new SpecialViewDataRequestsVM(new ErrorMessageBoxService());
        }
    }
}