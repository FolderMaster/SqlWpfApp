using System.Collections.ObjectModel;

namespace ViewModel.Classes
{
    public class Connection
    {
        public ObservableCollection<Authorization> Authorizations { get; set; } = new();

        public string DataSource { get; set; } = "";

        public string InitialCatalog { get; set; } = "";

        public bool IsTlsConnection { get; set; } = true;

        public bool IsColumnEncryption { get; set; } = true;

        public bool IsTrustServerCertificate { get; set; } = true;
    }
}
