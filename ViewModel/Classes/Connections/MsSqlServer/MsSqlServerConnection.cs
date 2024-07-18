using System.Collections.ObjectModel;

namespace ViewModel.Classes.Connections.MsSqlServer
{
    public class MsSqlServerConnection
    {
        public ObservableCollection<MsSqlServerCredential> Credentials { get; set; } = [];

        public string DataSource { get; set; } = "";

        public string InitialCatalog { get; set; } = "";

        public bool IsTlsConnection { get; set; } = true;

        public bool IsColumnEncryption { get; set; } = true;

        public bool IsTrustServerCertificate { get; set; } = true;
    }
}
