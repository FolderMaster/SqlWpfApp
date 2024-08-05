using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

using ViewModel.Classes;
using ViewModel.Interfaces.Services.Data;

namespace ViewModel.Dependencies.Data
{
    public class ImageService : IImageService
    {
        private static IDictionary<ImageFormat, FileFormat> _imageFormats =
            new Dictionary<ImageFormat, FileFormat>()
            {
                [ImageFormat.Png] = new FileFormat("Png", ["png"]),
                [ImageFormat.Jpeg] = new FileFormat("Jpeg", ["jpeg", "jpg"]),
                [ImageFormat.Gif] = new FileFormat("Gif", ["gif"]),
                [ImageFormat.Bmp] = new FileFormat("Bmp", ["bmp"]),
                [ImageFormat.Tiff] = new FileFormat("Tiff", ["tiff", "tif"]),
                [ImageFormat.Webp] = new FileFormat("Webp", ["webp"]),
                [ImageFormat.Heif] = new FileFormat("Heif", ["heif", "heic"]),
                [ImageFormat.Icon] = new FileFormat("Icon", ["ico"]),
                [ImageFormat.Wmf] = new FileFormat("Wmf", ["wmf"]),
                [ImageFormat.Emf] = new FileFormat("Emf", ["emf"]),
                [ImageFormat.Exif] = new FileFormat("Exif", []),
            };

        public IEnumerable<FileFormat> ImageFormats => _imageFormats.Values;

        public bool IsImage(byte[] data)
        {
            try
            {
                using (var stream = new MemoryStream(data))
                {
                    using (var image = Image.FromStream(stream))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public FileFormat? GetImageFormat(byte[] data)
        {
            using (var stream = new MemoryStream(data))
            {
                using (var image = Image.FromStream(stream))
                {
                    var imageFormat = image.RawFormat;
                    if (_imageFormats.ContainsKey(imageFormat))
                    {
                        return _imageFormats[imageFormat];
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
    }
}
