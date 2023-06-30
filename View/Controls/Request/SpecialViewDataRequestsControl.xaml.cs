using System.Windows.Controls;

using ViewModel.VMs;
using View.Implementations.MessageBoxes;
using ViewModel.Interfaces;

namespace View.Controls.Request
{
    public partial class SpecialViewDataRequestsControl : UserControl
    {
        public static IDataBaseContextCreator DataBaseContextCreator { get; set; }

        public SpecialViewDataRequestsControl()
        {
            InitializeComponent();

            DataContext = new SpecialViewDataRequestsVM(DataBaseContextCreator,
                new ErrorMessageBoxService());
        }
    }
}