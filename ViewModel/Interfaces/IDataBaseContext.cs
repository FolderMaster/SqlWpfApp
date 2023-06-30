using Microsoft.EntityFrameworkCore;

namespace ViewModel.Interfaces
{
    public interface IDataBaseContext
    {
        public string ConnectionString { get; }

        public DbSet<TEntity> Set<TEntity>() where TEntity : class;

        public int SaveChanges<TEntity>() where TEntity : class;
    }
}