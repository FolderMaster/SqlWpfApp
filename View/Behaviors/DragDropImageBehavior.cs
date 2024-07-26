using Microsoft.Xaml.Behaviors;

using System.Windows.Input;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media.Imaging;
using System.IO;

using ViewModel.Interfaces.Services.Files;
using ViewModel.Interfaces.Services;
using ViewModel.Services;

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
        /// Возвращает и задаёт сервис сообщений.
        /// </summary>
        public IMessageService MessageService
        {
            get => (IMessageService)GetValue(MessageServiceProperty);
            set => SetValue(MessageServiceProperty, value);
        }

        /// <summary>
        /// Возвращает и задаёт сервис ресурсов.
        /// </summary>
        public IResourceService ResourceService
        {
            get => (IResourceService)GetValue(ResourceServiceProperty);
            set => SetValue(ResourceServiceProperty, value);
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
        /// Свойство зависимости <see cref="Image"/>.
        /// </summary>
        public static DependencyProperty ImageProperty = DependencyProperty.Register(nameof(Image),
            typeof(byte[]), typeof(DragDropImageBehavior), new FrameworkPropertyMetadata());

        /// <summary>
        /// Свойство зависимости <see cref="MessageService"/>.
        /// </summary>
        public static DependencyProperty MessageServiceProperty = DependencyProperty.Register
            (nameof(MessageService), typeof(IMessageService), typeof(DragDropImageBehavior),
            new FrameworkPropertyMetadata());

        /// <summary>
        /// Свойство зависимости <see cref="ResourceService"/>.
        /// </summary>
        public static DependencyProperty ResourceServiceProperty = DependencyProperty.Register
            (nameof(ResourceService), typeof(IResourceService), typeof(DragDropImageBehavior),
            new FrameworkPropertyMetadata());

        /// <summary>
        /// Свойство зависимости <see cref="FileService"/>.
        /// </summary>
        public static DependencyProperty FileServiceProperty = DependencyProperty.Register
            (nameof(FileService), typeof(IFileService), typeof(DragDropImageBehavior),
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
                MessengerService.ExecuteWithExceptionMessage(ResourceService, MessageService, () =>
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
                var filePath = FileService.GetFullPath(fileName);
                MessengerService.ExecuteWithExceptionMessage(ResourceService, MessageService,
                    () => FileService.Save(filePath, Image));

                if (Image != null)
                {
                    var dataObject = new DataObject();
                    dataObject.SetData(DataFormats.FileDrop, new string[] { filePath }, true);
                    dataObject.SetData(DataFormats.Bitmap, Image);
                    DragDrop.DoDragDrop(image, dataObject, DragDropEffects.Copy);
                }
            }  
        }
    }
}