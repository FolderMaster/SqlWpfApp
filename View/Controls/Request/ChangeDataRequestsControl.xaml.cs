using System.Windows.Controls;

using ViewModel.VMs.Request;
using View.Implementations.MessageBoxes;
using ViewModel.Interfaces;

namespace View.Controls.Request
{
    public partial class ChangeDataRequestsControl : UserControl
    {
        public static IDataBaseContextCreator DataBaseContextCreator { get; set; }

        public ChangeDataRequestsControl()
        {
            InitializeComponent();

            DataContext = new ChangeDataRequestsVM(DataBaseContextCreator,
                new ErrorMessageBoxService());
        }
    }
}