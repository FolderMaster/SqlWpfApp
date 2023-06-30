namespace ViewModel.Interfaces
{
    public interface IDataBaseContextCreator
    {
        public IDataBaseContext? Result { get; }

        public void Create(string connectionString);
    }
}