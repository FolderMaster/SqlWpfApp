using System.Windows.Controls;

using ViewModel.VMs.Request;
using View.Implementations.MessageBoxes;
using ViewModel.Interfaces;

namespace View.Controls.Request
{
    public partial class SpecialChangeDataRequestsControl : UserControl
    {
        public static IDataBaseContextCreator DataBaseContextCreator { get; set; }

        public SpecialChangeDataRequestsControl()
        {
            InitializeComponent();

            DataContext = new SpecialChangeDataRequestsVM(DataBaseContextCreator,
                new ErrorMessageBoxService());
        }
    }
}