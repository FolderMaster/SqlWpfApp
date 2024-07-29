namespace Model.ObservableObjects
{
    public class ObservableArgs
    {
        public string Name { get; private set; }

        public ObservableObject Owner { get; private set; }

        public object? NewValue { get; private set; }

        public object? OldValue { get; private set; }

        public ObservableArgs(string name, ObservableObject owner,
            object? oldValue, object? newValue)
        {
            Name = name;
            Owner = owner;
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}
