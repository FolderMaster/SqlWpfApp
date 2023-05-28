using CommunityToolkit.Mvvm.Input;
using System;

using SQLiteWpfApp.ViewModels.Services;

namespace SQLiteWpfApp.ViewModels.VMs
{
    public class ControlDbSetVM<T> : DbSetVM<T> where T : class, new()
    {
        public RelayCommand FirstCommand { get; private set; }

        public RelayCommand BackCommand { get; private set; }

        public RelayCommand NextCommand { get; private set; }

        public RelayCommand LastCommand { get; private set; }

        public RelayCommand AddCommand { get; private set; }

        public RelayCommand RemoveCommand { get; private set; }

        public int SelectedNumber
        {
            get => SelectedIndex + 1;
            set
            {
                if (value != SelectedNumber)
                {
                    if ((value <= Count && value >= 1) || (Count == 0 && value == 0))
                    {
                        SelectedIndex = value - 1;
                    }
                }
                OnPropertyChanged(nameof(SelectedNumber));
            }
        }

        public ControlDbSetVM(IMessageService messageService, Action closeAction) :
            base(messageService, closeAction)
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

        protected override void SelectedIndexChanged()
        {
            OnPropertyChanged(nameof(SelectedNumber));
            FirstCommand?.NotifyCanExecuteChanged();
            BackCommand?.NotifyCanExecuteChanged();
            NextCommand?.NotifyCanExecuteChanged();
            LastCommand?.NotifyCanExecuteChanged();
        }
    }
}