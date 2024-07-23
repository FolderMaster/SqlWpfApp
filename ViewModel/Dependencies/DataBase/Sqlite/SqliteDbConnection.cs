using System.Data.SQLite;
using System.ComponentModel;
using System.Collections.ObjectModel;

using ViewModel.Interfaces.DataBase;
using ViewModel.Classes.Connections.Sqlite;

namespace ViewModel.Dependencies.DataBase.Sqlite
{
    public class SqliteDbConnection : BaseDbConnection
    {
        private readonly SQLiteConnectionStringBuilder _builder = new();

        private SqliteConnectionData? _connection;

        public override object Data { get; set; } =
            new ObservableCollection<SqliteConnectionData>();

        public SqliteConnectionData? Connection
        {
            get => _connection;
            set
            {
                if (_connection != value)
                {
                    _connection = value;
                    UpdateCanConnect();
                }
                var oldValue = _connection;
                if (_connection != value)
                {
                    if (oldValue != null)
                    {
                        oldValue.ErrorsChanged -= ErrorsChanged;
                    }
                    _connection = value;
                    if (_connection != null)
                    {
                        _connection.ErrorsChanged += ErrorsChanged;
                    }
                    UpdateCanConnect();
                }
            }
        }

        public override IDbContext Connect() => new SqliteDbContext(GetConnectionString());

        public override string ToString() => "SQLite";

        private void UpdateCanConnect()
        {
            if (_connection != null && !_connection.HasErrors)
            {
                CanConnect = true;
            }
            else
            {
                CanConnect = false;
            }
        }

        private string GetConnectionString()
        {
            _builder.DataSource = Connection?.DataSource;
            _builder.Password = Connection?.Password;

            return _builder.ConnectionString;
        }

        private void ErrorsChanged(object? sender, DataErrorsChangedEventArgs e) =>
            UpdateCanConnect();
    }
}
