using Model;

namespace ViewModel.Classes.Connections.MsSqlServer
{
    public class MsSqlServerConnectionData : PropertyUpdaterService
    {
        public string _dataSource;

        public string _intialCatalog;

        public string DataSource
        {
            get => _dataSource;
            set => UpdateProperty(ref _dataSource, value,
                ValueValidator.AssertStringIsNotNullOrEmpty);
        }

        public string InitialCatalog
        {
            get => _intialCatalog;
            set => UpdateProperty(ref _intialCatalog, value,
                ValueValidator.AssertStringIsNotNullOrEmpty);
        }

        public bool IsTlsConnection { get; set; }

        public bool IsColumnEncryption { get; set; }

        public bool IsTrustServerCertificate { get; set; }
    }
}
