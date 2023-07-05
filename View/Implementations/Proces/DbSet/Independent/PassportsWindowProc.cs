using System.Windows;

using View.Implementations.ResourceService;
using View.Windows.DbSet.Independent;

using ViewModel.Interfaces;

namespace View.Implementations.Proces.DbSet.Independent
{
    public class PassportsWindowProc : WindowProc
    {
        private IGettingFileService _gettingOpenFileService;

        private IGettingFileService _gettingSaveFileService;

        private IFileService _fileService;

        public PassportsWindowProc(IDbContextBuilder dbContextCreator,
            IWindowResourceService windowResourceService, IMessageService messageService,
            IGettingFileService gettingOpenFileService, IGettingFileService gettingSaveFileService,
            IFileService fileService) :
            base(dbContextCreator, windowResourceService, messageService)
        {
            _gettingOpenFileService = gettingOpenFileService;
            _gettingSaveFileService = gettingSaveFileService;
            _fileService = fileService;
        }

        protected override Window CreateWindow(IDbContextBuilder dbContextCreator,
            IWindowResourceService windowResourceService, IMessageService messageService) =>
            new PassportsWindow(dbContextCreator, windowResourceService, messageService,
                _gettingOpenFileService, _gettingSaveFileService, _fileService);
    }
}