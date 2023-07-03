namespace ViewModel.Interfaces
{
    public interface IDbContextCreator
    {
        public IDbContext? Result { get; }

        public void Create(string connectionString);
    }
}