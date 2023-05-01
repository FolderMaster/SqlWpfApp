using Microsoft.EntityFrameworkCore;
using System;

namespace SQLiteWpfApp.ViewModels
{
    public class DbConnectionService<T> where T : class
    {
        private Action<DbContext, DbSet<T>> _connectDb;

        public DbConnectionService(Action<DbContext, DbSet<T>> connectDb)
        {
            _connectDb = connectDb;
        }

        public void ConnectDb(DbContext dbContext, DbSet<T> dbSet) => _connectDb(dbContext, dbSet);
    }
}