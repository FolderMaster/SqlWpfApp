using Microsoft.EntityFrameworkCore;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using ViewModel.Services;
using ViewModel.Interfaces.Services;
using ViewModel.Interfaces.Services.Messages;
using ViewModel.Interfaces;

namespace ViewModel.VMs.DbSet
{
    /// <summary>
    /// Класс представления модели представления таблицы из базы данных с финальным и локальными
    /// представлениями таблицы из базы данных, количеством элементов, выбранным элементом,
    /// выбранным индексом, текстом поиска, текстом фильтра, названием выбранного свойства и
    /// командой сохранения.
    /// </summary>
    /// <typeparam name="T">Тип сущности таблицы.</typeparam>
    public class DbSetVM<T> : ObservableObject where T : class
    {
        /// <summary>
        /// Создатель контекста базы данных.
        /// </summary>
        private readonly ISession _dbContextBuilder;

        /// <summary>
        /// Сервис послания сообщений.
        /// </summary>
        protected IMessengerService _messengerService;

        /// <summary>
        /// Сервис ресурсов.
        /// </summary>
        protected IResourceService _resourceService;

        /// <summary>
        /// Представление таблицы из базы данных.
        /// </summary>
        protected DbSet<T> _dbSet;

        /// <summary>
        /// Локальное представление таблицы из базы данных.
        /// </summary>
        protected ObservableCollection<T>? _dbSetLocal = null;

        /// <summary>
        /// Финальное локальное представление таблицы из базы данных.
        /// </summary>
        protected ObservableCollection<T>? _finalDbSetLocal = null;

        /// <summary>
        /// Выбранный элемент.
        /// </summary>
        protected T? _selectedItem = null;

        /// <summary>
        /// Текст поиска.
        /// </summary>
        protected string _searchText = "";

        /// <summary>
        /// Текст фильтра.
        /// </summary>
        protected string _filterText = "";

        /// <summary>
        /// Список методов возврата свойств.
        /// </summary>
        protected List<PropertyInfo> _getMethodList;

        /// <summary>
        /// Возвращает и задаёт локальное представление таблицы из базы данных.
        /// </summary>
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

        /// <summary>
        /// Возвращает и задаёт финальное локальное представление таблицы из базы данных.
        /// </summary>
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

        /// <summary>
        /// Возвращает количество элементов финального локального представления таблицы из базы
        /// данных.
        /// </summary>
        public int Count => FinalDbSetLocal != null ? FinalDbSetLocal.Count : 0;

        /// <summary>
        /// Возвращает и задаёт выбранный элемент.
        /// </summary>
        public T? SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (SetProperty(ref _selectedItem, value))
                {
                    OnPropertyChanged(nameof(SelectedIndex));
                    OnSelectedIndexChanged();
                    OnSelectedItemChanged();
                    SelectedItemChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Возвращает и задаёт выбранный индекс.
        /// </summary>
        public int SelectedIndex
        {
            get => Count > 0 ? FinalDbSetLocal.IndexOf(SelectedItem) : -1;
            set
            {
                SelectedItem = value != -1 ? FinalDbSetLocal[value] : null;
                OnPropertyChanged(nameof(SelectedIndex));
                OnSelectedIndexChanged();
            }
        }

        /// <summary>
        /// Возвращает и задаёт текст поиска.
        /// </summary>
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

        /// <summary>
        /// Возвращает и задаёт текст фильтра.
        /// </summary>
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

        /// <summary>
        /// Возвращает и задаёт название выбранного свойства.
        /// </summary>
        public string SelectedPropertyName { get; set; } = "";

        /// <summary>
        /// Возвращает и задаёт команду сохранения.
        /// </summary>
        public RelayCommand SaveCommand { get; private set; }

        /// <summary>
        /// Обработчик события изменения выбранного элемента.
        /// </summary>
        public event EventHandler SelectedItemChanged;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="DbSetVM{T}"/>.
        /// </summary>
        /// <param name="dbContextBuilder">Создатель контекста базы данных.</param>
        /// <param name="resourceService">Сервис ресурсов.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        public DbSetVM(ISession dbContextBuilder, IResourceService resourceService,
            IMessageService messageService)
        {
            _dbContextBuilder = dbContextBuilder;
            _resourceService = resourceService;
            _messengerService = new MessengerService(_resourceService, messageService);
            _messengerService.ExecuteWithExceptionMessage(() =>
                DbSetLocal = _dbContextBuilder.DbContext.GetDbSetLocal<T>(), () =>
                SaveCommand?.NotifyCanExecuteChanged());
            SaveCommand = new RelayCommand(() =>
                _messengerService.ExecuteWithExceptionMessage(() =>
                {
                    _dbContextBuilder.DbContext.SaveChanges<T>();
                    _dbContextBuilder.DbContext.Reload<T>();
                }, () =>
                {
                    _dbContextBuilder.DbContext.RejectChanges<T>();
                }
                ), () => DbSetLocal != null);
            _getMethodList =
                typeof(T).GetProperties().Where((p) => p.GetGetMethod() != null).ToList();
        }

        /// <summary>
        /// Метод, выполняющийся после изменения выбранного индекса.
        /// </summary>
        protected virtual void OnSelectedIndexChanged() { }

        /// <summary>
        /// Метод, выполняющийся после изменения выбранного элемента.
        /// </summary>
        protected virtual void OnSelectedItemChanged() { }

        /// <summary>
        /// Осуществляет фильтрацию.
        /// </summary>
        private void Filter()
        {
            var selectedIndex = SelectedIndex;

            ObservableCollection<T> collection;
            if (!string.IsNullOrEmpty(FilterText))
            {
                List<PropertyInfo> properties = GetPropertiesForCondition();

                collection = [];
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
                OnSelectedIndexChanged();
            }
            OnPropertyChanged(nameof(Count));
        }

        /// <summary>
        /// Осуществляет поиск.
        /// </summary>
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

        /// <summary>
        /// Проверяет найдено ли совпадение текста в значении свойства.
        /// </summary>
        /// <param name="matchText">Текст для поиска совпадения.</param>
        /// <param name="property">Свойство.</param>
        /// <param name="item">Элемент.</param>
        /// <returns>Логическое значение, указывающее было ли найдено совпадение в значении
        /// свойства.</returns>
        private static bool IsMatchTextInValueOfProperty(string matchText, PropertyInfo property, T item)
        {
            var value = property.GetValue(item);
            var text = value != null ? value.ToString() : "";
            return text.IndexOf(matchText, StringComparison.OrdinalIgnoreCase) != -1;
        }

        /// <summary>
        /// Возвращает свойства по условию.
        /// </summary>
        /// <returns>Список свойств.</returns>
        private List<PropertyInfo> GetPropertiesForCondition() =>
            string.IsNullOrEmpty(SelectedPropertyName) ? _getMethodList :
                [_getMethodList.First((p) => p.Name == SelectedPropertyName)];

        private void FinalDbSetLocal_CollectionChanged(object? sender,
            NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    var newItem = e.NewItems[0];
                    DbSetLocal.Add((T)newItem);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    var oldItem = e.OldItems[0];
                    DbSetLocal.Remove((T)oldItem);
                    break;
            }
        }
    }
}