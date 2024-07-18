using Microsoft.Data.SqlClient;

using ViewModel.Classes.Connections.MsSqlServer;
using ViewModel.Interfaces.DataBase;

namespace ViewModel.Dependencies.DataBase.MsSqlServer
{
    public class MsSqlServerDbConnection : IDbConnection
    {
        private readonly SqlConnectionStringBuilder _builder = new();

        public MsSqlServerConnection? Connection { get; set; }

        public MsSqlServerCredential? Credential { get; set; }

        public string? ConnectionString { get; private set; }

        public bool CanConnect => false;

        private void UpdateConnectionString()
        {
            _builder.DataSource = Connection?.DataSource;
            _builder.InitialCatalog = Connection?.InitialCatalog;
            _builder.Encrypt = Connection?.IsTlsConnection;
            _builder.ColumnEncryptionSetting = Connection?.IsColumnEncryption == true ?
                SqlConnectionColumnEncryptionSetting.Enabled :
                SqlConnectionColumnEncryptionSetting.Disabled;
            _builder.TrustServerCertificate = Connection?.IsTrustServerCertificate == true;

            _builder.UserID = Credential?.User;
            _builder.Password = Credential?.Password;

            ConnectionString = _builder.ConnectionString;
        }

        public IDbContext Connect() =>
            new MsSqlServerDbContext(ConnectionString);

        public override string ToString() => "MS SQL Server";
    }
}
