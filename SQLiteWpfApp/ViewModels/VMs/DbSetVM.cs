using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using SQLiteWpfApp.ViewModels.Services;

namespace SQLiteWpfApp.ViewModels.VMs
{
    public class DbSetVM<T> : ObservableObject where T : class
    {
        protected IMessageService _messageService;

        protected DbSet<T> _dbSet;

        protected ObservableCollection<T>? _dbSetLocal = null;

        protected ObservableCollection<T>? _finalDbSetLocal = null;

        protected T? _selectedItem = null;

        protected int _selectedIndex = -1;

        protected string _searchText = "";

        protected bool _isFilter = false;

        protected List<PropertyInfo> _getMethodList;

        protected DbSet<T> DbSet
        {
            get => _dbSet;
            set
            {
                SetProperty(ref _dbSet, value);
                try
                {
                    DbSet.Load();
                    DbSetLocal = DbSet.Local.ToObservableCollection();
                }
                catch (Exception ex)
                {
                    _messageService.ShowMessage(ex.Message, "Error!");
                }
            }
        }

        public ObservableCollection<T>? DbSetLocal
        {
            get => _dbSetLocal;
            set
            {
                if(SetProperty(ref _dbSetLocal, value))
                {
                    Find();
                    SelectedIndex = Count > 0 ? 0 : -1;
                }
            }
        }

        public ObservableCollection<T>? FinalDbSetLocal
        {
            get => _finalDbSetLocal;
            protected set
            {
                if(FinalDbSetLocal != value)
                {
                    if(FinalDbSetLocal != null)
                    {
                        FinalDbSetLocal.CollectionChanged -= FinalDbSetLocal_CollectionChanged;
                    }
                    SetProperty(ref _finalDbSetLocal, value);
                    FinalDbSetLocal.CollectionChanged += FinalDbSetLocal_CollectionChanged;
                }
            }
        }

        public int Count => FinalDbSetLocal != null ? FinalDbSetLocal.Count : 0;

        public T? SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (SetProperty(ref _selectedItem, value))
                {
                    SelectedIndex = FinalDbSetLocal.IndexOf(SelectedItem);
                    SelectedItemChanged();
                    ItemChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                if (SetProperty(ref _selectedIndex, value))
                {
                    SelectedItem = SelectedIndex != -1 ? FinalDbSetLocal[SelectedIndex] : null;
                    SelectedIndexChanged();
                }
            }
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                if (SetProperty(ref _searchText, value))
                {
                    Find();
                }
            }
        }

        public bool IsFilter
        {
            get => _isFilter;
            set
            {
                if (SetProperty(ref _isFilter, value))
                {
                    Find();
                }
            }
        }

        public ICommand SaveCommand { get; private set; }

        public ICommand CloseCommand { get; private set; }

        public event EventHandler ItemChanged;

        public DbSetVM(IMessageService messageService)
        {
            _messageService = messageService;
            DbSet = DataBaseContext.Instance.Set<T>();
            SaveCommand = new RelayCommand(() =>
            {
                try
                {
                    DataBaseContext.Instance.SaveChanges<T>();
                }
                catch (Exception ex)
                {
                    _messageService.ShowMessage(ex.Message, "Error!");
                }
            }, () => DbSet != null);
            CloseCommand = new RelayCommand(() => { });
            _getMethodList = typeof(T).GetProperties().Where((p) => p.GetGetMethod() != null).ToList();
        }

        public DbSetVM(IMessageService messageService, Action closeAction) : this(messageService)
            => CloseCommand = new RelayCommand(() => closeAction());

        protected virtual void SelectedIndexChanged() { }

        protected virtual void SelectedItemChanged() { }

        protected void Find()
        {
            if(!string.IsNullOrEmpty(SearchText))
            {
                if (IsFilter)
                {
                    var collection = new ObservableCollection<T>();
                    foreach (var item in DbSetLocal)
                    {
                        foreach (var property in _getMethodList)
                        {
                            var value = property.GetValue(item);
                            var text = value != null ? value.ToString() : "";
                            if (text.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) != -1)
                            {
                                collection.Add(item);
                                break;
                            }
                        }
                    }
                    FinalDbSetLocal = collection;
                }
                else
                {
                    FinalDbSetLocal = new ObservableCollection<T>(DbSetLocal);
                    foreach (var item in DbSetLocal)
                    {
                        bool doFind = false;
                        foreach (var property in _getMethodList)
                        {
                            var value = property.GetValue(item);
                            var text = value != null ? value.ToString() : "";
                            if (text.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) != -1)
                            {
                                doFind = true;
                                break;
                            }
                        }
                        if (doFind)
                        {
                            SelectedItem = item;
                            break;
                        }
                    }
                }
            }
            else
            {
                FinalDbSetLocal = new ObservableCollection<T>(DbSetLocal);
            }

            var selectedIndex = FinalDbSetLocal.IndexOf(SelectedItem);
            if (selectedIndex == -1 && Count > 0)
            {
                SelectedIndex = 0;
            }
            else if(selectedIndex != -1 && Count > 0)
            {
                SelectedIndex = selectedIndex;
            }
            else if(Count == 0)
            {
                SelectedIndex = -1;
            }
            OnPropertyChanged(nameof(Count));
        }

        private void FinalDbSetLocal_CollectionChanged(object? sender,
            NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add: DbSetLocal.Add((T)e.NewItems[0]); break;
                case NotifyCollectionChangedAction.Remove: DbSetLocal.Remove((T)e.OldItems[0]);
                    break;
            }
        }
    }
}