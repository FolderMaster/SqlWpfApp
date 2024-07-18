using View.Implementations.MessageBoxes;
using View.Implementations.ResourceService;

namespace View.Implementations.Proces.MessageBoxes
{
    public class MessageBoxProc : BaseProc
    {
        private static string _titleKey = "MessageTitle";

        private static string _descriptionKey = "MessageDescription";

        private readonly BaseMessageBoxService _messageBoxService;

        public MessageBoxProc(string name, IWindowResourceService windowResourceService,
            BaseMessageBoxService messageBoxService) : base(name, windowResourceService) =>
            _messageBoxService = messageBoxService;

        public override void Abort() { }

        public override void Invoke()
        {
            var title = _windowResourceService.GetString(_name + _titleKey);
            var message = _windowResourceService.GetString(_name + _descriptionKey);
            if (_messageBoxService.ShowMessage(message, title))
            {
                TrueResultAction();
            }
        }

        public virtual void TrueResultAction() { }
    }
}
