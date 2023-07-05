namespace Model
{
    /// <summary>
    /// Интерфейс генератора индентификаторов с метод получения их.
    /// </summary>
    public interface IIdGenerator
    {
        /// <summary>
        /// Возвращает идентификатор.
        /// </summary>
        /// <returns>Идентификатор.</returns>
        public long GetId();
    }
}