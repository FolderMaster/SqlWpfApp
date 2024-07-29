using Model;
using Model.ObservableObjects;

namespace ViewModel.Classes.Connections.Sqlite
{
    public class SqliteConnectionData : ObservableObject
    {
        public static readonly ObservableProperty DataSourceProperty = RegisterProperty
            (typeof(SqliteConnectionData), nameof(DataSource), null,
            [(o) => ValueValidator.AssertStringIsNotNullOrEmpty((string)o.NewValue, o.Name)]);

        public string DataSource
        {
            get => (string)GetProperty(DataSourceProperty);
            set => SetProperty(value, DataSourceProperty);
        }

        public string Password { get; set; }

        static SqliteConnectionData() { }
    }
}
