using System;

using ViewModel.Interfaces.DataBase;

namespace ViewModel.Dependencies.DataBase
{
    public abstract class BaseDbConnection : IDbConnection
    {
        private bool _canConnect;

        public abstract object Data { get; set; }

        public bool CanConnect
        {
            get => _canConnect;
            protected set
            {
                if (_canConnect != value)
                {
                    _canConnect = value;
                    CanConnectChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public event EventHandler? CanConnectChanged;

        public abstract IDbContext Connect();
    }
}
