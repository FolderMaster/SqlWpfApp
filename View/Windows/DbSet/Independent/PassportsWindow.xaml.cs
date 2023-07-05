using System.Windows;

using ViewModel.VMs.DbSet;
using ViewModel.Interfaces;

namespace View.Windows.DbSet.Independent
{
    public partial class PassportsWindow : Window
    {
        public PassportsWindow(IDbContextBuilder dbContextCreator,
            IResourceService resourceService, IMessageService messageService,
            IGettingFileService gettingOpenFileService, IGettingFileService gettingSaveFileService,
            IFileService fileService)
        {
            InitializeComponent();

            DataContext = new PassportsVM(dbContextCreator, resourceService, messageService,
                gettingOpenFileService, gettingSaveFileService, fileService);
        }
    }
}