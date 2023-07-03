﻿using CommunityToolkit.Mvvm.Input;
using System;
using System.IO;

using Model.Independent;
using ViewModel.Interfaces;

namespace ViewModel.VMs.DbSet
{
    public class PassportsVM : ControlDbSetVM<Passport>
    {
        public RelayCommand LoadImageCommand { get; private set; }

        public RelayCommand SaveImageCommand { get; private set; }

        public PassportsVM(IDbContextCreator dataBaseContextCreator, 
            IMessageService messageService, IGettingFileService openFileService,
            IGettingFileService saveFileService) : base(dataBaseContextCreator, messageService)
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
                    catch (Exception ex)
                    {
                        MessageService.ShowMessage(ex.Message, "Error!");
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
                        MessageService.ShowMessage(ex.Message, "Error!");
                    }
                }
            }, () => SelectedItem != null);
        }

        protected override void SelectedItemChanged()
        {
            SaveImageCommand?.NotifyCanExecuteChanged();
            LoadImageCommand?.NotifyCanExecuteChanged();
        }
    }
}