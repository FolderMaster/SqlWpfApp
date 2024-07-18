using CommunityToolkit.Mvvm.Input;

namespace ViewModel.Interfaces.Proces
{
    public interface IProcData
    {
        public IRelayCommand Command { get; }

        public object Metadata { get; }
    }
}
