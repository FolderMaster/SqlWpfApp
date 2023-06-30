using System.Windows;
using ViewModel.Interfaces;

namespace View.Implementations
{
    public class AppCloseable : IAppCloseable
    {
        public void CloseApp() => Application.Current.Shutdown();
    }
}