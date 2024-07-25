using View.Implementations.ResourceService;

using ViewModel.Classes;
using ViewModel.Interfaces.Proces;

namespace View.Implementations.Proces
{
    public abstract class BaseProc : IProc
    {
        private static string _titleKey = "Header";

        private static string _pathKey = "Path";

        private static string _iconKey = "Icon";

        private static string _gestureKey = "Input";

        private static string _gestureTextKey = "InputText";

        protected readonly string _name;

        /// <summary>
        /// Сервис ресурсов окна.
        /// </summary>
        protected IWindowResourceService _windowResourceService;

        public IProcData Data { get; private set; }

        protected BaseProc(string name, IWindowResourceService windowResourceService)
        {
            _name = name;
            _windowResourceService = windowResourceService;
            Data = new ProcData(Invoke, GetMetadata(), CanExecute);
        }

        public abstract void Abort();

        public abstract void Invoke();

        protected virtual bool CanExecute() => true;

        public object GetMetadata() => new ProcMetadata(
            _windowResourceService.GetString(_name + _titleKey),
            _windowResourceService.GetString(_name + _pathKey),
            _windowResourceService.GetResource(_name + _iconKey),
            _windowResourceService.GetResource(_name + _gestureKey),
            _windowResourceService.GetString(_name + _gestureTextKey));
    }
}
