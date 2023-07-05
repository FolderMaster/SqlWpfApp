namespace ViewModel.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса печати с методом печати документа.
    /// </summary>
    public interface IPrintService
    {
        /// <summary>
        /// Печатает документ.
        /// </summary>
        /// <param name="document">Документ.</param>
        /// <param name="description">Описание.</param>
        void Print(object document, string description);
    }
}