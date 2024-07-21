using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using ViewModel.Classes.Connections.Sqlite;

namespace ViewModel.VMs.Connections
{
    public partial class SqliteConnectionVM : ObservableObject
    {
        private IList<SqliteConnectionData>? _connections =
            new ObservableCollection<SqliteConnectionData>();

        [ObservableProperty]
        private SqliteConnectionData? selectedConnection;

        [ObservableProperty]
        private string connectionText;

        public IList<SqliteConnectionData>? Connections
        {
            get => _connections;
            set
            {
                if (SetProperty(ref _connections, value))
                {
                    SelectedConnection = value != null && value.Any() ? value[0] : null;
                }
            }
        }

        public RelayCommand AddConnectionCommand { get; private set; }

        public RelayCommand RemoveSelectedConnectionCommand { get; private set; }

        public SqliteConnectionVM()
        {
            AddConnectionCommand = new RelayCommand(() => {
                var newConnectionData = new SqliteConnectionData()
                {
                    DataSource = ConnectionText
                };
                Connections.Add(newConnectionData);
                SelectedConnection = newConnectionData;
            });
            RemoveSelectedConnectionCommand = new RelayCommand(() =>
            {
                int selectedIndex = Connections.IndexOf(SelectedConnection);
                Connections.Remove(SelectedConnection);
                if (Connections.Any())
                {
                    SelectedConnection = selectedIndex < Connections.Count ?
                        Connections[selectedIndex] : Connections[^1];
                }
                else
                {
                    SelectedConnection = null;
                }
            });
        }
    }
}
