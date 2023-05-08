using Microsoft.EntityFrameworkCore;
using System;

namespace SQLiteWpfApp.ViewModels
{
    public class DataBaseConnectionService<T> where T : class
    {
        private Action<DataBaseContext, DbSet<T>> _connectDb;

        public DataBaseConnectionService(Action<DataBaseContext, DbSet<T>> connectDb)
            => _connectDb = connectDb;

        public void ConnectDb(DataBaseContext dbContext, DbSet<T> dbSet) =>
            _connectDb(dbContext, dbSet);
    }
}