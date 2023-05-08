using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows;
using System.IO;

using SQLiteWpfApp.Models.Independent;

using DataObject = System.Windows.DataObject;

namespace SQLiteWpfApp.ViewModels.VMs
{
    public class PassportsVM : DataBaseVM<Passport>, INotifyPropertyChanged
    {
        private int _selectedIndex = -1;

        private Passport? _selectedPassport = null;

        public ICommand LoadImageCommand { get; private set; }

        public ICommand SaveImageCommand { get; private set; }

        public ICommand DropImageCommand { get; private set; }

        public ICommand DragImageCommand { get; private set; }

        public ICommand FirstCommand { get; private set; }

        public ICommand BackCommand { get; private set; }

        public ICommand NextCommand { get; private set; }

        public ICommand LastCommand { get; private set; }

        public ICommand AddCommand { get; private set; }

        public ICommand RemoveCommand { get; private set; }

        public int Count => DataBaseLocal.Count;

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                if (value != SelectedIndex)
                {
                    _selectedIndex = value;
                    SelectedPassport = SelectedIndex != -1 ? DataBaseLocal[SelectedIndex] : null;
                    InvokePropertyChanged(nameof(SelectedIndex));
                    InvokePropertyChanged(nameof(SelectedNumber));
                }
            }
        }

        public int SelectedNumber
        {
            get => SelectedIndex + 1;
            set
            {
                if (value != SelectedIndex)
                {
                    if ((value <= DataBaseLocal.Count && value >= 1) || (DataBaseLocal.Count == 0
                        && value == 0))
                        SelectedIndex = value - 1;
                }
                InvokePropertyChanged(nameof(SelectedNumber));
            }
        }

        public Passport? SelectedPassport
        {
            get => _selectedPassport;
            set
            {
                if(value != SelectedPassport)
                {
                    _selectedPassport = value;
                    SelectedIndex = DataBaseLocal.IndexOf(SelectedPassport);
                    InvokePropertyChanged(nameof(SelectedPassport));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public PassportsVM(IMessageService messageService, DataBaseContext dbContext,
            DbSet<Passport> dbSet) : base(messageService, dbContext, dbSet)
        {
            SelectedIndex = DataBaseLocal.Count > 0 ? 0 : -1;
            FirstCommand = new RelayCommand((parameter) => SelectedIndex = 0,
                (parameter) => DataBaseLocal.Count > 0 && SelectedIndex != 0);
            BackCommand = new RelayCommand((parameter) => SelectedIndex -= 1,
                (parameter) => DataBaseLocal.Count > 0 && SelectedIndex != 0);
            NextCommand = new RelayCommand((parameter) => SelectedIndex += 1,
                (parameter) => DataBaseLocal.Count > 0 &&
                SelectedIndex != DataBaseLocal.Count - 1);
            LastCommand = new RelayCommand((parameter) => SelectedIndex = DataBaseLocal.Count - 1,
                (parameter) => DataBaseLocal.Count > 0 &&
                SelectedIndex != DataBaseLocal.Count - 1);
            AddCommand = new RelayCommand((parameter) =>
            {
                DataBaseLocal.Add(new Passport());
                InvokePropertyChanged(nameof(Count));
                LastCommand.Execute(null);
            });
            RemoveCommand = new RelayCommand((parameter) =>
            {
                int selectedIndex = DataBaseLocal.IndexOf(SelectedPassport);
                DataBaseLocal.Remove(SelectedPassport);
                InvokePropertyChanged(nameof(Count));
                if (DataBaseLocal.Count > 0)
                {
                    SelectedPassport = selectedIndex < DataBaseLocal.Count ?
                        DataBaseLocal[SelectedIndex] : DataBaseLocal[DataBaseLocal.Count - 1];
                }
                else
                {
                    SelectedPassport = null;
                }
            }, (parameter) => DataBaseLocal.Count > 0);
            LoadImageCommand = new RelayCommand((parameter) =>
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Images (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg";
                dialog.Multiselect = false;
                if (dialog.ShowDialog() == true)
                {
                    try
                    {
                        SelectedPassport.Scan = File.ReadAllBytes(dialog.FileName);
                    }
                    catch(Exception ex)
                    {
                        _messageService.ShowMessage(ex.Message, "Error!");
                    }
                }
            }, (parameter) => SelectedPassport != null);
            SaveImageCommand = new RelayCommand((parameter) =>
            {
                SaveFileDialog dialog = new SaveFileDialog();
                if (dialog.ShowDialog() == true)
                {
                    File.WriteAllBytes(dialog.FileName, SelectedPassport.Scan);
                }
            }, (parameter) => SelectedPassport != null);
            DropImageCommand = new RelayCommand((parameter) =>
            {
                var fileNames = ((DragEventArgs)parameter).Data.GetData(DataFormats.FileDrop)
                    as string[];
                if (fileNames != null)
                {
                    try
                    {
                        SelectedPassport.Scan = File.ReadAllBytes(fileNames[0]);
                    }
                    catch (Exception ex)
                    {
                        _messageService.ShowMessage(ex.Message, "Error!");
                    }
                }
            }, (parameter) => SelectedPassport != null);
            DragImageCommand = new RelayCommand((parameter) =>
            {
                var fileName = "temp";
                var dataObject = new DataObject();
                dataObject.SetData(DataFormats.FileDrop, new string[] { fileName }, true);
                dataObject.SetData(DataFormats.Bitmap, SelectedPassport.Scan);
                DragDrop.DoDragDrop(parameter as DependencyObject, dataObject, DragDropEffects.Copy);
            }, (parameter) => SelectedPassport != null);
        }

        private void InvokePropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
