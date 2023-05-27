using System;
using System.Windows.Input;
using System.Windows;
using System.IO;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;

using DataObject = System.Windows.DataObject;

using SQLiteWpfApp.Models.Independent;
using SQLiteWpfApp.ViewModels.Services;

namespace SQLiteWpfApp.ViewModels.VMs
{
    public class PassportsVM : ControlDbSetVM<Passport>
    {
        public RelayCommand LoadImageCommand { get; private set; }

        public RelayCommand SaveImageCommand { get; private set; }

        public RelayCommand DropImageCommand { get; private set; }

        public RelayCommand DragImageCommand { get; private set; }

        public PassportsVM(IMessageService messageService, Action closeAction,
            IGettingFileService openFileService, IGettingFileService saveFileService) :
            base(messageService, closeAction)
        {
            LoadImageCommand = new RelayCommand(() =>
            {
                var filePath = openFileService.GetFilePath();
                if (filePath != null)
                {
                    try
                    {
                        SelectedItem.Scan = File.ReadAllBytes(filePath);
                    }
                    catch(Exception ex)
                    {
                        _messageService.ShowMessage(ex.Message, "Error!");
                    }
                }
            }, () => SelectedItem != null);
            SaveImageCommand = new RelayCommand(() =>
            {
                var filePath = saveFileService.GetFilePath();
                if (filePath != null)
                {
                    try
                    {
                        File.WriteAllBytes(filePath, SelectedItem.Scan);
                    }
                    catch (Exception ex)
                    {
                        _messageService.ShowMessage(ex.Message, "Error!");
                    }
                }
            }, () => SelectedItem != null);
            DropImageCommand = new RelayCommand(() =>
            {
                var filePath = openFileService.GetFilePath();
                //var fileNames = ((DragEventArgs)parameter).Data.GetData(DataFormats.FileDrop)
                //    as string[];
                //if (fileNames != null)
                //{
                //    try
                //    {
                //        SelectedPassport.Scan = File.ReadAllBytes(fileNames[0]);
                //    }
                //    catch (Exception ex)
                //    {
                //        _messageService.ShowMessage(ex.Message, "Error!");
                //    }
                //}
            }, () => SelectedItem != null);
            DragImageCommand = new RelayCommand(() =>
            {
                var filePath = saveFileService.GetFilePath();
                //var fileName = "temp";
                //var dataObject = new DataObject();
                //dataObject.SetData(DataFormats.FileDrop, new string[] { fileName }, true);
                //dataObject.SetData(DataFormats.Bitmap, SelectedPassport.Scan);
                //DragDrop.DoDragDrop(parameter as DependencyObject, dataObject, DragDropEffects.Copy);
            }, () => SelectedItem != null);
        }

        protected override void SelectedItemChanged()
        {
            SaveImageCommand?.NotifyCanExecuteChanged();
            LoadImageCommand?.NotifyCanExecuteChanged();
            DropImageCommand?.NotifyCanExecuteChanged();
            DragImageCommand?.NotifyCanExecuteChanged();
        }
    }
}