using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Interfaces;

namespace ViewModel.Dependencies
{
    public class SQLiteDbContextCreator : IDbContextCreator
    {
        public IDbContext? Result { get; private set; }

        public void Create(string connectionString) =>
            Result = new SQLiteDbContext(connectionString);
    }
}