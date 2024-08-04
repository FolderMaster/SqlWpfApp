using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

using Model.ObservableObjects;

namespace ViewModel.Classes
{
    public class TableChangesSet : ObservableObject
    {
        public static ObservableProperty AddedCountProperty = RegisterProperty
            (typeof(TableChangesSet), nameof(AddedCount), 0, null,
            (o) => ((TableChangesSet)o.Owner).TotalCount += (int)o.NewValue - (int)o.OldValue);

        public static ObservableProperty RemovedCountProperty = RegisterProperty
            (typeof(TableChangesSet), nameof(RemovedCount), 0, null,
            (o) => ((TableChangesSet)o.Owner).TotalCount += (int)o.NewValue - (int)o.OldValue);

        public static ObservableProperty ModifiedCountProperty = RegisterProperty
            (typeof(TableChangesSet), nameof(ModifiedCount), 0, null,
            (o) => ((TableChangesSet)o.Owner).TotalCount += (int)o.NewValue - (int)o.OldValue);

        public static ObservableProperty TotalCountProperty = RegisterProperty
            (typeof(TableChangesSet), nameof(TotalCount), 0);

        private readonly List<object> _addedList = new List<object>();

        private readonly List<object> _removedList = new List<object>();

        private readonly List<object> _modifiedList = new List<object>();

        public int AddedCount
        {
            get => (int)GetValue(AddedCountProperty);
            set => SetValue(value, AddedCountProperty);
        }

        public int RemovedCount
        {
            get => (int)GetValue(RemovedCountProperty);
            set => SetValue(value, RemovedCountProperty);
        }

        public int ModifiedCount
        {
            get => (int)GetValue(ModifiedCountProperty);
            set => SetValue(value, ModifiedCountProperty);
        }

        public int TotalCount
        {
            get => (int)GetValue(TotalCountProperty);
            private set => SetValue(value, TotalCountProperty);
        }

        static TableChangesSet() { }

        public TableChangesSet(IEnumerable dbSetLocal)
        {
            foreach (var item in dbSetLocal)
            {
                if(item is INotifyPropertyChanged notifyProperty)
                {
                    notifyProperty.PropertyChanged += Notify_PropertyChanged;
                }
            }
            if (dbSetLocal is INotifyCollectionChanged notifyCollection)
            {
                notifyCollection.CollectionChanged += DbSetLocal_CollectionChanged;
            }
        }

        private void DbSetLocal_CollectionChanged(object? sender,
            NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    var addedItem = e.NewItems[0];
                    _addedList.Add(addedItem);
                    AddedCount++;
                    break;
                case NotifyCollectionChangedAction.Remove:
                    var removedItem = e.OldItems[0];
                    if (_addedList.Contains(removedItem))
                    {
                        _addedList.Remove(removedItem);
                        AddedCount--;
                    }
                    else
                    {
                        _removedList.Add(removedItem);
                        RemovedCount++;
                        if (removedItem is INotifyPropertyChanged removedNotify)
                        {
                            removedNotify.PropertyChanged -= Notify_PropertyChanged;
                        }
                    }
                    break;
            }
        }

        private void Notify_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (!_modifiedList.Contains(sender))
            {
                _modifiedList.Add(sender);
                ModifiedCount++;
            }
        }
    }
}
