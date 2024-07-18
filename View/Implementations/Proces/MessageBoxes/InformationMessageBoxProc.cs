using View.Implementations.MessageBoxes;
using View.Implementations.ResourceService;

namespace View.Implementations.Proces.MessageBoxes
{
    public class InformationMessageBoxProc : MessageBoxProc
    {
        public InformationMessageBoxProc(IWindowResourceService windowResourceService,
            InformationMessageBoxService messageBoxService) :
            base("Information", windowResourceService, messageBoxService) { }
    }
}
