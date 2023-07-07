namespace Model
{
    /// <summary>
    /// Интерфейс генератора идентификаторов с метод получения их.
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