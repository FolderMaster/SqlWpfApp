using System.Windows;
using View.Implementations.ResourceService;

namespace View.Windows.DbSet.Dependent
{
    public partial class ThreeGridDbSetWindow : Window
    {
        public ThreeGridDbSetWindow(IWindowResourceService windowResourceService,
            string headerKey, string iconKey, object dataContext)
        {
            InitializeComponent();

            Title = windowResourceService.GetHeader(headerKey);
            Icon = windowResourceService.GetIcon(iconKey);
            DataContext = dataContext;
        }
    }
}