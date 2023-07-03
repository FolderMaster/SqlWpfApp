using System.Windows;
using System.Windows.Media.Imaging;

namespace View.Services
{
    public static class AppResourceService
    {
        public static string HeaderEndPath => "Header";

        public static string IconEndPath => "Icon";

        public static object GetResource(string resourcePath) =>
            Application.Current.Resources[resourcePath];

        public static string GetHeader(string headerName) =>
            GetResource(headerName + HeaderEndPath) as string;

        public static BitmapSource GetIcon(string headerName) =>
            GetResource(headerName + HeaderEndPath) as BitmapImage;
    }
}