using Microsoft.Xaml.Behaviors;

using System.Windows.Input;
using System.Windows.Controls;
using System.Windows;

using ViewModel.Interfaces;
using ViewModel.Services;

namespace View.Behaviors
{
    public class DragDropImageBehavior : Behavior<Image>
    {
        public byte[] Image
        {
            get => (byte[])GetValue(ImageProperty);
            set => SetValue(ImageProperty, value);
        }

        public MessengerService MessengerService
        {
            get => (MessengerService)GetValue(MessengerServiceProperty);
            set => SetValue(MessengerServiceProperty, value);
        }

        public IFileService FileService
        {
            get => (IFileService)GetValue(FileServiceProperty);
            set => SetValue(FileServiceProperty, value);
        }

        public IPathService PathService
        {
            get => (IPathService)GetValue(PathServiceProperty);
            set => SetValue(PathServiceProperty, value);
        }

        public static DependencyProperty ImageProperty = DependencyProperty.Register(nameof(Image),
            typeof(byte[]), typeof(DragDropImageBehavior), new FrameworkPropertyMetadata());

        public static DependencyProperty MessengerServiceProperty = DependencyProperty.Register
            (nameof(MessengerService), typeof(MessengerService), typeof(DragDropImageBehavior),
            new FrameworkPropertyMetadata());

        public static DependencyProperty FileServiceProperty = DependencyProperty.Register
            (nameof(FileService), typeof(IFileService), typeof(DragDropImageBehavior),
            new FrameworkPropertyMetadata());

        public static DependencyProperty PathServiceProperty = DependencyProperty.Register
            (nameof(PathService), typeof(IPathService), typeof(DragDropImageBehavior),
            new FrameworkPropertyMetadata());

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Drop += AssociatedObject_Drop;
            AssociatedObject.PreviewMouseLeftButtonDown +=
                AssociatedObject_PreviewMouseLeftButtonDown;
        }

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
                    Image = FileService.Load(fileNames[0]));
            }
        }

        private void AssociatedObject_PreviewMouseLeftButtonDown(object sender,
            MouseButtonEventArgs e)
        {
            if (sender is Image image /*&& image.Source is BitmapSource bitmapSource*/)
            {
                var fileName = "temp";
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