using CommunityToolkit.Mvvm.Input;
using ViewModel.Interfaces.DbContext;
using ViewModel.Interfaces.Services;
using ViewModel.Interfaces.Services.Messages;

namespace ViewModel.VMs.DbSet
{
    /// <summary>
    /// Класс представления модели представления таблицы из базы данных с финальным и локальными
    /// представлениями таблицы из базы данных, количеством элементов, выбранным элементом,
    /// выбранным индексом, выбранным номером, текстом поиска, текстом фильтра, названием
    /// выбранного свойства и командами сохранения, переходами к первому, последнему, предыдущему,
    /// следующему элементу, удаления и добавления элемента.
    /// </summary>
    /// <typeparam name="T">Тип сущности таблицы.</typeparam>
    public class ControlDbSetVM<T> : DbSetVM<T> where T : class, new()
    {
        /// <summary>
        /// Возвращает и задаёт команду перехода к первому элементу.
        /// </summary>
        public RelayCommand FirstCommand { get; private set; }

        /// <summary>
        /// Возвращает и задаёт команду перехода к предыдущему элементу.
        /// </summary>
        public RelayCommand BackCommand { get; private set; }

        /// <summary>
        /// Возвращает и задаёт команду перехода к следующему элементу.
        /// </summary>
        public RelayCommand NextCommand { get; private set; }

        /// <summary>
        /// Возвращает и задаёт команду перехода к последнему элементу.
        /// </summary>
        public RelayCommand LastCommand { get; private set; }

        /// <summary>
        /// Возвращает и задаёт команду добавления элемента.
        /// </summary>
        public RelayCommand AddCommand { get; private set; }

        /// <summary>
        /// Возвращает и задаёт команду удаления элемента.
        /// </summary>
        public RelayCommand RemoveCommand { get; private set; }

        /// <summary>
        /// Возвращает и задаёт выбранный номер.
        /// </summary>
        public int SelectedNumber
        {
            get => SelectedIndex + 1;
            set
            {
                if (value != SelectedNumber)
                {
                    if (value <= Count && value >= 1 || Count == 0 && value == 0)
                    {
                        SelectedIndex = value - 1;
                    }
                }
                OnPropertyChanged(nameof(SelectedNumber));
            }
        }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="ControlDbSetVM{T}"/>.
        /// </summary>
        /// <param name="dataBaseContextBuilder">Создатель контекста базы данных.</param>
        /// <param name="resourceService">Сервис ресурсов.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        public ControlDbSetVM(IDbContextBuilder dataBaseContextBuilder,
            IResourceService resourceService, IMessageService messageService) :
            base(dataBaseContextBuilder, resourceService, messageService)
        {
            FirstCommand = new RelayCommand(() => SelectedIndex = 0,
                () => Count > 0 && SelectedIndex != 0);
            BackCommand = new RelayCommand(() => SelectedIndex -= 1,
                () => Count > 0 && SelectedIndex != 0);
            NextCommand = new RelayCommand(() => SelectedIndex += 1,
                () => Count > 0 && SelectedIndex != Count - 1);
            LastCommand = new RelayCommand(() => SelectedIndex = Count - 1,
                () => Count > 0 && SelectedIndex != Count - 1);
            AddCommand = new RelayCommand(() =>
            {
                FinalDbSetLocal.Add(new T());
                RemoveCommand?.NotifyCanExecuteChanged();
                OnPropertyChanged(nameof(Count));
                LastCommand.Execute(null);
            }, () => DbSetLocal != null);
            RemoveCommand = new RelayCommand(() =>
            {
                var selectedIndex = FinalDbSetLocal.IndexOf(SelectedItem);
                FinalDbSetLocal.Remove(SelectedItem);
                RemoveCommand?.NotifyCanExecuteChanged();
                OnPropertyChanged(nameof(Count));
                if (Count > 0)
                {
                    SelectedItem = selectedIndex < Count ? FinalDbSetLocal[selectedIndex] :
                        FinalDbSetLocal[Count - 1];
                }
                else
                {
                    SelectedItem = null;
                }
            }, () => Count > 0);
        }

        /// <summary>
        /// Метод, выполняющийся после изменения выбранного индекса.
        /// </summary>
        protected override void OnSelectedIndexChanged()
        {
            OnPropertyChanged(nameof(SelectedNumber));
            FirstCommand?.NotifyCanExecuteChanged();
            BackCommand?.NotifyCanExecuteChanged();
            NextCommand?.NotifyCanExecuteChanged();
            LastCommand?.NotifyCanExecuteChanged();
        }
    }
}