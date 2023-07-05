using System.Windows;
using System.Windows.Media.Imaging;

namespace View.Implementations.ResourceService
{
    public class WindowResourceService : IWindowResourceService
    {
        private static string _headerEndKey = "Header";

        private static string _iconEndKey = "Icon";

        public object GetResource(string resourceKey) =>
            Application.Current.Resources[resourceKey];

        public string GetString(string stringKey) => GetResource(stringKey) as string;

        public string GetHeader(string headerKey) => GetString(headerKey + _headerEndKey);

        public BitmapSource GetIcon(string iconKey) =>
            GetResource(iconKey + _iconEndKey) as BitmapSource;
    }
}