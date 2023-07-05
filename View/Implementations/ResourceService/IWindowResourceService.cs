using System.Windows.Media.Imaging;

using ViewModel.Interfaces;

namespace View.Implementations.ResourceService
{
    public interface IWindowResourceService : IResourceService
    {
        public BitmapSource GetIcon(string iconKey);

        public string GetHeader(string headerKey);
    }
}