using Microsoft.Xaml.Behaviors;

using System;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows;
using System.IO;

using ViewModel.Interfaces;

namespace View.Behaviors
{
    public class DragDropImageBehavior : Behavior<Image>
    {
        public byte[] Image
        {
            get => (byte[])GetValue(ImageProperty);
            set => SetValue(ImageProperty, value);
        }

        public IMessageService MessageService
        {
            get => (IMessageService)GetValue(MessageServiceProperty);
            set => SetValue(MessageServiceProperty, value);
        }

        public static DependencyProperty ImageProperty = DependencyProperty.Register(nameof(Image),
            typeof(byte[]), typeof(DragDropImageBehavior), new FrameworkPropertyMetadata());

        public static DependencyProperty MessageServiceProperty = DependencyProperty.Register
            (nameof(MessageService), typeof(IMessageService), typeof(DragDropImageBehavior),
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
                try
                {
                    Image = File.ReadAllBytes(fileNames[0]);
                }
                catch(Exception ex)
                {
                    MessageService?.ShowMessage(ex.Message, "Error!");
                }
            }
        }

        private void AssociatedObject_PreviewMouseLeftButtonDown(object sender,
            MouseButtonEventArgs e)
        {
            if (sender is Image image /*&& image.Source is BitmapSource bitmapSource*/)
            {
                var fileName = "temp";
                var filePath = Path.GetFullPath(fileName);
                File.WriteAllBytes(fileName, Image);

                var dataObject = new DataObject();
                dataObject.SetData(DataFormats.FileDrop, new string[] { filePath }, true);
                dataObject.SetData(DataFormats.Bitmap, Image);

                DragDrop.DoDragDrop(image, dataObject, DragDropEffects.Copy);
            }  
        }
    }
}