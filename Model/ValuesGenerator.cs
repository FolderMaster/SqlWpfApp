namespace Model
{
    public static class ValuesGenerator
    {
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