using Model;
using Model.ObservableObjects;

namespace ViewModel.Classes.Connections.MsSqlServer
{
    public class MsSqlServerConnectionData : ObservableObject
    {
        public static readonly ObservableProperty DataSourceProperty = RegisterProperty
            (typeof(MsSqlServerConnectionData), nameof(DataSource), null,
            [(o) => ValueValidator.AssertStringIsNotNullOrEmpty((string)o.NewValue, o.Property.Name)]);

        public static readonly ObservableProperty InitialCatalogProperty = RegisterProperty
            (typeof(MsSqlServerConnectionData), nameof(InitialCatalog), null,
            [(o) => ValueValidator.AssertStringIsNotNullOrEmpty((string)o.NewValue, o.Property.Name)]);

        public string DataSource
        {
            get => (string)GetValue(DataSourceProperty);
            set => SetValue(value, DataSourceProperty);
        }

        public string InitialCatalog
        {
            get => (string)GetValue(InitialCatalogProperty);
            set => SetValue(value, InitialCatalogProperty);
        }

        public bool IsTlsConnection { get; set; }

        public bool IsColumnEncryption { get; set; }

        public bool IsTrustServerCertificate { get; set; }

        static MsSqlServerConnectionData() { }
    }
}
