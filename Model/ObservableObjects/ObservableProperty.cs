namespace Model.ObservableObjects
{
    public class ObservableProperty
    {
        private readonly object? _defaultValue;

        private readonly Dictionary<ObservableObject, object?> _values = new();

        private readonly Action<ObservableArgs>? _callback;

        private readonly IEnumerable<Func<ObservableArgs, string?>>? _validations;

        public string Name { get; private set; }

        public ObservableProperty(string name, object? defaultValue,
            Action<ObservableArgs>? callback,
            IEnumerable<Func<ObservableArgs, string?>>? validations)
        {
            Name = name;
            _defaultValue = defaultValue;
            _callback = callback;
            _validations = validations;
        }

        public void CreateValueForOwner(ObservableObject owner) =>
            _values.Add(owner, _defaultValue);

        public void RemoveValueForOwner(ObservableObject owner) =>
            _values.Remove(owner);

        public object? GetValueForOwner(ObservableObject owner) => _values[owner];

        public bool UpdateValueForOwner(ObservableObject owner, object? value)
        {
            if (IsNotCompare(_values[owner], value))
            {
                _values[owner] = value;
                _callback?.Invoke(new ObservableArgs(Name, owner, value));
                return true;
            }
            return false;
        }

        public IEnumerable<string> Validate(ObservableObject owner)
        {
            var result = new List<string>();
            if (_validations != null)
            {
                var value = _values[owner];
                foreach (var validation in _validations)
                {
                    var error = validation(new ObservableArgs(Name, owner, value));
                    if (error != null)
                    {
                        result.Add(error);
                    }
                }
            }
            if (result.Any())
            {
                return result;
            }
            return null;
        }

        private bool IsNotCompare(object? x, object? y)
        {
            if (x == null && y == null)
            {
                return false;
            }
            if (x == null || y == null)
            {
                return true;
            }
            if(x.Equals(y))
            {
                return false;
            }
            return true;
        }
    }
}
