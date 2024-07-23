namespace Model.ObservableObjects
{
    public class ObservableArgs
    {
        public string Name { get; private set; }

        public ObservableObject Owner { get; private set; }

        public object? Value { get; private set; }

        public ObservableArgs(string name, ObservableObject owner, object? value)
        {
            Name = name;
            Owner = owner;
            Value = value;
        }
    }
}
