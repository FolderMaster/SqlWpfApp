namespace ViewModel.Interfaces
{
    /// <summary>
    /// Интерфейс процедуры с методом вызова.
    /// </summary>
    public interface IProc
    {
        /// <summary>
        /// Вызывает процедуру.
        /// </summary>
        public void Invoke();

        public void Abort();
    }
}