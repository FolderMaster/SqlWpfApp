namespace ViewModel.Interfaces.DataBase
{
    public interface IDbConnection
    {
        public string? ConnectionString { get; }

        public bool CanConnect { get; }

        public IDbContext Connect();
    }
}
