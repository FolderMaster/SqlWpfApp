namespace Model
{
    /// <summary>
    /// Класс генератора значений.
    /// </summary>
    public static class ValuesGenerator
    {
        /// <summary>
        /// Генерирует значения.
        /// </summary>
        /// <param name="idGenerator">Генератор идентификаторов.</param>
        /// <param name="checkFunc">Функция проверки в итерации.</param>
        /// <param name="generationAction">Действие генерации значений в итерации.</param>
        /// <param name="endAction">Действие в конце.</param>
        public static void GenerateValues(IdGenerator idGenerator, Func<bool> checkFunc,
            Action<long> generationAction, Action endAction)
        {
            long id = idGenerator.GetId();
            generationAction(id);
            for (;checkFunc(); id = idGenerator.GetId())
            {
                generationAction(id);
            }
            endAction();
        }
    }
}