using Model;

namespace ViewModel.Classes.Connections.Sqlite
{
    public class SqliteConnectionData : PropertyUpdaterService
    {
        public string _dataSource;

        public string DataSource
        {
            get => _dataSource;
            set => UpdateProperty(ref _dataSource, value,
                ValueValidator.AssertStringIsNotNullOrEmpty);
        }

        public string Password { get; set; }
    }
}
