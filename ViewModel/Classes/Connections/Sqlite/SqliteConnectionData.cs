using Model;
using Model.ObservableObjects;

namespace ViewModel.Classes.Connections.Sqlite
{
    public class SqliteConnectionData : ObservableObject
    {
        public static readonly ObservableProperty DataSourceProperty = RegisterProperty
            (typeof(SqliteConnectionData), nameof(DataSource), null,
            [(o) => ValueValidator.AssertStringIsNotNullOrEmpty((string)o.NewValue, o.Property.Name)]);

        public string DataSource
        {
            get => (string)GetValue(DataSourceProperty);
            set => SetValue(value, DataSourceProperty);
        }

        public string Password { get; set; }

        static SqliteConnectionData() { }
    }
}
