namespace ViewModel.Classes
{
    public class PatternMatch
    {
        public object Value { get; private set; }

        public int Index { get; private set; }

        public int Length { get; private set; }

        public PatternMatch(object value, int index, int length)
        {
            Value = value;
            Index = index;
            Length = length;
        }
    }
}
