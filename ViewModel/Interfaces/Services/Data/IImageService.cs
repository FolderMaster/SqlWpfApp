using System.Collections.Generic;

using ViewModel.Classes;

namespace ViewModel.Interfaces.Services.Data
{
    public interface IImageService
    {
        public IEnumerable<FileFormat> ImageFormats { get; }

        public bool IsImage(byte[] data);

        public FileFormat? GetImageFormat(byte[] data);
    }
}
