using System.Runtime.CompilerServices;

namespace Model
{
    public class ValueValidator
    {
        public static string? AssertOnNotNullValue<T>(T value,
            [CallerMemberName] string? name = null)
            => value == null ? $"{name} must be not null!" : null;

        public static string? AssertOnPositiveValue(int value,
            [CallerMemberName] string? name = null)
            => value < 0 ? $"{name} must be positive!" : null;

        public static string? AssertOnNaturalValue(int value,
            [CallerMemberName] string? name = null)
            => value <= 0 ? $"{name} must be natural!" : null;

        public static string? AssertStringOnLessLength(string value, int maxLength,
            [CallerMemberName] string? name = null)
            => value.Length > maxLength ? $"Length of {name} must be less than {maxLength}!" :
                null;

        public static string? AssertStringOnEqualLength(string value, int length,
            [CallerMemberName] string? name = null)
            => value.Length != length ? $"Length of {name} must be equal {length}!" : null;
    }
}
