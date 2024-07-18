using System;
using ViewModel.Interfaces.DataBase;

namespace ViewModel.Interfaces
{
    /// <summary>
    /// Интерфейс создателя контекста базы данных с методом создания и результатом.
    /// </summary>
    public interface ISession
    {
        /// <summary>
        /// Возвращает результат создания контекста базы данных.
        /// </summary>
        public IDbContext? DbContext { get; set; }

        public event EventHandler? DbContextChanged;
    }
}