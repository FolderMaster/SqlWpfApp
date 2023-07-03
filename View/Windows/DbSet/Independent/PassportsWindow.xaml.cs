using System.Windows;
using System.Collections.Generic;

using ViewModel.VMs.DbSet;
using ViewModel.Interfaces;

namespace View.Windows.DbSet.Independent
{
    public partial class PassportsWindow : Window
    {
        public PassportsWindow(IDbContextCreator dbContextCreator, IMessageService messageService,
            IGettingFileService gettingOpenFileService, IGettingFileService gettingSaveFileService)
        {
            InitializeComponent();

            DataContext = new List<object>()
            {
                new PassportsVM(dbContextCreator, messageService, gettingOpenFileService,
                    gettingSaveFileService)
            };
        }
    }
}