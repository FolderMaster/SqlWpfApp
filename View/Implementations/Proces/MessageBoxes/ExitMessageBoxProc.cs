using View.Implementations.MessageBoxes;
using View.Implementations.ResourceService;

using ViewModel.Interfaces;

namespace View.Implementations.Proces.MessageBoxes
{
    public class ExitMessageBoxProc : MessageBoxProc
    {
        private IAppCloseable _appCloseable;

        public ExitMessageBoxProc(IWindowResourceService windowResourceService,
            QuestionMessageBoxService messageBoxService, IAppCloseable appCloseable) :
            base("Exit", windowResourceService, messageBoxService) => _appCloseable = appCloseable;

        public override void TrueResultAction()
        {
            _appCloseable.CloseApp();
        }
    }
}
