using System.Windows;

using View.Windows.DbSet.Independent;

using ViewModel.Interfaces;

namespace View.Implementations.Proces.DbSet.Independent
{
    public class PassportsWindowProc : WindowProc
    {
        private IGettingFileService _gettingOpenFileService;

        private IGettingFileService _gettingSaveFileService;

        public PassportsWindowProc(IDbContextCreator dbContextCreator,
            IMessageService messageService, IGettingFileService gettingOpenFileService,
            IGettingFileService gettingSaveFileService) : base(dbContextCreator, messageService)
        {
            _gettingOpenFileService = gettingOpenFileService;
            _gettingSaveFileService = gettingSaveFileService;
        }

        protected override Window CreateWindow(IDbContextCreator dbContextCreator,
            IMessageService messageService) => new PassportsWindow(dbContextCreator,
                messageService, _gettingOpenFileService, _gettingSaveFileService);
    }
}