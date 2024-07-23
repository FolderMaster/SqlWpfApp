namespace Model
{
    public class ValueValidator
    {
        public static string? AssertOnNotNullValue(object? value, string name) =>
            value == null ? $"{name} must be not null!" : null;

        public static string? AssertOnPositiveValue(int value, string name) =>
            value < 0 ? $"{name} must be positive!" : null;

        public static string? AssertOnNaturalValue(int value, string name) =>
            value <= 0 ? $"{name} must be natural!" : null;

        public static string? AssertStringOnLessLength(string value, int maxLength, string name) =>
            value.Length > maxLength ? $"Length of {name} must be less than {maxLength}!" : null;

        public static string? AssertStringIsNotNullOrEmpty(string? value, string name) =>
            string.IsNullOrEmpty(value) ? $"{name} mustn't be null or empty!" : null;

        public static string? AssertStringOnEqualLength(string value, int length, string name) =>
            value.Length != length ? $"Length of {name} must be equal {length}!" : null;
    }
}
