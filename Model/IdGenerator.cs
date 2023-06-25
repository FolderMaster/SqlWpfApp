namespace Model
{
    public class IdGenerator
    {
        private long _id = 0;

        public IdGenerator() { }

        public IdGenerator(long id) => _id = id;

        public long GetId() => _id++;
    }
}