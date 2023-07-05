using System.Windows;
using View.Implementations.ResourceService;

namespace View.Windows.DbSet.Independent
{
    public partial class GridDbSetWindow : Window
    {
        public GridDbSetWindow(IWindowResourceService windowResourceService, string headerKey,
            string iconKey, object dataContext)
        {
            InitializeComponent();

            Title = windowResourceService.GetHeader(headerKey);
            Icon = windowResourceService.GetIcon(iconKey);
            DataContext = dataContext;
        }
    }
}