namespace Model
{
    /// <summary>
    /// Класс генератора идентификаторов.
    /// </summary>
    public class IdGenerator
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        private long _id = 0;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="IdGenerator"/> по умолчанию.
        /// </summary>
        public IdGenerator() { }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="IdGenerator"/>.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        public IdGenerator(long id) => _id = id;

        /// <summary>
        /// Возвращает идентификатор.
        /// </summary>
        /// <returns>Идентификатор.</returns>
        public long GetId() => _id++;
    }
}