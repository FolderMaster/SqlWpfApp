using Microsoft.Xaml.Behaviors;

using System.Windows.Input;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media.Imaging;
using System.IO;

using ViewModel.Interfaces.Services.Files;
using ViewModel.Interfaces.Services.Messages;

namespace View.Behaviors
{
    /// <summary>
    /// Класс поведения <see cref="Image"/> перетаскивания изображения.
    /// </summary>
    public class DragDropImageBehavior : Behavior<Image>
    {
        /// <summary>
        /// Возвращает и задаёт изображение.
        /// </summary>
        public byte[] Image
        {
            get => (byte[])GetValue(ImageProperty);
            set => SetValue(ImageProperty, value);
        }

        /// <summary>
        /// Возвращает и задаёт сервис послания сообщений.
        /// </summary>
        public IMessengerService MessengerService
        {
            get => (IMessengerService)GetValue(MessengerServiceProperty);
            set => SetValue(MessengerServiceProperty, value);
        }

        /// <summary>
        /// Возвращает и задаёт файловый сервис.
        /// </summary>
        public IFileService FileService
        {
            get => (IFileService)GetValue(FileServiceProperty);
            set => SetValue(FileServiceProperty, value);
        }

        /// <summary>
        /// Возвращает и задаёт сервис путей.
        /// </summary>
        public IPathService PathService
        {
            get => (IPathService)GetValue(PathServiceProperty);
            set => SetValue(PathServiceProperty, value);
        }

        /// <summary>
        /// Свойство зависимости <see cref="Image"/>.
        /// </summary>
        public static DependencyProperty ImageProperty = DependencyProperty.Register(nameof(Image),
            typeof(byte[]), typeof(DragDropImageBehavior), new FrameworkPropertyMetadata());

        /// <summary>
        /// Свойство зависимости <see cref="MessengerService"/>.
        /// </summary>
        public static DependencyProperty MessengerServiceProperty = DependencyProperty.Register
            (nameof(MessengerService), typeof(IMessengerService), typeof(DragDropImageBehavior),
            new FrameworkPropertyMetadata());

        /// <summary>
        /// Свойство зависимости <see cref="FileService"/>.
        /// </summary>
        public static DependencyProperty FileServiceProperty = DependencyProperty.Register
            (nameof(FileService), typeof(IFileService), typeof(DragDropImageBehavior),
            new FrameworkPropertyMetadata());

        /// <summary>
        /// Свойство зависимости <see cref="PathService"/>.
        /// </summary>
        public static DependencyProperty PathServiceProperty = DependencyProperty.Register
            (nameof(PathService), typeof(IPathService), typeof(DragDropImageBehavior),
            new FrameworkPropertyMetadata());

        /// <summary>
        /// Прикрепляет поведение к элементу управления.
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Drop += AssociatedObject_Drop;
            AssociatedObject.PreviewMouseLeftButtonDown +=
                AssociatedObject_PreviewMouseLeftButtonDown;
        }

        /// <summary>
        /// Открепляет поведение к элементу управления.
        /// </summary>
        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.Drop -= AssociatedObject_Drop;
            AssociatedObject.PreviewMouseLeftButtonDown -=
                AssociatedObject_PreviewMouseLeftButtonDown;
        }

        private void AssociatedObject_Drop(object sender, DragEventArgs e)
        {
            var fileNames = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (fileNames != null)
            {
                MessengerService.ExecuteWithExceptionMessage(() =>
                {
                    var bitmapImage = new BitmapImage();
                    using (var stream = new MemoryStream(FileService.Load(fileNames[0])))
                    {
                        bitmapImage.BeginInit();
                        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                        bitmapImage.StreamSource = stream;
                        bitmapImage.EndInit();
                    }
                    var name = bitmapImage.Format.ToString();
                    byte[] newImage;
                    using (var stream = new MemoryStream())
                    {
                        var encoder = new JpegBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                        encoder.Save(stream);
                        newImage = stream.ToArray();
                    }
                    Image = newImage;
                });
            }
        }

        private void AssociatedObject_PreviewMouseLeftButtonDown(object sender,
            MouseButtonEventArgs e)
        {
            if (sender is Image image && image.Source != null)
            {
                var fileName = "temp.jpeg";
                var filePath = PathService.GetFullPath(fileName);
                MessengerService.ExecuteWithExceptionMessage(() =>
                    FileService.Save(filePath, Image));

                var dataObject = new DataObject();
                dataObject.SetData(DataFormats.FileDrop, new string[] { filePath }, true);
                dataObject.SetData(DataFormats.Bitmap, Image);

                DragDrop.DoDragDrop(image, dataObject, DragDropEffects.Copy);
            }  
        }
    }
}