using ViewModel.Classes;

namespace ViewModel.Interfaces.Proces
{
    /// <summary>
    /// Интерфейс процедуры с методом вызова.
    /// </summary>
    public interface IProc
    {
        public IProcData Data { get; }

        /// <summary>
        /// Вызывает процедуру.
        /// </summary>
        public void Invoke();

        public void Abort();
    }
}