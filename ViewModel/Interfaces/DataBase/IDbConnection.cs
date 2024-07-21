using System;

namespace ViewModel.Interfaces.DataBase
{
    public interface IDbConnection
    {
        public bool CanConnect { get; }

        public event EventHandler? CanConnectChanged;

        public IDbContext Connect();
    }
}
