using Model;
using Model.ObservableObjects;

namespace ViewModel.Classes.Connections.MsSqlServer
{
    public class MsSqlServerConnectionData : ObservableObject
    {
        public static readonly ObservableProperty DataSourceProperty = RegisterProperty
            (typeof(MsSqlServerConnectionData), nameof(DataSource), null,
            [(o) => ValueValidator.AssertStringIsNotNullOrEmpty((string)o.NewValue, o.Name)]);

        public static readonly ObservableProperty InitialCatalogProperty = RegisterProperty
            (typeof(MsSqlServerConnectionData), nameof(InitialCatalog), null,
            [(o) => ValueValidator.AssertStringIsNotNullOrEmpty((string)o.NewValue, o.Name)]);

        public string DataSource
        {
            get => (string)GetProperty(DataSourceProperty);
            set => SetProperty(value, DataSourceProperty);
        }

        public string InitialCatalog
        {
            get => (string)GetProperty(InitialCatalogProperty);
            set => SetProperty(value, InitialCatalogProperty);
        }

        public bool IsTlsConnection { get; set; }

        public bool IsColumnEncryption { get; set; }

        public bool IsTrustServerCertificate { get; set; }

        static MsSqlServerConnectionData() { }
    }
}
