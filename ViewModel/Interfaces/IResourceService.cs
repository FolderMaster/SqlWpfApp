namespace ViewModel.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса ресурсов с методами получения ресурсов и строк.
    /// </summary>
    public interface IResourceService
    {
        /// <summary>
        /// Возвращает ресурс.
        /// </summary>
        /// <param name="resourceKey">Ключ ресурса.</param>
        /// <returns>Ресурс.</returns>
        public object GetResource(string resourceKey);

        /// <summary>
        /// Возвращает строку.
        /// </summary>
        /// <param name="stringKey">Ключ строки.</param>
        /// <returns>Строка.</returns>
        public string GetString(string stringKey);
    }
}