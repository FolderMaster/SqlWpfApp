using System;

namespace ViewModel.Interfaces.DataBase
{
    public interface IDbConnection
    {
        public object Data { get; set; }

        public bool CanConnect { get; }

        public event EventHandler? CanConnectChanged;

        public IDbContext Connect();
    }
}
