using System.Windows.Controls;

using ViewModel.VMs.Request;
using View.Implementations.MessageBoxes;
using ViewModel.Interfaces;

namespace View.Controls.Request
{
    public partial class ViewDataRequestsControl : UserControl
    {
        public static IDataBaseContextCreator DataBaseContextCreator { get; set; }

        public ViewDataRequestsControl()
        {
            InitializeComponent();

            DataContext = new ViewDataRequestsVM(DataBaseContextCreator,
                new ErrorMessageBoxService());
        }
    }
}