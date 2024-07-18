using Microsoft.Data.Sqlite;
using ViewModel.Interfaces.DataBase;

namespace ViewModel.Dependencies.DataBase.Sqlite
{
    public class SqliteDbConnection : IDbConnection
    {
        private string? _connectionString;

        public string? ConnectionString
        {
            get => _connectionString;
            private set => _connectionString = value;
        }

        private void UpdateConnectionString()
        {
            var builder = new SqliteConnectionStringBuilder()
            {
            };
            ConnectionString = builder.ConnectionString;
        }

        public bool CanConnect => true;

        public IDbContext Connect() =>
            new SqliteDbContext(ConnectionString);

        public override string ToString() => "SQLite";
    }
}
