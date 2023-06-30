using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Interfaces;

namespace ViewModel.Dependencies
{
    public class SQLiteDataBaseContextCreator : IDataBaseContextCreator
    {
        public IDataBaseContext? Result { get; private set; }

        public void Create(string connectionString) =>
            Result = new SQLiteDataBaseContext(connectionString);
    }
}