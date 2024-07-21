using Microsoft.Data.SqlClient;
using System.ComponentModel;

using ViewModel.Classes.Connections.MsSqlServer;
using ViewModel.Interfaces.DataBase;

namespace ViewModel.Dependencies.DataBase.MsSqlServer
{
    public class MsSqlServerDbConnection : BaseDbConnection
    {
        private readonly SqlConnectionStringBuilder _builder = new();

        private MsSqlServerConnectionData? _connection;

        private MsSqlServerCredentialData? _credential;

        public MsSqlServerConnectionData? Connection
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

        public MsSqlServerCredentialData? Credential
        {
            get => _credential;
            set
            {
                var oldValue = _credential;
                if (_credential != value)
                {
                    if(oldValue != null)
                    {
                        oldValue.ErrorsChanged -= ErrorsChanged;
                    }
                    _credential = value;
                    if (_credential != null)
                    {
                        _credential.ErrorsChanged += ErrorsChanged;
                    }
                    UpdateCanConnect();
                }
            }
        }

        public override IDbContext Connect() => new MsSqlServerDbContext(GetConnectionString());

        public override string ToString() => "MS SQL Server";

        private void UpdateCanConnect()
        {
            if (_connection != null && _credential != null &&
                !_connection.HasErrors && !_credential.HasErrors)
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
            _builder.InitialCatalog = Connection?.InitialCatalog;
            _builder.Encrypt = Connection?.IsTlsConnection;
            _builder.ColumnEncryptionSetting = Connection?.IsColumnEncryption == true ?
                SqlConnectionColumnEncryptionSetting.Enabled :
                SqlConnectionColumnEncryptionSetting.Disabled;
            _builder.TrustServerCertificate = Connection?.IsTrustServerCertificate == true;

            _builder.UserID = Credential?.User;
            _builder.Password = Credential?.Password;

            return _builder.ConnectionString;
        }

        private void ErrorsChanged(object? sender, DataErrorsChangedEventArgs e) =>
            UpdateCanConnect();
    }
}
