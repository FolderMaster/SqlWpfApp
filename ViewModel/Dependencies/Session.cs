using System;

using ViewModel.Interfaces;
using ViewModel.Interfaces.DataBase;

namespace ViewModel.Dependencies
{
    public class Session : ISession
    {
        private IDbContext? _dbContext;

        public IDbContext? DbContext
        {
            get => _dbContext;
            set
            {
                if (_dbContext != value)
                {
                    _dbContext = value;
                    DbContextChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public event EventHandler? DbContextChanged;
    }
}
