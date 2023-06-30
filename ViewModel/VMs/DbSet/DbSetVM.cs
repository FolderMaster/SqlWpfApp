using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;

using ViewModel.Interfaces;

namespace ViewModel.VMs.DbSet
{
    public class DbSetVM<T> : ObservableObject where T : class
    {
        protected DbSet<T> _dbSet;

        protected ObservableCollection<T>? _dbSetLocal = null;

        protected ObservableCollection<T>? _finalDbSetLocal = null;

        protected T? _selectedItem = null;

        protected string _searchText = "";

        protected string _filterText = "";

        protected List<PropertyInfo> _getMethodList;

        protected DbSet<T> DbSet
        {
            get => _dbSet;
            set
            {
                if (SetProperty(ref _dbSet, value))
                {
                    DbSet.Load();
                    DbSetLocal = DbSet.Local.ToObservableCollection();
                }
            }
        }

        public IMessageService MessageService { get; private set; }

        public ObservableCollection<T>? DbSetLocal
        {
            get => _dbSetLocal;
            set
            {
                if (SetProperty(ref _dbSetLocal, value))
                {
                    Filter();
                    Search();
                }
            }
        }

        public ObservableCollection<T>? FinalDbSetLocal
        {
            get => _finalDbSetLocal;
            protected set
            {
                if (FinalDbSetLocal != value)
                {
                    if (FinalDbSetLocal != null)
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
                    OnPropertyChanged(nameof(SelectedIndex));
                    SelectedIndexChanged();
                    SelectedItemChanged();
                    ItemChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public int SelectedIndex
        {
            get => Count > 0 ? FinalDbSetLocal.IndexOf(SelectedItem) : -1;
            set
            {
                SelectedItem = value != -1 ? FinalDbSetLocal[value] : null;
                OnPropertyChanged(nameof(SelectedIndex));
                SelectedIndexChanged();
            }
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                if (SetProperty(ref _searchText, value))
                {
                    Search();
                }
            }
        }

        public string FilterText
        {
            get => _filterText;
            set
            {
                if (SetProperty(ref _filterText, value))
                {
                    Filter();
                    Search();
                }
            }
        }

        public string SelectedPropertyName { get; set; } = "";

        public RelayCommand SaveCommand { get; private set; }

        public event EventHandler ItemChanged;

        public DbSetVM(IDataBaseContextCreator dataBaseContextCreator, IMessageService messageService)
        {
            MessageService = messageService;
            try
            {
                DbSet = dataBaseContextCreator.Result.Set<T>();
            }
            catch (Exception ex)
            {
                MessageService.ShowMessage(ex.Message, "Error!");
                SaveCommand?.NotifyCanExecuteChanged();
            }
            SaveCommand = new RelayCommand(() =>
            {
                try
                {
                    dataBaseContextCreator.Result.SaveChanges<T>();
                }
                catch (Exception ex)
                {
                    MessageService.ShowMessage(ex.Message, "Error!");
                }
            }, () => DbSet != null);
            _getMethodList =
                typeof(T).GetProperties().Where((p) => p.GetGetMethod() != null).ToList();
        }

        protected virtual void SelectedIndexChanged() { }

        protected virtual void SelectedItemChanged() { }

        private void Filter()
        {
            var selectedIndex = SelectedIndex;

            ObservableCollection<T> collection;
            if (!string.IsNullOrEmpty(FilterText))
            {
                List<PropertyInfo> properties = GetPropertiesForCondition();

                collection = new ObservableCollection<T>();
                foreach (var item in DbSetLocal)
                {
                    foreach (var property in properties)
                    {
                        var doFind = IsMatchTextInValueOfProperty(FilterText, property, item);
                        if (doFind)
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
                collection = new ObservableCollection<T>(DbSetLocal);
            }
            FinalDbSetLocal = collection;

            if (!FinalDbSetLocal.Contains(SelectedItem))
            {
                SelectedIndex = Count > 0 ? 0 : -1;
            }
            else if (selectedIndex != SelectedIndex)
            {
                OnPropertyChanged(nameof(SelectedIndex));
                SelectedIndexChanged();
            }
            OnPropertyChanged(nameof(Count));
        }

        private void Search()
        {
            if (!string.IsNullOrEmpty(SearchText))
            {
                List<PropertyInfo> properties = GetPropertiesForCondition();

                foreach (var item in FinalDbSetLocal)
                {
                    bool doFind = false;
                    foreach (var property in properties)
                    {
                        doFind = IsMatchTextInValueOfProperty(SearchText, property, item);
                        if (doFind)
                        {
                            SelectedItem = item;
                            break;
                        }
                    }
                    if (doFind)
                    {
                        break;
                    }
                }
            }
        }

        private bool IsMatchTextInValueOfProperty(string matchText, PropertyInfo property, T item)
        {
            var value = property.GetValue(item);
            var text = value != null ? value.ToString() : "";
            return text.IndexOf(matchText, StringComparison.OrdinalIgnoreCase) != -1;
        }

        private List<PropertyInfo> GetPropertiesForCondition() =>
            string.IsNullOrEmpty(SelectedPropertyName) ? _getMethodList : new List<PropertyInfo>()
            { _getMethodList.First((p) => p.Name == SelectedPropertyName) };

        private void FinalDbSetLocal_CollectionChanged(object? sender,
            NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add: DbSetLocal.Add((T)e.NewItems[0]); break;
                case NotifyCollectionChangedAction.Remove:
                    DbSetLocal.Remove((T)e.OldItems[0]);
                    break;
            }
        }
    }
}