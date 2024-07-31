namespace Model.ObservableObjects
{
    public class ObservableArgs
    {
        public ObservableObject Owner { get; private set; }

        public ObservableProperty Property { get; private set; }

        public object? NewValue { get; private set; }

        public object? OldValue { get; private set; }

        public ObservableArgs(ObservableProperty property, ObservableObject owner,
            object? oldValue, object? newValue)
        {
            Property = property;
            Owner = owner;
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}
